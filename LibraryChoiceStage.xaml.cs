using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using WrathModdingHelper.Libraries;

namespace WrathModdingHelper
{
    public class LibraryChoice
    {
        public LibraryChoice(ILibraryProvider provider)
        {
            Provider = provider;
        }

        public Prop<bool> Enabled { get; set; } = new(false);
        public ILibraryProvider Provider { get; private set; }
        public Prop<bool> Valid { get; set; } = new(true);
    }

    /// <summary>
    /// Interaction logic for LibraryChoiceStage.xaml
    /// </summary>
    public partial class LibraryChoiceStage : UserControl, IWizardStage
    {
        private readonly List<string> badnessReasons = new();

        private bool stalled;

        private ModCreationStuff? state;

        public LibraryChoiceStage()
        {
            AddChoice(new Libraries.BPCore.BPCoreProvider());
            AddChoice(new Libraries.TTTCore.TTTCoreProvider());

            InitializeComponent();
        }

        public event WizardStageChangedDelegate? OnWizardStageChanged;
        public bool CanProgress => !stalled;

        public ObservableCollection<LibraryChoice> AllLibraryChoices { get; set; } = new();

        public string StallReason => string.Join(" & ", badnessReasons);

        public string Title => "Choose which libraries to use for your mod";

        public Control View => this;

        public void Begin(ModCreationStuff state)
        {
            this.state = state;
        }

        public void End(ModCreationStuff state)
        {
            state.LibraryChoices = AllLibraryChoices.Where(ch => ch.Enabled).Select(ch => ch.Provider);
        }

        private void AddChoice(ILibraryProvider provider)
        {
            var choice = new LibraryChoice(provider);
            choice.Enabled.PropertyChanged += LibEnabledOrDisabled;
            AllLibraryChoices.Add(choice);
        }
        private void LibEnabledOrDisabled(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var enabled = AllLibraryChoices.Where(ch => ch.Enabled).Select(ch => ch.Provider);
            foreach (var choice in AllLibraryChoices)
            {
                choice.Valid.Value = choice.Provider.CanAdd(enabled);
            }

            badnessReasons.Clear();
            stalled = false;
            if (state != null)
            {
                foreach (var choice in enabled)
                {
                    if (!choice.CheckDependencies(state.WrathPath, out var badness))
                    {
                        stalled = true;
                        badnessReasons.Add(badness);
                    }

                }
            }

            OnWizardStageChanged?.Invoke();
        }
    }
}
