using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace LeeSiwoo.UnityScreenshot.Editor
{
    internal static class ScreenshotController
    {
        internal const string ShortcutId = "Unity Screenshot/Capture Game View";
        private const string MenuRoot = "Tools/Unity Screenshot/";
        private const double CaptureTimeoutSeconds = 10d;

        private static readonly List<PendingCapture> PendingCaptures = new List<PendingCapture>();
        private static bool isWatchingCaptures;

        [Shortcut(ShortcutId, KeyCode.F12)]
        private static void CaptureFromShortcut()
        {
            Capture();
        }

        [MenuItem(MenuRoot + "Capture Game View (F12)", priority = 1)]
        private static void CaptureFromMenu()
        {
            Capture();
        }

        [MenuItem(MenuRoot + "Open Screenshot Folder", priority = 20)]
        internal static void OpenScreenshotFolder()
        {
            string directory = ScreenshotSettings.instance.AbsoluteOutputDirectory;
            Directory.CreateDirectory(directory);
            EditorUtility.RevealInFinder(directory);
        }

        internal static void Capture()
        {
            try
            {
                string directory = ScreenshotSettings.instance.AbsoluteOutputDirectory;
                Directory.CreateDirectory(directory);

                string fileName = $"Screenshot_{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.png";
                string path = Path.Combine(directory, fileName);

                EditorWindow gameView = FocusGameView();
                EditorApplication.delayCall += () => CaptureFocusedGameView(gameView, path, fileName);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                ShowNotification("Failed to save screenshot");
            }
        }

        private static EditorWindow FocusGameView()
        {
            Type gameViewType = typeof(EditorWindow).Assembly.GetType("UnityEditor.GameView");
            if (gameViewType == null)
            {
                throw new InvalidOperationException("Could not find the Unity Game view window.");
            }

            EditorWindow gameView = EditorWindow.GetWindow(gameViewType);
            gameView.Focus();
            gameView.Repaint();
            return gameView;
        }

        private static void CaptureFocusedGameView(EditorWindow gameView, string path, string fileName)
        {
            try
            {
                if (gameView == null)
                {
                    gameView = FocusGameView();
                }
                else
                {
                    gameView.Focus();
                    gameView.Repaint();
                }

                ScreenCapture.CaptureScreenshot(path);
                EditorApplication.QueuePlayerLoopUpdate();
                WatchCapture(path, fileName);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                ShowNotification("Failed to save screenshot");
            }
        }

        private static void WatchCapture(string path, string fileName)
        {
            PendingCaptures.Add(new PendingCapture(path, fileName, EditorApplication.timeSinceStartup));

            if (isWatchingCaptures)
            {
                return;
            }

            isWatchingCaptures = true;
            EditorApplication.update += CheckPendingCaptures;
        }

        private static void CheckPendingCaptures()
        {
            double now = EditorApplication.timeSinceStartup;

            for (int index = PendingCaptures.Count - 1; index >= 0; index--)
            {
                PendingCapture capture = PendingCaptures[index];

                if (IsCaptureReady(capture.Path))
                {
                    Debug.Log($"[Unity Screenshot] Saved to: {capture.Path}");
                    ShowNotification($"Screenshot saved\n{capture.FileName}");
                    PendingCaptures.RemoveAt(index);
                    continue;
                }

                if (now - capture.StartedAt < CaptureTimeoutSeconds)
                {
                    continue;
                }

                Debug.LogWarning($"[Unity Screenshot] Timed out while saving: {capture.Path}");
                ShowNotification("Screenshot capture timed out");
                PendingCaptures.RemoveAt(index);
            }

            if (PendingCaptures.Count > 0)
            {
                return;
            }

            EditorApplication.update -= CheckPendingCaptures;
            isWatchingCaptures = false;
        }

        private static bool IsCaptureReady(string path)
        {
            try
            {
                return File.Exists(path) && new FileInfo(path).Length > 0;
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        private static void ShowNotification(string message)
        {
            EditorWindow.focusedWindow?.ShowNotification(new GUIContent(message));
        }

        private sealed class PendingCapture
        {
            internal PendingCapture(string path, string fileName, double startedAt)
            {
                Path = path;
                FileName = fileName;
                StartedAt = startedAt;
            }

            internal string Path { get; }
            internal string FileName { get; }
            internal double StartedAt { get; }
        }
    }
}
