
using EventLogListener.Contract;
using EventLogListener.Contract.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventLogListenerService
{

    public partial class EventService : ServiceBase
    {

        private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        private Thread _thread;

        public EventService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            _thread = new Thread(WorkerThreadFunc);
            _thread.Name = "Global EventLogWatcher Worker Thread";
            _thread.IsBackground = true;
            _thread.Start();

        }

        protected override void OnStop()
        {
            _shutdownEvent.Set();
            if (!_thread.Join(3000))
            {
                // give the thread 3 seconds to stop
                _thread.Abort();
            }
            //StreamWriterExtention.WriteToFile("Service Stop Running: {0}-{1}", DateTime.Now.ToString("yyyy'-'MM'-'dd hh:mm:ss"));
        }

        private void WorkerThreadFunc()
        {

            ///TODOM Melih: Case 1- Listener.Subscribe() in sürekli dinme yapması gerekiyor nasıl. !!!!!!

            //Memory leak oluşturuyor ve tepki süresi çok düşük
            //Kapatılmalı
            while (!_shutdownEvent.WaitOne(0))
            {
                //Replace the Sleep() call with the work you need to do
                //Thread.Sleep(1000);


                //MemoryLeak Oluşuyor nedenini tespit et.
                Listener.Subscribe();

            }


            ///TODO Melih: Case 2
            //Whileın dışına konulduğuda fonksiyon a gidiyor ama hemen ardondan fonksiyonu sonlandırıyor
            //fonksiyon içersindeki Console.ReadLİne() ı beklemiyor.
            //Listener.Subscribe();

        }

    }

}
