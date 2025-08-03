using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000391 : Event
    {
        public S11000391()
        {
            this.EventID = 11000391;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JJDFlags> mask = new BitMask<JJDFlags>(pc.CMask["JJD"]);

            if (pc.Level > 44)
            {
                if (pc.Job == PC_JOB.RANGER || pc.Job == PC_JOB.EXPLORER || pc.Job == PC_JOB.TREASUREHUNTER)
                {
                    if (mask.Test(JJDFlags.杰利科收集任务结束))
                    {
                        Say(pc, 112, "……$R;" +
                            "$R嗯……$R;" +
                            "要用什么颜色呢……$R;");
                        return;
                    }
                    if (mask.Test(JJDFlags.杰利科收集任务开始) && mask.Test(JJDFlags.给予杰利科))
                    {
                        if (pc.Gender == PC_GENDER.FEMALE)
                        {
                            if (CheckInventory(pc, 50062850, 1))
                            {
                                mask.SetValue(JJDFlags.杰利科收集任务结束, true);
                                GiveItem(pc, 50062850, 1);
                                Say(pc, 131, "来！这是表达感谢的礼物$R;" +
                                    "相当好的皮鞋啊!!$R;");
                                return;
                            }
                            Say(pc, 131, "为了表达我的谢意$R;" +
                                "去把东西减少后再来吧$R;");
                            return;
                        }
                        if (CheckInventory(pc, 50013350, 1))
                        {
                            mask.SetValue(JJDFlags.杰利科收集任务结束, true);
                            GiveItem(pc, 50013350, 1);
                            Say(pc, 131, "来！这是表达感谢的礼物$R;" +
                                "相当好的皮鞋啊!!$R;");
                            return;
                        }
                        Say(pc, 131, "为了表达我的谢意$R;" +
                            "去把东西减少后再来吧$R;");
                        return;
                    }
                    if (mask.Test(JJDFlags.杰利科收集任务开始) && !mask.Test(JJDFlags.给予杰利科))
                    {
                        if (CountItem(pc, 10032800) >= 1
                            && CountItem(pc, 10032801) >= 1
                            && CountItem(pc, 10032802) >= 1
                            && CountItem(pc, 10032803) >= 1
                            && CountItem(pc, 10032804) >= 1
                            && CountItem(pc, 10032805) >= 1
                            && CountItem(pc, 10032806) >= 1
                            && CountItem(pc, 10032807) >= 1
                            && CountItem(pc, 10032808) >= 1
                            && CountItem(pc, 10032809) >= 1
                            && CountItem(pc, 10032810) >= 1
                            && CountItem(pc, 10032811) >= 1)
                        {
                            mask.SetValue(JJDFlags.给予杰利科, true);
                            TakeItem(pc, 10032800, 1);
                            TakeItem(pc, 10032801, 1);
                            TakeItem(pc, 10032802, 1);
                            TakeItem(pc, 10032803, 1);
                            TakeItem(pc, 10032804, 1);
                            TakeItem(pc, 10032805, 1);
                            TakeItem(pc, 10032806, 1);
                            TakeItem(pc, 10032807, 1);
                            TakeItem(pc, 10032808, 1);
                            TakeItem(pc, 10032809, 1);
                            TakeItem(pc, 10032810, 1);
                            TakeItem(pc, 10032811, 1);
                            Say(pc, 131, "哦哦!!$R;" +
                                "真的有12种那么多的杰利科吗!$R;" +
                                "$R这个真是谢谢啊!$R;");
                            Say(pc, 131, "给了他收集的杰利科$R;");
                            if (pc.Gender == PC_GENDER.FEMALE)
                            {
                                if (CheckInventory(pc, 50062850, 1))
                                {

                                    mask.SetValue(JJDFlags.杰利科收集任务结束, true);
                                    GiveItem(pc, 50062850, 1);
                                    Say(pc, 131, "来！这是表达感谢的礼物$R;" +
                                        "相当好的皮鞋啊!!$R;");
                                    return;
                                }
                                Say(pc, 131, "为了表达我的谢意$R;" +
                                    "去把东西减少后再来吧$R;");
                                return;
                            }
                            if (CheckInventory(pc, 50013350, 1))
                            {

                                mask.SetValue(JJDFlags.杰利科收集任务结束, true);
                                GiveItem(pc, 50013350, 1);
                                Say(pc, 131, "来！这是表达感谢的礼物$R;" +
                                    "相当好的皮鞋啊!!$R;");
                                return;
                            }
                            Say(pc, 131, "为了表达我的谢意$R;" +
                                "去把东西减少后再来吧$R;");
                            return;
                        }
                        Say(pc, 131, "虽然现在只是传言$R;" +
                            "杰利科在这个世界上听説有12种呢$R;" +
                            "$R再多努力一点找找看吧$R;");
                        return;
                    }
                    Say(pc, 11000391, 112, "……$R;" +
                        "$R嗯……真的这个颜色没关系吗?$R;");
                    Say(pc, 11000392, 132, "我觉得蓝色比天蓝色更适合女生?$R;");
                    Say(pc, 11000393, 131, "黄色已经厌倦了…$R;" +
                        "草绿色怎么样?$R;");
                    Say(pc, 11000394, 131, "那个不是跟绿色差不多吗$R;");
                    Say(pc, 11000393, 112, "虽然那样$R;" +
                        "但是黄色的形象有点…$R;");
                    Say(pc, 11000395, 132, "啊，对，对!黄色是有点那个…$R;" +
                        "$R喜欢的人虽然喜欢$R;");
                    Say(pc, 11000391, 131, "稍等$R;" +
                        "$R都有什么颜色$R;" +
                        "你到底知不知道啊?$R;");
                    Say(pc, 11000394, 131, "也对，杰利科的材料是吧?$R;" +
                        "$R虽然见过黄色和蓝色$R;" +
                        "全部的话我就不知道了$R;");
                    Say(pc, 11000392, 131, "嗯…不知道有什么颜色的话$R;" +
                        "选择什么颜色好就困难了$R;");
                    Say(pc, 11000395, 132, "嗯…是啊$R;");
                    Say(pc, 11000391, 131, "好的…首先$R;" +
                        "开始收集杰利科的事情吧$R;" +
                        "$R各自收集自己知道的杰利科!$R;");
                    Say(pc, 11000393, 131, "好!!$R;");
                    Say(pc, 11000391, 131, "你也要协助吗?不是强制性的…$R;" +
                        "$R当然会有答谢的$R;");
                    switch (Select(pc, "怎么做呢？", "", "拒绝!", "好"))
                    {
                        case 1:
                            break;
                        case 2:
                            mask.SetValue(JJDFlags.杰利科收集任务开始, true);
                            Say(pc, 131, "好!那就拜托了!$R;" +
                                "把世界上的杰利科全都收集來吧!$R;");
                            break;
                    }
                    return;
                }
            }

            Say(pc, 11000391, 131, "我们是把强烈颜色$R;" +
                "标榜突击的军团队员$R;");
            Say(pc, 11000391, 140, "热血，队长，红怀特$R;");
            Say(pc, 11000392, 141, "一点红的蓝布$R;");
            Say(pc, 11000393, 140, "有意思的黄耶劳$R;");
            Say(pc, 11000394, 140, "天下壮士绿杰$R;");
            Say(pc, 11000395, 140, "孤单的狼黑百特!$R;");
            Say(pc, 11000391, 131, "5个聚在一起颜色丰富的突击队员!$R;");
        }
    }
}
