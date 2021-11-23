using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//?€?¨åœ°??ä¸Šå?(10023000) NPC?ºæœ¬ä¿¡æ¯:?‡é??†äºº(18000200) X:133 Y:147
namespace SagaScript.M10023000
{
    public class S11001692 : Event
    {
        public S11001692()
        {
            this.EventID = 11001692;
        }

        public override void OnEvent(ActorPC pc)
        {
                            switch (Select(pc, "Âò¶«Î÷Âğ£¿", "", "Âò", "²»Âò"))
                            {
                                case 1:
                    OpenShopBuy(pc, 414);
                    break;
                            }
        }
    }
}
