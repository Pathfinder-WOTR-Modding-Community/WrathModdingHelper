using System.Collections.Generic;
using System.Linq;
using WrathModdingHelper.Templates;

namespace WrathModdingHelper.Libraries.BPCore
{
    public class BPCoreProvider : ILibraryProvider
    {
        public string Id => "bpcore";
        public string Title => "Blueprint Core";

        public string Brief => "Helpers to create and modify blueprints";
        public IEnumerable<string> ProjectFileFragment => Helpers.NoProjectFragment;

        public IEnumerable<TutorialLink> Links => Helpers.Sequence(
            new TutorialLink("https://wittlewolfie.github.io/WW-Blueprint-Core/articles/intro.html", "BlueprintCore - Usage documentation"),
            new TutorialLink("https://github.com/Pathfinder-WOTR-Modding-Community/BlueprintCore-Template", "BlueprintCore - Examples"));

        public IEnumerable<NugetPackage> NugetPackages => Helpers.Sequence(
            new NugetPackage("WW-Blueprint-Core", "*"));

        public IEnumerable<string> MergeAssemblies => Helpers.Sequence("BlueprintCore.dll");

        public bool CanAdd(IEnumerable<ILibraryProvider> currentSelections) => !currentSelections.Any(s => s.Id is "ttt-core");

        public bool CheckDependencies(string wrathPath, out string badness)
        {
            badness = "";
            return true;
        }

        public void GenerateProject(string projectDirectory, CommonState commonTemplateState) { }
    }
}
