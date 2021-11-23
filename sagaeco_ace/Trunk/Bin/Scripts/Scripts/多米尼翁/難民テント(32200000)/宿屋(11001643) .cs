using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M32200000
{
    public class S11001643 : Event
    {
        public S11001643()
        {
            this.EventID = 11001643;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<emojie_00> emojie_00_mask = new BitMask<emojie_00>(pc.CMask["emojie_00"]);
            //int selection;
            Say(pc, 136, "……。（呼呼大睡）$R;" +
            "$P……啊，是客人……？$R;" +
            "$R休息的話，就在那邊$R;" +
            "隨便找個位置吧……。$R;" +
            "$P不過住宿費我還是要好好收的……。$R;", "宿屋");
            switch (Select(pc, "住一晚上要500金幣。", "", "不用了","住一晚上","想利用道具"))
            {
                case 2:
                    if (pc.Gold < 500)
                    {
                        Say(pc, 131, "金幣不足$R;");
                        return;
                    }
                    pc.Gold -= 500;
                    PlaySound(pc, 4001, false, 100, 50);
                    Fade(pc, FadeType.Out, FadeEffect.Black);
                    //FADE OUT BLACK
                    Wait(pc, 10000);
                    //FADE IN
                    Heal(pc);
                    Wait(pc, 1000);
                    NPCShow(pc, 11001642);//让NPC出现
                    NPCHide(pc, 11001643);//隐藏NPC
                    Fade(pc, FadeType.In, FadeEffect.Black);
                    Say(pc, 131, "消除了一些疲勞了嗎？$R;");
                    emojie_00_mask.SetValue(emojie_00.开关, false);
                    return;
            }
        }
    }
}


        
   


