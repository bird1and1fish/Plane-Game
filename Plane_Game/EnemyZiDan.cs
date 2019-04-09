using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Plane_Game.Properties;

namespace Plane_Game
{
    class EnemyZiDan:ZiDan
    {
        private static Image imgEnemyZiDan1 = Resources.Zidan3;
        private static Image imgEnemyZiDan2 = Resources.Zidan4;
        public EnemyZiDan(PlaneFather pf, int speed, int power, int type) : base(pf, GetZiDanImage(type), speed, power)
        { }
        public static Image GetZiDanImage(int type)
        {
            switch (type)
            {
                case 0:
                case 1:
                    return imgEnemyZiDan1;
                case 2:
                case 3:
                    return imgEnemyZiDan2;
            }
            return null;
        }
    }
}
