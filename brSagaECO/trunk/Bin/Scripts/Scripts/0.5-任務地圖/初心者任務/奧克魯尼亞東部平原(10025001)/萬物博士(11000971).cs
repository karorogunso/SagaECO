using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:萬物博士(11000971) X:56 Y:121
namespace SagaScript.M10025001
{
    public class S11000971 : Event
    {
        public S11000971()
        {
            this.EventID = 11000971;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000971, 131, "您好!$R;" +
                                   "$P那裡的大叔…不是、不是!$R;" +
                                   "長官們的話都聽說過嗎?$R;" +
                                   "$P雖然叫「混城騎士團」，$R;" +
                                   "其實他們之間的關係不太好呀!$R;" +
                                   "$P嗯…$R;" +
                                   "$R那些人，雖然勸您加入軍隊，$R;" +
                                   "但是在奧克魯尼亞大陸冒險，$R;" +
                                   "不一定要加入軍隊的啊…$R;" +
                                   "$P嗯…$R;" +
                                   "$R當然，加入軍隊的話，$R;" +
                                   "會方便一些，但是…$R;" +
                                   "$P有什麼不懂的，隨時來找我吧!$R;" +
                                   "$R我經常在「阿高普路斯市」的$R;" +
                                   "「下城」中央附近徘徊的喔!$R;", "萬物博士");
        }
    }
}
