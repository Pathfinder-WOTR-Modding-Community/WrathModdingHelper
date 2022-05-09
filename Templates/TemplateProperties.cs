using System.Collections.Generic;
using System.Linq;
using WrathModdingHelper.Libraries;

namespace WrathModdingHelper.Templates
{
    public partial class csproj
    {
        public string InjectedFragments { get; set; } = "";
        public IEnumerable<NugetPackage> NugetPackages { get; set; } = Helpers.NoNuget;
        public IEnumerable<string> MergeAssemblies { get; set; } = Helpers.NoMerge;
        public string PreDeployTarget => MergeAssemblies.Any() ? "ILRepack" : "Build";
    }

    public partial class Info
    {
        public ModCreationStuff Stuff { get; set; } = new();
    }

    public class CommonState
    {
        public string DefaultNamespace = "";
        public string ModId = "";

    }

    public partial class Main
    {
        public CommonState Common = new();
    }
    public partial class ModletExample
    {
        public CommonState Common = new();
    }
}
