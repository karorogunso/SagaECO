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
                "$R我叫活動木偶塞爾曼德喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶塞爾曼德打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000730, 131, "是用神秘之火做成，$R擁有火焰力量的活動木偶唷$R;" +
                "$P變成我以後，$R可以使用『火焰鞭』，$R把周圍燃燒成火之鞭喔$R;" +
                "$P還有變身期間『HP』會自然恢復$R;" +
                "$R在炎熱的地方，體力也不會减退呀$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶塞爾曼德打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000730, 131, "怎麼樣？試試變成我嗎？$R;");
            switch (Select(pc, "怎麼辦呢？", "", "我想變身", "放棄"))
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