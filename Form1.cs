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
using System.Media;
using System.IO;
using NAudio.Wave;

namespace UniversalAudioPlayer {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            string extPath = Environment.CurrentDirectory + "\\extensions";
            if (!Directory.Exists(extPath)) {
                Directory.CreateDirectory(extPath);
            }
        }

        private void Tick_Tick(object sender, EventArgs e) {
            SecondsToHHMMSS(Convert.ToSingle(textBox1.Text), out string hours, out string minutes, out string seconds, out string fractionalSeconds);
            TimeLabel.Text = $"{hours}:{minutes}:{seconds}{fractionalSeconds}{Environment.NewLine}/{Environment.NewLine}0:0:00.00";
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

        private void openItem_Click(object sender, EventArgs e) {
            openFileDialog.ShowDialog(this);
        }

        private void exitItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void optionsItem_Click(object sender, EventArgs e) {
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            var audioFile = new AudioFileReader(openFileDialog.FileName);
            SoundPlayer soundPlayer = new SoundPlayer(openFileDialog.OpenFile());
            soundPlayer.Play();
            wait((int)(audioFile.Length / (audioFile.WaveFormat.SampleRate * audioFile.WaveFormat.Channels * (audioFile.WaveFormat.BitsPerSample / 8))));
            soundPlayer.Dispose();
        }

        public void wait(int milliseconds) {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0)
                return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled) {
                Application.DoEvents();
            }
        }
    }
}
