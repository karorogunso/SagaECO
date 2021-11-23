using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10042000
{
    public class S11000111 : Event
    {
        public S11000111()
        {
            this.EventID = 11000111;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JLFlags> mask = new BitMask<JLFlags>(pc.CMask["JL"]);
            BitMask<Job2X_06> Job2X_06_mask = pc.CMask["Job2X_06"];
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];
            BitMask<Puppet_Fish> Puppet_Fish_mask = pc.CMask["Puppet_Fish"];

            if (Crystal_mask.Test(Crystal.開始收集) &&
                !Crystal_mask.Test(Crystal.炎之精靈) &&
                CountItem(pc, 10014300) >= 1)
            {
                if (!Crystal_mask.Test(Crystal.光之精靈) &&
                    !Crystal_mask.Test(Crystal.暗之精靈) &&
                    !Crystal_mask.Test(Crystal.水之精靈) &&
                    !Crystal_mask.Test(Crystal.土之精靈) &&
                    !Crystal_mask.Test(Crystal.風之精靈) &&
                    CountItem(pc, 10014300) >= 1)
                {
                    Crystal_mask.SetValue(Crystal.炎之精靈, true);
                    Say(pc, 131, "$R我叫铁骑！是火之精灵$R;" +
                        "$R什么事情呢？$R;" +
                        "$P…$R;" +
                        "在『水晶』上$R;" +
                        "注入力量就行了吗？$R;" +
                        "$R那很简单！$R;" +
                        "$P那么走吧～$R;");
                    PlaySound(pc, 3120, false, 100, 50);
                    ShowEffect(pc, 4032);
                    Say(pc, 131, "『水晶』里注入力量了$R;" +
                        "…?$R;" +
                        "不能再$R;" +
                        "注入我的力量了$R;");
                    return;
                }
                Say(pc, 131, "您好$R;" +
                    "$R我叫铁骑！是火之精灵$R;" +
                    "$R什么事情呢？$R;" +
                    "$P…$R;" +
                    "什么？在『水晶』上注入力量？$R;" +
                    "$R…??$R;" +
                    "已经有别的精灵力量了$R;" +
                    "$P我的力量不能注入水晶了$R;");
                return;
            }

            if (CountItem(pc, 10013800) >= 1)
            {
                Say(pc, 131, "啊，那是『烈焰蜥蜴的项链』$R;" +
                    "$R想跟烈焰蜥蜴达成契约吗？$R;");
                switch (Select(pc, "怎么办呢?", "", "达成契约", "放弃"))
                {
                    case 1:
                        Say(pc, 131, "我想要一点您的血呀$R;");
                        Wait(pc, 0);
                        PlaySound(pc, 2041, false, 100, 50);
                        //PARAM ME.HP = -1
                        pc.HP--;
                        
                        if(Puppet_Fish_mask.Test(Puppet_Fish.幫助過))
                        {
                            Say(pc, 361, "呕！$R;");
                            Say(pc, 131, "呸呸！不好吃的！$R;" +
                                "有鱼人的味道阿$R;" +
                                "$R对不起$R;" +
                                "不想接受帮助过鱼人的人…$R;" +
                                "抱歉不能进行契约了$R;");
                            return;
                        }
                        //*/
                        Say(pc, 131, "唔唔！又甜又好吃$R;" +
                            "$R好！契约成立！$R;");
                        Wait(pc, 0);
                        PlaySound(pc, 3017, false, 100, 50);
                        ShowEffect(pc, 8014);
                        Wait(pc, 2000);
                        Say(pc, 131, "$R『烈焰蜥蜴的项链』$R;" +
                            "变成了『活动木偶烈焰蜥蜴』$R;");
                        TakeItem(pc, 10013800, 1);
                        GiveItem(pc, 10013850, 1);
                        Say(pc, 131, "性格粗暴，但还算是不错的小子呀！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我误会您了$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10035600) >= 1)
            {
                Say(pc, 131, "啊！$R『毒蜥蜴的尾巴』！$R;" +
                    "您拥有那神奇的东西呀$R;" +
                    "$P把『毒蜥蜴尾巴』和$R;" +
                    "最漂亮的火之宝石合成$R;" +
                    "会变成珍贵的道具哦$R;" +
                    "$P找我有什么事情吗？$R;");
            }
            if (!mask.Test(JLFlags.炎之精靈第一次對話))
            {
                Say(pc, 131, "您好！$R;" +
                    "$R我叫铁骑！是火之精灵$R;" +
                    "$R有什么事呢？$R;");
                mask.SetValue(JLFlags.炎之精靈第一次對話, true);
            }
            else
            {
                Say(pc, 131, "您好！$R;" +
                    "找我有什么事情吗？$R;");
            }
            if (Job2X_06_mask.Test(Job2X_06.朝拜炎之精靈))
            {
                Say(pc, 131, "加油！$R;");
                return;
            }
            if (Job2X_06_mask.Test(Job2X_06.進階轉職開始))
            {
                Job2X_06_mask.SetValue(Job2X_06.朝拜炎之精靈, true);
                Say(pc, 131, "哇！好久没看到的巡礼者呀…$R;" +
                    "$P以前很多人上这里来的…$R;" +
                    "$P希望得到火焰的庇护吗$R;");
                ShowEffect(pc, 4032);
                return;
            }
            if (CountItem(pc, 60091051) >= 1 && CountItem(pc, 10012100) >= 1)
            {
                Say(pc, 131, "啊，是『心型红宝石』呀$R;" +
                    "为了交换力量$R;" +
                    "在『猎人战弓』里$R;" +
                    "注入火之力是吗？$R;");
                switch (Select(pc, "怎么办呢？", "", "让他把火之力注入", "放弃"))
                {
                    case 1:
                        if (CheckInventory(pc, 60091052, 1))
                        {
                            PlaySound(pc, 3068, false, 100, 50);
                            Wait(pc, 2000);
                            TakeItem(pc, 60091051, 1);
                            TakeItem(pc, 10012100, 1);
                            GiveItem(pc, 60091052, 1);
                            Say(pc, 131, "得到了『猎人战弓（火）』$R;");
                            Say(pc, 131, "『心型红宝石』我就收下了！谢谢您了$R;" +
                                "有空再来玩！$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了，不能给您呀$R;" +
                            "减少道具后，再来吧$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我误会您了$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10011201) >= 1 && CountItem(pc, 10026400) >= 1)
            {
                Say(pc, 131, "啊！是『火焰召唤石』呀$R;" +
                    "$R想用火焰召唤石在$R;" +
                    "『木箭』上注入火之力是吗？$R;");
                switch (Select(pc, "怎么办呢?", "", "注入火之力", "放弃"))
                {
                    case 1:
                        PlaySound(pc, 3014, false, 100, 50);
                        Wait(pc, 3000);
                        int MJ;
                        MJ = 0;
                        while (CountItem(pc, 10026400) > MJ)
                        {
                            MJ++;
                        }
                        if (MJ > 200)
                        {
                            if (CheckInventory(pc, 10026500, 200))
                            {
                                TakeItem(pc, 10011201, 1);
                                TakeItem(pc, 10026400, 200);
                                GiveItem(pc, 10026500, 200);
                                Say(pc, 131, "『木箭』变成了『火焰之箭』$R;");
                                Say(pc, 131, "哎，太累了！$R;" +
                                    "$R今天就到这儿吧$R;" +
                                    "还想再继续做的话$R;" +
                                    "把『火焰召唤石』拿来吧！$R;");
                                return;
                            }
                            else if (CheckInventory(pc, 10026500, 200))
                            {
                                TakeItem(pc, 10011201, 1);
                                TakeItem(pc, 10026401, 200);
                                GiveItem(pc, 10026500, 200);
                                Say(pc, 131, "『自己做的箭』变成了『火焰之箭』$R;");
                                Say(pc, 131, "哎，太累了！$R;" +
                                    "$R今天就到这儿吧$R;" +
                                    "还想再继续做的话$R;" +
                                    "把『火焰召唤石』拿来吧！$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了，不能给您呀$R;" +
                                "减少道具后，再来吧$R;");
                            return;
                        }
                        TakeItem(pc, 10011201, 1);
                        TakeItem(pc, 10026400, (ushort)MJ);
                        GiveItem(pc, 10026500, (ushort)MJ);
                        Say(pc, 131, "『木箭』变成了『火焰之箭』！$R;");
                        Say(pc, 131, "谢谢给我『火焰召唤石』！$R;" +
                            "请再来玩呀！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我误会您了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "…?$R;" +
                "什么也没有$R;");
        }
    }
}
