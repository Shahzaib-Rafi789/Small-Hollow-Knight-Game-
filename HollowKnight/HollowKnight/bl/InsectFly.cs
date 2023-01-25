using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HollowKnight.bl
{
    public class InsectFly : Enemy
    {
        public InsectFly(PictureBox apperance, string name, int lives, double health, int steps, double attackPower, string state, string direction) : base(apperance, name, lives, health, steps, attackPower, state, direction)
        {
        }
    }
}
