using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//?�?�地??上�?(10023000) NPC?�本信息:?��??�人(18000200) X:133 Y:147
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
                            switch (Select(pc, "������", "", "��", "����"))
                            {
                                case 1:
                    OpenShopBuy(pc, 414);
                    break;
                            }
        }
    }
}
