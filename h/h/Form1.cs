using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using h.bl;

namespace h
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            PictureBox PlayerApperance = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(PlayerApperance)).BeginInit();
            this.SuspendLayout();
            PlayerApperance.SizeMode = PictureBoxSizeMode.CenterImage;
            PlayerApperance.BackColor = Color.Transparent;
            PlayerApperance.BackgroundImage = Properties.Resources.InAirRight;
            PlayerApperance.Location = new Point(12, this.Height / 2 + 10);
            PlayerApperance.TabIndex = 0;
            PlayerApperance.TabStop = false;
            Controls.Add(PlayerApperance);

            Player Grimmkin = new Player(PlayerApperance, 5, 70, "Idle", "Right", 100, 20);
        }
    }
}
