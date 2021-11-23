using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:活動木偶虎姆拉(11000732) X:172 Y:49
namespace SagaScript.M10071000
{
    public class S11000732 : Event
    {
        public S11000732()
        {
            this.EventID = 11000732;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Say(pc, 11000732, 131, "我叫活動木偶虎姆拉$R;" +
                "$R是火焰精靈之王喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶虎姆拉打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000732, 131, "我是用火做成，$R擁有火焰力量的活動木偶唷$R;" +
                "$P變成我，$R可以使用『漩渦』技能，$R用火焰燃燒周圍$R;" +
                "$P還有變身期間『HP』會自然恢復$R;" +
                "$R在炎熱的地方，體力也不會减退呀$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶虎姆拉打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000732, 131, "怎麼樣？試試變成我嗎？$R;");
            switch (Select(pc, "怎麼辦呢？", "", "我想變身", "放棄"))
            {
                case 1:
                    ActivateMarionette(pc, 10021700);
                    ShowEffect(pc, 8015);
                    Heal(pc);
                    break;

                case 2:
                    break;
            }
        }
    }
}




