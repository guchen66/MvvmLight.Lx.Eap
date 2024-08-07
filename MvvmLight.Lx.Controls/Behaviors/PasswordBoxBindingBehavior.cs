
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
namespace MvvmLight.Lx.Controls.Behaviors
{
    public class PasswordBoxBindingBehavior : Behavior<PasswordBox>
    {
        //
        // 摘要:
        //     Gets or sets the bindable Password property on the PasswordBox control. This
        //     is a dependency property.
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxBindingBehavior), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordPropertyChanged));

     //   private static readonly DependencyProperty IsChangingProperty = DependencyProperty.RegisterAttached("IsChanging", typeof(bool), typeof(PasswordBoxBindingBehavior), new UIPropertyMetadata(BooleanBoxes.FalseBox));

        private static readonly DependencyProperty SelectionProperty = DependencyProperty.RegisterAttached("Selection", typeof(TextSelection), typeof(PasswordBoxBindingBehavior), new UIPropertyMetadata((object)null));

        private static readonly DependencyProperty RevealedPasswordTextBoxProperty = DependencyProperty.RegisterAttached("RevealedPasswordTextBox", typeof(TextBox), typeof(PasswordBoxBindingBehavior), new UIPropertyMetadata((object)null));

        [Category("MahApps.Metro")]
        public static string GetPassword(DependencyObject dpo)
        {
            return (string)dpo.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject dpo, string value)
        {
            dpo.SetValue(PasswordProperty, value);
        }

        //
        // 摘要:
        //     Handles changes to the 'Password' attached property.
        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordBoxPasswordChanged;
                passwordBox.Password = (string)e.NewValue;
               /* if (!GetIsChanging(passwordBox))
                {
                    passwordBox.Password = (string)e.NewValue;
                }*/

                passwordBox.PasswordChanged += PasswordBoxPasswordChanged;
            }
        }

        //
        // 摘要:
        //     Handle the 'PasswordChanged'-event on the PasswordBox
        private static void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
          //  SetIsChanging(passwordBox, value: true);
            SetPassword(passwordBox, passwordBox.Password);
         //   SetIsChanging(passwordBox, value: false);
        }

        private static void SetRevealedPasswordCaretIndex(PasswordBox passwordBox)
        {
            TextBox revealedPasswordTextBox = GetRevealedPasswordTextBox(passwordBox);
            if (revealedPasswordTextBox != null)
            {
                int passwordBoxCaretPosition = GetPasswordBoxCaretPosition(passwordBox);
                revealedPasswordTextBox.CaretIndex = passwordBoxCaretPosition;
            }
        }

        private static int GetPasswordBoxCaretPosition(PasswordBox passwordBox)
        {
            TextSelection selection = GetSelection(passwordBox);
            object obj = (selection?.GetType().GetInterfaces().FirstOrDefault((Type i) => i.Name == "ITextRange"))?.GetProperty("Start")?.GetGetMethod()?.Invoke(selection, null);
            return (obj?.GetType().GetProperty("Offset", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj, null) as int?).GetValueOrDefault(0);
        }

        //
        // 摘要:
        //     Called after the behavior is attached to an AssociatedObject.
        //
        // 言论：
        //     Override this to hook up functionality to the AssociatedObject.
        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.PasswordChanged += PasswordBoxPasswordChanged;
            base.AssociatedObject.Loaded += PasswordBoxLoaded;
            TextSelection selection = GetSelection(base.AssociatedObject);
            if (selection != null)
            {
                selection.Changed += PasswordBoxSelectionChanged;
            }
        }

        private void PasswordBoxLoaded(object sender, RoutedEventArgs e)
        {
            SetPassword(base.AssociatedObject, base.AssociatedObject.Password);
            TextBox textBox = base.AssociatedObject.FindChild<TextBox>("RevealedPassword");
            if (textBox == null)
            {
                return;
            }

            TextSelection selection = GetSelection(base.AssociatedObject);
            if (selection == null)
            {
                selection = base.AssociatedObject.GetType().GetProperty("Selection", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(base.AssociatedObject, null) as TextSelection;
                SetSelection(base.AssociatedObject, selection);
                if (selection != null)
                {
                    SetRevealedPasswordTextBox(base.AssociatedObject, textBox);
                    SetRevealedPasswordCaretIndex(base.AssociatedObject);
                    selection.Changed += PasswordBoxSelectionChanged;
                }
            }
        }

        private void PasswordBoxSelectionChanged(object sender, EventArgs e)
        {
            SetRevealedPasswordCaretIndex(base.AssociatedObject);
        }

        //
        // 摘要:
        //     Called when the behavior is being detached from its AssociatedObject, but before
        //     it has actually occurred.
        //
        // 言论：
        //     Override this to unhook functionality from the AssociatedObject.
        protected override void OnDetaching()
        {
            if (base.AssociatedObject != null)
            {
                TextSelection selection = GetSelection(base.AssociatedObject);
                if (selection != null)
                {
                    selection.Changed -= PasswordBoxSelectionChanged;
                }

                base.AssociatedObject.Loaded -= PasswordBoxLoaded;
                base.AssociatedObject.PasswordChanged -= PasswordBoxPasswordChanged;
            }

            base.OnDetaching();
        }

     /*   private static bool GetIsChanging(UIElement element)
        {
            return (bool)element.GetValue(IsChangingProperty);
        }

        private static void SetIsChanging(UIElement element, bool value)
        {
            element.SetValue(IsChangingProperty, BooleanBoxes.Box(value));
        }*/

        private static TextSelection GetSelection(DependencyObject obj)
        {
            return (TextSelection)obj.GetValue(SelectionProperty);
        }

        private static void SetSelection(DependencyObject obj, TextSelection value)
        {
            obj.SetValue(SelectionProperty, value);
        }

        private static TextBox GetRevealedPasswordTextBox(DependencyObject obj)
        {
            return (TextBox)obj.GetValue(RevealedPasswordTextBoxProperty);
        }

        private static void SetRevealedPasswordTextBox(DependencyObject obj, TextBox value)
        {
            obj.SetValue(RevealedPasswordTextBoxProperty, value);
        }
    }
}
