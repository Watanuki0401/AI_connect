using System;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using AI.Talk.Editor.Api;
using FNF.Utility;
using test_AIVOICE.host;

namespace test_AIVOICE.IPC {
    public class IpcServer : IDisposable {
        private readonly BouyomiChanRemoting _Object;
        private readonly IpcServerChannel _Server;
        private Client client;
        
        public IpcServer(Client cliant) {
            _Object = new BouyomiChanRemoting(this);
            _Server = new IpcServerChannel("BouyomiChan");

            ChannelServices.RegisterChannel(_Server, false);
            RemotingServices.Marshal(_Object, "Remoting", typeof(BouyomiChanRemoting));
            this.client = cliant;
        }

        public void Dispose() {
            RemotingServices.Disconnect(_Object);
            _Server.StopListening(null);
            ChannelServices.UnregisterChannel(_Server);
        }

        public int AddTalkTask(string sTalkText) {
            client.SendText(sTalkText);
            return 0;
        }
    }
}
