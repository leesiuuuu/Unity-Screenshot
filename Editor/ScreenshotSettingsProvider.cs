using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace LeeSiwoo.UnityScreenshot.Editor
{
    internal static class ScreenshotSettingsProvider
    {
        [SettingsProvider]
        private static SettingsProvider CreateProvider()
        {
            return new SettingsProvider("Project/Unity Screenshot", SettingsScope.Project)
            {
                label = "Unity Screenshot",
                guiHandler = DrawSettings,
                keywords = new HashSet<string>
                {
                    "screenshot",
                    "capture",
                    "F12",
                    "shortcut",
                    "Game View"
                }
            };
        }

        private static void DrawSettings(string searchContext)
        {
            ScreenshotSettings settings = ScreenshotSettings.instance;

            EditorGUILayout.LabelField("Capture", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "Enter Play Mode and use the shortcut to capture the current Game view as a PNG.",
                MessageType.Info);

            ShortcutBinding binding = ShortcutManager.instance.GetShortcutBinding(ScreenshotController.ShortcutId);
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PrefixLabel("Capture Shortcut");
                EditorGUILayout.SelectableLabel(binding.ToString(), EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));

                if (GUILayout.Button("Edit Shortcuts", GUILayout.Width(110f)))
                {
                    if (!EditorApplication.ExecuteMenuItem("Edit/Shortcuts..."))
                    {
                        SettingsService.OpenUserPreferences("Shortcuts");
                    }
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Output", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            string outputDirectory = EditorGUILayout.TextField("Screenshot Folder", settings.OutputDirectory);
            if (EditorGUI.EndChangeCheck())
            {
                settings.SetOutputDirectory(outputDirectory);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Browse..."))
                {
                    string selected = EditorUtility.OpenFolderPanel(
                        "Choose Screenshot Folder",
                        settings.AbsoluteOutputDirectory,
                        string.Empty);

                    if (!string.IsNullOrEmpty(selected))
                    {
                        settings.SetOutputDirectory(selected);
                    }
                }

                if (GUILayout.Button("Open Folder"))
                {
                    ScreenshotController.OpenScreenshotFolder();
                }

                if (GUILayout.Button("Reset"))
                {
                    settings.ResetOutputDirectory();
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Resolved Path", EditorStyles.miniLabel);
            EditorGUILayout.SelectableLabel(
                settings.AbsoluteOutputDirectory,
                EditorStyles.textField,
                GUILayout.Height(EditorGUIUtility.singleLineHeight));
        }
    }
}
