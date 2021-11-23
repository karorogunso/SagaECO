
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
    public class S911000128 : Event
    {
        public S911000128()
        {
            this.EventID = 911000128;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 911000128) >= 1 && pc.CInt["进阶技能解锁13000"] != 1)
            {
                TakeItem(pc, 911000128, 1);
                Say(pc, 0, "解锁了4级【纯白信仰】的学习条件！$R$R你获得了一个技能点");
                pc.CInt["进阶技能解锁13000"] = 1;//解锁
                ShowEffect(pc, 5380);
                if (pc.AInt["4级【纯白信仰】技能点获得"] != 1)
                {
                    pc.SkillPoint3 += 1;//得到一个技能点
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();//发送玩家信息封包
                    pc.AInt["4级【纯白信仰】技能点获得"] = 1;//技能点获取标记
                }
            }
            return;
        }
    }
}

