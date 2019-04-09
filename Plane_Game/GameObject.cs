using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Plane_Game
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    abstract class GameObject
    {
        /// <summary>
        /// 游戏的父类
        /// </summary>
        #region 横纵坐标、宽度、高度、速度、生命、方向
        public int X
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
        public int Height
        {
            get;
            set;
        }
        public int Speed
        {
            get;
            set;
        }
        public int Life
        {
            get;
            set;
        }
        public Direction Dir
        {
            get;
            set;
        }
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public GameObject(int x, int y, int width, int height, int speed, int life, Direction dir)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Speed = speed;
            this.Life = life;
            this.Dir = dir;
        }
        public GameObject(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        //抽象绘制图像函数
        public abstract void Draw(Graphics g);
        //碰撞检测函数,返回当前游戏对象的矩形
        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }
        //移动的虚方法，如果子类不同时，可以重写
        public virtual void Move()
        {
            //根据不同的方向进行移动
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
            //判断游戏对象是否超出了窗体
            if (this.X <= 0)
                this.X = 0;
            if (this.X >= 347 - this.Width)
                this.X = 347 - this.Width;
            if (this.Y <= 0)
                this.Y = 0;
            if (this.Y >= 644 - this.Height)
                this.Y = 644 - this.Height;
        }

    }
}
