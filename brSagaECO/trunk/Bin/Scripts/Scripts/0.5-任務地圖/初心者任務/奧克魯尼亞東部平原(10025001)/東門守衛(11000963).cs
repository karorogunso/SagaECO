using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:東門守衛(11000963) X:61 Y:134
namespace SagaScript.M10025001
{
    public class S11000963 : Event
    {
        public S11000963()
        {
            this.EventID = 11000963;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000963, 131, "啊! 辛苦了!$R;" +
                                   "$R繼續往前走的話，$R;" +
                                   "是奧克魯尼亞大陸最大的貿易城市$R;" +
                                   "「阿高普路斯市」的入口$R;", "東門守衛");
        }
    }
}
