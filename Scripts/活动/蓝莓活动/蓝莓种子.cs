
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
    public class S110005605 : Event
    {
        public S110005605()
        {
            this.EventID = 110005605;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID != 10056000)
            {
                Say(pc, 0, "蓝莓种子只能在哞哞草原种植哦。");
                return;
            }
            if (CountItem(pc, 110005605) >= 1)
            {
                TakeItem(pc, 110005605, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            SInt["全服蓝莓种植"]++;
            pc.AInt["个人蓝莓种植"]++;
            SInt["当前小时全服种植的蓝莓"]++;
ShowEffect(pc, 8020);
            SagaMap.Skill.SkillHandler.SendSystemMessage(pc, "你种下了一个蓝莓种子。你已经种植了：" + pc.AInt["个人蓝莓种植"] + " 个种子，当前小时全服已种植了："+ SInt["当前小时全服种植的蓝莓"] + " 个种子，全服总共已种植了：" + SInt["全服蓝莓种植"] + " 个种子。");
            if (Global.Random.Next(0, 50 + pc.AInt["个人蓝莓种植"]/2) == 30)
            {
                switch (Global.Random.Next(1, 3))
                {
                    case 1:
                        int gold = Global.Random.Next(10000, 200000);
                        pc.Gold += gold;
                        Say(pc, 0, "你在挖土种种子的时候，$R有了意外的发现！$R$R不知道是谁掉落的"+gold+"G！");
                        break;
                    case 2:
                        GiveItem(pc, 910000116, 1);
                        Say(pc, 0, "你在挖土种种子的时候，$R突然吹来了一张 CP1000！");
                        break;
                    case 3:
                        GiveItem(pc, 910000069, 1);
                        Say(pc, 0, "你在挖土种种子的时候，$R突然被 任务点增加勋章[50点] 给绊倒了！");
                        break;
                }

            }
        }
    }
}

