using System;
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

        [Shortcut(ShortcutId, KeyCode.F12)]
        private static void CaptureFromShortcut()
        {
            Capture();
        }

        [MenuItem(MenuRoot + "Capture Game View", priority = 1)]
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
            if (!EditorApplication.isPlaying)
            {
                const string message = "Enter Play Mode before capturing the Game view.";
                Debug.LogWarning($"[Unity Screenshot] {message}");
                ShowNotification(message);
                return;
            }

            try
            {
                string directory = ScreenshotSettings.instance.AbsoluteOutputDirectory;
                Directory.CreateDirectory(directory);

                string fileName = $"Screenshot_{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.png";
                string path = Path.Combine(directory, fileName);

                ScreenCapture.CaptureScreenshot(path);

                Debug.Log($"[Unity Screenshot] Saved to: {path}");
                ShowNotification($"Screenshot saved\n{fileName}");
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                ShowNotification("Failed to save screenshot");
            }
        }

        private static void ShowNotification(string message)
        {
            EditorWindow.focusedWindow?.ShowNotification(new GUIContent(message));
        }
    }
}

