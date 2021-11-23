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

            Say(pc, 131, "欢迎到摩戈政府大厦$R;");

            switch (Select(pc, "做什么呢？", "", "清洁计划", "什么也不做"))
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