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
                                   "$R找我有事吗?$R;", "阿克罗波利斯泰迪");

            switch (Select(pc, "找我有事吗?", "", "我想回到 「阿克罗波利斯」", "没事"))
            {
                case 1:
                    Say(pc, 18000001, 131, "为什么拜托我?$R;", "阿克罗波利斯泰迪");

                    switch (Select(pc, "为什么拜托我?", "", "就不知道为什么…", "怎么? 你不知道吗?"))
                    {
                        case 1:
                            Say(pc, 18000001, 131, "我的名字?$R;" +
                                                   "哦哦~ 是不是看出来，$R;" +
                                                   "我是从「阿克罗波利斯」来的啊?$R;" +
                                                   "$R好，我特别带你去「阿克罗波利斯」吧。$R;" +
                                                   "$P怎么样?$R;", "阿克罗波利斯泰迪");

                            switch (Select(pc, "要回去「阿克罗波利斯」吗?", "", "回去", "不回去"))
                            {
                                case 1:
                                    Say(pc, 18000001, 131, "来吧!$R;" +
                                                           "$R那么就向「阿克罗波利斯」出发吧!$R;", "阿克罗波利斯泰迪");

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