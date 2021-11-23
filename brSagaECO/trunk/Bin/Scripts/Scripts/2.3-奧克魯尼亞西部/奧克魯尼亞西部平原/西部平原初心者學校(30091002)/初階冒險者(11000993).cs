using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:西部平原初心者學校(30091002) NPC基本信息:初階冒險者(11000993) X:8 Y:9
namespace SagaScript.M30091002
{
    public class S11000993 : Event
    {
        public S11000993()
        {
            this.EventID = 11000993;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.已經與男初階冒險者進行第一次對話))
            {
                初次與初階冒險者進行對話(pc);
                return;
            }

            Say(pc, 11000993, 0, "啊，您好!$R;", "初階冒險者");
        }

        void 初次與初階冒險者進行對話(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.已經與男初階冒險者進行第一次對話, true);

            Say(pc, 11000993, 0, "啊，您好!$R;" +
                                 "$R您也是第一次來「阿高普路斯市」嗎?$R;" +
                                 "$P我也是剛到沒多久，$R;" +
                                 "所以在聽老師說明呢!$R;" +
                                 "$R就像老師說的，$R;" +
                                 "我們這些來到「阿高普路斯市」$R;" +
                                 "沒多久的新手，$R;" +
                                 "最好向冒險者前輩們，$R;" +
                                 "請假一下冒險的方法。$R;" +
                                 "$P您如果有不知道的問題，$R;" +
                                 "就問問老師吧?$R;", "初階冒險者");

            switch (Select(pc, "要繼續聊天嗎?", "", "您的衣服很好看啊!", "…好了"))
            {
                case 1:
                    Say(pc, 11000993, 0, "啊，是嗎?$R;" +
                                         "謝謝稱讚呀!$R;" +
                                         "$P這是在「泰迪島」買的。$R;" +
                                         "$R嗯? …「泰迪島」是什麼?$R;" +
                                         "$P事實上…雖然無法相信$R;" +
                                         "但是在「上城」那$R;" +
                                         "有個泰迪娃娃跟我搭話。$R;" +
                                         "$R回答後，就變得精神恍惚，$R;" +
                                         "一醒來就發現自己在南邊的島上。$R;" +
                                         "$P是真的! 不是做夢啊!$R;" +
                                         "$R這衣服就是在那島上的商人那買的!$R;" +
                                         "$P那時候雖然沒什麼錢。$R;" +
                                         "$R但我按照那商人說的做，$R;" +
                                         "把島上遍布的岩石和草叢裡，$R;" +
                                         "獲得的道具賣給商人，$R;" +
                                         "賺到了一些錢。$R;" +
                                         "$P我就拿著那些錢回到城市裡，$R;" +
                                         "就可以買到各種物品了。$R;", "初階冒險者");
                    break;

                case 2:
                    break;
            }
        }
    }
}
