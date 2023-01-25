using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HollowKnight.bl
{
    public class Fire
    {
        PictureBox fireApperance;   
        string fireDirection;

        public Fire(PictureBox fireApperance, string fireDirection)
        {
            this.FireApperance = fireApperance;
            this.FireDirection = fireDirection;
        }
        public PictureBox FireApperance { get => fireApperance; set => fireApperance = value; }
        public string FireDirection { get => fireDirection; set => fireDirection = value; }

        public Fire DrawFire(Image Img, Point location, string direction)
        {
            PictureBox pbFire = new PictureBox();
            pbFire.SizeMode = PictureBoxSizeMode.AutoSize;
            pbFire.BackColor = Color.Transparent;
            pbFire.Image = Img;
            pbFire.Location = location;

            return new Fire(pbFire, direction);
        }
    }
}
