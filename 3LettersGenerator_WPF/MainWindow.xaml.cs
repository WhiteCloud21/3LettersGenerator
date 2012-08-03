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
using System.Threading;

namespace _3LettersGenerator_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly char[] cc_Letters = {
                                                    'Q',
                                                    'W',
                                                    'E',
                                                    'R',
                                                    'T',
                                                    'Y',
                                                    'U',
                                                    'I',
                                                    'O',
                                                    'P',
                                                    'A',
                                                    'S',
                                                    'D',
                                                    'F',
                                                    'G',
                                                    'H',
                                                    'J',
                                                    'K',
                                                    'L',
                                                    'Z',
                                                    'X',
                                                    'C',
                                                    'V',
                                                    'B',
                                                    'N',
                                                    'M'
                                                   };

        private bool cf_isClosed = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        public char[] cp_Letters
        {
            get
            {
                return cc_Letters;
            }
            set
            {

            }
        }

        private void uc_btnGo_Click(object sender, RoutedEventArgs e)
        {
            w_bottomPanel.Children.Clear();
            w_topPanel.Children.Clear();
            Dictionary<char, ucLetterControl> _dic = new Dictionary<char, ucLetterControl>();
            foreach (char _ch in cc_Letters)
            {
                var _lc = new ucLetterControl()
                {
                    FontSize = 32,
                    cp_Letter = _ch,
                };
                _dic.Add(_ch, _lc);
                w_bottomPanel.Children.Add(_lc);
            }

            Random _r = new Random();
            for (int c = 0; c < 3 && !cf_isClosed; c++)
            {
                foreach (ucLetterControl _con in _dic.Values)
                {
                    _con.IsEnabled = true;
                }

                List<char> _okChars = new List<char>(cc_Letters);
                while (_okChars.Count > 1 && !cf_isClosed)
                {
                    Thread.Sleep(50);
                    char _ch = _okChars[_r.Next(0, _okChars.Count)];
                    uc_mainLetter.cp_Letter = _ch;
                    if (_r.Next(10) > 7)
                    {
                        // :(
                        uc_mainLetter.IsEnabled = false;
                        _dic[_ch].IsEnabled = false;
                        _okChars.Remove(_ch);
                    }
                    else
                    {
                        uc_mainLetter.IsEnabled = true;
                    }
                    InvalidateVisual();
                    App.Current.DoEvents();
                }
                if (!cf_isClosed)
                {
                    w_topPanel.Children.Add(new ucLetterControl()
                    {
                        FontSize = 32,
                        cp_Letter = _okChars[0]
                    });
                    App.Current.DoEvents();
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            cf_isClosed = true;
        }
    }
}
