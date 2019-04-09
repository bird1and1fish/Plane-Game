using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace Plane_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitialGame();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //在窗体加载的时候 解决窗体闪烁问题
            //将图像绘制到缓冲区减少闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        }
        static Random r = new Random();
        //初始化游戏
        public void InitialGame()
        {
            //初始化背景
            SingleObject.GetSingle().AddGameObject(new BackGround(0,-850,5));
            //初始化玩家飞机
            SingleObject.GetSingle().AddGameObject(new PlaneHero(100, 100, 5, 50, Direction.Up));
            AddEnemy(16);
            //游戏BGM设置
        }
        public static void AddEnemy(int n)
        {
            for (int i = 0; i < n; i++)
            {
                SingleObject.GetSingle().AddGameObject(new PlaneEnemy(r.Next(0, 250), -200, r.Next(0, 4)));
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //窗体被重新绘制时执行当前事件
            SingleObject.GetSingle().Draw(e.Graphics);
            //增加敌机数量
            if (SingleObject.GetSingle().listPlaneEnemy.Count < 16)
            {
                int n = 16 - SingleObject.GetSingle().listPlaneEnemy.Count;
                AddEnemy(n);
            }
            //绘制生命值和分数
            string score = "当前分数: ";
            score += SingleObject.GetSingle().Score.ToString();
            e.Graphics.DrawString(score, new Font("宋体", 15, FontStyle.Bold), Brushes.Red, new Point(0, 0));
            string life = "剩余生命值: ";
            life += SingleObject.GetSingle().PH.Life.ToString();
            e.Graphics.DrawString(life, new Font("宋体", 15, FontStyle.Bold), Brushes.Red, new Point(180, 0));
            string endscore = "最终分数: ";
            endscore += SingleObject.GetSingle().Score.ToString();
            if (SingleObject.GetSingle().PH.Life <= 0)
            {
                e.Graphics.DrawString("游戏结束", new Font("宋体", 25, FontStyle.Bold), Brushes.Red, new Point(0, 250));
                e.Graphics.DrawString(endscore, new Font("宋体", 25, FontStyle.Bold), Brushes.Red, new Point(0, 300));
                e.Graphics.DrawString("点击鼠标左键重新游戏", new Font("宋体", 25, FontStyle.Bold), Brushes.Red, new Point(0, 350));
                Form2 frm = new Form2();
                frm.Show();
                frm.TopMost = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //每50毫秒让窗体重绘
            this.Invalidate();
            SingleObject.GetSingle().PZJC();
            if (SingleObject.GetSingle().PH.Life <= 0)
            {
                timerBG.Stop();
                timerFire.Stop();
            }
            
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //鼠标移动时执行事件
            SingleObject.GetSingle().PH.MouseMove(e);
        }
        private void timerFire_Tick(object sender, EventArgs e)
        {
            SingleObject.GetSingle().PH.Fire();
            for (int i = 0; i < SingleObject.GetSingle().listPlaneEnemy.Count; i++)
            {
                if (r.Next(0, 101) >= 97)
                {
                    SingleObject.GetSingle().listPlaneEnemy[i].Fire();
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (SingleObject.GetSingle().PH.Life <= 0)
            {
                SingleObject.GetSingle().Remake(e);
                timerBG.Start();
                timerFire.Start();
            }
        }
        public static void AddName(EventArgs e, string str)
        {
            if (str != "")
            {
                FileStream fs = new FileStream(@"e:\游戏存档.txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                StreamReader sr = new StreamReader(fs);
                string a = sr.ReadLine();
                while (a != null)
                    a = sr.ReadLine();
                string b = str + ": ";
                b += SingleObject.GetSingle().Score.ToString();
                sw.WriteLine(b);
                sw.Close();
                sr.Close();
            }
        }
    }
}
