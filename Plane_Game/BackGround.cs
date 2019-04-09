using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plane_Game.Properties;
using System.Drawing;
namespace Plane_Game
{
    class BackGround:GameObject
    {
        //导入背景图片
        static private Image imgBG = Resources.background;
        //构造函数
        public BackGround(int x, int y, int speed) : base(x, y, imgBG.Width, imgBG.Height, speed, 0, Direction.Down)
        { }
        public override void Draw(Graphics g)
        {
            this.Y += this.Speed;
            if (this.Y == 0)
                this.Y = -850;
            g.DrawImage(imgBG, this.X, this.Y);
        }
    }
}
