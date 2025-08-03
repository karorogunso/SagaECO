using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30099001
{
    public class S11000858 : Event
    {
        public S11000858()
        {
            this.EventID = 11000858;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这裡是迪奥曼特先生$R;" +
                "私设的制炼所$R;" +
                "$R除了相关人员都回去吧$R;");
        }
    }
}