using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M32200001
{
    public class S11001578 : Event
    {
        public S11001578()
        {
            this.EventID = 11001578;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<emojie_00> emojie_00_mask = new BitMask<emojie_00>(pc.CMask["emojie_00"]);
            //int selection;
            Say(pc, 136, "呼啊……。$R;" +
            "$R……歡迎光臨。$R;" +
            "客人……？$R;" +
            "$P要是累了的話$R;" +
            "可以再這裏休息哦～……。$R;" +
            "$R……天亮了的話$R;" +
            "我會叫妳起床的～……。$R;", "野宿屋");
            if (Select(pc, "休憩嗎？", "", "不要", "要", "想利用商店") == 2)
            {
                Say(pc, 11001578, 136, "那請$R;" +
                   "隨便找個適合睡覺的地方$R;" +
                   "睡吧～。$R;" +
                   "$R那麽，晚安咯。$R;", "野宿屋");
                Fade(pc, FadeType.Out, FadeEffect.Black);
                Wait(pc, 8250);
                Wait(pc, 990);
                PlaySound(pc, 4001, false, 100, 50);
                NPCShow(pc, 11001577);//让NPC出现
                NPCHide(pc, 11001578);//隐藏NPC
                emojie_00_mask.SetValue(emojie_00.开关, false);
                Fade(pc, FadeType.In, FadeEffect.Black);
            }
        }
    }
}

        
   


