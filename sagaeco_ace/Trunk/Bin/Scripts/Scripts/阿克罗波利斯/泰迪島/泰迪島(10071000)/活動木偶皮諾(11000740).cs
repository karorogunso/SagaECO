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
                "我叫活动木偶皮诺喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶皮諾打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000740, 131, "我身体是植物，$R是拥有大地力量的活动木偶呀$R;" +
                "$P变成我的模样，$R可以使用『爬籐榕』技能，$R把自己周围的魔物$R弄得动弹不得喔$R;" +
                "$P还有变身期间，$R『MP』会自动恢复的$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶皮諾打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000740, 131, "怎么样？想变成我这样吗？$R;");
            switch (Select(pc, "怎么办呢？", "", "我想变身", "放弃"))
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