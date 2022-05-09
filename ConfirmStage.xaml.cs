using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WrathModdingHelper.Libraries;

namespace WrathModdingHelper
{
    /// <summary>
    /// Interaction logic for ConfirmStage.xaml
    /// </summary>
    public partial class ConfirmStage : UserControl, IWizardStage
    {
        public ConfirmStage()
        {
            InitializeComponent();
        }

        public bool CanProgress => true;
        public string StallReason => "";

        public string Title => "Complete!";

        public Control View => this;

        public ObservableCollection<TutorialLink> TutorialLinks { get; set; } = new();

        private readonly TutorialLink genericIntroTutorial = new(){ Link = "https://github.com/WittleWolfie/OwlcatModdingWiki/wiki", Text = "General modding tutorials" };

        #pragma warning disable CS0067
        public event WizardStageChangedDelegate? OnWizardStageChanged;
#pragma warning restore CS0067

        private ModCreationStuff? state;

        public void Begin(ModCreationStuff state)
        {
            TutorialLinks.Clear();
            TutorialLinks.Add(genericIntroTutorial);

            foreach (var link in state.LibraryChoices.SelectMany(lib => lib.Links))
                TutorialLinks.Add(link);
                

            this.state = state;

        }

        public void End(ModCreationStuff state)
        {
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e) => WMH.OnHyperlinkClicked(sender, e);

        private void GenerateProject(object sender, RoutedEventArgs e)
        {
            if (state == null) return;

            Templates.csproj csproj = new();

            StringBuilder builder = new();
            List<string> merge = new();
            List<NugetPackage> packages = new();

            foreach (var lib in state.LibraryChoices)
            {
                builder.AppendLine("    <ItemGroup>");
                foreach (var fragmentLine in lib.ProjectFileFragment)
                {
                    builder.Append("        ");
                    builder.AppendLine(fragmentLine);
                }
                builder.AppendLine("    </ItemGroup>");
                builder.AppendLine();

                merge.AddRange(lib.MergeAssemblies);
                packages.AddRange(lib.NugetPackages);

            }

            csproj.InjectedFragments = builder.ToString();
            csproj.NugetPackages = packages;
            csproj.MergeAssemblies = merge;

            Templates.CommonState templateState = new()
            {
                DefaultNamespace = state.ModId,
                ModId = state.ModId,
            };

            Templates.Info info = new();
            info.Stuff = state;

            Templates.Main main = new();
            main.Common = templateState;

            Templates.ModletExample exampleModlet = new();
            exampleModlet.Common = templateState;

            Directory.CreateDirectory(state.ModFolder);
            var modletDir = Directory.CreateDirectory(Path.Combine(state.ModFolder, "Modlets"));
            var projFile = Path.Combine(state.ModFolder, $"{state.ModId}.csproj");
            File.WriteAllText(projFile, csproj.TransformText());
            File.WriteAllText(Path.Combine(state.ModFolder, "Info.json"), info.TransformText());
            File.WriteAllText(Path.Combine(state.ModFolder, "Main.cs"), main.TransformText());

            File.WriteAllText(Path.Combine(modletDir.FullName, "ExampleModlet.cs"), exampleModlet.TransformText());

            foreach (var lib in state.LibraryChoices)
            {
                lib.GenerateProject(state.ModFolder, templateState);
            }

            Debug.WriteLine(App.MSBuild(projFile, "-r", "-t:Clean"));

        }
    }
}
