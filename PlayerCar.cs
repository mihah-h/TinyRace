using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TinyRace
{
    class PlayerCar 
    {
        public int Width { get; } = 60;
        public int Height { get; } = 80;
        public int X { get; set; }
        public int Y { get; set; }
        public Image Image { get; set; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\car_1.png");
        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }
        public bool GoUp { get; set; }
        public bool GoDown { get; set; }
        public bool Jump { get; set; }
        public int JumpsQuantity { get; set; }
        public int Score { get; set; } 
        public int Record { get; set; } 
        public PlayerCar(int x, int y, int record)
        {
            X = x;
            Y = y;
            Record = record;
        }
    }
}
