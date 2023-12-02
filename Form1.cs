using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversalAudioPlayer {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Tick_Tick(object sender, EventArgs e) {
            SecondsToHHMMSS(Convert.ToSingle(textBox1.Text), out string hours, out string minutes, out seconds, out fractionalSeconds);
            TimeLabel.Text = $"{hours}:{minutes}:{seconds}{fractionalSeconds}{Environment.NewLine}/{Environment.NewLine}0: 0:00.00";
        }

        public void SecondsToHHMMSS(float secondsIn, out string hoursOut, out string minutesOut, out string secondsOut, out string fractionalSecondsOut) {
            int hours = (int)Convert.ToSingle(secondsIn) / 3600;
            int remainingSeconds = (int)Convert.ToSingle(secondsIn) % 3600;
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;
            float fractionalSeconds = secondsIn - (int)secondsIn;
            fractionalSecondsOut = fractionalSeconds.ToString(".##");
            minutesOut = minutes.ToString("00");
            secondsOut = seconds.ToString("00");
            hoursOut = hours.ToString();
        }

        private void DoTimeResize(object sender, EventArgs e) {
            TimePanel.Size = new Size(ClientSize.Width - 20, 100);
            TimeLabel.Location = new Point((TimePanel.Size.Width / 2) - TimeLabel.Size.Width / 2, 12);
        }
    }
}
