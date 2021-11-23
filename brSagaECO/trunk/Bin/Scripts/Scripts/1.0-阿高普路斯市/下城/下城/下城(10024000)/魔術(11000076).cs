using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:魔術(11000076) X:122 Y:49
namespace SagaScript.M10024000
{
    public class S11000076 : Event
    {
        public S11000076()
        {
            this.EventID = 11000076;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);

            if (JobBasic_05_mask.Test(JobBasic_05.選擇轉職為魔法師))
            {
                魔法師轉職任務(pc);
                return;
            }

            Say(pc, 11000076, 131, "新生魔法的真諦…$R;" +
                                   "有誰瞭解呢?$R;", "魔術");
        }

        void 魔法師轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);

            int selection;

            if (!JobBasic_05_mask.Test(JobBasic_05.已經從魔術那裡聽取有關新生魔法的知識))
            {
                Say(pc, 11000076, 131, pc.Name + "對吧?$R;" +
                                       "$R我從總管那裡聽說過了，$R;" +
                                       "您想學習「新生魔法」是吧?$R;", "魔術");

                switch (Select(pc, "想瞭解關於「新生魔法」嗎?", "", "不怎麼想…", "不用了"))
                {
                    case 1:
                        JobBasic_05_mask.SetValue(JobBasic_05.已經從魔術那裡聽取有關新生魔法的知識, true); 
                        break;

                    case 2:
                        return;
                }
            }
            else
            {
                Say(pc, 11000076, 131, "還有不瞭解的地方?$R;", "魔術");
            }

            Say(pc, 11000076, 131, "讓我介紹一下「新生魔法」吧!$R;", "魔術");

            selection = Select(pc, "想知道什麼呢?", "", "關於「新生魔法」", "「新生魔法」跟「屬性魔法」有什麼不同?", "有什麼魔法?", "「新生魔法」名字的由來?", "不用了");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000076, 131, "這個應該從總管那裡聽說過吧?$R;" +
                                               "$R「新生魔法」是不需要火、水等$R;" +
                                               "屬性力量的新魔法!$R;" +
                                               "$P跟「屬性魔法」不同，$R;" +
                                               "是利用『次元的扭轉』中，$R;" +
                                               "所產生的能源。$R;" +
                                               "$R有關『次元的扭轉』，$R;" +
                                               "現在還沒有詳細的資料。$R;" +
                                               "$P雖然抱有疑問的人很多…$R;" +
                                               "塔妮亞和道米尼種族，$R;" +
                                               "一起生活過的「那個世界」…$R;" +
                                               "$R如果知道去「那個世界」的方法的話，$R;" +
                                               "就可以大規模擴展$R;" +
                                               "「新生魔法」的研究呢!$R;", "魔術");
                        break;

                    case 2:
                        Say(pc, 11000076, 131, "「新生魔法」是不會受屬性拘束的。$R;" +
                                               "這個應該從總管那裡聽說過吧?$R;" +
                                               "$R這個其實是什麼意思呢?$R;" +
                                               "讓我來替您做個解釋一下吧。$R;" +
                                               "$P屬性這東西有強弱關係。$R;" +
                                               "$R火比水強，但比風弱，$R;" +
                                               "風比火強，但比土弱。$R;" +
                                               "$P火之力強的地方，$R;" +
                                               "火之魔法就很強。$R;" +
                                               "但是水之魔法威力就接近0。$R;" +
                                               "$P所以只有水之魔法的話，$R;" +
                                               "在火之力強的地方戰鬥就特別困難。$R;" +
                                               "$P當然其他屬性情況也一樣。$R;" +
                                               "$P使用「屬性魔法」的人，$R;" +
                                               "在不同地方能力也會跟著不同。$R;" +
                                               "$R而且魔物也有屬性之分，$R;" +
                                               "所以要算好強弱關係才好!$R;" +
                                               "$P挺複雜吧?$R;" +
                                               "$R但是在「新生魔法」裡沒有這些麻煩，$R;" +
                                               "不管在什麼地方面對什麼魔物，$R;" +
                                               "魔法的效果都不會變的。$R;" +
                                               "$P我認為「新生魔法」這樣比較方便，$R;" +
                                               "可是每個人的想法都不同呢。$R;", "魔術");
                        break;

                    case 3:
                        Say(pc, 11000076, 131, "告訴您幾個具代表性的魔法吧!$R;" +
                                               "$P攻擊魔法的『魔法片』。$R;" +
                                               "$P以分身欺騙敵人的『魔法師的殘像』。$R;" +
                                               "$P保護身體，$R;" +
                                               "免受風雪、灼熱火花傷害的$R;" +
                                               "『溫暖的氣流』和『抗火護盾』。$R;" +
                                               "$P提高防禦力的『元靈護盾』。$R;" +
                                               "$P讓魔物中毒的『劇毒詛咒』。$R;" +
                                               "$P除了這些，還有很多呢!$R;", "魔術");
                        break;

                    case 4:
                        Say(pc, 11000076, 131, "「新生魔法」是最近才發現的，$R;" +
                                               "所以歷史比較短。$R;" +
                                               "$P為了跟「屬性魔法」區分，$R;" +
                                               "以新產生的魔法的意思命名的。$R;", "魔術");
                        break;
                }

                selection = Select(pc, "想知道什麼呢?", "", "關於「新生魔法」", "「新生魔法」跟「屬性魔法」有什麼不同?", "有什麼魔法?", "「新生魔法」名字的由來?", "不用了");
            } 

            Say(pc, 11000076, 131, "下次再來!$R;", "魔術");
        }
    }
}
