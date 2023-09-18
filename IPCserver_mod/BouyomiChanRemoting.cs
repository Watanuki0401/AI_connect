using System;
using test_AIVOICE.IPC;

namespace FNF.Utility {
    class BouyomiChanRemoting : MarshalByRefObject {
        private readonly IpcServer _Server;

        public BouyomiChanRemoting(IpcServer server) {
            this._Server = server;
        }

        public override object InitializeLifetimeService() {
            return null;
        }

        public void AddTalkTask(string sTalkText) {
            _Server.AddTalkTask(sTalkText);
        }

        public void AddTalkTask(string sTalkText, int iSpeed, int iVolume, int vType) {
            _Server.AddTalkTask(sTalkText);
        }

        public void AddTalkTask(string sTalkText, int iSpeed, int iTone, int iVolume, int vType) {
            _Server.AddTalkTask(sTalkText);
        }

        public int AddTalkTask2(string sTalkText) {
            return _Server.AddTalkTask(sTalkText);
        }

        public int AddTalkTask2(string sTalkText, int iSpeed, int iTone, int iVolume, int vType) {
            return _Server.AddTalkTask(sTalkText);
        }

        public void ClearTaskTasks() { }

        public void SkipTalkTask() { }

        public int TalkTaskCount { get; internal set; }

        public int NowTaskId { get; internal set; }

        public bool NowPlaying { get; internal set; }

        public bool Pause { get; internal set; }

    }
}
