using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Animation;

namespace _3LettersGenerator_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly char[] cc_Letters = {
                                                    'A',
                                                    'B',
                                                    'C',
                                                    'D',
                                                    'E',
                                                    'F',
                                                    'G',
                                                    'H',
                                                    'I',
                                                    'J',
                                                    'K',
                                                    'L',
                                                    'M',
                                                    'N',
                                                    'O',
                                                    'P',
                                                    'Q',
                                                    'R',
                                                    'S',
                                                    'T',
                                                    'U',
                                                    'V',
                                                    'W',
                                                    'X',
                                                    'Y',
                                                    'Z'
                                                   };

        private bool cf_isClosed = false;

        private Timer cf_timer = null;
        private Object cf_timerLockObj = new Object();

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

        private void cm_timerCallback(object state)
        {
            cState _state = state as cState;

            if (_state.cf_OkChars.Count > 1 && !cf_isClosed)
            {
                _state.cf_CurrentChar = _state.cf_OkChars[_state.cf_R.Next(0, _state.cf_OkChars.Count)];
                if (_state.cf_R.Next(10) > 5)
                {
                    _state.cf_CurrentState = false;
                    _state.cf_OkChars.Remove(_state.cf_CurrentChar);
                }
                else
                {
                    _state.cf_CurrentState = true;
                }
                Dispatcher.Invoke((Action<cState>)cm_showCurrentLetter, _state);
            }
            else
            {

                lock (cf_timerLockObj)
                {
                    if (cf_timer != null)
                        cf_timer.Change(1000, 50);
                }
                _state.cf_LetterNum++;
                if (!cf_isClosed)
                {
                    Dispatcher.Invoke((Action<cState>)cm_showGeneratedLetter, _state);
                }
                if (_state.cf_LetterNum <= 2)
                {
                    Dispatcher.Invoke((Action<cState>)cm_initNewLetter, _state);
                    _state.cf_OkChars = new List<char>(cc_Letters);
                }
                else
                {
                    cm_stopTimer();
                }
            }
        }

        private void cm_showCurrentLetter(cState state)
        {
            uc_mainLetter.cp_Letter = state.cf_CurrentChar;
            if (state.cf_CurrentState)
            {
                uc_mainLetter.IsEnabled = true;
            }
            else
            {
                // :(
                uc_mainLetter.IsEnabled = false;
                state.cf_Dic[state.cf_CurrentChar].IsEnabled = false;
            }
        }

        private void cm_showGeneratedLetter(cState state)
        {
            w_topPanel.Children.Add(new ucTopLetterControl()
            { 
                cp_Letter = state.cf_OkChars[0]
            });
        }

        private void cm_initNewLetter(cState state)
        {
            foreach (ucBottomLetterControl _con in state.cf_Dic.Values)
            {
                _con.IsEnabled = true;
            }
        }

        private void uc_btnGo_Click(object sender, RoutedEventArgs e)
        {
            cm_stopTimer();
            w_bottomPanel.Children.Clear();
            w_topPanel.Children.Clear();
            Dictionary<char, ucBottomLetterControl> _dic = new Dictionary<char, ucBottomLetterControl>();
            foreach (char _ch in cc_Letters)
            {
                var _lc = new ucBottomLetterControl()
                {
                    cp_Letter = _ch,
                };
                _dic.Add(_ch, _lc);
                w_bottomPanel.Children.Add(_lc);
            }

            Random _r = new Random();

            cf_timer = new Timer(
                cm_timerCallback,
                new cState()
                {
                    cf_Dic = _dic,
                    cf_OkChars = new List<char>(cc_Letters)
                },
                0,
                50);
        }

        private void cm_stopTimer()
        {
            lock (cf_timerLockObj)
            {
                if (cf_timer != null)
                    cf_timer.Dispose();
                cf_timer = null;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            cm_stopTimer();
            cf_isClosed = true;
        }

        private class cState
        {
            public Random cf_R = new Random();
            public Dictionary<char, ucBottomLetterControl> cf_Dic;
            public List<char> cf_OkChars;

            public int cf_LetterNum = 0;
            public char cf_CurrentChar;
            /// <summary>
            /// true - принимаю, false - не принимаю.
            /// </summary>
            public bool cf_CurrentState;
        }
    }
}
