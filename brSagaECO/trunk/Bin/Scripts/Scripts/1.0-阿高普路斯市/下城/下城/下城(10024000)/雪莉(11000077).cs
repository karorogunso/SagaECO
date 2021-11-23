using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:雪莉(11000077) X:136 Y:133
namespace SagaScript.M10024000
{
    public class S11000077 : Event
    {
        public S11000077()
        {
            this.EventID = 11000077;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            if (JobBasic_06_mask.Test(JobBasic_06.選擇轉職為元素使))
            {
                元素使轉職任務(pc);
                return;
            }

            ShowEffect(pc, 11000077, 9923);

            Say(pc, 11000077, 131, "呼呼呼，嚇一跳啊?$R;", "雪莉");

            Say(pc, 11000077, 131, "這個叫『婚禮花球』，$R;" +
                                   "很漂亮吧?$R;" +
                                   "$R有『花』和『剪刀』的話，$R" +
                                   "就可以透過「精製道具」製作的。", "雪莉");
        }

        void 元素使轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            int selection;

            if (!JobBasic_06_mask.Test(JobBasic_06.已經從雪莉那裡聽取有關屬性魔法的知識))
            {
                Say(pc, 11000077, 131, pc.Name + "?$R;" +
                                       "$R我從總管那裡聽說過了，$R;" +
                                       "您想學習「屬性魔法」是吧?$R;", "雪莉");

                switch (Select(pc, "想瞭解關於「屬性魔法」嗎?", "", "我想知道", "不怎麼想…"))
                {
                    case 1:
                        JobBasic_06_mask.SetValue(JobBasic_06.已經從雪莉那裡聽取有關屬性魔法的知識, true);  
                        break;

                    case 2:
                        return;
                }
            }
            else
            {
                Say(pc, 11000077, 131, "又來了?$R;", "雪莉");
            }

            Say(pc, 11000077, 131, "讓我介紹一下「屬性魔法」吧!$R;", "雪莉");

            selection = Select(pc, "想知道什麼呢?", "", "關於「屬性魔法", "有哪幾種屬性?", "我想知道關於光和闇屬性!", "有什麼魔法?", "不用了");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000077, 131, "應該從總管那裡聽說了吧?$R;" +
                                               "$R『元素使』利用的是火/水/風/土$R;" +
                                               "4種精靈的力量。$R;" +
                                               "$P這是從古代傳下來的魔法，$R;" +
                                               "從古代開始一直都是研究對像。$R;" +
                                               "$R所以到現在，有很多東西都發現了。$R;" +
                                               "$P雖然要把這些都放進腦袋裡，$R;" +
                                               "是有比較困難。$R;" +
                                               "$R但是這些內容都很重要，$R;" +
                                               "所以不能忘記啊!$R;" +
                                               "$P直接現在為止，$R;" +
                                               "研究發現屬性中有強弱關係。$R;" +
                                               "$P火比水強，但比風弱。$R;" +
                                               "風比火強，但比土弱。$R;" +
                                               "土比風強，但比水弱。$R;" +
                                               "水比土強，但比火弱。$R;" +
                                               "$P強弱關係就像鐵鏈一樣連起來，$R;" +
                                               "形成了完美的平衡。$R;" +
                                               "$P我們『元素使』不停的磨練，$R;" +
                                               "一眼就看破平衡的能力，$R;" +
                                               "自然就會比別人還強了。$R;", "雪莉");
                        break;

                    case 2:
                        Say(pc, 11000077, 131, "屬性在大地上也看的出來。$R;" +
                                               "$P在熱的地上，火之力強。$R;" +
                                               "在冷的地上，水之力強。$R;" +
                                               "$R在颳風的地方，風之力強。$R;" +
                                               "在森林，土之力強，$R;" +
                                               "$P此外，有時候魔物也有屬性的。$R;" +
                                               "$P火之力強的魔物，$R;" +
                                               "大多出現在火之力強的地方。$R;" +
                                               "$P但也不一定全都那樣，$R;" +
                                               "所以只能當作參考吧!$R;", "雪莉");
                        break;

                    case 3:
                        Say(pc, 11000077, 131, "『光』吞滅『闇』，$R;" +
                                               "『闇』吞滅『一切』，$R;" +
                                               "『一切』又能把『光』吞滅。$R;" +
                                               "$R這說明了光、闇和$R;" +
                                               "4種屬性的力量關係。$R;" +
                                               "$P基本上，$R;" +
                                               "所有屬性全部都一樣的。$R;" +
                                               "$P只是區分操作而已。$R;" +
                                               "$R那樣的話可以操作多種屬性的$R;" +
                                               "『元素使』不是比較好呢?$R;", "雪莉");
                        break;

                    case 4:
                        Say(pc, 11000077, 131, "告訴您幾個具代表性的魔法吧!$R;" +
                                               "$P火的攻擊魔法『紅蓮焰』。$R;" +
                                               "$P水的攻擊魔法『寒冰箭』。$R;" +
                                               "$P風的攻擊魔法『狂風之刃』。$R;" +
                                               "$P土的攻擊魔法『大地震盪』。$R;" +
                                               "$P給武器注入屬性力量的$R;" +
                                               "『武器系列』的魔法。$R;" +
                                               "$P讓您知道周圍屬性的『屬性調查』。$R;" +
                                               "$P除了以上這些，$R;" +
                                               "還有很多不同的魔法呢!$R;", "雪莉");
                        break;
                }

                selection = Select(pc, "想知道什麼呢?", "", "關於「屬性魔法", "有哪幾種屬性?", "我想知道關於光和闇屬性!", "有什麼魔法?", "不用了");
            }

            Say(pc, 11000077, 131, "再來啊!$R;", "雪莉");
        }
    }
}
