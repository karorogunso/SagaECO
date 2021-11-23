
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace 东部地牢副本
{
    public partial class 东部地牢 : Event
    {
        /// <summary>
        /// 限制等级
        /// </summary>
        byte lv;
        /// <summary>
        /// 最大人数
        /// </summary>
        byte MaxMember;
        /// <summary>
        /// 需求任务点
        /// </summary>
        ushort QuestPoint;

        string RealmName = "东部地牢";
        void 副本对话单人(ActorPC pc)
        {
            Difficulty diff = Difficulty.Single_Easy;
            switch (Select(pc, "请选择【" + RealmName + "】的难度(单人)", "", "单人普通（10任务点）", "单人困难(30任务点)","离开"))
            {
                case 1:
                    diff = Difficulty.Single_Normal;
                    pc.CInt["东部地牢难度"] = 2;
                    break;
                case 2:
                    diff = Difficulty.Single_Hard;
                    pc.CInt["东部地牢难度"] = 3;
                    break;
                default:
                    return;
            }
            IniLimit(diff);
            if(!检查单人(pc, diff)) return;//如果检查失败，则直接返回该函数
            if (!生成地图单人(pc, diff)) return;//如果在生成地图时发生错误，则直接返回该函数
            生成怪物(pc, diff, true);
            初始化变量(pc);
            附加单人普通BUFF(pc);
            传送单人(pc, diff);
            
        }
        void 副本对话多人(ActorPC pc)
        {
            Difficulty diff = Difficulty.Easy;
            switch (Select(pc, "请选择【" + RealmName + "】的难度(多人)", "", "多人普通（4人，10任务点）", "多人困难（4人，30任务点）", "离开"))
            {
                case 1:
                    diff = Difficulty.Normal;
                    pc.CInt["东部地牢难度"] = 2;
                    break;
                case 2:
                    diff = Difficulty.Hard;
                    pc.CInt["东部地牢难度"] = 3;
                    break;
                default:
                    return;
            }
            IniLimit(diff);
            if (!检查多人(pc, diff)) return;//如果检查失败，则直接返回该函数
            if (!进入申请(pc, diff)) return;//如果在申请过程中检查失败，则直接返回该函数
            if (!生成地图多人(pc, diff)) return;//如果在生成地图时发生错误，则直接返回该函数
            生成怪物(pc, diff,false);
            初始化变量(pc);
            传送多人(pc, diff);

        }
        /// <summary>
        /// 发送系统信息给队伍所有人
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="msg">信息内容</param>
        void SendSystemMessageToMember(ActorPC pc, string msg)
        {
            if (pc.Party == null) return;//如果玩家没有队伍，则直接返回
            foreach (var item in pc.Party.Members.Values)//遍历玩家队伍成员
            {
                if (item.Online)//如果队员在线
                    SagaMap.Network.Client.MapClient.FromActorPC(item).SendSystemMessage(msg);//发送系统信息
            }
        }
    }
}