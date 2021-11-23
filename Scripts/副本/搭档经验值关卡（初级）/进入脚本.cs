
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Text;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaMap.Network.Client;
using System.Globalization;
using SagaScript.Chinese.Enums;
namespace 每日地牢
{
    public partial class 每日地牢 : Event
    {
        public 每日地牢()
        {
            this.EventID = 980000101;
        }

        public override void OnEvent(ActorPC pc)
        {
            MapClient client = MapClient.FromActorPC(pc);
            if (pc.AStr["每日地牢记录"] == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                client.SendSystemMessage("你今天已经入场过了，请明天再来吧。");
                return;
            }
            if(pc.Party != null)
            {
                Say(pc, 0, "组队状态下不能进入");
                return;
            }
            else
            {
                pc.Buff.单枪匹马 = true;
                pc.AStr["每日地牢记录"] = DateTime.Now.ToString("yyyy-MM-dd");
                pc.TInt["每日地牢地图ID"] = CreateMapInstance(60912000, 30131001, 6, 8, true, 0, true);
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap((uint)pc.TInt["每日地牢地图ID"]);
                map.SpawnCustomMob(10000000, map.ID, 14680000, 0, 0, 66, 31, 100, 5, 0, 蚂蚁Info(), 蚂蚁AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14640000, 0, 0, 66, 31, 100, 5, 0, 蜈蚣Info(), 蜈蚣AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14310500, 0, 0, 66, 31, 100, 3, 0, 激光图腾Info(), 激光图腾AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14310100, 0, 0, 66, 31, 100, 3, 0, 修复图腾Info(), 修复图腾AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10970900, 0, 0, 66, 31, 100, 4, 0, 侦察机Info(), 侦察机AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 15620050, 0, 0, 66, 31, 100, 4, 0, 机灵小鬼Info(), 机灵小鬼AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14330000, 0, 0, 66, 31, 100, 4, 0, 狙击手Info(), 狙击手AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14690400, 0, 0, 66, 31, 100, 4, 0, 防空炮手Info(), 防空炮手AI(), null, 0);

                map.SpawnCustomMob(10000000, map.ID, 14710500, 0, 10010100, 1, 148, 31, 0, 1, 0, 重卫炮手Info(), 重卫炮手AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 17580001, 0, 10010100, 1, 148, 30, 0, 1, 0, 青林妖姬Info(), 青林妖姬AI(), null, 0);
                Warp(pc, (uint)pc.TInt["每日地牢地图ID"], 0, 29);
            }
        }
    }
}

