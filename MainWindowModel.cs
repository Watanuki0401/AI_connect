using System.ComponentModel;

namespace test_AIVOICE {
    class MainWindowModel : INotifyPropertyChanged {

        /// <summary>
        /// 参考元 : https://aivoice.jp/manual/editor/_downloads/4d23ee8983888969ed37a1ad365b253d/WpfSample.zip
        /// </summary>

        // プロパティの値が変更された際に発生.
        public event PropertyChangedEventHandler PropertyChanged;

        // プロパティの値が変更された際に呼び出し.
        protected virtual void OnPropertyChanged(string name) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #region StatusText

        private string _StatusText = "";

        public string StatusText {
            get { return _StatusText; }
            set {
                if (_StatusText != value) {
                    _StatusText = value;
                    OnPropertyChanged(nameof(StatusText));
                }
            }
        }

        #endregion // StatusText

        #region ShutDownHost

        private bool _ShutdownHost = true;

        public bool ShutdownHost {
            get { return _ShutdownHost; }
            set {
                if (ShutdownHost != value) {
                    ShutdownHost = value;
                    OnPropertyChanged(nameof(ShutdownHost));
                }
            }
        }

        #endregion // shutdownhost

        #region AvailableHosts

        private string[] _AvailableHosts = new string[0];

        public string[] AvailableHosts {
            get { return _AvailableHosts; }
            set {
                if (_AvailableHosts != value) {
                    _AvailableHosts = value;
                    OnPropertyChanged(nameof(AvailableHosts));
                }
            }
        }

        #endregion //AvailableHosts

        #region CurrentHost

        private string _CurrentHost = "";

        public string CurrentHost {
            get { return _CurrentHost; }
            set {
                if (_CurrentHost != value) {
                    _CurrentHost = value;
                    OnPropertyChanged(nameof(CurrentHost));
                }
            }
        }

        #endregion // CurrentHost

    }
}
