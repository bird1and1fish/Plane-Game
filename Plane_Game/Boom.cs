﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plane_Game.Properties;

namespace Plane_Game
{
    class Boom:GameObject
    {
        private static Image[] imgs = {
            Resources.Boom1,
            Resources.Boom2,
            Resources.Boom3
        };
        public Boom(int x, int y) : base(x, y)
        { }
        public override void Draw(Graphics g)
        {
            for (int i = 0; i < imgs.Length; i++)
            {
                g.DrawImage(imgs[i], this.X, this.Y);
            }
            SingleObject.GetSingle().RemoveObject(this);
        }
    }
}
