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
using System.IO;

namespace project_zeta
{
    public partial class Main_Screen : Form
    {
        //Make a threshold for matching
        internal static int threshold_score = 200;

        //Make a variable to store collected points
        internal static int collected_points = 0;

        //Make a boolean to prevent inappropriate pop-ups
        internal static bool avoid_pop_ups = false;

        public Main_Screen()
        {
            InitializeComponent();
        }

        private void Main_Screen_Load(object sender, EventArgs e)
        {
        }

        private void check_eligibility_button_Click(object sender, EventArgs e)
        {
            //Show message if the person experiencing an acute disease
            if (acute_disease.Checked)
            {
                string message = "Maybe you can wait until the disease has gone.";
                MessageBox.Show(message, "Wait", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                avoid_pop_ups = true;
            } else if (claims_to_be_healthy.Checked == false)
            {
                avoid_pop_ups = false;
            }

            //Show message if the person claiming to have no disease but has not proved it yet
            if (claims_to_be_healthy.Checked)
            {
                string message = "Do not date with this person until he or she proves that they do not have any disease. Try Skype instead for the date.";
                MessageBox.Show(message, "Slow Down", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                avoid_pop_ups = true;
            }
            else if (acute_disease.Checked == false)
            {
                avoid_pop_ups = false;
            }

            //Show message if the person has mental health disorder diagnosis not dangerous to the public
            if (not_dangerous.Checked)
            {
                string message = "While dating with this person, try to get to know more about their diagnosis. Try to select crowded places for the date for your own safety.";
                MessageBox.Show(message, "Take Precautions", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //Increment the collected score based on selected values

            //Personality Group
            if (average_personality.Checked)
                collected_points += 10;
            else if(nerd_personality.Checked)
                collected_points += 5;
            else if (amateur_personality.Checked)
                collected_points += 0;
            else if (alpha_personality.Checked)
                collected_points -= 5;
            else if(promicuous_personality.Checked)
                collected_points -= 10;

            //Degree Group
            if (top_university.Checked)
                collected_points += 10;
            else if (average_university.Checked)
                collected_points += 8;
            else if (cc_with_transfer.Checked)
                collected_points += 5;
            else if (cc_without_transfer.Checked || cc_without_transfer.Checked)
                collected_points += 0;

            //Family Members Group
            if (college_grad_with_stem_radioButton.Checked)
                collected_points += 10;
            else if (college_grad.Checked)
                collected_points += 8;
            else if (not_college_graduate_radioButton.Checked)
                collected_points += 0;

            //Car Group
            if (has_car.Checked)
                collected_points += 10;
            else if (no_car.Checked)
                collected_points += 0;

            //Talking Group
            if (common_interests.Checked)
                collected_points += 10;
            else if (emotional_attraction.Checked)
                collected_points += 6;

            //Response Group
            if (stable.Checked)
                collected_points += 10;
            else if (slow_down.Checked && (claims_to_be_healthy.Checked == false || acute_disease.Checked == false))
            {
                string message = "Person you are talking to has slowed down the conversation with you. Your possible relationship may not stable. Be cautious before and after the date!";
                MessageBox.Show(message, "Be Careful", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                collected_points += 5;
            }

            //Interest Match Group
            if (almost_all_of_them.Checked)
                collected_points += 10;
            else if (certain_interests.Checked)
                collected_points += 7;
            else if (small_interests.Checked)
                collected_points += 5;
            else if(no_interest.Checked)
                collected_points += 0;

            //Physical Attraction Group
            if (very_attractive.Checked)
                collected_points += 10;
            else if (somewhat_attractive.Checked)
                collected_points += 8;

            //Politics Group
            if (moderate.Checked)
                collected_points += 10;
            else if (liberal.Checked)
                collected_points += 8;
            else if (conservative.Checked)
                collected_points += 5;
            else if (apolitical.Checked)
                collected_points += 0;

            //Sexual Position
            if (versatile.Checked)
                collected_points += 10;
            else if (bottom.Checked)
                collected_points += 5;

            //Location Group
            if (live_with_you.Checked)
                collected_points += 10;
            else if (wants_you_to_live_with.Checked)
                collected_points += 7;

            //Foreign Language Group
            if (more_than_one.Checked)
                collected_points += 10;
            else if (no_language.Checked)
                collected_points += 0;

            //Give extra point if they want to come out or planning to
            if (open.Checked)
                collected_points += 10;

            //Give extra point if they are honest
            if (honest.Checked)
                collected_points += 10;

            //Foreign Language Group
            if (more_than_one.Checked)
                collected_points += 10;
            else if (no_language.Checked)
                collected_points += 0;

            //Religion Group
            if (irelligious.Checked)
                collected_points += 10;
            else if (no_practice.Checked)
                collected_points += 5;
            else if (religious.Checked)
                collected_points += 0;

            //Travel Group
            if(interested_in_travelling.Checked)
                collected_points += 10;
            else if (not_interested_travelling.Checked)
                collected_points += 0;

            //Give extra credit if they proved they do not have disease
            if (healthy.Checked)
                collected_points += 10;

            //Mental Health Category
            if (mentally_stable.Checked)
                collected_points += 10;
            else if (not_dangerous.Checked)
                collected_points += 0;

            //Alcohol Category
            if (socialising.Checked)
                collected_points += 10;
            else if (no_alcohol.Checked)
                collected_points += 5;
            else if (frequent.Checked)
                collected_points -= 5;

            //Give extra credit if no smoking
            if (no_smoking.Checked)
                collected_points += 10;

            //If collected points are below the threshold score, reject the date
            /*
            * Immediately reject the date if the date falls out under these categories;
            * Lower Eligibility Score
            * Little to No Talking
            * Ghosting
            * Lack of Physical Attractiveness
            * Not planning to come out (LGBT Only)
            * Sexual Position Missmatch(LGBT Only)
            * Wants to split in the future
            * Communicable Disease
            * Danger to the Public
            * Marijuana Consumption
            * Dishonesty
            */
            if (collected_points < threshold_score || (marijuana.Checked || dangerous.Checked || infected_person.Checked || dishonest.Checked || wantstosplit.Checked ||
                top.Checked || not_planning.Checked || not_attractive.Checked || ghosting.Checked || no_talk.Checked || severe_restrictions.Checked
                || cigarettes.Checked) && (avoid_pop_ups == false))
            {
                rejection reject = new rejection();
                reject.Show();
            }

            //If collected points are below the threshold score, approve the date
            else if ((marijuana.Checked == false || dangerous.Checked == false || infected_person.Checked == false || dishonest.Checked == false || wantstosplit.Checked == false ||
                top.Checked == false || not_planning.Checked == false || not_attractive.Checked == false || ghosting.Checked == false || no_talk.Checked == false || severe_restrictions.Checked == false
                || cigarettes.Checked == false) && (collected_points >= threshold_score) && (avoid_pop_ups == false))
            {
                acceptance accept = new acceptance();
                accept.Show();
            }

        }

        private void exactMatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set threshold to 200
            threshold_score = 200;

            //Change the visual state
            exactMatchToolStripMenuItem.Checked = true;
            perfectMatchToolStripMenuItem.Checked = false;
            stillPossibleToolStripMenuItem.Checked = false;
            tossupToolStripMenuItem.Checked = false;
            smallChanceToolStripMenuItem.Checked = false;
        }

        private void perfectMatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set threshold to 160
            threshold_score = 160;

            //Change the visual state
            exactMatchToolStripMenuItem.Checked = false;
            perfectMatchToolStripMenuItem.Checked = true;
            stillPossibleToolStripMenuItem.Checked = false;
            tossupToolStripMenuItem.Checked = false;
            smallChanceToolStripMenuItem.Checked = false;
        }

        private void stillPossibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set threshold to 120
            threshold_score = 120;

            //Change the visual state
            exactMatchToolStripMenuItem.Checked = false;
            perfectMatchToolStripMenuItem.Checked = false;
            stillPossibleToolStripMenuItem.Checked = true;
            tossupToolStripMenuItem.Checked = false;
            smallChanceToolStripMenuItem.Checked = false;
        }

        private void tossupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set threshold to 100
            threshold_score = 100;

            //Change the visual state
            exactMatchToolStripMenuItem.Checked = false;
            perfectMatchToolStripMenuItem.Checked = false;
            stillPossibleToolStripMenuItem.Checked = false;
            tossupToolStripMenuItem.Checked = true;
            smallChanceToolStripMenuItem.Checked = false;
        }

        private void smallChanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set threshold to 25
            threshold_score = 25;

            //Change the visual state
            exactMatchToolStripMenuItem.Checked = false;
            perfectMatchToolStripMenuItem.Checked = false;
            stillPossibleToolStripMenuItem.Checked = false;
            tossupToolStripMenuItem.Checked = false;
            smallChanceToolStripMenuItem.Checked = true;
        }

        private void about_Click(object sender, EventArgs e)
        {
            //Show about box
            About about = new About();
            about.Show();
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            //Let the user upload the picture of the date
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "JPG Files (*.jpg)| *.jpg";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                //Stretch the image
                user_profile_pic.SizeMode = PictureBoxSizeMode.StretchImage;

                //Set the image
                user_profile_pic.Image = Image.FromFile(fileOpen.FileName);
            }
            fileOpen.Dispose();
        }
    }
}
