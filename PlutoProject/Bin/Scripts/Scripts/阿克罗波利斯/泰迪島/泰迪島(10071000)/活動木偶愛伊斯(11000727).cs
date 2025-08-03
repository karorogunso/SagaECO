using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:活動木偶愛伊斯(11000727) X:171 Y:46
namespace SagaScript.M10071000
{
    public class S11000727 : Event
    {
        public S11000727()
        {
            this.EventID = 11000727;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Say(pc, 11000727, 131, "您好$R;" +
                                   "我是活动木偶冰精灵喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶愛伊斯打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000727, 131, "是用神奇的冰做成，$R有水灵力量的活动木偶哦$R;" +
                                   "$P变成我，可以使用技能『急速冷冻』$R把周围的魔物冻僵，$R弄的它们动弹不得喔。$R;" +
                                   "$P还有变身期间$R『MP』可以自然恢复$R;" +
                                   "$R在寒冷的地方，体力也不会下降呀$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶愛伊斯打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000727, 131, "怎么样？想变成我吗？$R;");
            switch (Select(pc, "怎么办呢？？", "", "我想变身", "放弃"))
            {
                case 1:
                    ActivateMarionette(pc, 10019300);
                    ShowEffect(pc, 8015);
                    Heal(pc);
                    break;

                case 2:
                    break;
            }
        }
    }
}