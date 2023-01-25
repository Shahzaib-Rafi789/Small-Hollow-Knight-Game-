using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HollowKnight.bl
{
    abstract public class Enemy
    {
        protected PictureBox apperance;
        protected string name;
        protected int lives;
        protected double health;
        protected int steps;
        protected double attackPower;
        protected string state;
        protected string direction;
        protected List<Fire> enemyFires;

        public Enemy(PictureBox apperance, string name ,int lives, double health, int steps, double attackPower, string state, string direction)
        {
            this.Apperance = apperance;
            this.Name = name;
            this.Lives = lives;
            this.Health = health;
            this.Steps = steps;
            this.AttackPower = attackPower;
            this.State = state;
            this.Direction = direction;
            this.EnemyFires = new List<Fire>();
        }

        public void EnemyAction()
        {
            Random r = new Random();
            int n = r.Next(0, 70);

            if(n == 25 && state != "Death")
            {
                Fire f = new Fire(null, null);
                Image img = Properties.Resources.EnemyFliesFire;
                f = f.DrawFire(img, new System.Drawing.Point(Apperance.Left - 24, Apperance.Top + 30), "Left");
                Program.form1.Controls.Add(f.FireApperance);
                EnemyFires.Add(f);
            }
        }

        public void MoveEnemyFires()
        {
            foreach (Fire fire in EnemyFires)
            {
                if (fire.FireDirection == "Left")
                {
                    fire.FireApperance.Left -= 20;
                }
                else if (fire.FireDirection == "Right")
                {
                    fire.FireApperance.Left += 20;
                }
            }
        }

        public void RemoveEnemyFires(int formWidth)
        {
            for (int i = 0; i < EnemyFires.Count; i++)
            {
                if (EnemyFires[i].FireDirection == "Left")
                {
                    if (EnemyFires[i].FireApperance.Right < 0)
                    {
                        EnemyFires.RemoveAt(i);
                    }
                }
                else if (EnemyFires[i].FireDirection == "Right")
                {
                    if (EnemyFires[i].FireApperance.Right - 5 > formWidth)
                    {
                        EnemyFires.RemoveAt(i);
                    }
                }
            }
        }

        public PictureBox Apperance { get => apperance; set => apperance = value; }
        public string Name { get => name; set => name = value; }
        public int Lives { get => lives; set => lives = value; }
        public double Health { get => health; set => health = value; }
        public int Steps { get => steps; set => steps = value; }
        public double AttackPower { get => attackPower; set => attackPower = value; }
        public string State { get => state; set => state = value; }
        public string Direction { get => direction; set => direction = value; }
        public List<Fire> EnemyFires { get => enemyFires; set => enemyFires = value; }
    }
}
