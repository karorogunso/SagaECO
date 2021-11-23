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
            Say(pc, 11000941, 131, "照箭頭走的話，$R;" +
                                   "就可以看到「阿高普路斯市」唷!$R;" +
                                   "$R「提多」就在橋上!$R;" +
                                   "去跟他聊聊吧!$R;" +
                                   "$P「提多」是塔妮亞族的男孩，$R;" +
                                   "應該會在橋附近等您的。$R;", "瑪莎");

            Navigate(pc, 1, 128);
        }
    }
}
