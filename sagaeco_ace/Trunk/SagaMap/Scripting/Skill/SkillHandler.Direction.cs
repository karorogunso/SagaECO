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
        /// </summary>
        /// <param name="sActor">人物</param>
        /// <returns>方向</returns>
        public ActorDirection GetDirection(Actor sActor)
        {
            return (ActorDirection)(Math.Ceiling((double)(sActor.Dir / 45)));
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
