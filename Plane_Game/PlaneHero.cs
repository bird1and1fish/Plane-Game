using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plane_Game.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace Plane_Game
{
    class PlaneHero:PlaneFather
    {
        static Image ImgPlane = Resources.Plane2;
        public PlaneHero(int x, int y, int speed, int life, Direction dir) : base(x, y, ImgPlane, speed, life, dir)
        {
            this.Width /= 2;
            this.Height /= 2;
        }
        //重写Draw函数
        public override void Draw(Graphics g)
        {
            g.DrawImage(ImgPlane, this.X, this.Y,this.Width,this.Height);
        }
        //飞机跟随鼠标移动
        public void MouseMove(MouseEventArgs e)
        {
            this.X = e.X - this.Width / 2;
            this.Y = e.Y - this.Height / 2;
            if (this.X >= 347 - this.Width)
                this.X = 347 - this.Width;
            if (this.X <= 0)
                this.X = 0;
            if (this.Y >= 644 - this.Height)
                this.Y = 644 - this.Height;
            if (this.Y <= 0)
                this.Y = 0;
        }
        //提供一个发子弹函数
        public void Fire()
        {
            SingleObject.GetSingle().AddGameObject(new HeroZiDan(this, 10, 3));
        }
        public override void IsOver()
        {
            if (this.Life <= 0)
            {
                //游戏结束并播放爆炸图片
                SingleObject.GetSingle().AddGameObject(new Boom(this.X, this.Y));
            }
        }
    }
}
