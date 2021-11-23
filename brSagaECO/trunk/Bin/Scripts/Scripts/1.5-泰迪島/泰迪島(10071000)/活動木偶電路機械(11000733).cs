using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:活動木偶電路機械(11000733) X:174 Y:47
namespace SagaScript.M10071000
{
    public class S11000733 : Event
    {
        public S11000733()
        {
            this.EventID = 11000733;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Say(pc, 11000733, 131, "咇咇$R;" +
                "我叫活動木偶塔依喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶塔依打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000733, 131, "我是用機械做成的，$R擁有神風力量的活動木偶唷$R;" +
                "$P變成我，$R可以使用『萬雷衝擊』技能，$R在周圍引起防靜電的障壁。$R;" +
                "$P還有變身期間『HP』會自然恢復$R;" +
                "$R且在水或真空裡，不會有任何變化唷$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶塔依打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000733, 131, "怎麼樣？試試變成咇咇-$R我嗎？$R;");
            switch (Select(pc, "怎麼辦呢？", "", "我想變身", "放棄"))
            {
                case 1:
                    ActivateMarionette(pc, 10030001);
                    ShowEffect(pc, 8015);
                    Heal(pc);
                    break;

                case 2:
                    break;
            }
        }
    }
}