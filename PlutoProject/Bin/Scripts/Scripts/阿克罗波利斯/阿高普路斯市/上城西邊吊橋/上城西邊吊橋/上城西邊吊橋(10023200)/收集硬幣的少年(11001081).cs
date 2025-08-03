using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10023200
{
    public class S11001081 : Event
    {
        public S11001081()
        {
            this.EventID = 11001081;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10009551) >= 1)
            {
                Say(pc, 0, 0, "���ŤӤ������󡻽o��$R;" +
                            "���Ԇ᣿$R;", "");
                if (Select(pc, "�o��", "", "�o", "���o") == 1)
                {
                    TakeItem(pc, 10009551, 1);
                    GiveRandomTreasure(pc, "gudai1");
                    return;
                }
            }

            Say(pc, 0, 0, "���ŤӤ�������������$R;" +
            "...��$R;", "");

        }
    }
}
