using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東方海角(10018003) NPC基本信息:大地之精靈(11001432) X:43 Y:95
namespace SagaScript.M10018003
{
    public class S11001432 : Event
    {
        public S11001432()
        {
            this.EventID = 11001432;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;

            Say(pc, 11001432, 131, "您好!$R;" +
                                   "$R我是尼奧，$R;" +
                                   "是大地之精靈喔!$R;" +
                                   "$P有什麼事嗎?$R;", "大地之精靈");

            selection = Select(pc, "想問什麼呢?", "", "什麼叫「精靈」", "您在做什麼?", "什麼也不問");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11001432, 131, "是這樣嗎?$R;" +
                                               "$R我是掌管「土」之力的精靈。$R;" +
                                               "$P知道「土屬性」嗎?$R;" +
                                               "$P這個世界存在著各種「屬性」。$R;" +
                                               "$P有火/水/風/土$R;" +
                                               "還有光和闇屬性唷!$R;" +
                                               "$P屬性大部分都突顯在地區。$R;" +
                                               "$R偶爾也在魔物、武器或防具上。$R;" +
                                               "$P炎熱的地方$R;" +
                                               "「火」屬性較強，$R;" +
                                               "$R寒冷的地方$R;" +
                                               "「水」屬性較強$R;" +
                                               "$P颳風的地方$R;" +
                                               "「風」屬性較強，$R;" +
                                               "$R綠蔭的地方$R;" +
                                               "「土」屬性較強。$R;" +
                                               "$P它們之間沒有力量的相互關係。$R;" +
                                               "$R但是各屬性有專門的職業，$R;" +
                                               "你直接去問他們吧。$R;", "大地之精靈");
                        break;

                    case 2:
                        Say(pc, 11001432, 131, "掌管屬性的精靈周圍，$R;" +
                                               "會有相同屬性的「水晶」唷!$R;" +
                                               "$P下次來的時候找找看吧?$R;" +
                                               "$P可以利用從水晶裡，$R;" +
                                               "抽取出來的「召喚石」，$R;" +
                                               "來灌注「某種」屬性唷!$R;" +
                                               "$P需要的話，歡迎隨時過來。$R;" +
                                               "$P掌管其他屬性的精靈，$R;" +
                                               "也擁有同樣的能力喔!$R;", "大地之精靈");
                        break;

                    case 3:
                        Say(pc, 11001432, 131, "是嗎?$R;" +
                                               "需要的話，隨時過來吧!$R;", "大地之精靈");
                        return;
                }

                selection = Select(pc, "想問什麼呢?", "", "什麼叫「精靈」", "您在做什麼?", "什麼也不問");
            }
        }
    }
}
