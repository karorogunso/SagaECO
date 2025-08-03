using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30170000
{
    public class S11000208 : Event
    {
        public S11000208()
        {
            this.EventID = 11000208;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<YGLFlags> mask = new BitMask<YGLFlags>(pc.CMask["YGL"]);
            if (pc.Level > 44)
            {
                if (pc.Job == PC_JOB.MERCHANT ||
                    pc.Job == PC_JOB.TRADER ||
                    pc.Job == PC_JOB.GAMBLER)
                {

                    if (mask.Test(YGLFlags.石像收集任务结束))
                    {
                        Say(pc, 131, "冰精灵！太漂亮了$R;");
                        return;
                    }
                    if (mask.Test(YGLFlags.给予石像))
                    {
                        if (pc.Gender == PC_GENDER.FEMALE)
                        {
                            if (CheckInventory(pc, 50013650, 1))
                            {
                                mask.SetValue(YGLFlags.石像收集任务结束, true);
                                GiveItem(pc, 50013650, 1);
                                Say(pc, 131, "拿报酬吧$R;" +
                                    "怎么样？满意吗？$R;");
                                return;
                            }
                            Say(pc, 131, "会给您报酬的$R;" +
                                "先把行李减少后，再来吧$R;");
                            return;
                        }
                        if (CheckInventory(pc, 50025750, 1))
                        {
                            mask.SetValue(YGLFlags.石像收集任务结束, true);
                            GiveItem(pc, 50025750, 1);
                            Say(pc, 131, "拿报酬吧$R;" +
                                "怎么样？满意吗？$R;");
                            return;
                        }
                        Say(pc, 131, "会给您报酬的$R;" +
                            "先把行李减少后，再来吧$R;");
                        return;
                    }
                    if (mask.Test(YGLFlags.石像收集任务开始))
                    {
                        if (CountItem(pc, 10011502) >= 1 && CountItem(pc, 10019201) >= 1 && CountItem(pc, 10019301) >= 1 && CountItem(pc, 10019401) >= 1 && CountItem(pc, 10022001) >= 1 && CountItem(pc, 10030004) >= 1)
                        {
                            mask.SetValue(YGLFlags.给予石像, true);
                            Say(pc, 131, "真的给我送来了！$R;" +
                                "哇……$R;" +
                                "让我看看…$R;");
                            Say(pc, 131, "给他看了石像$R;");
                            Say(pc, 131, "呀！冰精灵真漂亮，$R;" +
                                "那就这个好了$R;" +
                                "$R以我这么大年龄$R;" +
                                "有点不好意思呀$R;" +
                                "$P…$R;" +
                                "不管了……$R;" +
                                "$R好！就选这个了$R;" +
                                "选定石像冰精灵了！$R;");
                            if (pc.Gender == PC_GENDER.FEMALE)
                            {
                                if (CheckInventory(pc, 50013650, 1))
                                {
                                    mask.SetValue(YGLFlags.石像收集任务结束, true);
                                    GiveItem(pc, 50013650, 1);
                                    Say(pc, 131, "拿报酬吧$R;" +
                                        "怎么样？满意吗？$R;");
                                    return;
                                }
                                Say(pc, 131, "会给您报酬的$R;" +
                                    "先把行李减少后，再来吧$R;");
                                return;
                            }
                            if (CheckInventory(pc, 50025750, 1))
                            {
                                mask.SetValue(YGLFlags.石像收集任务结束, true);
                                GiveItem(pc, 50025750, 1);
                                Say(pc, 131, "拿报酬吧$R;" +
                                    "怎么样？满意吗？$R;");
                                return;
                            }
                            Say(pc, 131, "会给您报酬的$R;" +
                                "先把行李减少后，再来吧$R;");
                            return;
                        }
                        Say(pc, 131, "$P我想看的石像有$R;" +
                            "石像矿石精灵$R;" +
                            "石像曼陀罗$R;" +
                            "石像冰精灵$R;" +
                            "石像鱼人$R;" +
                            "石像泰迪$R;" +
                            "石像电路机械$R;" +
                            "等六个$R;" +
                            "$P火属性石像呢，$R;" +
                            "在寒冷的诺森召唤，好像很可怜$R;" +
                            "所以算了$R;" +
                            "$R那拜托您了$R;");
                        return;
                    }
                    Say(pc, 131, "看样子您是商人呀？$R;" +
                        "使用什么石像呢？$R;");
                    Say(pc, 131, "嗯$R;" +
                        "" + pc.Name + "使用哪一种石像呢？！$R;" +
                        "用过后怎么样呀？$R;" +
                        "想不想用别的石像呢？$R;" +
                        "$P虽然阿克罗波利斯有各种石像，$R;" +
                        "但我在这里有很多事情$R;" +
                        "不能随便离开诺森呀$R;" +
                        "$R您能不能拿各种石像，给我看呢？$R;" +
                        "$P当然我会给您报酬的$R;" +
                        "不是要石像，只是想看一看啦$R;" +
                        "$R看过后会还给您的$R;" +
                        "怎么样呢？$R;");
                    switch (Select(pc, "怎么办呢？", "", "不要", "知道了"))
                    {
                        case 1:
                            break;
                        case 2:
                            mask.SetValue(YGLFlags.石像收集任务开始, true);
                            Say(pc, 131, "谢谢！太谢谢了$R;" +
                                "$P我想看的石像有$R;" +
                                "石像矿石精灵$R;" +
                                "石像曼陀罗$R;" +
                                "石像冰精灵$R;" +
                                "石像鱼人$R;" +
                                "石像泰迪$R;" +
                                "石像电路机械$R;" +
                                "等六个$R;" +
                                "$P火属性石像呢，$R;" +
                                "在寒冷的诺森召唤，好像很可怜呀$R;" +
                                "所以算了$R;" +
                                "$R那拜托您了$R;");
                            break;
                    }
                    return;
                }
            }
            Say(pc, 131, "…！！$R您是不是？$R从阿克罗尼亚来的呀？$R;" +
                "$P难得您找到这偏僻的地方！$R冷不冷？$R赶快来暖和一下吧。$R;");
        }
    }
}
