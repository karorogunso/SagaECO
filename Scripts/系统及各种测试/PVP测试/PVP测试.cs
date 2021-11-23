using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public partial class PVPTEST : Event
    {
        public PVPTEST()
        {
            this.EventID = 50015003;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.Account.GMLevel >= 250)
            {
                string s = "关闭";
                if (SInt["PVP状态"] == 1) s = "开启";
                switch (Select(pc, "【管理员模式】当前PVP状态：" + s, "", "吧自己改为西国", "玩家回城+奖励", "刷两边的石像", "将等待室的玩家传送至战场", "发送战斗信息", "离开"))
                {
                    case 1:
                        return;
                    case 2:
                        foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                        {
                            if (item.Character.MapID == 20080019)
                            {
                                SagaMap.Network.Client.MapClient.FromActorPC(item.Character).RevivePC(item.Character);
                                Warp(item.Character, 20080018, 20, 20);
                                //GiveItem(item.Character, 910000034, 1);
                            }
                        }
                        return;
                    case 3:
                        刷怪(20080019);
                        return;
                    case 4:
                        byte count = 0;
                        foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                        {
                            if (count == 0 && item.Character.MapID == 20080018)
                            {
                                item.Character.TInt["复活次数"] = 999;
                                item.Character.TInt["副本复活标记"] = 2;
                                item.Character.TInt["副本savemapid"] = 20080019;
                                item.Character.TInt["副本savemapX"] = 7;
                                item.Character.TInt["副本savemapY"] = 25;
                                item.Character.TInt["伤害统计"] = 0;
                                item.Character.TInt["治疗统计"] = 0;
                                item.Character.TInt["受伤害统计"] = 0;
                                item.Character.TInt["受治疗统计"] = 0;
                                item.Character.TInt["PVP连杀"] = 0;
                                item.Character.TInt["PVP最大连杀"] = 0;
                                item.Character.TInt["死亡统计"] = 0;
                                item.Character.TInt["击杀统计"] = 0;
                                Warp(item.Character, 20080019, 9, 25);
                                count = 1;
                                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                                item.Character.Mode = PlayerMode.KNIGHT_NORTH;
                                map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, item.Character, true);
                            }
                            else if (item.Character.MapID == 20080018)
                            {
                                item.Character.TInt["复活次数"] = 999;
                                item.Character.TInt["副本复活标记"] = 2;
                                item.Character.TInt["副本savemapid"] = 20080019;
                                item.Character.TInt["副本savemapX"] = 41;
                                item.Character.TInt["副本savemapY"] = 25;
                                item.Character.TInt["伤害统计"] = 0;
                                item.Character.TInt["治疗统计"] = 0;
                                item.Character.TInt["受伤害统计"] = 0;
                                item.Character.TInt["受治疗统计"] = 0;
                                item.Character.TInt["PVP连杀"] = 0;
                                item.Character.TInt["PVP最大连杀"] = 0;
                                item.Character.TInt["死亡统计"] = 0;
                                item.Character.TInt["击杀统计"] = 0;
                                Warp(item.Character, 20080019, 39, 25);
                                count = 0;
                                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                                item.Character.Mode = PlayerMode.KNIGHT_SOUTH;
                                map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, item.Character, true);
                            }

                        }
                        return;
                    case 5:
                        foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                        {
                            string op = "南国";
                            if (item.Character.Mode == PlayerMode.KNIGHT_NORTH)
                                op = "北国";
                            if (item.Character.Mode == PlayerMode.KNIGHT_WEST)
                                op = "西国";
                            string sd = "【" + op + "】玩家" + item.Character.Name + " 在本次PVP总共击杀玩家：" + item.Character.TInt["击杀统计"].ToString() +
                                " 最大连杀次数：" + item.Character.TInt["PVP最大连杀"].ToString() +
                                " 造成伤害：" + item.Character.TInt["伤害统计"].ToString() +
                                " 受到伤害：" + item.Character.TInt["受伤害统计"].ToString() +
                                 " 共治疗：" + item.Character.TInt["治疗统计"].ToString() +
                                 " 共受到治疗：" + item.Character.TInt["受治疗统计"].ToString() +
                                 " 总死亡次数：" + item.Character.TInt["死亡统计"];
                            foreach (var pcs in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                            {
                                if (pcs.Character.Online)
                                    SagaMap.Network.Client.MapClient.FromActorPC(pcs.Character).SendSystemMessage(sd);
                            }
                        }
                        break;
                    case 6:
                        return;
                }
            }
            if (SInt["PVP状态"] == 0)
            {
                Say(pc, 131, "抱歉，现在PVP为关闭状态");
                return;
            }
        }
    }
}

