using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞北部平原(10014000) NPC基本信息:路標(12002009) X:62 Y:253
namespace SagaScript.M10014000
{
    public class S12002009 : Event
    {
        public S12002009()
        {
            this.EventID = 12002009;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "前面是去「帕斯特」的捷徑。$R;" +
                          "那裏很容易迷路的。$R;", " ");

            switch (Select(pc, "後面寫了些什麼?", "", "看看吧", "不看"))
            {
                case 1:
                    Say(pc, 0, 0, "把『杰利科』分成四等份。$R;" +
                                  "$P把泡泡放在雞蛋裡$R;" +
                                  "做出『泡沫蛋白』。$R;" +
                                  "$R把『泡沫蛋白』和『砂糖』攪勻。$R;" +
                                  "$R在放『杰利科』攪勻，$R;" +
                                  "就能做出軟綿綿的『棉果子』了。$R;" +
                                  "$P『杰利科』很神奇呢!$R;");
                    break;

                case 2:
                    break;
            }
        }
    }
}
