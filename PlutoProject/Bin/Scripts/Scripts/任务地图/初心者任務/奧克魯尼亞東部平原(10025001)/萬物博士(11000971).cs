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
                                   "$P那里的大叔…不是、不是!$R;" +
                                   "长官们的话都听说过吗?$R;" +
                                   "$P虽然叫「混成骑士团」，$R;" +
                                   "其实他们之间的关系不太好呀!$R;" +
                                   "$P嗯…$R;" +
                                   "$R那些人，虽然劝您加入军队，$R;" +
                                   "但是在阿克罗尼亚大陆冒险，$R;" +
                                   "不一定要加入军队的啊…$R;" +
                                   "$P嗯…$R;" +
                                   "$R当然，加入军队的话，$R;" +
                                   "会方便一些，但是…$R;" +
                                   "$P有什么不懂的，随时来找我吧!$R;" +
                                   "$R我经常在「阿克罗波利斯」的$R;" +
                                   "「下城」中央附近徘徊的喔!$R;", "万事通");
        }
    }
}
