using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace HollowKnight.bl
{
    public class HUD
    {
        PictureBox pbImage;
        ProgressBar healthBar;
        ProgressBar staminaBar;

        public HUD(ProgressBar healthBar, ProgressBar staminaBar)
        {
            this.HealthBar = healthBar;
            this.StaminaBar = staminaBar;
        }

        public HUD(PictureBox pb, ProgressBar healthBar, ProgressBar staminaBar)
        {
            this.pbImage = pb;
            this.HealthBar = healthBar;
            this.StaminaBar = staminaBar;
        }

        public HUD CreatePlayerHUD(int health, int stamina)
        {
            PictureBox pb = new PictureBox();
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            pb.Image = Properties.Resources.mask_removebg_preview;
            pb.Location = new Point(25, 25);

            HealthBar.Value = health;
            StaminaBar.Value = stamina;
            HealthBar.ForeColor = Color.Gainsboro;
            HealthBar.BackColor = Color.WhiteSmoke;
            HealthBar.Location = new Point(20 + pb.Width, 25 + 20);
            HealthBar.Size = new Size(HealthBar.Width * 3 ,HealthBar.Height - HealthBar.Height/4);
            HealthBar.Style = ProgressBarStyle.Continuous;

            StaminaBar.ForeColor = Color.Gainsboro;
            StaminaBar.BackColor = Color.WhiteSmoke;
            staminaBar.Location = new Point(17 + pb.Width, HealthBar.Location.Y + HealthBar.Height +1);
            StaminaBar.Size = new Size(StaminaBar.Width * 2, StaminaBar.Height - StaminaBar.Height/2);
            StaminaBar.Style = ProgressBarStyle.Continuous;

            return new HUD(pb, HealthBar, StaminaBar);
        }

        public ProgressBar HealthBar { get => healthBar; set => healthBar = value; }
        public ProgressBar StaminaBar { get => staminaBar; set => staminaBar = value; }
        public PictureBox PbImage { get => pbImage; set => pbImage = value; }
    }
}
