using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrathModdingHelper.Templates;

namespace WrathModdingHelper.Libraries
{
    public interface ILibraryProvider
    {
        string Id { get; }
        string Title { get; }
        string Brief { get; }
        IEnumerable<TutorialLink> Links { get; }

        IEnumerable<string> ProjectFileFragment { get; }

        bool CanAdd(IEnumerable<ILibraryProvider> currentSelections);
        IEnumerable<NugetPackage> NugetPackages { get; }
        IEnumerable<string> MergeAssemblies { get; }

        bool CheckDependencies(string wrathPath, out string badnesS);

        void GenerateProject(string projectDirectory, CommonState commonTemplateState);

    }

    public static class Helpers
    {
        public static IEnumerable<NugetPackage> NoNuget => Enumerable.Empty<NugetPackage>();
        public static IEnumerable<string> NoMerge => Enumerable.Empty<string>();

        public static IEnumerable<string> NoProjectFragment => Enumerable.Empty<string>();

        public static IEnumerable<T> Sequence<T>(params T[] entries)
        {
            foreach (var entry in entries)
                yield return entry;
        }

        public static void DeleteDirectory(string targetDir)
        {
            if (!Directory.Exists(targetDir)) return;

            string[] files = Directory.GetFiles(targetDir);
            string[] dirs = Directory.GetDirectories(targetDir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(targetDir, false);
        }

    }
    public class NugetPackage
    {
        public string Name;
        public string Version;

        public NugetPackage(string name, string version)
        {
            Name = name;
            Version = version;
        }
    }
}
