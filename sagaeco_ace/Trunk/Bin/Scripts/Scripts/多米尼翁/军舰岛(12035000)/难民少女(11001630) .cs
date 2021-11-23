using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M12035000
{
    public class S11001630 : Event
    {
        public S11001630()
        {
            this.EventID = 11001630;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Say(pc, 111, "姐姐、姐姐。$R;" +
                "我肚子餓……。$R;", "難民少女");
            }
            else
            {
                Say(pc, 111, "哥哥、哥哥 。$R;" +
                "我肚子餓……。$R;", "難民少女");
            }
            Say(pc, 11001629, 131, "姐姐的包里不是$R;" +
             "還有點心嗎？$R;" +
             "$R吃那個吧。$R;" +
            "把姐姐那份吃了也行。$R;", "難民女性");

        }
    }
}


        
   


