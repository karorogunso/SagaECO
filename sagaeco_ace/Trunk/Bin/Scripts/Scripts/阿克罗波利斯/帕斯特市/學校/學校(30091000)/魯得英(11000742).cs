using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30091000
{
    public class S11000742 : Event
    {
        public S11000742()
        {
            this.EventID = 11000742;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "您知道吗？$R;" +
                "$R丛林里有圣地!$R;" +
                "老爷爷说的呢$R;");
            Say(pc, 11000741, 131, "哇！$R;" +
                "但是圣地是什么？$R;");
            Say(pc, 131, "那个啊…就是神圣的地方！$R;" +
                "也就是说…丛林里的重要的地方！$R;");
            Say(pc, 11000741, 131, "噢噢！！$R;" +
                "但是怎么去啊？$R;");
            Say(pc, 131, "那个啊…$R;" +
                "绿色石头！对！$R;" +
                "听说绿色石头会带路？$R;");
            Say(pc, 11000741, 131, "哇！$R;" +
                "但是绿色石头是什么？$R;");
            Say(pc, 131, "那个啊…不知道！$R;" +
                "不要问我任何东西！$R;");
            Say(pc, 11000741, 131, "呀!$R;" +
                "对不起对不起！$R;");
        }
    }
}