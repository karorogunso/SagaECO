using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:活動木偶皮諾(11000740) X:172 Y:45
namespace SagaScript.M10071000
{
    public class S11000740 : Event
    {
        public S11000740()
        {
            this.EventID = 11000740;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Say(pc, 11000740, 131, "您好$R;" +
                "我叫活動木偶皮諾喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶皮諾打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000740, 131, "我身體是植物，$R是擁有大地力量的活動木偶呀$R;" +
                "$P變成我的模樣，$R可以使用『爬籐榕』技能，$R把自己周圍的魔物$R弄得動彈不得喔$R;" +
                "$P還有變身期間，$R『MP』會自動恢復的$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶皮諾打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000740, 131, "怎麼樣？想變成我這樣嗎？$R;");
            switch (Select(pc, "怎麼辦呢？", "", "我想變身", "放棄"))
            {
                case 1:
                    ActivateMarionette(pc, 10027000);
                    ShowEffect(pc, 8015);
                    Heal(pc);
                    break;

                case 2:
                    break;
            }
        }
    }
}