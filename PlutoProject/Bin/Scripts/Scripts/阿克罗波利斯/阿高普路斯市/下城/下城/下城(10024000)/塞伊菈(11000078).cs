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

            Say(pc, 11000078, 131, "……(沉思中)$R;", "塞伊菈");
        }

        void 祭司轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            int selection;

            if (!JobBasic_07_mask.Test(JobBasic_07.已經從菲爾那裡聽取有關治療魔法的知識))
            {
                Say(pc, 11000078, 131, pc.Name + "是你吧?$R;" +
                                       "$R我从「白之圣堂祭司」那边听说了，$R;" +
                                       "听说您想进一步了解「治疗魔法」，$R;" +
                                       "是吗?$R;", "塞伊菈");

                switch (Select(pc, "想了解关于「治疗魔法」吗?", "", "我想知道", "不怎么想…"))
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
                Say(pc, 11000078, 131, "还有不了解的地方?$R;", "塞伊菈");
            }

            Say(pc, 11000078, 131, "让我说明一下关于「治疗魔法」吧!$R;", "塞伊菈");

            selection = Select(pc, "想知道什么呢?", "", "关于「治疗魔法」", "成为祭司有什么好处?", "关于「属性」", "「属性」会在哪里?", "有什么魔法?", "不用了");

            while (selection != 6) 
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000078, 131, "虽然在广义上叫「治疗魔法」，$R;" +
                                               "其实正式名称是「属性魔法」$R;" +
                                               "$P「属性魔法」是火/水/光/暗等，$R;" +
                                               "利用其中一种精灵力量的魔法。$R;" +
                                               "$P在「治疗魔法」中，$R;" +
                                               "也有利用「光」精灵的力量。$R;" +
                                               "$P「光属性」魔法中有很多像$R;" +
                                               "『恢复』一样具有治疗作用的魔法。$R;" +
                                               "所以通常叫「治疗魔法」。$R;", "塞伊菈");
                        break;

                    case 2:
                        Say(pc, 11000078, 131, "只有我们『祭司』$R;" +
                                               "才能使用「治疗魔法」。$R;" +
                                               "$P如果要治疗其他职业玩家的伤亡，$R;" +
                                               "就需要更多的力量。$R;" +
                                               "$P如果您觉得治疗别人很有意义。$R;" +
                                               "$R慈祥又热情的话，不如当『祭司』吧!$R;" +
                                               "$P跟自己一起冒险的人幸福，$R;" +
                                               "$R就是祭司的幸福。$R;", "塞伊菈");
                        break;

                    case 3:
                        Say(pc, 11000078, 131, "属性有『火/水/风/土/暗』$R;" +
                                               "还有我们使用的『光』，一共分成6种。$R;" +
                                               "$P在属性之间也有强弱关系，$R;" +
                                               "古语有云……$R;" +
                                               "$P『光』会吞灭『暗』，$R;" +
                                               "『暗』会吞掉『一切』。$R;" +
                                               "$P『一切』又会吞灭『光』。$R;" +
                                               "$P这裡说的『一切』就是指$R;" +
                                               "火/水/风/地，4种属性。$R;" +
                                               "$P4种属性虽然有强弱关系，$R;" +
                                               "但是跟「光属性」无关，$R;" +
                                               "所以说明就省略了。$R;", "塞伊菈");
                        break;

                    case 4:
                        Say(pc, 11000078, 131, "$P属性大部分都突显在地区。$R;" +
                                               "$R偶尔也在魔物、武器或防具上。$R;" +
                                               "$P炎热的地方$R;" +
                                               "「火」属性较强，$R;" +
                                               "$R寒冷的地方$R;" +
                                               "「水」属性较强$R;" +
                                               "$P刮风的地方$R;" +
                                               "「风」属性较强，$R;" +
                                               "$R绿荫的地方$R;" +
                                               "「地」属性较强。$R;" +
                                               "$P光之力最强的地方，$R;" +
                                               "是光之精灵的周边。$R;" +
                                               "其他的地方就不知道了。$R;" +
                                               "$P能找到那样的地方就好了!!$R;", "塞伊菈");
                        break;

                    case 5:
                        Say(pc, 11000078, 131, "告诉您几个具代表性的魔法吧!$R;" +
                                               "$P首先治愈HP的『恢复』!$R;" +
                                               "$P「光属性」的攻击魔法『神圣光珠』!$R;" +
                                               "$P恢复状态的『治疗系列』和$R;" +
                                               "提高对抗耐性的『抵抗系列』!$R;" +
                                               "$P还有把临死的肉体恢复到$R;" +
                                               "原来模样的『复活』等。$R;", "塞伊菈");
                        break;
                }

                selection = Select(pc, "想知道什么呢?", "", "关于「治疗魔法」", "成为祭司有什么好处?", "关于「属性」", "「属性」会在哪里?", "有什么魔法?", "不用了");
            }

            Say(pc, 11000078, 131, "下次再来啊~!$R;", "塞伊菈");
        }
    }
}
