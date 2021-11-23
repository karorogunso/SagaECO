using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:塔妮亞世界的幻影(30204000) NPC基本信息:微微(13000177) X:15 Y:20
namespace SagaScript.M30204000
{
    public class S13000177 : Event
    {
        public S13000177()
        {
            this.EventID = 13000177;
        }

        public override void OnEvent(ActorPC pc)
        {
            byte x, y;

            pc.CInt["Country"] = 0;

            Say(pc, 13000177, 131, "您好像…有點困惑啊?$R;" +
                                   "$R您是第一次來這裡吧?$R;", "微微");

            Say(pc, 13000177, 132, "我是微微，$R;" +
                                   "塔妮亞第三族的大天使。$R;" +
                                   "$P這裡是您的夢鄉唷!$R;" +
                                   "$R塔妮亞的世界$R;" +
                                   "擁有藍藍的天空$R;" +
                                   "真是美麗的地方呀。$R;", "微微");

            Say(pc, 13000177, 131, "您用不著擔心的，$R;" +
                                   "$R讓我帶領著您，前往ECO的世界吧!$R;", "微微");

            PlaySound(pc, 2122, false, 100, 50);
            ShowEffect(pc, 5607);
            Wait(pc, 2000);

            PlaySound(pc, 2122, false, 100, 50);
            ShowEffect(pc, 5607);
            Wait(pc, 1000);

            PlaySound(pc, 2122, false, 100, 50);
            ShowEffect(pc, 5607);
            Wait(pc, 2000);

            Say(pc, 13000177, 131, "啊!!$R;" +
                                   "$R…這是…$R;" +
                                   "這是『那些傢伙們』的電磁波…!?$R;", "微微");

            ShowEffect(pc, 4023);
            Wait(pc, 2000);

            pc.CInt["Beginner_Map"] = CreateMapInstance(50030000, 10023100, 250, 132);

            x = (byte)Global.Random.Next(2, 9);
            y = (byte)Global.Random.Next(2, 4);

            Warp(pc, (uint)pc.CInt["Beginner_Map"], x, y);
        }
    }
}
