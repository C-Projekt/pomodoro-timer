using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pomodoro_Timers
{
    public partial class pomoForm : Form
    {
        decimal pomoTime, breakTime, timeElapsed;
        bool isPomo = true, isStart = true, hasStarted = false;

        private void startBt_Click(object sender, EventArgs e)
        {
            if (isStart)
            {
                if (hasStarted)
                {
                    pomoTimer.Start();

                    ((Button)sender).Text = "Stop";
                    isStart = !isStart;

                    return;
                }
                pomoTime = pomoVal.Value * 60; breakTime = breakVal.Value * 60;
                timeElapsed = 0;

                pomoTimer.Start();

                ((Button)sender).Text = "Stop";
                isStart = !isStart;
                hasStarted = true;
            }
            else
            {
                pomoTimer.Stop();

                ((Button)sender).Text = "Start";
                isStart = !isStart;
            }
        }

        private void pomoTimer_Tick(object sender, EventArgs e)
        {
            timeElapsed += (decimal)(((Timer)sender).Interval) / 1000;

            if (timeElapsed >= (isPomo ? pomoTime : breakTime))
            {
                timeElapsed = 0;
                isPomo = !isPomo;
                this.Text = (isPomo ? "Pomodoro Timer" : "Break Timer!");

                // Play a system sound
                System.Media.SystemSounds.Asterisk.Play();
            }

            // Update progressbar display
            progBar.Value = (int)(timeElapsed / (isPomo ? pomoTime : breakTime) * 100);
        }

        private void resetBt_Click(object sender, EventArgs e)
        {
            timeElapsed = 0;
            progBar.Value = 0;
        }

        private void pomoVal_ValueChanged(object sender, EventArgs e)
        {
            pomoTime = ((NumericUpDown)sender).Value * 60;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            breakTime = ((NumericUpDown)sender).Value * 60;
        }

        public pomoForm()
        {
            InitializeComponent();
        }

        private void pomoForm_Load(object sender, EventArgs e)
        {
            pomoTime = pomoVal.Value * 60; breakTime = breakVal.Value * 60;
            timeElapsed = pomoTime;
        }
    }
}
