using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Plane_Game.Properties;

namespace Plane_Game
{
    class PlaneEnemy:PlaneFather
    {
        static Image img1 = Resources.Plane4;
        static Image img2 = Resources.Plane6;
        static Image img3 = Resources.Plane7;
        static Image img4 = Resources.Plane5;
        //声明标识来标记每一架飞机
        //0--飞机1   1--飞机2   2--飞机3   3--飞机4
        public int EnemyType
        {
            get;
            set;
        }
        //写三个函数分别返回不同飞机的图片、速度、生命值
        public static Image GetImage(int type)
        {
            switch (type)
            {
                case 0:
                    return img1;
                case 1:
                    return img2;
                case 2:
                    return img3;
                case 3:
                    return img4;
            }
            return null;
        }
        public static int GetLife(int type)
        {
            switch (type)
            {
                case 0:
                    return 5;
                case 1:
                    return 9;
                case 2:
                    return 11;
                case 3:
                    return 15;
            }
            return 0;
        }
        public static int GetSpeed(int type)
        {
            switch (type)
            {
                case 0:
                    return 5;
                case 1:
                    return 4;
                case 2:
                    return 3;
                case 3:
                    return 2;
            }
            return 0;
        }
        //敌人飞机的构造函数
        public PlaneEnemy(int x, int y, int type) : base(x, y, GetImage(type), GetSpeed(type), GetLife(type), Direction.Down)
        {
            this.Width /= 2;
            this.Height /= 2;
            this.EnemyType = type;
        }
        //重写父类中的Draw函数
        public override void Draw(Graphics g)
        {
            this.Move();
            switch (this.EnemyType)
            {
                case 0:
                    g.DrawImage(img1, this.X, this.Y, this.Width, this.Height);
                    break;
                case 1:
                    g.DrawImage(img2, this.X, this.Y, this.Width, this.Height);
                    break;
                case 2:
                    g.DrawImage(img3, this.X, this.Y, this.Width, this.Height);
                    break;
                case 3:
                    g.DrawImage(img4, this.X, this.Y, this.Width, this.Height);
                    break;
            }
        }
        static Random r = new Random();
        //重写父类中的Move函数
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
                case Direction.Left:
                    this.X -= this.Speed;
                    break;
                case Direction.Right:
                    this.X += this.Speed;
                    break;
            }
            if (this.X <= 0)
                this.X = 0;
            if (this.X >= 347 - this.Width)
                X = 347 - this.Width;
            if (this.Y >= 644)
            {
                this.Y = 800;
                //在游戏中移除飞机对象
                SingleObject.GetSingle().RemoveObject(this);
            }
            if (this.EnemyType == 0 && this.Y >= 250)
            {
                if (this.X >= 0 && this.X <= 140)
                {
                    this.X += r.Next(0, 30);
                }
                else if (this.X >= 140 && this.X <= 347 - this.Width)
                {
                    this.X -= r.Next(0, 30);
                }
            }
            if (this.EnemyType == 1 && this.Y >= 250)
            {
                if (this.X >= 0 && this.X <= 140)
                {
                    this.X += r.Next(0, 5);
                }
                else if (this.X >= 140 && this.X <= 347 - this.Width)
                {
                    this.X -= r.Next(0, 5);
                }
            }
            if (this.EnemyType == 2 && this.Y >= 250)
            {
                if (this.Speed < 4)
                    this.Speed = 4;
            }
            if (this.EnemyType == 3 && this.Y >= 250)
            {
                this.Y = 250;
                this.Speed = 0;
                this.X += r.Next(-5, 6);
            }
        }
        public override void IsOver()
        {
            if (this.Life <= 0)
            {
                SingleObject.GetSingle().RemoveObject(this);
                //播放爆炸图片
                SingleObject.GetSingle().AddGameObject(new Boom(this.X, this.Y));
                //根据飞机的种类给玩家进行加分
                switch (this.EnemyType)
                {
                    case 0:
                        SingleObject.GetSingle().Score += 50;
                        break;
                    case 1:
                        SingleObject.GetSingle().Score += 100;
                        break;
                    case 2:
                        SingleObject.GetSingle().Score += 150;
                        break;
                    case 3:
                        SingleObject.GetSingle().Score += 200;
                        break;
                }
            }
        }
        public void Fire()
        {
            if (this.EnemyType == 0)
                SingleObject.GetSingle().AddGameObject(new EnemyZiDan(this, 10, 5, this.EnemyType));
            else if (this.EnemyType == 1)
                SingleObject.GetSingle().AddGameObject(new EnemyZiDan(this, 10, 5, this.EnemyType));
            else if (this.EnemyType == 2)
                SingleObject.GetSingle().AddGameObject(new EnemyZiDan(this, 15, 10, this.EnemyType));
            else if (this.EnemyType == 3)
                SingleObject.GetSingle().AddGameObject(new EnemyZiDan(this, 15, 10, this.EnemyType));
        }
    }
}
