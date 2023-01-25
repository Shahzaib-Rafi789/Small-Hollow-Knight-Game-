using HollowKnight.bl;
using HollowKnight.dl;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using EZInput;

namespace HollowKnight
{
    public partial class Form1 : Form
    {
        Player Grimmkin;
        int LoopCounter = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreatePlayer();
            Enemies.CreateEnemy(this.Size);
            Enemies.CreateEnemy(this.Size);
        }

        private void CreatePlayer()
        {
            PictureBox PlayerApperance = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(PlayerApperance)).BeginInit();
            this.SuspendLayout();
            PlayerApperance.SizeMode = PictureBoxSizeMode.AutoSize;
            PlayerApperance.BackColor = Color.Transparent;
            PlayerApperance.Image = Properties.Resources.ToRightGif;
            PlayerApperance.Location = new System.Drawing.Point(12, this.Height / 2 + 100);
            HUD hud = new HUD(new ProgressBar(), new ProgressBar());
            hud = hud.CreatePlayerHUD(100,100);
            Controls.AddRange(new List<Control>(){ PlayerApperance, hud.PbImage, hud.HealthBar, hud.StaminaBar }.ToArray());


            Grimmkin = new Player(PlayerApperance, 5, 100, 25, 10, 40, "Idle", "Right", 100, 20, 0.75 , 10, hud);
        }
        
        private void GameControlLoop_Tick(object sender, EventArgs e)
        {
            LoopCounter++;
            Grimmkin.PlayerMovement(ref LoopCounter);
            Grimmkin.MovePlayerFires();
            Grimmkin.RemovePlayerFires(this.Width);
            Enemies.EnemiesAction(this.Width);
            CheckFireCollisons();
        }

        private void CheckFireCollisons()
        {
            int index;
            for (int i =0; i<Grimmkin.PlayerFires.Count;i++)
            {
                for(int j = 0; j<Enemies.EnemiesList.Count; j++)
                {
                    if (Enemies.EnemiesList[j].State != "Death" && Grimmkin.PlayerFires.Count != 0)
                    {
                        if(Enemies.EnemiesList[j].Apperance.Bounds.IntersectsWith(Grimmkin.PlayerFires[i].FireApperance.Bounds))
                        {
                            Grimmkin.PlayerFires[i].FireApperance.Hide();
                            Controls.Remove(Grimmkin.PlayerFires[i].FireApperance);
                            Grimmkin.PlayerFires.RemoveAt(i);

                            if (Enemies.EnemiesList[j].Health - Grimmkin.AttackPower <=0)
                            {
                                Enemies.EnemiesList[j].Health = 0;
                                Enemies.EnemiesList[j].State = "Death";
                                Controls.Remove(Enemies.EnemiesList[j].Apperance);
                                //Enemies.RemoveEnemy(Enemies.EnemiesList[j]);
                            }
                            else
                            {
                                Enemies.EnemiesList[j].Health -= Grimmkin.AttackPower;
                            }
                        }
                    }
                }
            }

            foreach(Enemy i in Enemies.EnemiesList)
            {
                for(int j=0; j<i.EnemyFires.Count; j++)
                {

                }
            }
            
        }
    }
}
