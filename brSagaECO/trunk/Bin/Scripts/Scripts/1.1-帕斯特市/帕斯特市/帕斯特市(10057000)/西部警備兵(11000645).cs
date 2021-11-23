using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000645 : Event
    {
        public S11000645()
        {
            this.EventID = 11000645;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000645, 131, "歡迎來到帕斯特市！$R;" +
                "雖然没有什麽好看的地方$R;" +
                "但是空氣清新，$R居住環境非常不錯哦！$R;" +
                "$R哇哈哈哈哈！$R;");
            Say(pc, 11000644, 500, "汪汪!!汪汪汪!$R;");
        }
    }
}