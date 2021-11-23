using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:咖啡館店員(11000965) X:105 Y:136
namespace SagaScript.M10025001
{
    public class S11000965 : Event
    {
        public S11000965()
        {
            this.EventID = 11000965;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000965, 131, "您好!$R;" +
                                   "$R我是「阿高普路斯市」的「下城」的$R;" +
                                   "「咖啡館」的店員!$R;" +
                                   "$P我是來這裡宣傳的!$R;" +
                                   "$R那開始宣傳吧!!!$R;" +
                                   "$P歡迎光臨!$R;" +
                                   "歡迎光臨!!$R;" +
                                   "$R世界上對初心者最親切的咖啡館，$R;" +
                                   "在「阿高普路斯市」的「下城」東邊階梯下面唷!$R;" +
                                   "$P在咖啡館裡除了出售飲料外，$R;" +
                                   "也有介紹工作的(任務服務台)!!$R;" +
                                   "$P雖然任務服務台，$R;" +
                                   "只提供介紹任務的服務，$R;" +
                                   "但「阿高普路斯市」周邊的，$R;" +
                                   "東、南、西、北四個平原裡都有分店唷!$R;" +
                                   "希望多多光顧呀!!!$R;", "咖啡館店員");
        }
    }
}
