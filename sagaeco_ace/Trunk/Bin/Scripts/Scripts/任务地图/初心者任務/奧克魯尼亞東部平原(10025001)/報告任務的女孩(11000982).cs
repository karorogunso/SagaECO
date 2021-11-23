using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:報告任務的女孩(11000982) X:103 Y:133
namespace SagaScript.M10025001
{
    public class S11000982 : Event
    {
        public S11000982()
        {
            this.EventID = 11000982;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.轉交感謝信任務完成))
            {
                轉交感謝信任務(pc);
                return;
            }

            Say(pc, 11000982, 131, "过了这路端的桥，$R;" +
                                   "就是在大陆中心的「阿克罗波利斯」喔!$R;" +
                                   "$R那么，加油哦~!!$R;", "报告任务的女孩");            
        }

        void 轉交感謝信任務(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.轉交感謝信任務開始))
            {
                Say(pc, 11000982, 131, "初心者 您好~!$R;" +
                                       "$R嗯? 我吗?$R;" +
                                       "我是为了报告「任务」情况，$R;" +
                                       "来酒馆分店的。$R;" +
                                       "$P「任务」是指，$R;" +
                                       "在 ECO世界里发生的事件。$R;" +
                                       "$R详细的去问酒馆店员吧?$R;" +
                                       "指导结束后去问一下吧!$R;" +
                                       "$P哎，真是的。$R;" +
                                       "$R虽然不是「任务」…$R;" +
                                       "但可以拜托您一件事吗?$R;" +
                                       "$P有东西想要交给，$R;" +
                                       "那边的 「初心者向导」啊!$R;", "报告任务的女孩");

                switch (Select(pc, "怎么做呢?", "", "好的", "因为有点忙…"))
                {
                    case 1:
                        Beginner_02_mask.SetValue(Beginner_02.轉交感謝信任務開始, true);

                        Say(pc, 11000982, 131, "真的?$R;" +
                                               "$R哇啊! 非常感谢!!$R;" +
                                               "$P这封信啊…$R;" +
                                               "$R一想到要给他就有点害羞呢!$R;" +
                                               "$P因为他帮了我很多，$R;" +
                                               "所以想给他感谢信。可是…$R;" +
                                               "$R哇! 脸又红了!$R;" +
                                               "求求您! 把这个交给他吧!!$R;", "报告任务的女孩");

                        PlaySound(pc, 2040, false, 100, 50);
                        GiveItem(pc, 10043190, 1);
                        Say(pc, 0, 0, "收到了『感谢信』！$R;", " ");
                        return;

                    case 2:
                        Say(pc, 11000982, 131, "是吗? 那样的话，就没办法了。$R;" +
                                               "$P过了这路端的桥，$R;" +
                                               "就是「阿克罗波利斯」了。$R;" +
                                               "$R那么，加油喔~!!$R;", "报告任务的女孩");
                        return;
                }
            }

            if (Beginner_02_mask.Test(Beginner_02.已經把信轉交給初心者嚮導))
            {
                Beginner_02_mask.SetValue(Beginner_02.轉交感謝信任務完成, true);

                Say(pc, 11000982, 131, "啊? 送给他了!?$R;", "报告任务的女孩");

                Say(pc, 0, 0, "要告诉她已经把信交给他了，$R;" +
                              "还有从「初心者向导」那带来了口讯。$R;", " ");

                Say(pc, 11000982, 131, "他说很开心吗? 那太好了!!$R;" +
                                       "$R真的非常感谢，$R;" +
                                       "啊…给您这个作为谢礼吧!$R;", "报告任务的女孩");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 10043302, 1);
                Say(pc, 0, 0, "得到『鸡蛋三明治』!$R;", " ");

                Say(pc, 11000982, 131, "本来想晚一点吃才带来的，$R;" +
                                       "不知道为什么，心总是噗噗地跳。$R;" +
                                       "$R…对不起、对不起!$R;" +
                                       "说这些很没趣吧?$R;" +
                                       "$P我现在拜托您的事情，$R;" +
                                       "就像「任务」那样。$R;" +
                                       "$R「任务」中$R;" +
                                       "有「击退任务」、$R;" +
                                       "「采集任务」、$R;" +
                                       "「搬运任务」这三种哦!$R;" +
                                       "$P刚刚送信跟「搬运任务」比较相似。$R;" +
                                       "$R简单来说，就是做 「跑腿」的。$R;" +
                                       "$P关于「任务」的详细说明，$R;" +
                                       "到酒馆店员那力听听吧?$R;" +
                                       "$R我也听过店员的说明，$R;" +
                                       "讲的很简单很容易明白哦!$R;" +
                                       "$P过了这路端的桥，$R;" +
                                       "就是「阿克罗波利斯」了!$R;" +
                                       "$R那么，加油喔~!!$R;", "报告任务的女孩");
            }
            else
            {
                Say(pc, 11000982, 131, "「初心者向导」$R;" +
                                       "就在后方学校的前面呀!$R;" +
                                       "$R把这信带过去吧! 拜托了!$R;", "报告任务的女孩");
            }
        }
    }
}
