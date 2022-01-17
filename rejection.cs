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
    public partial class rejection : Form
    {
        public rejection()
        {
            InitializeComponent();
            //Play sound when the application loads.
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\Windows Foreground.wav");
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

        private void rejection_Load(object sender, EventArgs e)
        {

        }
    }
}
