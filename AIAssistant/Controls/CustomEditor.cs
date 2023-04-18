using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls.Internals;
using AIAssistant.Utils;

namespace AIAssistant.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty(nameof(Text))]
    public class CustomEditor : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEditor), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomEditor), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), BindingMode.OneWay);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CustomEditor), Color.FromHex("#4f6367"), BindingMode.OneWay);

        private readonly Editor _editor;

        public CustomEditor()
        {
            _editor = new Editor
            {
                FontSize = FontSize,
                TextColor = TextColor
            };

            Content = _editor;

            _editor.TextChanged += OnTextChanged;
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set
            {
                SetValue(FontSizeProperty, value);
                _editor.FontSize = value;
            }
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set
            {
                SetValue(TextColorProperty, value);
                _editor.TextColor = value;
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Format the code - this will depend on the formatter you're using
            var formattedCode = CodeFormatter.Format(_editor.Text);

            // Set the formatted text as the Text property so it will be sent up to the ViewModel
            Text = formattedCode;
        }
    }
}

