using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Plane_Game
{
    abstract class PlaneFather:GameObject
    {
        private Image ImgPlane;
        public PlaneFather(int x, int y, Image img, int speed, int life, Direction dir) : base(x, y, img.Width, img.Height, speed, life, dir)
        {
            this.ImgPlane = img;
        }
        //飞机父类不需要重写Draw函数
        //判断是否死亡的函数
        public abstract void IsOver();
    }
}
