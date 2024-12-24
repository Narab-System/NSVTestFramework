using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSVTestFramework.StressTesting
{
    public class RappidMouse
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        private int _delay;
        private bool _stopMouseMover = false;
        private Thread _myThread;
        public int counter;
        public RappidMouse(int delay)
        {
            _delay = delay;
        }

        public void StartTest()
        {
            if (_myThread != null)
                return;

            _stopMouseMover = false;
            _myThread = new Thread(new ThreadStart(RandomMove));
            _myThread.Start();
        }

        public void StopTest()
        {
            _stopMouseMover = true;
            _myThread.Join();
        }

        private void RandomMove()
        {
            int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

            Random random = new Random();

            while (!_stopMouseMover) 
            {
                int x = random.Next(0, width);
                int y = random.Next(0, height);

                SetCursorPos(x, y);
                counter++;
                Thread.Sleep(_delay); 
            }
        }
    }
}
