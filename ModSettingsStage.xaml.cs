using Ookii.Dialogs.Wpf;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WrathModdingHelper.Libraries;

namespace WrathModdingHelper
{
    /// <summary>
    /// Interaction logic for ModSettingsStage.xaml
    /// </summary>
    public partial class ModSettingsStage : UserControl, IWizardStage
    {
        public event WizardStageChangedDelegate? OnWizardStageChanged;


        public ModSettingsStage()
        {
            InitializeComponent();
            if (App.FindGameFolder(out var path))
                WrathPath.Value = path;

            Folder.Value = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Helpers.DeleteDirectory(Path.Combine(Folder.Value, "bubbletest"));

            WrathPath.PropertyChanged += (sender, e) => OnWizardStageChanged?.Invoke();
            FolderAndName.PropertyChanged += (sender, e) => OnWizardStageChanged?.Invoke();
            DataContext = this;
        }

        public string Title => "Mod Setup";

        public Control View => this;

        public void Begin(ModCreationStuff state)
        {
            Author.Value = state.Author;
            ModName.Value = state.ModName;
        }

        public void End(ModCreationStuff state) {
            state.ModName = ModName.Value;
            state.WrathPath = WrathPath.Value;
            state.ModFolder = FolderAndName.Value;
            state.ModId = ModID.Value;
        }
        public Prop<string> Folder { get; } = new("");

        public Prop<string> WrathPath { get; } = new("");
        public Prop<string> Author { get; } = new("Your Name");
        public Prop<string> ModName { get; } = new("bubbletest"); // new("My First Mod - " + Guid.NewGuid().ToString("N")[26..]);
        public DProp<string, string> ModID => ModName.To(n => n.Replace(" ", "").Replace("-", ""));
        public bool CanProgress => Directory.Exists(WrathPath.Value) && !Directory.Exists(FolderAndName.Value);
        public string StallReason
        {
            get
            {
                if (!Directory.Exists(WrathPath.Value))
                    return "Game Path must be valid";
                if (Directory.Exists(FolderAndName.Value))
                    return "Project folder already exists";

                return "";
            }
        }

        public DProp<string, string, string> FolderAndName => new(Folder, ModID, (folder, name) => $"{folder}{Path.DirectorySeparatorChar}{name}");
        
        private static bool GetFolder(string title, out string path)
        {
            path = "";
            VistaFolderBrowserDialog? dialog = new()
            {
                Description = title,
                UseDescriptionForTitle = true // This applies to the Vista style dialog only, not the old dialog.
            };

            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
            {
                throw new IOException("Pre-vista = no");
            }

            if (dialog.ShowDialog() == true)
            {
                path = dialog.SelectedPath;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SelectFolderPath(object sender, RoutedEventArgs e)
        {
            if (GetFolder( "Please select the parent folder in which your mod will be created.", out var path))
                Folder.Value = path;
        }

        private void SelectWrathPath(object sender, RoutedEventArgs e)
        {
            if (GetFolder("Please select the 'Pathfinder - Second Adventure' folder.", out var path))
                WrathPath.Value = path;
        }
    }

}
