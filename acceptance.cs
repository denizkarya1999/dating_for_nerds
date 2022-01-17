using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace project_zeta
{
    public partial class acceptance : Form
    {
        public acceptance()
        {
            InitializeComponent();
        }

        private void acceptance_Load(object sender, EventArgs e)
        {
            //Play sound when the application loads.
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\tada.wav");
            simpleSound.Play();

            // Reset scores when this pops up
            Main_Screen.threshold_score = 200;
            Main_Screen.collected_points = 0;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            //Close the form
            this.Close();
        }
    }
}
