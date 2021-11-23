using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002034 : Event
    {
        public S11002034()
        {
            this.EventID = 11002034;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10076100) > 0)
            {
                OpenShopBuy(pc, 289);
                TakeItem(pc, 10076100, 1);
                return;
            }
            Say(pc, 131, "……ハァ、コマッタ事ネ。$R;" +
            "$Pアイヤ！？$R;" +
            "ワタシ、アヤシイ、ナイネ！$R;" +
            "マダ、準備中ヨ～！$R;", "武器商人");

            Say(pc, 131, "嗚呼、ソウネ、オキャクサン！$R;" +
            "『水竜の角』知ラナイカ？$R;" +
            "ワタシ、探シテルヨ～。$R;", "武器商人");
           

        }
    }
}


        
   


