using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace WrathModdingHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            //Environment.SetEnvironmentVariable("WrathPath", "value", EnvironmentVariableTarget.User);

            if (App.DevEnv.Length == 0)
            {
                ContentTitle.Value = "Required tools are missing";
                Error_VS.Visibility = Visibility.Visible;
                return;
            }

            CurrentStage = 0;

            return;
        }

        private readonly ModCreationStuff state = new();

        public IWizardStage WizardStage => allStages[CurrentStage];

        private void Notify(params string[] properties)
        {
            foreach (var p in properties)
                PropertyChanged?.Invoke(this, new(p));
        }

        public string CannotProgressReason => WizardStage.CanProgress ? "" : WizardStage.StallReason;

        public bool HasNext => CurrentStage < allStages.Count - 1 && WizardStage.CanProgress;
        public bool HasPrev => CurrentStage > 0;

        private int _CurrentStage = 0;
        private int CurrentStage
        {
            get => _CurrentStage;
            set
            {
                string[] dependent = { nameof(WizardStage), nameof(HasNext), nameof(HasPrev), nameof(CannotProgressReason) };

                WizardStage.OnWizardStageChanged -= WizardStage_OnWizardStageChanged;
                WizardStage.End(state);

                if (value < 0 || value >= allStages.Count)
                {
                    //???
                    _CurrentStage = -1;
                    Notify(dependent);
                    return;
                }
                else
                {
                    _CurrentStage = value;
                }

                WizardStage.Begin(state);
                WizardStage.OnWizardStageChanged += WizardStage_OnWizardStageChanged;

                Frame.Children.Clear();
                Frame.Children.Add(WizardStage.View);
                ContentTitle.Value = WizardStage.Title;

                Notify(dependent);
            }
        }

        private void WizardStage_OnWizardStageChanged()
        {
            Notify(nameof(HasNext), nameof(CannotProgressReason));
        }

        private readonly List<IWizardStage> allStages = new()
        {
            new ModSettingsStage(),
            new LibraryChoiceStage(),
            new ConfirmStage(),
        };

        public event PropertyChangedEventHandler? PropertyChanged;

        public Prop<string> ContentTitle { get; set; } = new("<no-title>");


        private void OnNextClicked(object sender, RoutedEventArgs e)
        {
            CurrentStage++;
        }

        private void OnPrevClicked(object sender, RoutedEventArgs e)
        {
            CurrentStage--;
        }

        public void OnHyperlinkClicked(object sender, RoutedEventArgs e) => WMH.OnHyperlinkClicked(sender, e);
    }
}
