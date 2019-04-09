using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Plane_Game
{
    class SingleObject
    {
        //单例的构造函数私有化
        private SingleObject()
        { }
        //声明全局唯一的对象
        private static SingleObject _single = null;
        //静态函数返回一个唯一的对象
        public static SingleObject GetSingle()
        {
            if (_single == null)
                _single = new SingleObject();
            return _single;
        }
        public BackGround BG
        {
            get;
            set;
        }
        public PlaneHero PH
        {
            get;
            set;
        }
        //声明一个列表储存玩家子弹
        public List<HeroZiDan> listHeroZiDan = new List<HeroZiDan>();
        //声明一个列表储存敌人飞机
        public List<PlaneEnemy> listPlaneEnemy = new List<PlaneEnemy>();
        //声明一个列表储存敌人爆炸对象
        public List<Boom> listBoom = new List<Boom>();
        //声明一个列表储存敌人子弹对象
        public List<EnemyZiDan> listEnemyZiDan = new List<EnemyZiDan>();
        //声明一个属性来记录分数
        public int Score
        {
            get;
            set;
        }
        //增加游戏对象
        public void AddGameObject(GameObject go)
        {
            if (go is BackGround)
                this.BG = go as BackGround;
            else if (go is PlaneHero)
                this.PH = go as PlaneHero;
            else if (go is HeroZiDan)
                listHeroZiDan.Add(go as HeroZiDan);
            else if (go is PlaneEnemy)
                listPlaneEnemy.Add(go as PlaneEnemy);
            else if (go is Boom)
            {
                listBoom.Add(go as Boom);
            }
            else if (go is EnemyZiDan)
            {
                listEnemyZiDan.Add(go as EnemyZiDan);
            }
        }
        //移除游戏对象
        public void RemoveObject(GameObject go)
        {
            if (go is HeroZiDan)
                listHeroZiDan.Remove(go as HeroZiDan);
            else if (go is PlaneEnemy)
                listPlaneEnemy.Remove(go as PlaneEnemy);
            else if (go is Boom)
                listBoom.Remove(go as Boom);
            else if (go is EnemyZiDan)
                listEnemyZiDan.Remove(go as EnemyZiDan);
        }
        public void Draw(Graphics g)
        {
            this.BG.Draw(g);
            this.PH.Draw(g);
            for (int i = 0; i < listHeroZiDan.Count; i++)
            {
                listHeroZiDan[i].Draw(g);
            }
            for (int i = 0; i < listPlaneEnemy.Count; i++)
            {
                listPlaneEnemy[i].Draw(g);
            }
            for (int i = 0; i < listBoom.Count; i++)
            {
                listBoom[i].Draw(g);
            }
            for (int i = 0; i < listEnemyZiDan.Count; i++)
            {
                listEnemyZiDan[i].Draw(g);
            }
        }
        //碰撞检测
        public void PZJC()
        {
            #region 判断玩家飞机是否打到敌人身上
            for (int i=0; i < listHeroZiDan.Count; i++)
            {
                for (int j = 0; j < listPlaneEnemy.Count; j++)
                {
                    if (listHeroZiDan[i].GetRectangle().IntersectsWith(listPlaneEnemy[j].GetRectangle()))
                    {
                        listPlaneEnemy[j].Life -= listHeroZiDan[i].Power;
                        //判断敌人是否死亡
                        listPlaneEnemy[j].IsOver();
                        listHeroZiDan.Remove(listHeroZiDan[i]);
                        break;
                    }
                }
            }
            #endregion
            #region 判断敌人飞机的子弹是否打到玩家身上
            for (int i = 0; i < listEnemyZiDan.Count; i++)
            {
                if (listEnemyZiDan[i].GetRectangle().IntersectsWith(this.PH.GetRectangle()))
                {
                    this.PH.Life -= listEnemyZiDan[i].Power;
                    //判断自己是否死亡
                    AddGameObject(new Boom(PH.X, PH.Y));
                    this.PH.IsOver();
                    listEnemyZiDan.Remove(listEnemyZiDan[i]);
                }
            }
            #endregion
            #region 判断玩家是否和敌人飞机发生相撞
            for (int i = 0; i < listPlaneEnemy.Count; i++)
            {
                if (listPlaneEnemy[i].GetRectangle().IntersectsWith(this.PH.GetRectangle()))
                {
                    listPlaneEnemy[i].Life = 0;
                    listPlaneEnemy[i].IsOver();
                    AddGameObject(new Boom(PH.X, PH.Y));
                    if (listPlaneEnemy[i].EnemyType == 0 || listPlaneEnemy[i].EnemyType == 1)
                    {
                        this.PH.Life -= 5;
                    }
                    else if (listPlaneEnemy[i].EnemyType == 2 || listPlaneEnemy[i].EnemyType == 3)
                    {
                        this.PH.Life -= 10;
                    }
                    this.PH.IsOver();
                }
            }
            #endregion
        }
        public void Remake(MouseEventArgs e)
        {
            this.Score = 0;
            this.PH.Life = 50;
            //声明一个列表储存玩家子弹
            List<HeroZiDan> NewlistHeroZiDan = new List<HeroZiDan>();
            this.listHeroZiDan = NewlistHeroZiDan;
            //声明一个列表储存敌人飞机
            List <PlaneEnemy> NewlistPlaneEnemy = new List<PlaneEnemy>();
            this.listPlaneEnemy = NewlistPlaneEnemy;
            //声明一个列表储存敌人子弹对象
            List<EnemyZiDan> NewlistEnemyZiDan = new List<EnemyZiDan>();
            this.listEnemyZiDan = NewlistEnemyZiDan;
        }

    }
}
