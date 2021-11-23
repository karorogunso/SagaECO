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

            Say(pc, 11000077, 131, "呼呼呼，吓一跳啊?$R;", "夏莉");

            Say(pc, 11000077, 131, "这个叫『婚礼花球』，$R;" +
                                   "很漂亮吧?$R;" +
                                   "$R有『花』和『剪刀』的话，$R" +
                                   "就可以透过「精制道具」制作的。", "夏莉");
        }

        void 元素使轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            int selection;

            if (!JobBasic_06_mask.Test(JobBasic_06.已經從雪莉那裡聽取有關屬性魔法的知識))
            {
                Say(pc, 11000077, 131, pc.Name + "?$R;" +
                                       "$R我从总管那裡听说过了，$R;" +
                                       "您想学习「属性魔法」是吧?$R;", "夏莉");

                switch (Select(pc, "想了解关于「属性魔法」吗?", "", "我想知道", "不怎么想…"))
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
                Say(pc, 11000077, 131, "又来了?$R;", "夏莉");
            }

            Say(pc, 11000077, 131, "让我介绍一下「属性魔法」吧!$R;", "夏莉");

            selection = Select(pc, "想知道什么呢?", "", "关于「属性魔法", "有哪几种属性?", "我想知道关于光和暗属性!", "有什么魔法?", "不用了");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000077, 131, "应该从总管那里听说了吧?$R;" +
                                               "$R『元素使』利用的是火/水/风/地$R;" +
                                               "4种精灵的力量。$R;" +
                                               "$P这是从古代传下来的魔法，$R;" +
                                               "从古代开始一直都是研究对像。$R;" +
                                               "$R所以到现在，有很多东西都发现了。$R;" +
                                               "$P虽然要把这些都放进脑袋里，$R;" +
                                               "是有比较困难。$R;" +
                                               "$R但是这些内容都很重要，$R;" +
                                               "所以不能忘记啊!$R;" +
                                               "$P直接现在为止，$R;" +
                                               "研究发现属性中有强弱关系。$R;" +
                                               "$P火比水强，但比风弱。$R;" +
                                               "风比火强，但比地弱。$R;" +
                                               "地比风强，但比水弱。$R;" +
                                               "水比地强，但比火弱。$R;" +
                                               "$P强弱关系就像铁链一样连起来，$R;" +
                                               "形成了完美的平衡。$R;" +
                                               "$P我们『元素使』不停的磨练，$R;" +
                                               "一眼就看破平衡的能力，$R;" +
                                               "自然就会比别人还强了。$R;", "夏莉");
                        break;

                    case 2:
                        Say(pc, 11000077, 131, "属性在大地上也看的出来。$R;" +
                                               "$P在热的地上，火之力强。$R;" +
                                               "在冷的地上，水之力强。$R;" +
                                               "$R在刮风的地方，风之力强。$R;" +
                                               "在森林，地之力强，$R;" +
                                               "$P此外，有时候魔物也有属性的。$R;" +
                                               "$P火之力强的魔物，$R;" +
                                               "大多出现在火之力强的地方。$R;" +
                                               "$P但也不一定全都那样，$R;" +
                                               "所以只能当作参考吧!$R;", "夏莉");
                        break;

                    case 3:
                        Say(pc, 11000077, 131, "『光』吞灭『暗』，$R;" +
                                               "『暗』吞灭『一切』，$R;" +
                                               "『一切』又能把『光』吞灭。$R;" +
                                               "$R这说明了光、暗和$R;" +
                                               "4种属性的力量关系。$R;" +
                                               "$P基本上，$R;" +
                                               "所有属性全部都一样的。$R;" +
                                               "$P只是区分操作而已。$R;" +
                                               "$R那样的话可以操作多种属性的$R;" +
                                               "『元素使』不是比较好呢?$R;", "夏莉");
                        break;

                    case 4:
                        Say(pc, 11000077, 131, "告诉您几个具代表性的魔法吧!$R;" +
                                               "$P火的攻击魔法『火球术』。$R;" +
                                               "$P水的攻击魔法『冰箭术』。$R;" +
                                               "$P风的攻击魔法『落雷术』。$R;" +
                                               "$P土的攻击魔法『大地震荡』。$R;" +
                                               "$P给武器注入属性力量的$R;" +
                                               "『武器系列』的魔法。$R;" +
                                               "$P让您知道周围属性的『属性调查』。$R;" +
                                               "$P除了以上这些，$R;" +
                                               "还有很多不同的魔法呢!$R;", "夏莉");
                        break;
                }

                selection = Select(pc, "想知道什么呢?", "", "关于「属性魔法", "有哪几种属性?", "我想知道关于光和暗属性!", "有什么魔法?", "不用了");
            }

            Say(pc, 11000077, 131, "再来啊!$R;", "夏莉");
        }
    }
}
