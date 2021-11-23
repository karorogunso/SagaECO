
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaDB.Actor;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace WeeklyExploration
{
    public partial class KitaAreaBOSS : Event
    {
        public KitaAreaBOSS()
        {
            this.EventID = 10002168;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.Account.GMLevel > 200)
            {
                if (Select(pc, "刷BOSS吗？[请不要随意刷怪！]", "", "刷", "不刷") == 1)
                    刷怪();
                if (Select(pc, "是否传送所有玩家到前面", "", "传", "不") == 1)
                {
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.MapID == 20212000)
                            ChangeBGM(pc, 1026, true, 100, 50);
                    }
                    return;
                }
            }
            if (DateTime.Now.Hour >= 19 && DateTime.Now.Hour <= 23)
            {
                if (SStr["北国领主记录"] != DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    SStr["北国领主记录"] = DateTime.Now.ToString("yyyy-MM-dd");
                    刷怪();
                }
                Say(pc, 0, "轰隆隆————");
                pc.TInt["TempBGM"] = 1191;
                pc.TInt["复活次数"] = 5;
                pc.TInt["设定复活次数"] = 5;
                pc.TInt["副本复活标记"] = 3;

                pc.TInt["伤害统计"] = 0;
                pc.TInt["受伤害统计"] = 0;
                pc.TInt["受治疗统计"] = 0;
                pc.TInt["治疗溢出统计"] = 0;
                pc.TInt["死亡统计"] = 0;
                Warp(pc, 20212000, 103, 121);
            }
            else
            {
                Say(pc, 0, "领主战斗为19:00-23:59$R$R现在不能进入哦");
                return;
            }
        }
    }
}

