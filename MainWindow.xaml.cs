using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AI.Talk.Editor.Api;
using test_AIVOICE.IPC;
using test_AIVOICE.host;

namespace test_AIVOICE {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        private MainWindowModel _vm = new MainWindowModel();
        private Client cliant = new Client();


        public MainWindow() {
            InitializeComponent();

            this.DataContext = _vm;
            cliant.Init_Client();
        }

        #region Message

        private void ShowStatus(string message) {
            _vm.StatusText = message;
        }

        private void ShowError(Window owner, string message) {
            MessageBox.Show(owner, message, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            this.ShowStatus(message);
        }

        private void ShowError(string message) {
            this.ShowError(this, message);
        }

        #endregion

        #region (Dis)Connection to Host

        private void Startup() {
            try {
                cliant.tts_Startup();
                this.ShowStatus("ホストへの接続を開始しました。");
            }
            catch (Exception ex) {
                this.ShowError("ホストへの接続に失敗しました" + Environment.NewLine + ex.Message);
            }
        }

        private void Disconnect() {
            try {
                cliant._Controler.Disconnect();

                this.ShowStatus("ホストへの接続を終了しました。");
            }
            catch (Exception ex) {
                this.ShowError("ホストへの接続の終了に失敗しました。" + Environment.NewLine + ex.Message);
            }
        }

        #endregion

        #region Event Handler

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            _vm.AvailableHosts = cliant._Controler.GetAvailableHostNames();

            if (_vm.AvailableHosts.Count() > 0) {
                _vm.CurrentHost = _vm.AvailableHosts[0];
            }
        }

        private void Window_Closed(object sender, EventArgs e) {
            this.Disconnect();
            cliant.Queue.Dispose();

            if (_vm.ShutdownHost) {
                Task.Factory.StartNew(() => {
                    cliant._Controler.TerminateHost();
                });
            }
        }

        private void ButtonStartup_Click(object sender, RoutedEventArgs e) {
            try {
                cliant._Controler.Initialize(_vm.CurrentHost);

                this.Startup();
            }
            catch (Exception ex) {
                this.ShowError("接続処理でエラーが発生しました" + Environment.NewLine + ex.Message);
            }
        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e) {
            this.Disconnect();
        }

        private void ButtonTest_Click(object sender, RoutedEventArgs e) {
            try {
                string _demotext = "音声出力とテキスト転送のテストです";

                cliant._Controler.Text = _demotext;


                cliant._Controler.Play();

                this.ShowStatus("テスト成功");
            }
            catch (Exception ex) {
                this.ShowError("テスト失敗" + Environment.NewLine + ex.Message);
            }
        }

        private void ButtonIpcStartup_Click(object sender, RoutedEventArgs e) {
            try {
                IpcServer _Server = new IpcServer(cliant);

                this.ShowStatus("起動完了");
            }
            catch (Exception ex) {
                this.ShowError("起動失敗" + Environment.NewLine + ex.Message);
            }
        }

        #endregion // EventHandler

    }
}
