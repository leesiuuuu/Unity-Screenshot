using System.IO;
using UnityEditor;
using UnityEngine;

namespace LeeSiwoo.UnityScreenshot.Editor
{
    [FilePath("ProjectSettings/UnityScreenshotSettings.asset", FilePathAttribute.Location.ProjectFolder)]
    internal sealed class ScreenshotSettings : ScriptableSingleton<ScreenshotSettings>
    {
        private const string DefaultOutputDirectory = "Screenshots";

        [SerializeField]
        private string outputDirectory = DefaultOutputDirectory;

        internal string OutputDirectory => string.IsNullOrWhiteSpace(outputDirectory)
            ? DefaultOutputDirectory
            : outputDirectory;

        internal string AbsoluteOutputDirectory
        {
            get
            {
                string directory = OutputDirectory;
                if (Path.IsPathRooted(directory))
                {
                    return Path.GetFullPath(directory);
                }

                return Path.GetFullPath(Path.Combine(ProjectRoot, directory));
            }
        }

        internal static string ProjectRoot
        {
            get
            {
                DirectoryInfo parent = Directory.GetParent(Application.dataPath);
                return parent != null ? parent.FullName : Application.dataPath;
            }
        }

        internal void SetOutputDirectory(string directory)
        {
            outputDirectory = string.IsNullOrWhiteSpace(directory)
                ? DefaultOutputDirectory
                : directory.Trim();
            Save(true);
        }

        internal void ResetOutputDirectory()
        {
            outputDirectory = DefaultOutputDirectory;
            Save(true);
        }
    }
}

