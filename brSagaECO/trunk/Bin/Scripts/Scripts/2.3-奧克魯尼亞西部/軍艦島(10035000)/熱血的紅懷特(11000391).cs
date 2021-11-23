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
                            "要用什麽顔色呢……$R;");
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
                                Say(pc, 131, "來！這是表達感謝的禮物$R;" +
                                    "相當好的皮鞋啊!!$R;");
                                return;
                            }
                            Say(pc, 131, "來報答了$R;" +
                                "去把東西減少後再來吧$R;");
                            return;
                        }
                        if (CheckInventory(pc, 50013350, 1))
                        {
                            mask.SetValue(JJDFlags.杰利科收集任务结束, true);
                            GiveItem(pc, 50013350, 1);
                            Say(pc, 131, "來！這是表達感謝的禮物$R;" +
                                "相當好的皮鞋啊!!$R;");
                            return;
                        }
                        Say(pc, 131, "來報答了$R;" +
                            "去把東西減少後再來吧$R;");
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
                                "真的有12種那麽多的杰利科嗎!$R;" +
                                "$R這個真是謝謝阿!$R;");
                            Say(pc, 131, "給了他收集的杰利科$R;");
                            if (pc.Gender == PC_GENDER.FEMALE)
                            {
                                if (CheckInventory(pc, 50062850, 1))
                                {

                                    mask.SetValue(JJDFlags.杰利科收集任务结束, true);
                                    GiveItem(pc, 50062850, 1);
                                    Say(pc, 131, "來！這是表達感謝的禮物$R;" +
                                        "相當好的皮鞋啊!!$R;");
                                    return;
                                }
                                Say(pc, 131, "來報答了$R;" +
                                    "去把東西減少後再來吧$R;");
                                return;
                            }
                            if (CheckInventory(pc, 50013350, 1))
                            {

                                mask.SetValue(JJDFlags.杰利科收集任务结束, true);
                                GiveItem(pc, 50013350, 1);
                                Say(pc, 131, "來！這是表達感謝的禮物$R;" +
                                    "相當好的皮鞋啊!!$R;");
                                return;
                            }
                            Say(pc, 131, "來報答了$R;" +
                                "去把東西減少後再來吧$R;");
                            return;
                        }
                        Say(pc, 131, "雖然現在只是傳言$R;" +
                            "杰利科在這個世界上聽説有12種呢$R;" +
                            "$R再多努力一點找找看吧$R;");
                        return;
                    }
                    Say(pc, 11000391, 112, "……$R;" +
                        "$R嗯……真的這個顔色沒關係嗎?$R;");
                    Say(pc, 11000392, 132, "我覺得藍色比天藍色更適合女生?$R;");
                    Say(pc, 11000393, 131, "黃色已經厭倦了…$R;" +
                        "草綠色怎麽樣?$R;");
                    Say(pc, 11000394, 131, "那個不是跟綠色差不多嗎$R;");
                    Say(pc, 11000393, 112, "雖然那樣$R;" +
                        "但是黃色的形象有點…$R;");
                    Say(pc, 11000395, 132, "啊，對，對!黃色是有點那個…$R;" +
                        "$R喜歡的人雖然喜歡$R;");
                    Say(pc, 11000391, 131, "稍等$R;" +
                        "$R都有什麽顔色$R;" +
                        "你到底知不知道啊?$R;");
                    Say(pc, 11000394, 131, "也對，杰利科的材料是吧?$R;" +
                        "$R雖然見過黃色和藍色$R;" +
                        "全部的話我就不知道了$R;");
                    Say(pc, 11000392, 131, "嗯…不知道有什麽顔色的話$R;" +
                        "選擇什麽顔色好就困難了$R;");
                    Say(pc, 11000395, 132, "嗯…是啊$R;");
                    Say(pc, 11000391, 131, "好的…首先$R;" +
                        "開始收集杰利科的事情吧$R;" +
                        "$R各自收集自己知道的杰利科!$R;");
                    Say(pc, 11000393, 131, "好!!$R;");
                    Say(pc, 11000391, 131, "你也要協助嗎?不是強制性的…$R;" +
                        "$R當然會有答謝的$R;");
                    switch (Select(pc, "怎麼做呢？", "", "拒絕!", "好"))
                    {
                        case 1:
                            break;
                        case 2:
                            mask.SetValue(JJDFlags.杰利科收集任务开始, true);
                            Say(pc, 131, "好!那就拜託了!$R;" +
                                "把世界上的杰利科全都收集來吧!$R;");
                            break;
                    }
                    return;
                }
            }

            Say(pc, 11000391, 131, "我們是把強烈顏色$R;" +
                "標榜突擊的軍團隊員$R;");
            Say(pc, 11000391, 140, "熱血，隊長，紅懷特$R;");
            Say(pc, 11000392, 141, "一點紅的藍布$R;");
            Say(pc, 11000393, 140, "有意思的黃耶勞$R;");
            Say(pc, 11000394, 140, "天下壯士綠杰$R;");
            Say(pc, 11000395, 140, "孤單的狼黑佰特!$R;");
            Say(pc, 11000391, 131, "5個聚在一起顔色豐富的突擊隊員!$R;");
        }
    }
}
