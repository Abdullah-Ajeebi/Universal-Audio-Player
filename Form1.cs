using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversalAudioPlayer {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Tick_Tick(object sender, EventArgs e) {
            AudioLengthTablePanel.Size = new System.Drawing.Size(ClientSize.Width - 24, 100);
        }
    }
}
