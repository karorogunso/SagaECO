using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Item;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.SkillDefinations;
using SagaMap.Network.Client;
using SagaMap.Mob;
using SagaMap.ActorEventHandlers;
namespace SagaMap.Skill
{
    public partial class SkillHandler : Singleton<SkillHandler>
    {
        public class Point
        {
            public byte x, y;

        }
        /// <summary>
        /// 获取移动角度的坐标
        /// </summary>
        /// <param name="Dir">旋转角度</param>
        /// <param name="CirPoint">圆心坐标</param>
        /// <param name="Range">移动的距离</param>
        /// <returns>目标点的坐标</returns>
        public short[] GetNewPoint(double Dir, short X,short Y, int Range)
        {
            short[] pos = new short[2];
            Range *= 100;
            double Dir2 = (Dir-270) / 180 * Math.PI ;
            int newx = (int)(Range * Math.Cos(Dir2));
            int newy = (int)(Range * Math.Sin(Dir2));
            pos[0] = (short)(X + newx);
            pos[1] = (short)(Y - newy);
            return pos;
        }

        /// <summary>
        /// 获取两个坐标间的路径
        /// </summary>
        /// <param name="fromx">原x</param>
        /// <param name="fromy">原y</param>
        /// <param name="tox">目标x</param>
        /// <param name="toy">目标y</param>
        /// <returns>路径坐标集</returns>
        public List<Point> GetStraightPath(byte fromx, byte fromy, byte tox, byte toy)
        {
            List<Point> path = new List<Point>();
            if (fromx == tox && fromy == toy)
                return path;
            double k;// 
            double nowx = fromx;
            double nowy = fromy;
            int x = fromx;
            int y = fromy;
            sbyte addx, addy;
            if (Math.Abs(toy - fromy) <= Math.Abs(tox - fromx))
            {
                if (tox == fromx)
                {
                    if (fromy < toy)
                    {
                        for (int i = fromy + 1; i <= toy; i++)
                        {
                            Point t = new Point();
                            t.x = fromx;
                            t.y = (byte)i;
                            path.Add(t);
                        }
                    }
                    else
                    {
                        for (int i = fromy - 1; i <= toy; i--)
                        {
                            Point t = new Point();
                            t.x = fromx;
                            t.y = (byte)i;
                            path.Add(t);
                        }
                    }
                }
                else
                {
                    k = Math.Abs((double)(toy - fromy) / (tox - fromx));
                    if (toy < fromy)
                        addy = -1;
                    else
                        addy = 1;
                    if (tox < fromx)
                        addx = -1;
                    else
                        addx = 1;
                    while (Math.Round(nowx) != tox)
                    {
                        x += addx;
                        if (Math.Round(nowy) != Math.Round(nowy + k * addy))
                            y += addy;
                        nowx += addx;
                        nowy += k * addy;

                        Point t = new Point();
                        t.x = (byte)x;
                        t.y = (byte)y;
                        path.Add(t);
                    }
                }
            }
            else
            {
                if (toy == fromy)
                {
                    if (fromx < tox)
                    {
                        for (int i = fromx + 1; i <= tox; i++)
                        {
                            Point t = new Point();
                            t.x = (byte)i;
                            t.y = fromy;
                            path.Add(t);
                        }
                    }
                    else
                    {
                        for (int i = fromx - 1; i <= tox; i--)
                        {
                            Point t = new Point();
                            t.x = (byte)i;
                            t.y = fromy;
                            path.Add(t);
                        }
                    }
                }
                else
                {
                    k = Math.Abs((double)(tox - fromx) / (toy - fromy));
                    if (toy < fromy)
                        addy = -1;
                    else
                        addy = 1;
                    if (tox < fromx)
                        addx = -1;
                    else
                        addx = 1;
                    while (Math.Round(nowy) != toy)
                    {
                        y += addy;
                        if (Math.Round(nowx) != Math.Round(nowx + k * addx))
                            x += addx;
                        nowy += addy;
                        nowx += k * addx;

                        Point t = new Point();
                        t.x = (byte)x;
                        t.y = (byte)y;
                        path.Add(t);
                    }
                }
            }
            return path;
        }
        //放置Skill定義中所需的方向相關之Function
        //Place the direction functions which will used by SkillDefinations
        #region Direction
        /// <summary>
        /// 人物的方向
        /// </summary>
        public enum ActorDirection
        {
            South = 0,
            SouthEast = 7,
            East = 6,
            NorthEast = 5,
            North = 4,
            NorthWest = 3,
            West = 2,
            SouthWest = 1
        }
        /// <summary>
        /// 取得人物方向    
        /// </summary>//编程数学我完全不会啊！要是有更方便的方法就帮我写了吧orz 
        /// <param name="sActor">人物</param>
        /// <returns>方向</returns>
        public ActorDirection GetDirection(Actor sActor)
        {
            
            return (ActorDirection)(Math.Ceiling((double)(sActor.Dir / 45)));
        }
        public ActorDirection GetDirection(ushort dir)
        {
            return (ActorDirection)(Math.Ceiling((double)(dir / 45)));
        }
        /// <summary>
        /// 判断目标是否背向发起者
        /// </summary>
        /// <param name="sActor">发起者</param>
        /// <param name="dActor">目标</param>
        /// <returns>结果</returns>
        public bool GetIsBack(Actor sActor, Actor dActor)
        {
            switch (GetDirection(sActor))
            {
                case ActorDirection.East:
                    switch (GetDirection(dActor))
                    {
                        case ActorDirection.East:
                        case ActorDirection.NorthEast:
                        case ActorDirection.SouthEast:
                            return true;
                    }
                    return false;
                case ActorDirection.North:
                    switch (GetDirection(dActor))
                    {
                        case ActorDirection.North:
                        case ActorDirection.NorthWest:
                        case ActorDirection.NorthEast:
                            return true;
                    }
                    return false;
                case ActorDirection.South:
                    switch (GetDirection(dActor))
                    {
                        case ActorDirection.South:
                        case ActorDirection.SouthWest:
                        case ActorDirection.SouthEast:
                            return true;
                    }
                    return false;
                case ActorDirection.West:
                    switch (GetDirection(dActor))
                    {
                        case ActorDirection.West:
                        case ActorDirection.SouthWest:
                        case ActorDirection.NorthWest:
                            return true;
                    }
                    return false;
                case ActorDirection.NorthEast:
                    switch (GetDirection(dActor))
                    {
                        case ActorDirection.NorthEast:
                        case ActorDirection.North:
                        case ActorDirection.East:
                            return true;
                    }
                    return false;
                case ActorDirection.NorthWest:
                    switch (GetDirection(dActor))
                    {
                        case ActorDirection.NorthWest:
                        case ActorDirection.North:
                        case ActorDirection.West:
                            return true;
                    }
                    return false;
                case ActorDirection.SouthEast:
                    switch (GetDirection(dActor))
                    {
                        case ActorDirection.SouthEast:
                        case ActorDirection.South:
                        case ActorDirection.East:
                            return true;
                    }
                    return false;
                case ActorDirection.SouthWest:
                    switch (GetDirection(dActor))
                    {
                        case ActorDirection.SouthWest:
                        case ActorDirection.South:
                        case ActorDirection.West:
                            return true;
                    }
                    return false;
            }
            return false;
        }
        /// <summary>
        /// 隨機獲得actor周圍坐標
        /// </summary>
        /// <param name="map">地圖</param>
        /// <param name="Actor">目標</param>
        /// <param name="X">回傳X</param>
        /// <param name="Y">回傳Y</param>
        /// <param name="Round">範圍格</param>
        public void GetTRoundPos(Map map, Actor Actor, out byte X, out byte Y, byte Round)
        {
            byte iffx, iffy;
            iffx = SagaLib.Global.PosX16to8(Actor.X, map.Width);
            iffy = SagaLib.Global.PosY16to8(Actor.Y, map.Height);
            byte outx = 0, outy = 0;
            do
            {
                if (iffx + SagaLib.Global.Random.Next(-Round, Round) < 0)
                    outx = 0;
                else if (iffx + SagaLib.Global.Random.Next(-Round, Round) > 255)
                    outx = 255;
                else
                    outx = (byte)(iffx + SagaLib.Global.Random.Next(-Round, Round));
                if (iffy + SagaLib.Global.Random.Next(-Round, Round) < 0)
                    outy = 0;
                else if (iffx + SagaLib.Global.Random.Next(-Round, Round) > 255)
                    outy = 255;
                else
                    outy = (byte)(iffy + SagaLib.Global.Random.Next(-Round, Round));
            } while (iffx == outx && iffy == outy);
            X = outx;
            Y = outy;
        }
        /// <summary>
        /// 取得背后的坐标 
        /// </summary>//编程数学我完全不会啊！要是有更方便的方法就帮我写了吧orz 
        /// <param name="map">地圖</param>
        /// <param name="Actor">目标</param>
        /// <param name="XDiff">回传X</param>
        /// <param name="YDiff">回传Y</param>
        public void GetTBackPos(Map map, Actor Actor, out byte X, out byte Y)
        {
            GetTBackPos(map, Actor, out X, out Y, false);
        }
        public void GetTFrontPos(Map map, Actor Actor, out byte X, out byte Y)
        {
            GetTBackPos(map, Actor, out X, out Y, true);
        }
        public void GetTBackPos(Map map, Actor Actor, out byte X, out byte Y,bool front)
        {
            byte iffx, iffy;
            iffx = SagaLib.Global.PosX16to8(Actor.X, map.Width);
            iffy = SagaLib.Global.PosY16to8(Actor.Y, map.Height);
            switch (GetDirection(Actor.Dir))
            {
                case ActorDirection.East:
                    if (front)
                    {
                        X = (byte)(iffx + 1);
                        Y = iffy;
                    }
                    else
                    {
                        X = (byte)(iffx - 1);
                        Y = iffy;
                    }
                    break;
                case ActorDirection.SouthEast:
                    if (front)
                    {
                        X = (byte)(iffx + 1);
                        Y = (byte)(iffy + 1);
                    }
                    else
                    {
                        X = (byte)(iffx - 1);
                        Y = (byte)(iffy - 1);
                    }
                    break;
                case ActorDirection.South:
                    if (front)
                    {
                        X = iffx;
                        Y = (byte)(iffy + 1);
                    }
                    else
                    {
                        X = iffx;
                        Y = (byte)(iffy - 1);
                    }
                    break;
                case ActorDirection.SouthWest:
                    if (front)
                    {
                        X = (byte)(iffx - 1);
                        Y = (byte)(iffy + 1);
                    }
                    else
                    {
                        X = (byte)(iffx + 1);
                        Y = (byte)(iffy - 1);
                    }
                    break;
                case ActorDirection.West:
                    if (front)
                    {
                        X = (byte)(iffx - 1);
                        Y = iffy;
                    }
                    else
                    {
                        X = (byte)(iffx + 1);
                        Y = iffy;
                    }
                    break;
                case ActorDirection.NorthWest:
                    if (front)
                    {
                        X = (byte)(iffx - 1);
                        Y = (byte)(iffy - 1);
                    }
                    else
                    {
                        X = (byte)(iffx + 1);
                        Y = (byte)(iffy + 1);
                    }
                    break;
                case ActorDirection.North:
                    if (front)
                    {
                        X = iffx;
                        Y = (byte)(iffy - 1);
                    }
                    else
                    {
                        X = iffx;
                        Y = (byte)(iffy + 1);
                    }
                    break;
                case ActorDirection.NorthEast:
                    if (front)
                    {
                        X = (byte)(iffx + 1);
                        Y = (byte)(iffy - 1);
                    }
                    else
                    {
                        X = (byte)(iffx - 1);
                        Y = (byte)(iffy + 1);
                    }
                    break;
                default:
                    X=iffx;
                    Y = iffy;
                    break;
            }
        }
        /// <summary>
        /// 取得座標差(-sActordActor)
        /// </summary>
        /// <param name="map">地圖</param>
        /// <param name="sActor">使用技能的角色</param>
        /// <param name="dActor">目標角色</param>
        /// <param name="XDiff">回傳X的差異(格)</param>
        /// <param name="YDiff">回傳Y的差異(格)</param>
        public void GetXYDiff(Map map, Actor sActor, Actor dActor, out int XDiff, out int YDiff)
        {
            XDiff = SagaLib.Global.PosX16to8(dActor.X, map.Width) - SagaLib.Global.PosX16to8(sActor.X, map.Width);
            YDiff =SagaLib.Global.PosY16to8(sActor.Y, map.Height)- SagaLib.Global.PosY16to8(dActor.Y, map.Height) ;
        }
        /// <summary>
        /// 計算座標差之Hash值
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="SkillRange">技能範圍(EX.3x3=3) </param>
        /// <returns>座標之Hash值</returns>
        public int CalcPosHashCode(int x,int y , int SkillRange)
        {
            int nx = x + SkillRange;
            int ny = y + SkillRange;
            return nx * 100 + ny;
        }
        /// <summary>
        /// 取得對應之座標
        /// </summary>
        /// <param name="sActor">基準人物(原點)</param>
        /// <param name="XDiff">X座標偏移量(單位：格)</param>
        /// <param name="YDiff">Y座標偏移量(單位：格)</param>
        /// <param name="nx">回傳X</param>
        /// <param name="ny">回傳Y</param>
        /// <returns>是否正常(無溢位發生)</returns>
        public bool GetRelatedPos(Actor sActor,int XDiff,int YDiff, out short nx, out short ny)
        {
            byte nbx,nby=0;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            byte ox = SagaLib.Global.PosX16to8(sActor.X, map.Width);
            byte oy = SagaLib.Global.PosY16to8(sActor.Y, map.Height);
            bool ret = GetRelatedPos(sActor, XDiff, YDiff,ox,oy, out nbx, out nby);
            nx = SagaLib.Global.PosX8to16(nbx, map.Width);
            ny = SagaLib.Global.PosY8to16(nby, map.Height);
            return ret;
        }
        /// <summary>
        /// 取得對應之座標
        /// </summary>
        /// <param name="sActor">基準人物(原點)</param>
        /// <param name="XDiff">X座標偏移量(單位：格)</param>
        /// <param name="YDiff">Y座標偏移量(單位：格)</param>
        /// <param name="nx">回傳X</param>
        /// <param name="ny">回傳Y</param>
        /// <returns>是否正常(無溢位發生)</returns>
        public bool GetRelatedPos(Actor sActor, int XDiff, int YDiff, out byte nx, out byte ny)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //取得舊座標
            byte ox = SagaLib.Global.PosX16to8(sActor.X, map.Width);
            byte oy = SagaLib.Global.PosY16to8(sActor.Y, map.Height);
            return GetRelatedPos(sActor, XDiff, YDiff,ox,oy, out nx, out ny);
        }
        /// <summary>
        /// 取得對應之座標
        /// </summary>
        /// <param name="sActor">基準人物</param>
        /// <param name="XDiff">X座標偏移量(單位：格)</param>
        /// <param name="YDiff">Y座標偏移量(單位：格)</param>
        /// <param name="sx">原點X</param>
        /// <param name="sy">原點Y</param>
        /// <param name="nx">回傳X</param>
        /// <param name="ny">回傳Y</param>
        /// <returns>是否正常(無溢位發生)</returns>
        public bool GetRelatedPos(Actor sActor, int XDiff, int YDiff,byte sx,byte sy, out byte nx, out byte ny)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //取得舊座標
            byte ox = sx;
            byte oy = sy;
            //判斷溢位
            if ((ox == 0 && XDiff < 0) ||
                (ox == 0xff && XDiff > 0) ||
                (oy == 0 && YDiff < 0) ||
                (oy == 0xff && YDiff > 0))
            {
                nx = ox;
                ny = oy;
                return false;
            }
            //計算新座標
            nx = (byte)(ox + XDiff);
            ny = (byte)(oy + YDiff);
            return true;
        }
        #endregion

	}
}
