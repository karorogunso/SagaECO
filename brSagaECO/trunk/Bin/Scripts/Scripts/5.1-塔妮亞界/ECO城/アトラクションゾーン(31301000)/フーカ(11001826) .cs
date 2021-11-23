using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M31301000
{
    public class S11001826 : Event
    {
        public S11001826()
        {
            this.EventID = 11001826;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<ECOchen> ECOchen_mask = new BitMask<ECOchen>(pc.CMask["ECOchen"]);
            //int selection;
            if (ECOchen_mask.Test(ECOchen.打到放水龙后))
            {
                if (pc.ECoin >= 10000)
                {
                    if (Select(pc, "换么？", "", "换", "不换") == 1)
                    {
                        pc.ECoin -= 10000;
                        GiveItem(pc, 10066500, 1);
                        Say(pc, 131, "给你吧$R;" +
                        "其实我也不知道做什么用的。$R;", "フーカ");
                        return;
                    }
                }
                else
                {
                    Say(pc, 0, "$P没ecoin$R;", "フーカ");
                }
                Say(pc, 131, "!?$R;" +
                "你给我ecoin10,000我就给你个好东西。$R;", "フーカ");
                return;
            }
        
            Say(pc, 131, "噢呼、噢呼呼！$R;" +
            "$P金幣的山…。$R;" +
            "$唔、真是幸福的說。$R;" +
            "想要更多更多的說。$R;", "フーカ");
            /*
            Say(pc, 131, "むふっ、むふふっ！$R;" +
            "$Pコインの山…。$R;" +
            "$Rうーん、幸せですの。$R;" +
            "もっともっと増やしたいですの。$R;", "フーカ");
            */
        }
}

        
    }


