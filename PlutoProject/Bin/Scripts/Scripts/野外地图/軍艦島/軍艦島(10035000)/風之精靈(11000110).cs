using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000110 : Event
    {
        public S11000110()
        {
            this.EventID = 11000110;
        }

        public override void OnEvent(ActorPC pc)
        {

            BitMask<JJDFlags> mask = new BitMask<JJDFlags>(pc.CMask["JJD"]);
            BitMask<Job2X_06> Job2X_06_mask = pc.CMask["Job2X_06"];
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            if (Crystal_mask.Test(Crystal.開始收集) &&
                !Crystal_mask.Test(Crystal.風之精靈) &&
                CountItem(pc, 10014300) >= 1)
            {
                if (!Crystal_mask.Test(Crystal.光之精靈) &&
                    !Crystal_mask.Test(Crystal.暗之精靈) &&
                    !Crystal_mask.Test(Crystal.炎之精靈) &&
                    !Crystal_mask.Test(Crystal.土之精靈) &&
                    !Crystal_mask.Test(Crystal.水之精靈) &&
                    CountItem(pc, 10014300) >= 1)
                {
                    Crystal_mask.SetValue(Crystal.風之精靈, true);
                    Say(pc, 131, "您好$R;" +
                        "$R我叫菲尔！是风之精灵$R;" +
                        "$R什么事情呢？$R;" +
                        "$P…$R;" +
                        "什么？在『水晶』上注入力量？$R;" +
                        "$R不知道是什么，$R;" +
                        "但只要我能做到的，一定会帮您唷$R;" +
                        "$P那么～开始吧$R;");
                    PlaySound(pc, 3120, false, 100, 50);
                    ShowEffect(pc, 4033);
                    Say(pc, 131, "『水晶』里注入力量了$R;" +
                        "…?$R;" +
                        "再增加力量$R;" +
                        "已经不可能了$R;");
                    return;
                }
                Say(pc, 131, "您好$R;" +
                    "$R我的名字叫菲尔！是神风精灵$R;" +
                    "$R什么事情呢？$R;" +
                    "$P…$R;" +
                    "在『水晶』上注入力量？$R;" +
                    "$R…?$R;" +
                    "已经有别的精灵力量注入了$R;" +
                    "$P水晶现在不能注入我的力量哦$R;");
                return;
            }

            if (!mask.Test(JJDFlags.神風精靈第一次對話))
            {
                Say(pc, 131, "您好$R;" +
                    "$R我叫菲尔！是风之精灵$R;" +
                    "$R有什么事情吗？$R;");
                mask.SetValue(JJDFlags.神風精靈第一次對話, true);
            }
            else
            {
                Say(pc, 131, "您好$R;" +
                    "找我有什么事情呢？$R;");
            }

            if (Job2X_06_mask.Test(Job2X_06.朝拜神風精靈))
            {
                Say(pc, 131, "再见$R;");
                return;
            }

            if (Job2X_06_mask.Test(Job2X_06.進階轉職開始))
            {
                Job2X_06_mask.SetValue(Job2X_06.朝拜神風精靈, true);
                Say(pc, 131, "您真是一个奇怪的客人$R;" +
                    "$P元素术师是一个$R;" +
                    "很累的职业，请加油唷$R;" +
                    "$P希望您能得神风保护呀$R;");
                ShowEffect(pc, 4033);
                return;
            }

            if (CountItem(pc, 60091051) >= 1 && CountItem(pc, 10012000) >= 1)
            {

                Say(pc, 131, "啊，『方形黄玉』呀$R;" +
                    "交换方形黄玉$R;" +
                    "在『猎人战弓』里$R;" +
                    "注入风之力是吗？$R;");
                switch (Select(pc, "怎么办呢?", "", "拜托他注入风之力", "放弃"))
                {
                    case 1:
                        if (CheckInventory(pc, 60091055, 1))
                        {

                            PlaySound(pc, 3068, false, 100, 50);
                            Wait(pc, 2000);
                            TakeItem(pc, 60091051, 1);
                            TakeItem(pc, 10012000, 1);
                            GiveItem(pc, 60091055, 1);
                            Say(pc, 131, "得到了『猎人战弓（风）』$R;");
                            Say(pc, 131, "交出了『方形黄玉』$R;" +
                                "太感谢了$R;" +
                                "请再来玩呀！$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了，不能给您呀$R;" +
                            "减少行李后，再来吧$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我误会您了$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10011207) >= 1 && CountItem(pc, 10026400) >= 1)
            {

                Say(pc, 131, "啊！$R『风之召唤石』$R;" +
                    "$R嘻嘻！想用召唤石在$R;" +
                    "『木箭』上注入风之力是吗？$R;");
                switch (Select(pc, "怎么办呢?", "", "注入风之力", "放弃"))
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

                            if (CheckInventory(pc, 10026507, 200))
                            {

                                TakeItem(pc, 10011207, 1);
                                TakeItem(pc, 10026400, 200);
                                GiveItem(pc, 10026507, 200);
                                Say(pc, 131, "『木箭』变成了『风之箭』$R;");
                                Say(pc, 131, "哎…好困呀$R;" +
                                    "$R今天到这里吗？$R;" +
                                    "还想继续制作$R;" +
                                    "就把风之召唤石拿来吧！$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了，不能给您呀$R;" +
                                "减少道具后，再来吧$R;");
                            return;
                        }
                        TakeItem(pc, 10011207, 1);
                        TakeItem(pc, 10026400, (ushort)MJ);
                        GiveItem(pc, 10026507, (ushort)MJ);
                        Say(pc, 131, "『木箭』变成了『风之箭』$R;");
                        Say(pc, 131, "给我『风之召唤石』太感谢您了$R;" +
                            "请再来玩呀！$R;");

                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我误会您了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "…?$R;" +
                "什么也没有呀$R;");
        }
        //未知对话
        

        
    }
}
