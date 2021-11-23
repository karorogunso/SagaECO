using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Map;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public class 消耗性道具 : Item
    {
        public 消耗性道具()
        {
            //時空鑰匙
            Init(10000806, delegate(ActorPC pc)
            {
                try
                {
                    string oldSave;
                    oldSave = GetMapName(pc.SaveMap);
                    if (oldSave == "")
                    {
                        Warp(pc, 10023100, 250, 127);
                        SetHomePoint(pc, 10023100, 250, 127);
                        return;
                    }
                    Say(pc, 131, "是不是要歸還到『" + oldSave + "』$R;", " ");

                    switch (Select(pc, "怎麼辦呢？", "", "回去", "不用了"))
                    {
                        case 1:
                            if (CheckMapFlag(pc.SaveMap, MapFlags.Dominion) != CheckMapFlag(pc.MapID, MapFlags.Dominion))
                            {
                                Say(pc, 131, "抱歉,您當前的世界跟儲存的世界不同,無法時空鑰匙", " ");
                                return;
                            }
                            TakeItem(pc, 10022900, 1);
                            Warp(pc, pc.SaveMap, pc.SaveX, pc.SaveY);
                            break;
                        case 2:
                            break;
                    }
                }
                catch
                {
                    Warp(pc, 10023100, 250, 127);
                    SetHomePoint(pc, 10023100, 250, 127);
                }
            });
            //時空の鍵EX（エミル）
            Init(90000161, delegate(ActorPC pc)
            {
                if (CheckMapFlag(pc.MapID, MapFlags.Dominion))
               {
                   return;
                }
                switch (Select(pc, "要去哪里?", "", "フシギ団の砦", "不用了"))
                {
                    case 1:
                        Warp(pc, 10063100, 184, 140);
                        break;
                }
            });

            //火焰的翅膀
            Init(90000005, delegate(ActorPC pc)
            {
                if (CheckMapFlag(pc.MapID, MapFlags.Dominion))
               {
                   return;
                }
                Warp(pc, 20022000, 104, 30);
            });
            //マーメイドの涙
            Init(90000124, delegate(ActorPC pc)
            {
                ActivateMarionette(pc, 20390000);
                Heal(pc);
                ShowEffect(pc, 8015);
                TakeItem(pc, 10034503, 1);
            });

            //心之破片
            Init(11000599, delegate(ActorPC pc)
            {
                BitMask<Job2X_07> mask = pc.CMask["Job2X_07"];
                BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
                BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];
                BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
                BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];
                if (CheckMapFlag(pc.MapID, MapFlags.Dominion))
               {
                   return;
                }
                if (pc.PossesionedActors.Count != 0)
                {
                    return;
                }

                if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                    !Neko_06_amask.Test(Neko_06.获得杏子) &&
                    Neko_06_cmask.Test(Neko_06.詢問塔尼亞代表) &&
                    !Neko_06_cmask.Test(Neko_06.尋找阿伊斯))
                {
                    ShowEffect(pc, 4023);
                    Wait(pc, 1980);
                    pc.CInt["Neko_06_Map_03"] = CreateMapInstance(50037000, 10023000, 135, 64);
                    Warp(pc, (uint)pc.CInt["Neko_06_Map_03"], 11, 16);
                    return;
                }
                if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                    Neko_01_cmask.Test(Neko_01.得到裁縫阿姨的三角巾) &&
                    !Neko_01_cmask.Test(Neko_01.獲得桃子) &&
                    CountItem(pc, 10017904) >= 1)
                {
                    ShowEffect(pc, 4023);
                    Warp(pc, 30140003, 11, 15);
                    //WARP 786
                    return;
                }

                if (mask.Test(Job2X_07.轉職開始) && pc.Job == PC_JOB.VATES)//_3A84 && pc.Job = 61)
                {
                    ShowEffect(pc, 4023);
                    Warp(pc, 30140003, 11, 15);
                    //WARP 786
                    return;
                }
            });

            //塞爾曼德的心臟
            Init(90000006, delegate(ActorPC pc)
            {
                BitMask<Job2X_09> Job2X_09_mask = pc.CMask["Job2X_09"];
                Job2X_09_mask.SetValue(Job2X_09.使用塞爾曼德的心臟, true);
            });
            //ブルートオーブ（イベント）
            Init(12001189, delegate(ActorPC pc)
            {
                BitMask<Jief> Jief_cmask = pc.CMask["Jief"];
                pc.DominionReserveSkill = true;
                Jief_cmask.SetValue(Jief.使用过物品, true);
                TakeItem(pc, 10011700, 1);
            });

            //ステータスリセット(洗屬性點)
            Init(10057000, delegate(ActorPC pc)
            {
                if (pc.Race == PC_RACE.DEM)
                {
                    return;
                }
                Say(pc, 131, "狀態重新設定後$R;" +
                    "再分配獎勵點數嗎？$R;");
                switch (Select(pc, "狀態重新設定嗎？", "", "重新設定", "不用了"))
                {
                    case 1:
                        ResetStatusPoint(pc);
                        //STATUSRESET
                        TakeItem(pc, 10057000, 1);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        Say(pc, 131, "回到初始狀態囉$R;");
                        break;
                }

            });

            //スキルリセット(洗技能點)
            Init(10057001, delegate(ActorPC pc)
            {
                if (CheckMapFlag(pc.MapID, MapFlags.Dominion))
               {
                   return;
                }

                if (pc.Race == PC_RACE.DEM)
                {
                    return;
                }
                if (pc.Skills.Count == 0 &&
                    pc.Skills2.Count == 0)
                {
                    Say(pc, 131, "沒有持有技能喔$R;");
                    return;
                }

                Say(pc, 131, "所有技能進行重新設定嗎？$R;");
                switch (Select(pc, "技能進行重新設定嗎？", "", "重新設定", "不用了"))
                {
                    case 1:
                        ResetSkill(pc, 1);
                        ResetSkill(pc, 2);
                        //SKILLRESET_ALL
                        TakeItem(pc, 10057001, 1);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        Say(pc, 131, "技能進行重新設定了$R;");
                        break;
                }
            });

            //不明的鬍鬚(貓靈:桃子)
            Init(10000805, delegate(ActorPC pc)
            {
                BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
                BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];
                if (CheckMapFlag(pc.MapID, MapFlags.Dominion))
               {
                   return;
                }

                if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                    !Neko_01_cmask.Test(Neko_01.與瑪歐斯對話) &&
                    !Neko_01_cmask.Test(Neko_01.使用不明的鬍鬚))
                {
                    Neko_01_cmask.SetValue(Neko_01.使用不明的鬍鬚, true);
                    //_4A26 = true;
                    Warp(pc, 30054000, 3, 6);
                    //WARP 218
                    Say(pc, 0, 131, "喵$R;", " ");
                    return;
                }

            });

            //護髮劑
            Init(11000117, delegate(ActorPC pc)
            {
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    if (pc.HairStyle == 0 ||
                        pc.HairStyle == 16 ||
                        pc.HairStyle == 18)
                    {
                        pc.Wig = 255;
                        pc.HairStyle = 7;
                        ShowEffect(pc, 4112);
                        return;
                    }

                    if (pc.HairStyle == 1 ||
                        pc.HairStyle == 7 ||
                        pc.HairStyle == 9 ||
                        pc.HairStyle == 17)
                    {
                        pc.Wig = 255;
                        pc.HairStyle = 2;
                        ShowEffect(pc, 4112);
                        return;
                    }
                    if (pc.HairStyle == 2 ||
                        pc.HairStyle == 3 ||
                        pc.HairStyle == 4 ||
                        pc.HairStyle == 5 ||
                        pc.HairStyle == 6 ||
                        pc.HairStyle == 8 ||
                        pc.HairStyle == 10 ||
                        pc.HairStyle == 19 ||
                        pc.HairStyle == 20 ||
                        pc.HairStyle == 21 ||
                        pc.HairStyle == 22 ||
                        pc.HairStyle == 23 ||
                        pc.HairStyle == 24 ||
                        pc.HairStyle == 25)
                    {
                        Say(pc, 131, "哦！？$R;" +
                            "$P??…沒關係？$R;");
                        GiveItem(pc, 10000608, 1);
                        return;
                    }

                    if (pc.HairStyle == 14)
                    {
                        pc.Wig = 255;
                        pc.HairStyle = 0;
                        ShowEffect(pc, 4112);
                        return;
                    }
                    return;
                }
                if (pc.HairStyle == 0 ||
                    pc.HairStyle == 2 ||
                    pc.HairStyle == 5 ||
                    pc.HairStyle == 8 ||
                    pc.HairStyle == 12)
                {
                    pc.Wig = 255;
                    pc.HairStyle = 1;
                    ShowEffect(pc, 4112);
                    return;
                }

                if (pc.HairStyle == 1 ||
                    pc.HairStyle == 3 ||
                    pc.HairStyle == 6 ||
                    pc.HairStyle == 10 ||
                    pc.HairStyle == 13 ||
                    pc.HairStyle == 14 ||
                    pc.HairStyle == 15 ||
                    pc.HairStyle == 16 ||
                    pc.HairStyle == 20 ||
                    pc.HairStyle == 21 ||
                    pc.HairStyle == 22)
                {
                    Say(pc, 131, "哦！？$R;" +
                        "$P??…沒關係？$R;");
                    GiveItem(pc, 10000608, 1);
                    return;
                }

                if (pc.HairStyle == 4)
                {
                    pc.Wig = 255;
                    pc.HairStyle = 0;
                    ShowEffect(pc, 4112);
                    return;
                }
                if (pc.HairStyle == 7 ||
                    pc.HairStyle == 11)
                {
                    pc.Wig = 255;
                    pc.HairStyle = 4;
                    ShowEffect(pc, 4112);
                    return;
                }
            });

            //多發箭
            Init(90000033, delegate(ActorPC pc)
            {
                TakeItem(pc, 10026450, 1);
                GiveItem(pc, 10026400, 100);
            });

            //自己做的多發箭
            Init(90000034, delegate(ActorPC pc)
            {
                TakeItem(pc, 10026451, 1);
                GiveItem(pc, 10026401, 100);
            });

            //鋼鐵箭束
            Init(90000035, delegate(ActorPC pc)
            {
                TakeItem(pc, 10026651, 1);
                GiveItem(pc, 10026600, 100);
            });

            //彈膛（實彈）
            Init(90000040, delegate(ActorPC pc)
            {
                TakeItem(pc, 10055000, 1);
                GiveItem(pc, 10025900, 100);
            });

            //彈膛（藥彈）
            Init(90000041, delegate(ActorPC pc)
            {
                TakeItem(pc, 10055001, 1);
                GiveItem(pc, 10025904, 100);
            });

            //巨大的彈膛（實彈）
            Init(90000042, delegate(ActorPC pc)
            {
                TakeItem(pc, 10055100, 1);
                GiveItem(pc, 10055000, 3);
            });

            //巨大的彈膛（藥彈）
            Init(90000043, delegate(ActorPC pc)
            {
                TakeItem(pc, 10055101, 1);
                GiveItem(pc, 10055001, 3);
            });

            //飛翔帆材料目錄
            Init(90000056, delegate(ActorPC pc)
            {
                BitMask<FGarden> fgarden = pc.AMask["FGarden"];
                string Gztemp1 = "0";
                string Gztemp2 = "0";
                string Gztemp3 = "0";
                string Gztemp4 = "0";
                string Gztemp5 = "0";
                if (fgarden.Test(FGarden.委托飞空庭甲板)) { Gztemp1 ="1"; }
                if (fgarden.Test(FGarden.委托涡轮引擎)) { Gztemp2 = "1"; }
                if (fgarden.Test(FGarden.委托汽笛)) { Gztemp3 = "1"; }
                if (fgarden.Test(FGarden.委托飞行用帆)) { Gztemp4 = "1"; }
                if (fgarden.Test(FGarden.完全委托飞行用帆)) { Gztemp4 = "2"; }
                if (fgarden.Test(FGarden.委托飞行用大帆)) { Gztemp5 = "1"; }
                if (fgarden.Test(FGarden.完全委托飞行用大帆)) { Gztemp5 = "2"; }
                Say(pc, 131, "飛空艇甲板 " + Gztemp1 + "/1$R;" +
                    "渦輪引擎 " + Gztemp2 + "/1$R;" +
                    "汽笛 " + Gztemp3 + "/1$R;" +
                    "飛行用帆 " + Gztemp4 + "/2$R;" +
                    "飛行用大帆 " + Gztemp5 + "/2$R;", "飛翔帆的材料目錄");
            });

            //四葉草糖果
            Init(90000025, delegate(ActorPC pc)
            {
                PetRecover(pc, 15);
                TakeItem(pc, 10009111, 1);
            });

            //四葉草豆
            Init(90000027, delegate(ActorPC pc)
            {
                PetRecover(pc, 1);
                TakeItem(pc, 10034600, 1);
            });
        }
    }
}