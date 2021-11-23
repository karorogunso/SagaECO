using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10046000
{
    public class S11000166 : Event
    {
        public S11000166()
        {
            this.EventID = 11000166;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "歡迎來到阿伊恩薩烏斯的聯邦!$R;" +
                "$R要進阿伊恩薩烏斯聯邦的話$R;" +
                "需要加入屬於奧克魯尼亞$R;" +
                "或$R需要$R;" +
                "『阿伊恩薩烏斯許可證』$R;");
        }
        void 騎士團(ActorPC pc)
        {
            Say(pc, 131, "屬於奧克魯尼亞混城騎士團$R;" +
                "『南軍』嗎?$R;" +
                "$R請在裡面辦理一下入境手續$R;");
        }
        void 虎眼任務(ActorPC pc)
        {
            Say(pc, 131, "哎呀…!$R;" +
            "在擔心是不是在途中踫到$R;" +
            "「破壞ＲＸ１」或$R;" +
            "「緋紅巴嗚」了呢$R;" +
            "$P原來是喜歡跟隨人$R;" +
            "受到愛戴的存在$R;" +
            "$R但是現在成了襲擊$R;" +
            "人的可拍得存在$R;" +
            "$P它們可能是天上給人們的$R;" +
            "懲罰也不一定$R;" +
            "$R但是我相信$R;" +
            "總有一天又會$R;" +
            "重新可以生活在一起的$R;" +
            "$P我能講得故事就到此爲止了$R;");

            Say(pc, 131, "那現在往帕斯特去看看吧$R;" +
                "在國境附近橋前面的人$R;" +
                "會跟你講的$R;");
        }
    }
}
