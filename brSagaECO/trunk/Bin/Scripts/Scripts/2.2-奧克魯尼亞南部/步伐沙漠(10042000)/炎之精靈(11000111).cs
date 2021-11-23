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
                    Say(pc, 131, "$R我叫鐵騎！是火焰精靈$R;" +
                        "$R什麼事情呢？$R;" +
                        "$P…$R;" +
                        "在『水晶』上$R;" +
                        "注入力量就行了嗎？$R;" +
                        "$R那很簡單！$R;" +
                        "$P那麼走吧～$R;");
                    PlaySound(pc, 3120, false, 100, 50);
                    ShowEffect(pc, 4032);
                    Say(pc, 131, "『水晶』裡注入力量了$R;" +
                        "…?$R;" +
                        "不能再$R;" +
                        "注入我的力量了$R;");
                    return;
                }
                Say(pc, 131, "您好$R;" +
                    "$R我叫鐵騎！是火焰精靈$R;" +
                    "$R什麼事情呢？$R;" +
                    "$P…$R;" +
                    "什麼？在『水晶』上注入力量？$R;" +
                    "$R…??$R;" +
                    "已經有別的精靈力量了$R;" +
                    "$P我的力量不能注入水晶了$R;");
                return;
            }

            if (CountItem(pc, 10013800) >= 1)
            {
                Say(pc, 131, "啊，那是『塞爾曼德的項鏈』$R;" +
                    "$R想跟塞爾曼德達成契約嗎？$R;");
                switch (Select(pc, "怎麼辦呢?", "", "達成契約", "放棄"))
                {
                    case 1:
                        Say(pc, 131, "我想要一點您的血呀$R;");
                        Wait(pc, 0);
                        PlaySound(pc, 2041, false, 100, 50);
                        //PARAM ME.HP = -1
                        pc.HP--;
                        
                        if(Puppet_Fish_mask.Test(Puppet_Fish.幫助過))
                        {
                        Say(pc, 361, "嘔！$R;");
                        Say(pc, 131, "呸呸！不好吃的！$R;" +
                            "有瑪歐斯的味道阿$R;" +
                            "$R對不起$R;" +
                            "不想接受瑪歐斯幫助的人…$R;" +
                            "抱歉不能進行契約了$R;");
                            return;
                        }
                         //*/
                        Say(pc, 131, "唔唔！又甜又好吃$R;" +
                            "$R好！契約成立！$R;");
                        Wait(pc, 0);
                        PlaySound(pc, 3017, false, 100, 50);
                        ShowEffect(pc, 8014);
                        Wait(pc, 2000);
                        Say(pc, 131, "$R『塞爾曼德的項鏈』$R;" +
                            "變成了『活動木偶塞爾曼德』$R;");
                        TakeItem(pc, 10013800, 1);
                        GiveItem(pc, 10013850, 1);
                        Say(pc, 131, "性格粗暴，但還算是不錯的小子呀！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我誤會您了$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10035600) >= 1)
            {
                Say(pc, 131, "啊！$R『毒蜥蜴尾巴』！$R;" +
                    "您擁有那神奇的東西呀$R;" +
                    "$P把『毒蜥蜴尾巴』和$R;" +
                    "最漂亮的火之寶石合成$R;" +
                    "會變成珍貴的道具唷$R;" +
                    "$P找我有什麼事情嗎？$R;");
            }
            if (!mask.Test(JLFlags.炎之精靈第一次對話))
            {
                Say(pc, 131, "您好！$R;" +
                    "$R我叫鐵騎！是火焰精靈$R;" +
                    "$R有什麼事呢？$R;");
                mask.SetValue(JLFlags.炎之精靈第一次對話, true);
            }
            else
            {
                Say(pc, 131, "您好！$R;" +
                    "找我有什麼事情嗎？$R;");
            }
            if (Job2X_06_mask.Test(Job2X_06.朝拜炎之精靈))
            {
                Say(pc, 131, "加油！$R;");
                return;
            }
            if (Job2X_06_mask.Test(Job2X_06.進階轉職開始))
            {
                Job2X_06_mask.SetValue(Job2X_06.朝拜炎之精靈, true);
                Say(pc, 131, "哇！好久沒看到的巡禮者呀…$R;" +
                    "$P以前很多人上這裡來的…$R;" +
                    "$P希望您得火焰保護唷$R;");
                ShowEffect(pc, 4032);
                return;
            }
            if (CountItem(pc, 60091051) >= 1 && CountItem(pc, 10012100) >= 1)
            {
                Say(pc, 131, "啊，是『心型紅寶石』呀$R;" +
                    "為了交換心型紅寶石$R;" +
                    "在『獵弓』裡$R;" +
                    "注入火之力是嗎？$R;");
                switch (Select(pc, "怎麼辦呢？", "", "讓他把火之力注入", "放棄"))
                {
                    case 1:
                        if (CheckInventory(pc, 60091052, 1))
                        {
                            PlaySound(pc, 3068, false, 100, 50);
                            Wait(pc, 2000);
                            TakeItem(pc, 60091051, 1);
                            TakeItem(pc, 10012100, 1);
                            GiveItem(pc, 60091052, 1);
                            Say(pc, 131, "得到了『獵弓（火焰）』$R;");
                            Say(pc, 131, "『心型紅寶石』！太謝謝您了$R;" +
                                "再來玩唷！$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了，不能給您呀$R;" +
                            "減少道具後，再來吧$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我誤會您了$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10011201) >= 1 && CountItem(pc, 10026400) >= 1)
            {
                Say(pc, 131, "啊！是『火焰召喚石』呀$R;" +
                    "$R想用火焰召喚石在$R;" +
                    "『木箭』上注入火之力是嗎？$R;");
                switch (Select(pc, "怎麼辦呢?", "", "注入火之力", "放棄"))
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
                                Say(pc, 131, "『木箭』變成了『火焰之箭』$R;");
                                Say(pc, 131, "哎，太累了！$R;" +
                                    "$R今天就到這兒吧$R;" +
                                    "還想再繼續做的話$R;" +
                                    "把『火焰召喚石』拿來吧！$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了，不能給您呀$R;" +
                                "減少道具後，再來吧$R;");
                            return;
                        }
                        TakeItem(pc, 10011201, 1);
                        TakeItem(pc, 10026400, (ushort)MJ);
                        GiveItem(pc, 10026500, (ushort)MJ);
                        Say(pc, 131, "『木箭』變成了『火焰之箭』！$R;");
                        Say(pc, 131, "謝謝給我『火焰召喚石』！$R;" +
                            "請再來玩呀！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我誤會您了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "…?$R;" +
                "什麼也沒有$R;");
        }
    }
}
