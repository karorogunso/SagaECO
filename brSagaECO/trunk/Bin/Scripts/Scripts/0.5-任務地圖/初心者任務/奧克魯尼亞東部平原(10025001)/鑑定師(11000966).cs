using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:鑑定師(11000966) X:31 Y:121
namespace SagaScript.M10025001
{
    public class S11000966 : Event
    {
        public S11000966()
        {
            this.EventID = 11000966;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000966, 131, "我是鑑定師。$R;" +
                                   "$R正所謂老馬識途，$R;" +
                                   "以我人生的經驗，$R;" +
                                   "給未鑑定的道具來鑑定喔!$R;" +
                                   "$P未鑑定的道具，$R;" +
                                   "不先鑑定的話，是無法使用的。$R;" +
                                   "$P要鑑定的話，$R;" +
                                   "只有去學相關技能，$R;" +
                                   "或者叫我幫忙這兩種方法。$R;" +
                                   "$P我會一直在平原中央附近喔!$R;" +
                                   "發現未鑑定道具的話，$R;" +
                                   "就拿過來給我鑑定吧!$R;" +
                                   "$P告訴您一個訣竅吧!$R;" +
                                   "$R按住「Shift」鍵，$R;" +
                                   "同時點擊角色的周邊。$R;" +
                                   "$P可以簡單的改變角色的方向唷!$R;" +
                                   "$R即使在躺著或坐下的狀態，$R;" +
                                   "也可以變更方向呢!$R;", "鑑定師");
        }
    }
}
