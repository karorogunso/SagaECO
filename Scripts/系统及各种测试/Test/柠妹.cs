
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S90000020 : Event
    {
        public S90000020()
        {
            this.EventID = 90000020;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "欢迎来到二测BOSS测试！", "柠妹");
            switch(Select(pc,"请选择","","朋朋的挑战","离开"))
            {
                case 1:
                    switch(Select(pc,"请选择难度","","普通难度","挑战难度","离开"))
                    {
                        case 1:
                            
                            DefWarAdd(30021000, 10000000);
                            break;
                        case 2:
                            pc.DefWarShow = true;
                            break;
                        case 3:
                            DefWarSet(30021000, 10000000, 1, 1, 1);
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

