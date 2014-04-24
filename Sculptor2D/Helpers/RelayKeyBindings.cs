using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace KeyBindingsWPF
{
    public class RelayKeyBinding : KeyBinding
    {
        public static readonly DependencyProperty CommandBindingProperty =
            DependencyProperty.Register("CommandBinding", typeof(ICommand),
            typeof(RelayKeyBinding),
            new FrameworkPropertyMetadata(OnCommandBindingChanged));
        public ICommand CommandBinding
        {
            get { return (ICommand)GetValue(CommandBindingProperty); }
            set { SetValue(CommandBindingProperty, value); }
        }

        private static void OnCommandBindingChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var keyBinding = (RelayKeyBinding)d;
            keyBinding.Command = (ICommand)e.NewValue;
        }
    }
}
