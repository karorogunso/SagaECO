using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10064000
{
    public class S11000327 : Event
    {
        public S11000327()
        {
            this.EventID = 11000327;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<THSFlags> mask = new BitMask<THSFlags>(pc.CMask["THS"]);
            if (!mask.Test(THSFlags.塞爾曼德第一次對話))
            {
                mask.SetValue(THSFlags.塞爾曼德第一次對話, true);
                //_2a65 = true;
                Say(pc, 131, "接受火焰的守护吧$R;");
                SkillPointBonus(pc, 1);
                Wait(pc, 2000);
                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 2000);
                Say(pc, 131, "技能点数上升1点。$R;");
            }
            Say(pc, 131, "…$R;" +
                "这里是火焰精灵的圣地阿。$R;" +
                "我们的族长火焰凤凰$R;" +
                "在熔岩的深处$R;" +
                "火焰最烫的地方睡觉呢。$R;" +
                "$P人类的身体受不了的，$R;" +
                "请回去吧。$R;");
        }
    }
}
