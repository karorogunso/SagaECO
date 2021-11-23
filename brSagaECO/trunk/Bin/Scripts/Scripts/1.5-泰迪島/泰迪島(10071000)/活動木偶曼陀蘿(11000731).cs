using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:活動木偶曼陀蘿(11000731) X:170 Y:49
namespace SagaScript.M10071000
{
    public class S11000731 : Event
    {
        public S11000731()
        {
            this.EventID = 11000731;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Say(pc, 11000731, 131, "您好$R;" +
                "我是活動木偶曼陀蘿喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶曼陀蘿打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000731, 131, "我的身體由植物做成，$R是擁有大地力量的活動木偶唷$R;" +
                "$P變成我$R可以使用『尖叫』的技能，$R讓周圍的魔物造成混亂。$R;" +
                "$P還有變身期間『MP』會自然恢復呀$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶曼陀蘿打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000731, 131, "怎麼樣？$R試試變成我嗎？$R;");
            switch (Select(pc, "怎麼辦呢？", "", "我想變身", "放棄"))
            {
                case 1:
                    ActivateMarionette(pc, 10019200);
                    ShowEffect(pc, 8015);
                    Heal(pc);
                    break;

                case 2:
                    break;
            }
        }
    }
}