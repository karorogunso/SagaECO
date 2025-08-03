using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:活動木偶塞爾曼德(11000730) X:171 Y:48
namespace SagaScript.M10071000
{
    public class S11000730 : Event
    {
        public S11000730()
        {
            this.EventID = 11000730;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Say(pc, 11000730, 131, "您好$R;" +
                "$R我叫活动木偶烈焰蜥蜴喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶塞爾曼德打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000730, 131, "是用神秘之火做成，$R拥有火焰力量的活动木偶哦$R;" +
                "$P变成我以后，$R可以使用『火焰鞭』，$R把周围燃烧成火之鞭喔$R;" +
                "$P还有变身期间『HP』会自然恢复$R;" +
                "$R在炎热的地方，体力也不会减退呀$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶塞爾曼德打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000730, 131, "怎么样？试试变成我吗？$R;");
            switch (Select(pc, "怎么办呢？", "", "我想变身", "放弃"))
            {
                case 1:
                    ActivateMarionette(pc, 10013850);
                    ShowEffect(pc, 8015);
                    Heal(pc);
                    break;

                case 2:
                    break;
            }
        }
    }
}