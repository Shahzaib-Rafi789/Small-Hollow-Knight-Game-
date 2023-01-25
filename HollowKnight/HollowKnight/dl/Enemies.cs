using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HollowKnight.bl;
using System.Drawing;
using System.Windows.Forms;

namespace HollowKnight.dl
{
    class Enemies
    {
        static List<Enemy> enemiesList = new List<Enemy>();

        public static List<Enemy> EnemiesList { get => enemiesList; set => enemiesList = value; }

        public static void AddEnemyIntoList(Enemy E)
        {
            enemiesList.Add(E);
        }

        public static void CreateEnemy(Size activeForm)
        {
            Random RndEnemy = new Random(), RndPosition = new Random();

            PictureBox EnemyApperance = new PictureBox();
            EnemyApperance.SizeMode = PictureBoxSizeMode.AutoSize;
            EnemyApperance.BackColor = Color.Transparent;

            int n = RndEnemy.Next(1, 3);
            if (n == 1) { EnemyApperance.Image = Properties.Resources.HatchlingFlyLeft; }
            else if(n ==2) { EnemyApperance.Image = Properties.Resources.BooflyLeftGif; }

            Point P = new System.Drawing.Point(RndPosition.Next(0 + activeForm.Width / 2, activeForm.Width - activeForm.Width / 2), activeForm.Height / 2 + 150);
            while (!IsLocationFree(P, EnemyApperance))
            {
                P = new System.Drawing.Point(RndPosition.Next(0 + activeForm.Width / 6, activeForm.Width - activeForm.Width / 6), activeForm.Height / 2 + 150);
            }
            EnemyApperance.Location = P;
            Program.form1.Controls.Add(EnemyApperance);

            if (n == 1)
            {
                InsectFly fly = new InsectFly(EnemyApperance, "InsectFly", 1, 70, 10, 40, "Idle", "Right");
                AddEnemyIntoList(fly);
            }
            else if(n==2)
            {
                Boofly booFly = new Boofly(EnemyApperance, "Boofly", 1, 70, 12, 65, "Idle", "Right");
                AddEnemyIntoList(booFly);
            }
        }

        static public void EnemiesAction(int width)
        {
            foreach(Enemy i in enemiesList)
            {
                i.EnemyAction();
                i.MoveEnemyFires();
                i.RemoveEnemyFires(width);
            }
        }

        private static bool IsLocationFree(Point P, PictureBox pb)
        {
            pb.Location = P;
            foreach(Enemy i in enemiesList)
            {
                if(pb.Bounds.IntersectsWith(i.Apperance.Bounds))
                {
                    return false;
                }
            }

            return true;
        }

        static public void RemoveEnemy(Enemy i)
        {
            EnemiesList.Remove(i);
        }
    }
}
