using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Plane_Game
{
    class ZiDan:GameObject
    {
        private Image ImgZiDan;
        //记录子弹的威力
        public int Power
        {
            get;
            set;
        }
        public ZiDan(PlaneFather pf, Image img, int speed, int power) : base(pf.X + pf.Width / 2, pf.Y + pf.Height / 2, img.Width, img.Height, speed, 0, pf.Dir)
        {
            this.ImgZiDan = img;
            this.Power = power;
        }
        public override void Draw(Graphics g)
        {
            this.Move();
            g.DrawImage(ImgZiDan, this.X, this.Y);
        }
        public override void Move()
        {
            switch (this.Dir)
            {
                case Direction.Up:
                    this.Y -= this.Speed;
                    break;
                case Direction.Down:
                    this.Y += this.Speed;
                    break;
            }
            //使子弹可以打出窗体
            if (this.Y <= 0)
            {
                this.Y = -200;
                //在游戏中移除子弹对象
                SingleObject.GetSingle().RemoveObject(this);
            }
            if (this.Y >= 644)
            {
                this.Y = 800;
                //在游戏中移除子弹对象
                SingleObject.GetSingle().RemoveObject(this);
            }
        }
    }
}
