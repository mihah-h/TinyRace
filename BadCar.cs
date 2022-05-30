using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRace
{
    class BadCar
    {
        public int X { get; set; }
        public int Y { get; set; } = -200;
        public int Width { get; } = 60;
        public int Height { get; } = 80;
        public Image Image { get; }
       
        public int Spead { get; set; }

        public BadCar(int line1, Image playerImage, int spead)
        {
            X = 100 * line1 + 8 * line1 + 32;
            Image = playerImage;
            Spead = spead;
        }
    }
}
