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

            Say(pc, 11000732, 131, "我是活动木偶火焰凤凰啦$R;" +
                "$R是火焰精灵之王喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶虎姆拉打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000732, 131, "我是用火做成，$R拥有火焰力量的活动木偶哦$R;" +
                "$P变成我，$R可以使用『漩涡』技能，$R用火焰燃烧周围$R;" +
                "$P还有变身期间『HP』会自然恢复$R;" +
                "$R在炎热的地方，体力也不会减退呀$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶虎姆拉打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000732, 131, "怎么样？试试变成我吗？$R;");
            switch (Select(pc, "怎么办呢？", "", "我想变身", "放弃"))
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




