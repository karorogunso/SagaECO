using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:艾尔·夏尔（上层）(11023000)NPC基本信息:11002282-收藏家·米歇尔- X:68 Y:180
namespace SagaScript.M11023000
{
    public class S11002282 : Event
    {
    public S11002282()
        {
            this.EventID = 11002282;
        }


        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10083800) >= 1)
            {
                Say(pc, 11002282, 131, "喂,你$R;"+
                                        "$R;"+
                                        "就是你,别东张西望了$R;", "米歇尔");
                Say(pc, 11002282, 131, "你瞒不过我的,你身上有奖章吧$R;" +
                                        "反正你拿着也没什么用$R;" +
                                        "我用我身上所有的钱和你换,如何?$R;", "米歇尔");
                switch (Select(pc, "要让出奖章吗?", "", "是的", "不了"))
                {
                    case 1:
                        int a=0;
                        a = Global.Random.Next(1500, 974000);
                        Say(pc, 11002282, 131, "啊啊!奖章啊!!$R;", "米歇尔");
                        TakeItem(pc, 10083800, 1);
                        pc.Gold += a;
                        Say(pc, 0, 0, "得到了"+a+"金币");
                        break;
                    case 2:
                        Say(pc, 11002282, 131, "真是可惜$R;" +
                                        "如果改变想法的话就来找我吧$R;" +
                                        "我一直在这里$R;", "米歇尔");
                        break;
                }
            }
            else
            {
                Say(pc, 11002282, 111, "别来烦我散步$R;", "米歇尔");
            }
            
        }
    }
}
