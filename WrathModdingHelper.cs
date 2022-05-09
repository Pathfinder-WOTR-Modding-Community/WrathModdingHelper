using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WrathModdingHelper.Libraries;

namespace WrathModdingHelper
{
    public static class WMH
    {
        public static void OnHyperlinkClicked(object sender, RoutedEventArgs e)
        {
            if (sender is Hyperlink link)
            {
                Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri) { UseShellExecute = true, Verb = "open" });
            }
        }

    }
    public delegate void WizardStageChangedDelegate();

    public interface IWizardStage
    {
        string Title { get; }

        void Begin(ModCreationStuff state);
        void End(ModCreationStuff state);

        bool CanProgress { get; }
        string StallReason { get; }

        Control View { get; }

        event WizardStageChangedDelegate? OnWizardStageChanged;
    }


    public class ModCreationStuff
    {
        public string WrathPath = "";
        public IEnumerable<ILibraryProvider> LibraryChoices = Enumerable.Empty<ILibraryProvider>();
        public string Author = "Author";
        public string ModName = "ModName";
        internal string ModFolder = "";
        internal string ModId = "";
    }

    public interface IProp<T> : INotifyPropertyChanged
    {
        public T Value { get; }
    }

    public class DProp<TFrom1, TFrom2, T> : IProp<T>
    {
        private readonly IProp<TFrom1> prop1;
        private readonly IProp<TFrom2> prop2;
        private readonly Func<TFrom1, TFrom2, T> func;

        public DProp(IProp<TFrom1> prop1, IProp<TFrom2> prop2, Func<TFrom1, TFrom2, T> func)
        {
            this.prop1 = prop1;
            this.prop2 = prop2;
            this.func = func;

            prop1.PropertyChanged += AnyChanged;
            prop2.PropertyChanged += AnyChanged;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void AnyChanged(object? sender, PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, new(nameof(Value)));
        public T Value => func(prop1.Value, prop2.Value);

    }

    public class DProp<TFrom, T> : IProp<T>
    {
        private readonly IProp<TFrom> prop;
        private readonly Func<TFrom, T> func;

        public DProp(IProp<TFrom> prop, Func<TFrom, T> func)
        {
            this.prop = prop;
            this.func = func;

            prop.PropertyChanged += (sender, e) => PropertyChanged?.Invoke(this, new(nameof(Value)));
        }

        public T Value => func(prop.Value);

        public event PropertyChangedEventHandler? PropertyChanged;

    }


    public class Prop<T> : IProp<T>
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public DProp<T, To> To<To>(Func<T, To> func)
        {
            return new DProp<T, To>(this, func);
        }

        public Prop(T initial) {  _Value = initial; }
        public Prop() { }

        private T _Value = default!;
        public T Value {
            get => _Value;
            set {
                _Value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public static implicit operator T(Prop<T> p) => p.Value;

    }
}
