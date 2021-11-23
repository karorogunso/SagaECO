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
                "我是活动木偶电路机械喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶塔依打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000733, 131, "我是用机械做成的，$R拥有神风力量的活动木偶哦$R;" +
                "$P变成我，$R可以使用『万雷冲击』技能，$R在周围引起防静电的障壁。$R;" +
                "$P还有变身期间『HP』会自然恢复$R;" +
                "$R且在水或真空里，不会有任何变化哦$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶塔依打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000733, 131, "怎么样？试试变成咇咇的-$R我吗？$R;");
            switch (Select(pc, "怎么办呢？", "", "我想变身", "放弃"))
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