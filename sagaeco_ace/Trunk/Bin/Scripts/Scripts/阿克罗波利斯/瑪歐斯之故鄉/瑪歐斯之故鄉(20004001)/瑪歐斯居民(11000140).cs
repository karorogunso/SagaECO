using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004001
{
    public class S11000140 : Event
    {
        public S11000140()
        {
            this.EventID = 11000140;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Global.Random.Next(1,2) == 1)
                Say(pc, 11000140, 131, "这是鱼人的聚集地$R;" +
                    "$R是没有人知道的闭锁空间哦。$R;");

            Say(pc, 11000140, 131, "想找长老？$R;" +
                "$R他在里面，您找他有什么事吗？$R;");
        }
    }
}