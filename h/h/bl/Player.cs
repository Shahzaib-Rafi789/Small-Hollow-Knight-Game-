using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace h.bl
{
    public class Player
    {
        PictureBox apperance;
        int lives;
        double attackPower;
        string state;
        string diection;
        double stamina;
        double staminaDeplete;

        public Player(PictureBox apperance, int lives, double attackPower, string state, string diection, double stamina, double staminaDeplete)
        {
            this.Apperance = apperance;
            this.Lives = lives;
            this.AttackPower = attackPower;
            this.State = state;
            this.Diection = diection;
            this.Stamina = stamina;
            this.StaminaDeplete = staminaDeplete;
        }

        public PictureBox Apperance { get => apperance; set => apperance = value; }
        public int Lives { get => lives; set => lives = value; }
        public double AttackPower { get => attackPower; set => attackPower = value; }
        public string State { get => state; set => state = value; }
        public string Diection { get => diection; set => diection = value; }
        public double Stamina { get => stamina; set => stamina = value; }
        public double StaminaDeplete { get => staminaDeplete; set => staminaDeplete = value; }
    }
}
