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
                "我是活动木偶曼陀萝喔$R;");

            if (Tinyis_Land_01_mask.Test(Tinyis_Land_01.跟活動木偶曼陀蘿打完招呼))
            {
                招呼完毕继续变身(pc);
                return;
            }

            Say(pc, 11000731, 131, "我的身体由植物做成，$R是拥有大地力量的活动木偶哦$R;" +
                "$P变成我$R可以使用『尖叫』的技能，$R让周围的魔物造成混乱。$R;" +
                "$P还有变身期间『MP』会自然恢复呀$R;");
            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.跟活動木偶曼陀蘿打完招呼, true);
            招呼完毕继续变身(pc);
        }

        void 招呼完毕继续变身(ActorPC pc)
        {
            Say(pc, 11000731, 131, "怎么样？$R试试变成我吗？$R;");
            switch (Select(pc, "怎么办呢？", "", "我想变身", "放弃"))
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