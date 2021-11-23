using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30095000
{
    public class S11000853 : Event
    {
        public S11000853()
        {
            this.EventID = 11000853;

        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "歡迎到摩根政府大廈$R;");

            switch (Select(pc, "做什麼呢？", "", "清潔計劃", "什麼也不做"))
            {
                case 1:
                    //GOTO EVT1100085301;
                    break;
                case 2:

                    break;
            }
        }
    }
}