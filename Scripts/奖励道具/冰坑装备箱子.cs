
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
    public class S953000001 : Event
    {
        public S953000001()
        {
            this.EventID = 953000001;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(CountItem(pc, 953000001) >= 1)
            {
                Say(pc, 131, "让你拿到未开放的箱子了实在抱歉...$R$R由于冰坑副本还没出，$R这个箱子暂时开不了。$R$R不过你可以把她换成寒流箱子！$R是否要兑换成寒流箱子呢？");
                if (Select(pc, "是否要兑换成寒流箱子呢？", "", "换成寒流箱子", "还是算了") == 1)
                {
                    if (CountItem(pc, 953000001) >= 1)
                    {
                        TakeItem(pc, 953000001, 1);
                        GiveItem(pc, 953000021, 1);
                    }
                }
            }
        }
       
    }
}

