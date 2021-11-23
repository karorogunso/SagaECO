using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10004000
{
    public class S11000173 : Event
    {
        public S11000173()
        {
            this.EventID = 11000173;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "想知道關於什麽呢?", "", "北域", "凍結的耕道", "沒什麽可問的"))
            {
                case 1:
                    Say(pc, 131, "從這邊一直往北走就是『北域』$R;" +
                        "$R有往『諾頓王國』的巨大的橋$R;" +
                        "$P橋的周邊是村莊$R;" +
                        "所以可以在那邊休息$R;" +
                        "$R會很冷的用暖和的服裝$R;" +
                        "去比較好!!$R;");
                    break;
                case 2:
                    Say(pc, 131, "西邊有被叫為『凍結的耕道』的地牢$R;" +
                        "$R如果不想死的話$R;" +
                        "不要出現在那裡是上策!$R;");
                    break;
                case 3:
                    break;
            }
        }
    }
}
