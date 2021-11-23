using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:艾尔·夏尔（下层）(11024000)NPC基本信息:11002288-酒馆老板- X:190 Y:190
namespace SagaScript.M11024000
{
    public class S11002288 : Event
    {
    public S11002288()
        {
            this.EventID = 11002288;
        }


        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要不要喝一杯?", "", "买东西", "卖东西", "任务服务台", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 4);
                    break;

                case 2:
                    OpenShopSell(pc, 4);
                    break;

                case 3:
                    Say(pc, 11002288, 131, "......$R;", "酒馆老板");
                    Say(pc, 11002288, 131, "最近$R;"+
                                           "那个不知去向的城镇附近$R;"+ 
                                           "本来莫名其妙出现的庭院$R;"+
                                           "现在也出现了空间扭曲现象...$R;","酒馆老板" );
                    Say(pc, 11002288, 131, "我还是尽义务阻止你一下$R;" +
                                           "但我们也同时跟过去一样$R;"+
                                           "发布有关那里的任务$R;" +
                                           "当然这也表示,你去那里有可能$R;" +
                                           "根本遇不上你要找的东西$R;" +
                                           "所以去不去,还是看你自己的了$R;","酒馆老板");
                    HandleQuest(pc, 80);
                    break;
                case 4:
                    break;
            }  
        }
    }
}
