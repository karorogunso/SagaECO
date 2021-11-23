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
                        Say(pc, 131, "愛伊斯！太漂亮了$R;");
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
                                Say(pc, 131, "拿報酬吧$R;" +
                                    "怎麼樣？滿意嗎？$R;");
                                return;
                            }
                            Say(pc, 131, "會給您報酬的$R;" +
                                "先把行李減少後，再來吧$R;");
                            return;
                        }
                        if (CheckInventory(pc, 50025750, 1))
                        {
                            mask.SetValue(YGLFlags.石像收集任务结束, true);
                            GiveItem(pc, 50025750, 1);
                            Say(pc, 131, "拿報酬吧$R;" +
                                "怎麼樣？滿意嗎？$R;");
                            return;
                        }
                        Say(pc, 131, "會給您報酬的$R;" +
                            "先把行李減少後，再來吧$R;");
                        return;
                    }
                    if (mask.Test(YGLFlags.石像收集任务开始))
                    {
                        if (CountItem(pc, 10011502) >= 1 && CountItem(pc, 10019201) >= 1 && CountItem(pc, 10019301) >= 1 && CountItem(pc, 10019401) >= 1 && CountItem(pc, 10022001) >= 1 && CountItem(pc, 10030004) >= 1)
                        {
                            mask.SetValue(YGLFlags.给予石像, true);
                            Say(pc, 131, "真的給我送來了！$R;" +
                                "哇……$R;" +
                                "讓我看看…$R;");
                            Say(pc, 131, "給他看了石像$R;");
                            Say(pc, 131, "呀！愛伊斯真漂亮，$R;" +
                                "那就這個好了$R;" +
                                "$R以我這麼大年齡$R;" +
                                "有點不好意思呀$R;" +
                                "$P…$R;" +
                                "不管了……$R;" +
                                "$R好！就選這個了$R;" +
                                "選定石像愛伊斯了！$R;");
                            if (pc.Gender == PC_GENDER.FEMALE)
                            {
                                if (CheckInventory(pc, 50013650, 1))
                                {
                                    mask.SetValue(YGLFlags.石像收集任务结束, true);
                                    GiveItem(pc, 50013650, 1);
                                    Say(pc, 131, "拿報酬吧$R;" +
                                        "怎麼樣？滿意嗎？$R;");
                                    return;
                                }
                                Say(pc, 131, "會給您報酬的$R;" +
                                    "先把行李減少後，再來吧$R;");
                                return;
                            }
                            if (CheckInventory(pc, 50025750, 1))
                            {
                                mask.SetValue(YGLFlags.石像收集任务结束, true);
                                GiveItem(pc, 50025750, 1);
                                Say(pc, 131, "拿報酬吧$R;" +
                                    "怎麼樣？滿意嗎？$R;");
                                return;
                            }
                            Say(pc, 131, "會給您報酬的$R;" +
                                "先把行李減少後，再來吧$R;");
                            return;
                        }
                        Say(pc, 131, "$P我想看的石像有$R;" +
                            "石像綠礦石精靈$R;" +
                            "石像曼陀蘿$R;" +
                            "石像愛伊斯$R;" +
                            "石像瑪歐斯$R;" +
                            "石像泰迪$R;" +
                            "石像塔依$R;" +
                            "等六個$R;" +
                            "$P火屬性石像呢，$R;" +
                            "在寒冷的諾頓召喚，好像很可憐$R;" +
                            "所以算了$R;" +
                            "$R那拜託您了$R;");
                        return;
                    }
                    Say(pc, 131, "看樣子您是商人呀？$R;" +
                        "使用什麼石像呢？$R;");
                    Say(pc, 131, "嗯$R;" +
                        "" + pc.Name + "使用哪一種石像呢？！$R;" +
                        "用過後怎麼樣呀？$R;" +
                        "想不想用別的石像呢？$R;" +
                        "$P雖然阿高普路斯有各種石像，$R;" +
                        "但我在這裡有很多事情$R;" +
                        "不能隨便離開諾頓呀$R;" +
                        "$R您能不能拿各種石像，給我看呢？$R;" +
                        "$P當然我會給您報酬的$R;" +
                        "不是要石像，只是想看一看啦$R;" +
                        "$R看過後會還給您的$R;" +
                        "怎麼樣呢？$R;");
                    switch (Select(pc, "怎麼辦呢？", "", "不要", "知道了"))
                    {
                        case 1:
                            break;
                        case 2:
                            mask.SetValue(YGLFlags.石像收集任务开始, true);
                            Say(pc, 131, "謝謝！太謝謝了$R;" +
                                "$P我想看的石像有$R;" +
                                "石像綠礦石精靈$R;" +
                                "石像曼陀蘿$R;" +
                                "石像愛伊斯$R;" +
                                "石像瑪歐斯$R;" +
                                "石像泰迪$R;" +
                                "石像塔依$R;" +
                                "等六個$R;" +
                                "$P火屬性石像呢，$R;" +
                                "在寒冷的諾頓召喚，好像很可憐呀$R;" +
                                "所以算了$R;" +
                                "$R那拜託您了$R;");
                            break;
                    }
                    return;
                }
            }
            Say(pc, 131, "…！！$R您是不是？$R從奧克魯尼亞來的呀？$R;" +
                "$P難為您找到這偏僻的地方！$R冷不冷？$R趕快來暖和一下吧。$R;");
        }
    }
}
