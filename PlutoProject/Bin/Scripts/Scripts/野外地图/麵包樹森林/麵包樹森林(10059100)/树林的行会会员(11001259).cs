using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10059100
{
    public class S11001259 : Event
    {
        public S11001259()
        {
            this.EventID = 11001259;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "啊!$R累了吧?");
            switch (Select(pc, "进去休息一下吧", "", "嗯！好喔", "不用了"))
            {
                case 1:
                    if (pc.Gold >= 360)
                    {
                        pc.Gold -= 360;
                        Say(pc, 0, 131, "支付了360G.", " ");
                        Fade(pc, FadeType.Out, FadeEffect.Black);
                        Wait(pc, 3000);
                        Warp(pc, 10059101, 233, 35);
                        //Fade(pc, FadeType.Out, FadeEffect.Black);
                    }
                    else
                        Say(pc, 131, "钱不够呢.");
                    break;

            }
        }
    }
}