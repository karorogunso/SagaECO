using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M32200000
{
    public class S11001642 : Event
    {
        public S11001642()
        {
            this.EventID = 11001642;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<emojie_00> emojie_00_mask = new BitMask<emojie_00>(pc.CMask["emojie_00"]);
            //int selection;
            Say(pc, 209, "歡迎光臨～！$R;" +
            "$R住一晚上是500金幣$R;" +
            "能够完全恢復客人的身心哦！$R;" +
            "$R要是有什麽需求，比如藥什麽的$R;" +
            "都可以讓給妳哦～！$R;", "宿屋");
            switch (Select(pc, "住一晚上是500金幣～！", "", "不用了", "住一晚上", "休息到晚上", "想利用商店"))
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
                    Fade(pc, FadeType.In, FadeEffect.Black);
                    //FADE IN
                    Heal(pc);
                    Wait(pc, 1000);
                    Say(pc, 131, "消除了一些疲勞了嗎？$R;");
                    return;
                case 3:
                    Say(pc, 209, "到晚上了我會叫妳的。$R;" +
                    "$R好好的睡吧～。$R;", "宿屋");
                    Fade(pc, FadeType.Out, FadeEffect.Black);
                    Wait(pc, 8250);
                    Wait(pc, 990);
                    PlaySound(pc, 4001, false, 100, 50);
                    NPCShow(pc, 11001643);//让NPC出现
                    NPCHide(pc, 11001642);//隐藏NPC
                    emojie_00_mask.SetValue(emojie_00.开关, true);
                    Fade(pc, FadeType.In, FadeEffect.Black);
                    return;
            }
        }
    }
}


        
   


