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
                Say(pc, 131, "接受火焰的守護吧$R;");
                SkillPointBonus(pc, 1);
                Wait(pc, 2000);
                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 2000);
                Say(pc, 131, "技能點數上升1點。$R;");
            }
            Say(pc, 131, "…$R;" +
                "這裡是火焰精靈的聖地阿。$R;" +
                "我們的族長虎姆拉$R;" +
                "在熔岩的深處$R;" +
                "火焰最燙的地方睡覺呢。$R;" +
                "$P人類的身體受不了的，$R;" +
                "還是回去吧。$R;");
        }
    }
}
