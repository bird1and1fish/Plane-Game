using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Plane_Game.Properties;

namespace Plane_Game
{
    class HeroZiDan:ZiDan
    {
        private static Image imgHeroZiDan = Resources.Zidan1;
        public HeroZiDan(PlaneFather pf, int speed, int power) : base(pf, imgHeroZiDan, speed, power)
        { }
    }
}
