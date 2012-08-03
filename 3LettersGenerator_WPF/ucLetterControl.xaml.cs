using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3LettersGenerator_WPF
{
    /// <summary>
    /// Логика взаимодействия для ucLetterControl.xaml
    /// </summary>
    public partial class ucLetterControl : UserControl
    {
        public static readonly DependencyProperty cf_LetterProperty = DependencyProperty.Register(
            "cp_Letter",
            typeof(char),
            typeof(ucLetterControl),
            new PropertyMetadata(' ')
            );

        public char cp_Letter
        {
            get
            {
                return (char)GetValue(cf_LetterProperty);
            }
            set
            {
                SetValue(cf_LetterProperty, value);
            }
        }

        public ucLetterControl()
        {
            InitializeComponent();
        }
    }
}
