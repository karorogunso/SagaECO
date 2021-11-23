using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:菲爾(11000078) X:174 Y:76
namespace SagaScript.M10024000
{
    public class S11000078 : Event
    {
        public S11000078()
        {
            this.EventID = 11000078;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            if (JobBasic_07_mask.Test(JobBasic_07.選擇轉職為祭司))
            {
                祭司轉職任務(pc);
                return;
            }

            Say(pc, 11000078, 131, "……(沉思中)$R;", "菲爾");
        }

        void 祭司轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            int selection;

            if (!JobBasic_07_mask.Test(JobBasic_07.已經從菲爾那裡聽取有關治療魔法的知識))
            {
                Say(pc, 11000078, 131, pc.Name + "是你吧?$R;" +
                                       "$R我從「白之聖堂祭司」那邊聽說了，$R;" +
                                       "聽說您想進一步瞭解「治療魔法」，$R;" +
                                       "是嗎?$R;", "菲爾");

                switch (Select(pc, "想瞭解關於「治療魔法」嗎?", "", "我想知道", "不怎麼想…"))
                {
                    case 1:
                        JobBasic_07_mask.SetValue(JobBasic_07.已經從菲爾那裡聽取有關治療魔法的知識, true);
                        break;
                        
                    case 2:
                        return;
                }
            }
            else
            {
                Say(pc, 11000078, 131, "還有不瞭解的地方?$R;", "菲爾");
            }

            Say(pc, 11000078, 131, "讓我說明一下關於「治療魔法」吧!$R;", "菲爾");

            selection = Select(pc, "想知道什麼呢?", "", "關於「治療魔法」", "成為祭司有什麼好處?", "關於「屬性」", "「屬性」會在哪裡?", "有什麼魔法?", "不用了");

            while (selection != 6) 
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000078, 131, "雖然在廣義上叫「治療魔法」，$R;" +
                                               "其實正式名稱是「屬性魔法」$R;" +
                                               "$P「屬性魔法」是火/水/光/闇等，$R;" +
                                               "利用其中一種精靈力量的魔法。$R;" +
                                               "$P在「治療魔法」中，$R;" +
                                               "也有利用「光」精靈的力量。$R;" +
                                               "$P「光屬性」魔法中有很多像$R;" +
                                               "『恢復』一樣具有治療作用的魔法。$R;" +
                                               "所以通常叫「治療魔法」。$R;", "菲爾");
                        break;

                    case 2:
                        Say(pc, 11000078, 131, "只有我們『祭司』$R;" +
                                               "才能使用「治療魔法」。$R;" +
                                               "$P如果要治療其他職業玩家的傷亡，$R;" +
                                               "就需要更多的力量。$R;" +
                                               "$P如果您覺得治療別人很有意義。$R;" +
                                               "$R慈祥又熱情的話，不如當『祭司』吧!$R;" +
                                               "$P跟自己一起冒險的人幸福，$R;" +
                                               "$R就是祭司的幸福。$R;", "菲爾");
                        break;

                    case 3:
                        Say(pc, 11000078, 131, "屬性有『火/水/風/土/闇』$R;" +
                                               "還有我們使用的『光』，一共分成6種。$R;" +
                                               "$P在屬性之間也有強弱關係，$R;" +
                                               "古語有云……$R;" +
                                               "$P『光』會吞滅『闇』，$R;" +
                                               "『闇』會吞掉『一切』。$R;" +
                                               "$P『一切』又會吞滅『光』。$R;" +
                                               "$P這裡說的『一切』就是指$R;" +
                                               "火/水/風/土，4種屬性。$R;" +
                                               "$P4種屬性雖然有強弱關係，$R;" +
                                               "但是跟「光屬性」無關，$R;" +
                                               "所以說明就省略了。$R;", "菲爾");
                        break;

                    case 4:
                        Say(pc, 11000078, 131, "$P屬性大部分都突顯在地區。$R;" +
                                               "$R偶爾也在魔物、武器或防具上。$R;" +
                                               "$P炎熱的地方$R;" +
                                               "「火」屬性較強，$R;" +
                                               "$R寒冷的地方$R;" +
                                               "「水」屬性較強$R;" +
                                               "$P颳風的地方$R;" +
                                               "「風」屬性較強，$R;" +
                                               "$R綠蔭的地方$R;" +
                                               "「土」屬性較強。$R;" +
                                               "$P光之力最強的地方，$R;" +
                                               "是光之精靈的周邊。$R;" +
                                               "其他的地方就不知道了。$R;" +
                                               "$P能找到那樣的地方就好了!!$R;", "菲爾");
                        break;

                    case 5:
                        Say(pc, 11000078, 131, "告訴您幾個具代表性的魔法吧!$R;" +
                                               "$P首先治癒HP的『恢復』!$R;" +
                                               "$P「光屬性」的攻擊魔法『神聖光珠』!$R;" +
                                               "$P恢復狀態的『治療系列』和$R;" +
                                               "提高對抗耐性的『抵抗系列』!$R;" +
                                               "$P還有把臨死的肉體恢復到$R;" +
                                               "原來模樣的『復活』等。$R;", "菲爾");
                        break;
                }

                selection = Select(pc, "想知道什麼呢?", "", "關於「治療魔法」", "成為祭司有什麼好處?", "關於「屬性」", "「屬性」會在哪裡?", "有什麼魔法?", "不用了");
            }

            Say(pc, 11000078, 131, "下次再來啊~!$R;", "菲爾");
        }
    }
}
