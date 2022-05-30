using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRace
{
    class TinyCarImage
    {

        
        static public Image PlayerCar1 { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\car_1.png");
        static public Image PlayerCar2 { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\car_2.png");
        static public Image PlayerCar3 { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\car_3.png");
        static public Image BadCar1 { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\car_green_5.png");
        static public Image BadCar2 { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\car_blue_3.png");
        static public Image BadCar3 { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\car_black_1.png");
        static public Image BadCar4 { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\car_red_4.png");
        static public Image Background1 { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\road1.png");
        static public Image Background2 { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\road2.png");
        static public Image Background3 { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\road3.png");
        static public Image Background4 { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\road4.png");
        static public Image Go { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\go.png");
        static public Image Pause { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\pause.png");
        static public Image Resuming { get; } = new Bitmap("C:\\Users\\Mike\\OneDrive\\Рабочий стол\\Tiny Race\\TinyRace\\Image\\resuming.png");
    }
}
