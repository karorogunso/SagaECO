using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:活動木偶瑪歐斯(11000728) X:173 Y:46
namespace SagaScript.M10071000
{
    public class S11000728 : Event
    {
        public S11000728()
        {
            this.EventID = 11000728;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Say(pc, 11000728, 131, "嗨！$R;" +
                                  "$R我是活動木偶瑪歐斯喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶瑪歐斯打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000728, 131, "怎麼樣？我帥不帥呢？$R什麼！噁心？$R;" +
                                   "$R我是外形像魚，$R賦有水靈力量的活動木偶唷$R;" +
                                   "$P變成我，可以使用技能『魅惑腳踢』，$R把對方拉過來後甩出去，$R可以扔到很遠的地方呀。$R;" +
                                   "$P還有變身期間『SP』會自然恢復的$R;" +
                                   "$R在很冷的地方或水裡$R也不會受影響呀$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶瑪歐斯打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000728, 131, "怎麼樣？$R變身瑪歐斯嗎？$R;");
            switch (Select(pc, "怎麼辦呢？？", "", "我想變身", "放棄"))
            {
                case 1:
                    ActivateMarionette(pc, 10019400);
                    ShowEffect(pc, 8015);
                    Heal(pc);
                    break;

                case 2:
                    break;
            }
        }
    }
}