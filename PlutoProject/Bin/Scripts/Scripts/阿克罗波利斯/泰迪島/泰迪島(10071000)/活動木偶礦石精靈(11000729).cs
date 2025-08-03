using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:活動木偶礦石精靈(11000729) X:173 Y:48
namespace SagaScript.M10071000
{
    public class S11000729 : Event
    {
        public S11000729()
        {
            this.EventID = 11000729;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Say(pc, 11000729, 131, "……$R;" +
                "$R我是活动木偶矿石精灵唷$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶綠礦石精靈打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000729, 131, "是用宝石做成的活动木偶喔$R;" +
                "$P变成我以后，$R可以使用『透明化』技能$R把周围自己的队友透明化喔$R;" +
                "$P还有变身期间『SP』会自然恢复$R;" +
                "$R在水里和真空状态下$R体力不会下降呀$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶綠礦石精靈打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000729, 131, "怎么样？试试变成我吗？$R;");
            switch (Select(pc, "怎么办呢？", "", "我想变身", "放弃"))
            {
                case 1:
                    ActivateMarionette(pc, 10011500);
                    ShowEffect(pc, 8015);
                    Heal(pc);
                    break;

                case 2:
                    break;
            }
        }
    }
}