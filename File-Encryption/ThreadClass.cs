 using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace File_Encryption
{
    class ThreadClass
    {

        public static Thread mythread;
        public void StartThread()
        {
            mythread = new Thread(new ThreadStart(showwait));
            mythread.Start();
        }

        public void showwait()
        {
            WaitFrm wf = new WaitFrm();
            wf.ShowDialog();
        }

        public void EndThread()
        {
            mythread.Abort();
            
            mythread = null;
        }
    }
}
