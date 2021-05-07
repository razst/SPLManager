using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace packet_maker.MainExt
{
    public class NextPassTimer
    {
        private Timer SecondsTimer;
        private int SecondsTilPass;
        private Label TimerLabel;
        private Label[] LabelsToUpdate;
        public NextPassTimer()
        {
            SecondsTimer = new Timer();
            SecondsTimer.Interval = 1000;
            SecondsTimer.Tick += new EventHandler(Timer_Tick);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
