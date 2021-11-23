using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M50061000
{
    public class S11001654 : Event
    {
        public S11001654()
        {
            this.EventID = 11001654;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "お、おかしいな$R;" +
            "急に体が震えてきたよ……？$R;", "エミル");

            Say(pc, 11001655, 131, "あら？武者震いってやつ？$R;", "マーシャ");

            Say(pc, 131, "今更ながらちょっと$R;" +
            "不安になっちゃって……。$R;", "エミル");

            Say(pc, 11001655, 131, "本当、頼りないんだから……。$R;", "マーシャ");

        }
    }
}
            
        
     
    