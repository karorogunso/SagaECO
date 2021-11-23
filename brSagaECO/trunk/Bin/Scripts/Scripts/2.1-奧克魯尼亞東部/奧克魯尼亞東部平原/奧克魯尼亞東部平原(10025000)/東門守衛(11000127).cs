using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:東門守衛(11000127) X:61 Y:134
namespace SagaScript.M10025000
{
    public class S11000127 : Event
    {
        public S11000127()
        {
            this.EventID = 11000127;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000127, 131, "去「帕斯特」的路中，$R;" +
                                   "雖然很少兇惡的怪。$R;" +
                                   "$R說是非常安全的路，$R;" +
                                   "但是不可以掉以輕心啊!$R;", "東門守衛");
        }
    }
}
