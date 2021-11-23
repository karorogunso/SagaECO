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
            Say(pc, 131, "您知道嗎？$R;" +
                "$R叢林裡有聖地!$R;" +
                "老爺爺説的唷$R;");
            Say(pc, 11000741, 131, "哇！$R;" +
                "但是聖地是什麽？$R;");
            Say(pc, 131, "那個啊…就是神聖的地方！$R;" +
                "也就是說…叢林裡的重要的地方！$R;");
            Say(pc, 11000741, 131, "噢噢！！$R;" +
                "但是怎麽去阿？$R;");
            Say(pc, 131, "那個啊…$R;" +
                "綠色石頭！對！$R;" +
                "聽説綠色石頭會帶路？$R;");
            Say(pc, 11000741, 131, "哇！$R;" +
                "但是綠色石頭是什麽？$R;");
            Say(pc, 131, "那個啊…不知道！$R;" +
                "不要問我任何東西！$R;");
            Say(pc, 11000741, 131, "呀!$R;" +
                "對不起對不起！$R;");
        }
    }
}