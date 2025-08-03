using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000638 : Event
    {
        public S11000638()
        {
            this.EventID = 11000638;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "欢迎光临！住宿费是180金币！$R;");
            switch (Select(pc, "怎么做？", "", "住宿", "不住宿"))
            {
                case 1:
                    if (pc.Gold < 180)
                    {
                        Say(pc, 131, "金币不足$R;");
                        return;
                    }
                    pc.Gold -= 180;
                    PlaySound(pc, 4001, false, 100, 50);
                    if (CountItem(pc, 10012600) >= 1 && pc.Level > 49)//&& !_5A27)
                    {
                        Warp(pc, 30141003, 11, 14);
                        return;
                    }
                    Fade(pc, FadeType.Out, FadeEffect.Black);
                    //FADE OUT BLACK
                    Wait(pc, 10000);

                    Fade(pc, FadeType.In, FadeEffect.Black);
                    //FADE IN
                    Heal(pc);
                    Wait(pc, 1000);

                    Say(pc, 131, "消除了一些疲劳了吗？$R;");
                    break;
            }
        }
    }
}