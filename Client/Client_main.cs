using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AI.Talk.Editor.Api;

namespace test_AIVOICE.host {
    public class Client {
        public TtsControl _Controler;

        public void Init_Client() {
            _Controler = new TtsControl();
        }

        public void tts_Startup() {
            //ホストプログラムの起動.
            if (_Controler.Status == HostStatus.NotRunning) {
                _Controler.StartHost();
            }

            // ホストプログラムへの接続.
            _Controler.Connect();
        }

        public void SendText (string text) {
            _Controler.Text = text;
            _Controler.Play(); 
        }
    }
}
