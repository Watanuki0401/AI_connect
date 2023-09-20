using AI.Talk.Editor.Api;
using System.Collections.Concurrent;


namespace test_AIVOICE.host {
    public class Client {
        public TtsControl _Controler;
        private static readonly int MAX_QUE = 30;
        public BlockingCollection<string> Queue = null;

        public void Init_Client() {
            _Controler = new TtsControl();
            Queue = new BlockingCollection<string>();
        }

        public void tts_Startup() {
            //ホストプログラムの起動.
            if (_Controler.Status == HostStatus.NotRunning) {
                _Controler.StartHost();
            }

            // ホストプログラムへの接続.
            _Controler.Connect();
        }

        public void Addque(string text) {
            Queue.TryAdd(text);
            if (_Controler.Status == HostStatus.NotConnected) {
                _Controler.Connect();
            }
            if (_Controler.Status == HostStatus.Idle) {
                Receiver();
            }
        }

        public void Receiver() {
            string str;
            for (; ; ) {
                if (_Controler.Status == HostStatus.Idle) {
                    Queue.TryTake(out str);
                    if (str == null) break;
                    _Controler.Text = str;
                    _Controler.Play();
                }

            }
        }
    }
}
