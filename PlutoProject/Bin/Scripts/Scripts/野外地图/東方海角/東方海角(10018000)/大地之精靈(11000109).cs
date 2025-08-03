using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10018000
{
    public class S11000109 : Event
    {
        public S11000109()
        {
            this.EventID = 11000109;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DFHJFlags> mask = new BitMask<DFHJFlags>(pc.CMask["DFHJ"]);
            BitMask<Job2X_06> Job2X_06_mask = pc.CMask["Job2X_06"];
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            if (Crystal_mask.Test(Crystal.開始收集) &&
                !Crystal_mask.Test(Crystal.土之精靈) &&
                CountItem(pc, 10014300) >= 1)
            {
                if (!Crystal_mask.Test(Crystal.光之精靈) &&
                    !Crystal_mask.Test(Crystal.暗之精靈) &&
                    !Crystal_mask.Test(Crystal.炎之精靈) &&
                    !Crystal_mask.Test(Crystal.水之精靈) &&
                    !Crystal_mask.Test(Crystal.風之精靈) &&
                    CountItem(pc, 10014300) >= 1)
                {
                    Crystal_mask.SetValue(Crystal.土之精靈, true);
                    Say(pc, 131, "您好！$R;" +
                        "$R我的名字叫尼布！是地之精灵$R;" +
                        "$R有什么事呢？$R;" +
                        "$P…$R;" +
                        "$R什么？在“水晶”上注入力量？$R;" +
                        "$R这样做有什么意义吗？$R;" +
                        "虽然我也不知道，但好像挺好玩的$R;" +
                        "那么就帮您吧$R;" +
                        "$P准备好了吗？开始吧~$R;");
                    PlaySound(pc, 3120, false, 100, 50);
                    ShowEffect(pc, 4035);
                    Say(pc, 131, "在“水晶”里注入力量了$R;" +
                        "…?$R;" +
                        "好像已经不能$R;" +
                        "再增加力量了$R;");
                    return;
                }
                Say(pc, 131, "您好！$R;" +
                    "$R我的名字叫尼布！是地之精灵$R;" +
                    "$R有什么事呢？$R;" +
                    "$P…$R;" +
                    "$R什么？在“水晶”上注入力量？$R;" +
                    "$R…?$R;" +
                    "好像已经有别的精灵力量了$R;" +
                    "$P不能再在这里$R;" +
                    "注入我的力量了$R;");
                return;
            }

            if (!mask.Test(DFHJFlags.大地精靈第一次對話))
            {
                Say(pc, 131, "您好$R;" +
                "$R我叫尼布！是地之精灵$R;" +
                "$R什么事呢？$R;");
                mask.SetValue(DFHJFlags.大地精靈第一次對話, true);
            }
            else
            {
                Say(pc, 131, "您好$R;" +
                "找我什么事呢$R;");
            }

            if (Job2X_06_mask.Test(Job2X_06.朝拜大地精靈))
            {
                Say(pc, 131, "加油！$R;");
                return;
            }

            if (Job2X_06_mask.Test(Job2X_06.進階轉職開始))
            {
                Job2X_06_mask.SetValue(Job2X_06.朝拜大地精靈, true);
                //_3A98 = true;
                Say(pc, 131, "哇~太神奇了$R;" +
                    "没想到现在还有做这种事的人…$R;" +
                    "$P反正都到这来了，没理由不做吧？$R;" +
                    "$P希望您能得到大地的庇护吗$R;");
                ShowEffect(pc, 4035);
                return;
            }

            if (CountItem(pc, 60091051) >= 1 && CountItem(pc, 10012400) >= 1)
            {

                Say(pc, 131, "啊！是“祖母绿”呀$R;" +
                    "$R啊…为了交换祖母绿$R;" +
                    "在“猎人战弓”上$R;" +
                    "注入地之力是吗？$R;");
                switch (Select(pc, "怎么办呢?", "", "要求注入地之力", "放弃"))
                {
                    case 1:
                        if (CheckInventory(pc, 60091054, 1))
                        {
                            PlaySound(pc, 3068, false, 100, 50);
                            Wait(pc, 2000);
                            TakeItem(pc, 60091051, 1);
                            TakeItem(pc, 10012400, 1);
                            GiveItem(pc, 60091054, 1);
                            Say(pc, 131, "得到了“猎人战弓（地）”$R;");
                            Say(pc, 131, "谢谢您的“祖母绿”！$R;" +
                                "请再来玩呀！$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了，不能给您呀$R;" +
                            "我不喜欢不整理的人！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我误会了$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10011205) >= 1 && CountItem(pc, 10026400) >= 1)
            {

                Say(pc, 131, "哎！这是“大地召唤石”呀$R;" +
                    "$R啊！原来是想用召唤石$R;" +
                    "在“木箭”注入地之力阿$R;" +
                    "是这样吗？$R;");
                switch (Select(pc, "怎么办呢?", "", "注入地之力", "放弃"))
                {
                    case 1:

                        PlaySound(pc, 3068, false, 100, 50);
                        Wait(pc, 2000);
                        int MJ;
                        MJ = 0;
                        while (CountItem(pc, 10026400) > MJ)
                        {
                            MJ++;
                        }
                        if (MJ > 200)
                        {

                            if (CheckInventory(pc, 10026505, 200))
                            {

                                TakeItem(pc, 10011205, 1);
                                TakeItem(pc, 10026400, 200);
                                GiveItem(pc, 10026505, 200);
                                Say(pc, 131, "“木箭”变成了“地之箭”！$R;");
                                Say(pc, 131, "哎…太累了$R;" +
                                    "$R今天就到这儿吗？$R;" +
                                    "还想再做的话$R;" +
                                    "就把大地召唤石拿来吧！$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了，不能给您啊$R;" +
                                "真讨厌不喜欢整理的人呀！$R;");
                            return;
                        }
                        Say(pc, 131, "“木箭”变成了“地之箭”！$R;");
                        TakeItem(pc, 10011205, 1);
                        TakeItem(pc, 10026400, (ushort)MJ);
                        GiveItem(pc, 10026505, (ushort)MJ);
                        Say(pc, 131, "竟然给我“大地召唤石”$R太感谢了$R;" +
                            "下次再来吧！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我误会了$R;");
                        break;
                }
                return;
            }
        }
    }
}