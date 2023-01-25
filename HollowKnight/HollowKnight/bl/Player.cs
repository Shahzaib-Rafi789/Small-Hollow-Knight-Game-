using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using EZInput;

namespace HollowKnight.bl
{
    public class Player
    {
        PictureBox apperance;
        int lives;
        int steps;
        double health;
        HUD PlayerHUD;
        double attackPower;
        int attackGap;
        double attackStamina;
        string state;
        string direction;
        double stamina;
        double staminaRecover;
        double staminaDeplete;
        List<Fire> playerFires;

        public Player(PictureBox apperance, int lives, double health ,double attackPower, int attackGap, double attackStamina, string state, string direction, double stamina, double staminaDeplete, double staminaRecover, int steps, HUD hud)
        {
            this.Apperance = apperance;
            this.Lives = lives;
            this.Health = health;
            this.AttackPower = attackPower;
            this.AttackGap = attackGap;
            this.AttackStamina = attackStamina;
            this.State = state;
            this.Direction = direction;
            this.Stamina = stamina;
            this.StaminaDeplete = staminaDeplete;
            this.StaminaRecover = staminaRecover;
            PlayerFires = new List<Fire>();
            this.steps = steps;
            PlayerHUD = hud;
        }

        public void PlayerMovement(ref int loopCounter)
        {
            RecoverStamina();
            SetStateToIdle(loopCounter);

            if (Keyboard.IsKeyPressed(Key.RightArrow))
            {
                if (State != "Jump" && State != "InAir")
                {
                    ChangeStats("Idle", "Right", HollowKnight.Properties.Resources.ToRightGif);
                    Apperance.Left += Steps;
                }
                else
                {
                    if (Direction == "Left" && State == "InAir")
                    {
                        Direction = "Right";
                        Apperance.Image = HollowKnight.Properties.Resources.InAirRight;
                    }
                    Apperance.Left += (Steps / 2 + steps / 4);
                    
                }
            }

            if (Keyboard.IsKeyPressed(Key.LeftArrow))
            {
                if (State != "Jump" && State != "InAir")
                {
                    ChangeStats("Idle", "Left", Properties.Resources.ToLeftGif);
                    Apperance.Left -= Steps;
                }
                else
                {
                    if (Direction == "Right" && State == "InAir")
                    {
                        Direction = "Left";
                        Apperance.Image = HollowKnight.Properties.Resources.InAirLeft;
                    }
                    Apperance.Left -= (Steps / 2 + steps/4);
                }
            }

            if (Keyboard.IsKeyPressed(Key.UpArrow))
            {
                if (State != "Jump" && State != "InAir")
                {
                    Image ImageToSet;
                    if (Direction == "Left") { ImageToSet = Properties.Resources.jumpToLeft; }
                    else { ImageToSet = Properties.Resources.jumpToRight; }

                    ChangeStats("Jump", Direction, ImageToSet);
                    //Apperance.Top -= 3 * Steps;
                }
            }

            if (Keyboard.IsKeyPressed(Key.Space))
            {
                if (State == "Idle" && attackGap < loopCounter  && Stamina + 15 >= attackStamina)
                {
                    loopCounter = 0;
                    DepleteStamina();

                    Image ImageToSet;
                    if (Direction == "Right") { ImageToSet = Properties.Resources.AttackRight; }
                    else { ImageToSet = Properties.Resources.AttackLeft; }

                    ChangeStats("Attack", Direction, ImageToSet);

                    Image Img = Properties.Resources.Fire;
                    Fire PlayerFire = new Fire(null, null);
                    System.Drawing.Point location = new System.Drawing.Point();
                    if (Direction == "Right") { location = new System.Drawing.Point(Apperance.Right + 8, Apperance.Top + 70); }
                    else if (Direction == "Left") { location = new System.Drawing.Point(Apperance.Left - 24, Apperance.Top + 70); }

                    PlayerFire = PlayerFire.DrawFire(Img, location, Direction);
                    Program.form1.Controls.Add(PlayerFire.FireApperance);
                    AddFire(PlayerFire);
                    //SetToIdle();
                }
            }
        }

        private void DepleteStamina()
        {            
            if (Stamina <= StaminaDeplete) { Stamina = 0; }
            else { Stamina -= StaminaDeplete; }

            PlayerHUD.StaminaBar.Value = Convert.ToInt32(stamina);
        }

        private void RecoverStamina()
        {
            if (Stamina != 100)
            {
                if (Stamina + StaminaRecover >= 100) { Stamina = 100;}
                else { Stamina += StaminaRecover; }

                PlayerHUD.StaminaBar.Value = Convert.ToInt32(stamina);
            }
        }

        private void SetStateToIdle(int loopCounter)
        {
            if (State == "Attack")
            {
                if (loopCounter % 3 == 0)
                {
                    if (Direction == "Left") { ChangeStats("Idle", Direction, Properties.Resources.ToLeftGif); }
                    else if (Direction == "Right") { ChangeStats("Idle", Direction, Properties.Resources.ToRightGif); }
                }
            }
            else if (State == "Jump")
            {
                if(loopCounter % 2 == 0)
                {
                    if(apperance.Top > ((Program.form1.Height/2 + 100) - 10 * Steps))
                    {
                        apperance.Top -= 2 * Steps;
                    }
                    else
                    {                        
                        if (Direction == "Left") { ChangeStats("InAir", Direction, Properties.Resources.InAirLeft); }
                        else if (Direction == "Right") { ChangeStats("InAir", Direction, Properties.Resources.InAirRight); }
                    }
                }
            }
            else if (State == "InAir")
            {
                if (loopCounter % 2 == 0)
                {
                    if (apperance.Top < ((Program.form1.Height / 2 + 100)))
                    {
                        apperance.Top += 1 * Steps;
                    }
                    else
                    {
                        if (Direction == "Left") { ChangeStats("Idle", Direction, Properties.Resources.ToLeftGif); }
                        else if (Direction == "Right") { ChangeStats("Idle", Direction, Properties.Resources.ToRightGif); }
                    }
                }
            }
        }

        private void ChangeStats(string NewState, string Direction, Image ImageToSet)
        {
            if (State == NewState)
            {                
                if (!CompareDirection(Direction))
                {
                    this.Direction = Direction;
                    Apperance.Image = ImageToSet;
                }
            }
            else
            {
                State = NewState;
                this.Direction = Direction;
                Apperance.Image = ImageToSet;
                //if (State == "Idle")
                //{                    
                //    if(Direction == "Left") { }
                //}
            }
        }

        private bool CompareDirection(string Direction)
        {
            if(this.Direction == Direction) { return true; }
            return false;
        }

        public void AddFire(Fire fire)
        {
            PlayerFires.Add(fire);
        }

        public void MovePlayerFires()
        {
            foreach(Fire fire in PlayerFires)
            {
                if(fire.FireDirection == "Left") 
                {
                    fire.FireApperance.Left -= 20;
                }
                else if (fire.FireDirection == "Right")
                {
                    fire.FireApperance.Left += 20;
                }
            }
        }

        public void RemovePlayerFires(int formWidth)
        {
            for(int i=0; i<PlayerFires.Count; i++)
            {
                if (PlayerFires[i].FireDirection == "Left")
                {
                    if(PlayerFires[i].FireApperance.Right < 0)
                    {
                        PlayerFires.RemoveAt(i);                        
                    }
                }
                else if (PlayerFires[i].FireDirection == "Right")
                {
                    if (PlayerFires[i].FireApperance.Right -10 > formWidth)
                    {
                        PlayerFires.RemoveAt(i);
                    }
                }
            }
        }

        public PictureBox Apperance { get => apperance; set => apperance = value; }
        public int Lives { get => lives; set => lives = value; }
        public double AttackPower { get => attackPower; set => attackPower = value; }
        public string State { get => state; set => state = value; }
        public string Direction { get => direction; set => direction = value; }
        public double Stamina { get => stamina; set => stamina = value; }
        public double StaminaDeplete { get => staminaDeplete; set => staminaDeplete = value; }
        internal List<Fire> PlayerFires { get => playerFires; set => playerFires = value; }
        public int Steps { get => steps; set => steps = value; }
        public int AttackGap { get => attackGap; set => attackGap = value; }
        public double StaminaRecover { get => staminaRecover; set => staminaRecover = value; }
        public double AttackStamina { get => attackStamina; set => attackStamina = value; }
        public double Health { get => health; set => health = value; }
        
    }
}
