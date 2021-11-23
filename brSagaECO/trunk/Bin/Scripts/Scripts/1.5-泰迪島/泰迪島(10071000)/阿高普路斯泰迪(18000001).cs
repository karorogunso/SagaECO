using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:阿高普路斯泰迪(18000001) X:245 Y:86
namespace SagaScript.M10071000
{
    public class S18000001 : Event
    {
        public S18000001()
        {
            this.EventID = 18000001;
        }
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 18000001, 131, "啊?$R;" +
                                   "$R找我有事嗎?$R;", "阿高普路斯泰迪");

            switch (Select(pc, "找我有事嗎?", "", "我想回到 「阿高普路斯市」", "沒事"))
            {
                case 1:
                    Say(pc, 18000001, 131, "為什麼拜託我?$R;", "阿高普路斯泰迪");

                    switch (Select(pc, "為什麼拜託我?", "", "就不知道為什麼…", "怎麼? 你不知道嗎?"))
                    {
                        case 1:
                            Say(pc, 18000001, 131, "我的名字?$R;" +
                                                   "哦哦~ 是不是看出來，$R;" +
                                                   "我是從「阿高普路斯市」來的啊?$R;" +
                                                   "$R好，我特別帶你去「阿高普路斯市」吧。$R;" +
                                                   "$P怎麼樣?$R;", "阿高普路斯泰迪");

                            switch (Select(pc, "要回去「阿高普路斯市」嗎?", "", "回去", "不回去"))
                            {
                                case 1:
                                    Say(pc, 18000001, 131, "來吧!$R;" +
                                                           "$R那麼就向「阿高普路斯市」出發吧!$R;", "阿高普路斯泰迪");

                                    Warp(pc, 10023000, 126, 150);
                                    break;

                                case 2:
                                    break;
                            }
                            break;

                        case 2:
                            break;
                    }
                    break;

                case 2:
                    break;
            }
        }
    }
}