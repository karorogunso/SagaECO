using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:瑪莎(11000941) X:173 Y:96
namespace SagaScript.M10025001
{
    public class S11000941 : Event
    {
        public S11000941()
        {
            this.EventID = 11000941;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000941, 131, "照箭头走的话，$R;" +
                                   "就可以看到「阿克罗波利斯」哦!$R;" +
                                   "$R「泰塔斯」就在桥上!$R;" +
                                   "去跟他聊聊吧!$R;" +
                                   "$P「泰塔斯」是泰达尼亚族的男孩，$R;" +
                                   "应该会在桥附近等您的。$R;", "玛莎");

            Navigate(pc, 1, 128);
        }
    }
}
