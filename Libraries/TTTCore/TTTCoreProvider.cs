using System.Collections.Generic;
using System.IO;
using System.Linq;
using WrathModdingHelper.Templates;

namespace WrathModdingHelper.Libraries.TTTCore
{
    public class TTTCoreProvider : ILibraryProvider
    {
        public string Id => "ttt-core";
        public string Title => "TabletopTweaks - Core";
        public string Brief => "Vek's TabletopTweaks systems and utilities";
        public IEnumerable<string> ProjectFileFragment => Helpers.Sequence(
            @"<Reference Include=""TabletopTweaks-Core"">",
            @"    <HintPath>$(WrathPath)\Mods\TabletopTweaks-Core\TabletopTweaks-Core.dll</HintPath>",
            @"</Reference>"
            );

        public IEnumerable<TutorialLink> Links => Helpers.Sequence(
            new TutorialLink("https://github.com/Vek17/TabletopTweaks-Core", "TTT-Core - Github"),
            new TutorialLink("https://github.com/Vek17/TabletopTweaks-Base", "Complex example of usage"));

        public IEnumerable<NugetPackage> NugetPackages => Helpers.NoNuget;

        public IEnumerable<string> MergeAssemblies => Helpers.NoMerge;

        public bool CanAdd(IEnumerable<ILibraryProvider> currentSelections) => !currentSelections.Any(s => s.Id is "bpcore");

        public bool CheckDependencies(string wrathPath, out string badness)
        {
            badness = "";

            if (!Directory.Exists(Path.Combine(wrathPath, "Mods", "TabletopTweaks-Core")))
            {
                badness = "Please install the TabletopTweaks-Core in Wrath first";
                return false;
            }

            return true;
        }
        public void GenerateProject(string projectDirectory, CommonState commonTemplateState) {
            var tttRoot = Path.Combine(projectDirectory, "TTTCore");
            Directory.CreateDirectory(Path.Combine(tttRoot, "Config"));
            Directory.CreateDirectory(Path.Combine(tttRoot, "Localisation"));
            Directory.CreateDirectory(Path.Combine(tttRoot, "ExampleContent"));

            File.WriteAllText(Path.Combine(tttRoot, $"ModContext{commonTemplateState.ModId}.cs"), new ModContext() { Common = commonTemplateState }.TransformText());
        }
    }
}

