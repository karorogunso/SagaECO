using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:活動木偶瑪歐斯(11000728) X:173 Y:46
namespace SagaScript.M10071000
{
    public class S11000728 : Event
    {
        public S11000728()
        {
            this.EventID = 11000728;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Say(pc, 11000728, 131, "嗨！$R;" +
                                  "$R我是活动木偶鱼人喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶瑪歐斯打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000728, 131, "怎么样？我帅不帅呢？$R什么！噁心？$R;" +
                                   "$R我是外形像鱼，$R赋有水灵力量的活动木偶哦$R;" +
                                   "$P变成我，可以使用技能『魅惑脚踢』，$R把对方拉过来后甩出去，$R可以扔到很远的地方呀。$R;" +
                                   "$P还有变身期间『SP』会自然恢复的$R;" +
                                   "$R在很冷的地方或水里$R也不会受影响呀$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶瑪歐斯打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000728, 131, "怎么样？$R变身鱼人吗？$R;");
            switch (Select(pc, "怎么办呢？？", "", "我想变身", "放弃"))
            {
                case 1:
                    ActivateMarionette(pc, 10019400);
                    ShowEffect(pc, 8015);
                    Heal(pc);
                    break;

                case 2:
                    break;
            }
        }
    }
}