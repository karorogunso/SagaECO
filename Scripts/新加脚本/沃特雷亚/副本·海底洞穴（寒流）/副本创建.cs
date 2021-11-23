
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.ActorEventHandlers;
namespace 海底副本
{
    public partial class 海底副本 : Event
    {
        void 初始化变量(ActorPC pc)
        {

        }
        void 附加单人普通BUFF(ActorPC pc)
        {
            pc.Buff.单枪匹马 = true;
        }
        void 附加多人普通BUFF(ActorPC pc)
        {
            pc.Buff.黑暗压制 = true;
        }
        bool 生成地图单人(ActorPC pc, Difficulty diff)
        {
            if (pc.Party != null)//有队伍不生成
                return false;

            pc.TInt["S21180001"] = CreateMapInstance(21180001, 11058000, 128, 245, true, 0, true);//海底洞穴

            return true;
        }
        bool 生成地图多人(ActorPC pc, Difficulty diff)
        {
            if (pc.Party == null)//没有队伍不生成
                return false;
            if (pc != pc.Party.Leader)//不是队长不生成
                return false;

            pc.Party.TInt["S21180001"] = CreateMapInstance(21180001, 11058000, 128, 245, true, 0, true);//海底洞穴

            pc.Party.MaxMember = MaxMember;//让队伍成员上限改变
            return true;
        }
        void 传送单人(ActorPC pc, Difficulty diff)
        {
            SetReviveCountForSingle(pc  , diff);
            if (pc.QuestRemaining < QuestPoint)
                return;
            pc.QuestRemaining -= QuestPoint;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendQuestPoints();
            Warp(pc, (uint)pc.TInt["S21180001"], 68, 253);

        }
        void 传送多人(ActorPC pc, Difficulty diff)
        {
            foreach (var item in pc.Party.Members.Values)//遍历队伍的成员
            {
                if (item != null && item.Online)
                {
                    SetReviveCount(item, diff);
                    附加多人普通BUFF(item);
                    初始化变量(item);
                    Warp(item,(uint)pc.Party.TInt["S21180001"], 68, 253);
                }
            }
        }
        #region 刷怪
        uint GetMapID(ActorPC pc, string name, bool single)
        {
            uint mapid = 0;
            if (single) mapid = (uint)pc.TInt[name];
            else mapid = (uint)pc.Party.TInt[name];
            return mapid;
        }

        #endregion
    }
}