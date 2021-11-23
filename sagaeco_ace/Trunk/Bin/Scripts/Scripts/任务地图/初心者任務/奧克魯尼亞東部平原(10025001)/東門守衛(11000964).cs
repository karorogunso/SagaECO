using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:東門守衛(11000964) X:61 Y:121
namespace SagaScript.M10025001
{
    public class S11000964 : Event
    {
        public S11000964()
        {
            this.EventID = 11000964;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000964, 131, "啊! 辛苦了!$R;" +
                                   "$R继续往前走的话，$R;" +
                                   "是阿克罗尼亚大陆最大的贸易城市$R;" +
                                   "「阿克罗波利斯」的入口$R;", "东门守卫");
        }
    }
}
