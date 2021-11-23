using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M32200001
{
    public class S11001577 : Event
    {
        public S11001577()
        {
            this.EventID = 11001577;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<emojie_00> emojie_00_mask = new BitMask<emojie_00>(pc.CMask["emojie_00"]);
            //int selection;
            Say(pc, 135, "歡迎光臨！$R;" +
            "要來點藥品嗎～？$R;" +
            "$R另外，爲了大家能在這個帳篷$R;" +
            "隨時都能休息，一直開放著的哦$R;", "野宿屋");
            switch (Select(pc, "休息嗎？", "", "不要", "要", "想利用商店"))
            {
                case 2:
                    Say(pc, 11001578, 136, "那請$R;" +
                    "隨便找個適合睡覺的地方$R;" +
                    "睡吧～。$R;" +
                    "$R那麽，晚安咯。$R;", "野宿屋");
                    PlaySound(pc, 4001, false, 100, 50);
                    Fade(pc, FadeType.Out, FadeEffect.Black);
                    //FADE OUT BLACK
                    Wait(pc, 10000);
                    //FADE IN
                    Heal(pc);
                    Wait(pc, 1000);
                    NPCShow(pc, 11001578);//让NPC出现
                    NPCHide(pc, 11001577);//隐藏NPC
                    Fade(pc, FadeType.In, FadeEffect.Black);
                    emojie_00_mask.SetValue(emojie_00.开关, true);
                    return;
                case 3:
                    OpenShopBuy(pc, 245);
                    return;
            }
        }
    }
}


