using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000802 : Event
    {
        public S11000802()
        {
            this.EventID = 11000802;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            BitMask<FKTBJ> FKT_BJ = pc.AMask["FKTBJ"];
            if (!mask.Test(AYEFlags.允許領取))//_6A13)
            {
                Say(pc, 121, "您好!我是從唐卡來的$R飛空庭工匠的「飛空庭師」$R;" +
                    "$P好像這次航空規則完善了$R看奧克魯尼亞都這麼和平$R應該是理所當然的$R;" +
                    "$P所以我為了飛空庭的普及$R從唐卡派遣過來的$R;");
                if (pc.Level > 29)
                {
                    Say(pc, 121, "我現在$R把飛空庭的部件都發給大家!$R當然是免費的!$R;" +
                        "$P飛空庭部件雖然珍貴$R但很重，很難搬動$R所以領取的時候會比較慢$R所以想要的話，要遵守秩序喔$R;" +
                        "$P嗯？為什麼是免費的?$R;" +
                        "$R嗯!先說好喔$R所有飛空庭必須以唐卡為據點$R;" +
                        "$P是為了要防止有人不在唐卡登記$R偷偷的製造飛空庭$R;" +
                        "$R當然裝備或改造飛空庭會牽涉利益$R所以先想清楚啊$R;" +
                        "$P我們一星期會發送庫存部件一次$R所以每週過來一次吧!$R;" +
                        "$P還有，並不是所有的部件都給喔$R因為運輸費太貴了!$R;" +
                        "$R在這大陸上能簡單製作的$P就自己製作看看吧$R;");
                    mask.SetValue(AYEFlags.允許領取, true);
                    //_6A13 = true;
                }
                else
                {
                    Say(pc, 121, "嗯…$R;" +
                        "$R再深入的消息，$R等您變得更強後$R再告訴您吧$R;");
                    return;
                }
            }
            Say(pc, 121, "需要飛空庭的部件嗎?$R;" +
                "$R啊!因為部件太重$R收到以後可能無法動彈的$R所以把行李減少後，再來比吧$R;");
            switch (Select(pc, "接受飛空庭的部件嗎?", "", "接受", "放棄"))
            {
                case 1:
                    if (pc.Account.GMLevel >= 100)
                    {
                        switch (Select(pc, "管理者模式(除錯 注意！)", "", "第1週", "第2週", "第3週", "第4週", "第5週", "取消"))
                        {
                            case 1:
                                if (FKT_BJ.Test(FKTBJ.領取飛空庭基臺))//_Xb12)
                                {
                                    Say(pc, 121, "嗯?$R今天給您的，都已經轉交了嗎?$R;");
                                    return;
                                }
                                if (CheckInventory(pc, 10027300, 1))
                                {
                                    FKT_BJ.SetValue(FKTBJ.領取飛空庭基臺, true);
                                    //_Xb12 = true;
                                    GiveItem(pc, 10027300, 1);
                                    Say(pc, 121, "來看一下!是『飛空庭基臺』啊!$R;" +
                                        "$R這是成為飛空庭機身的重要部件!$R;");
                                    Say(pc, 135, "收到了『飛空庭基臺』!$R;");
                                    return;
                                }
                                Say(pc, 121, "行李好像滿了!$R把行李減少後再來吧!$R;");
                                break;
                            case 2:
                                if (FKT_BJ.Test(FKTBJ.領取飛空庭齒輪))//_Xb13)
                                {
                                    Say(pc, 121, "嗯?$R今天給您的，都已經轉交了嗎?$R;");
                                    return;
                                }
                                if (CheckInventory(pc, 10028200, 1))
                                {
                                    FKT_BJ.SetValue(FKTBJ.領取飛空庭齒輪, true);
                                    //_Xb13 = true;
                                    GiveItem(pc, 10028200, 1);
                                    Say(pc, 121, "來看一下!是『飛空庭齒輪』啊!$R;" +
                                        "$R這是連接旋轉帆和引擎的$R重要部件!$R;");
                                    Say(pc, 135, "收到了『飛空庭齒輪』!$R;");
                                    return;
                                }
                                Say(pc, 121, "行李好像滿了!$R把行李減少後再來吧!$R;");
                                break;
                            case 3:
                                if (FKT_BJ.Test(FKTBJ.領取舵輪))//_Xb14)
                                {
                                    Say(pc, 121, "嗯?$R今天給您的，都已經轉交了嗎?$R;");
                                    return;
                                }
                                if (CheckInventory(pc, 10028900, 1))
                                {
                                    FKT_BJ.SetValue(FKTBJ.領取舵輪, true);
                                    //_Xb14 = true;
                                    GiveItem(pc, 10028900, 1);
                                    Say(pc, 121, "來看一下!是『舵輪』啊!$R;" +
                                        "$R這是調整飛空庭的部件$R當然控制台也裝有自動導航裝置$R是重要部件!$R;");
                                    Say(pc, 135, "收到了『舵輪』!$R;");
                                    return;
                                }
                                Say(pc, 121, "行李好像滿了!$R把行李減少後再來吧!$R;");
                                break;
                            case 4:
                                if (FKT_BJ.Test(FKTBJ.領取催化劑))//_Xb15)
                                {
                                    Say(pc, 121, "嗯?$R今天給您的，都已經轉交了嗎?$R;");
                                    return;
                                }
                                if (CheckInventory(pc, 10027600, 1))
                                {
                                    FKT_BJ.SetValue(FKTBJ.領取催化劑, true);
                                    //_Xb15 = true;
                                    GiveItem(pc, 10027600, 1);
                                    Say(pc, 121, "來看一下!是『催化劑』啊!$R;" +
                                        "$R這是把引擎中排出的水蒸氣還原$R是使引擎半永久性使用的部件$R;");
                                    Say(pc, 135, "收到了『催化劑』!$R;");
                                    return;
                                }
                                Say(pc, 121, "行李好像滿了!$R把行李減少後再來吧!$R;");
                                break;
                            case 5:
                                if (FKT_BJ.Test(FKTBJ.領取飛空庭引擎))//_Xb16)
                                {
                                    Say(pc, 121, "嗯?$R今天給您的，都已經轉交了嗎?$R;");
                                    return;
                                }
                                if (CheckInventory(pc, 10027900, 1))
                                {
                                    FKT_BJ.SetValue(FKTBJ.領取飛空庭引擎, true);
                                    // _Xb16 = true;
                                    GiveItem(pc, 10027900, 1);
                                    Say(pc, 121, "來看一下!這是『飛空庭引擎』啊!$R;" +
                                        "$R用壓縮的氫氣為發動原料$R是的機械時代的出土品!$R;");
                                    Say(pc, 135, "收到了『飛空庭引擎』!$R;");
                                    return;
                                }
                                Say(pc, 121, "行李好像滿了!$R把行李減少後再來吧!$R;");
                                break;
                        }
                        return;
                    }
                    /*int y = int.Parse(DateTime.Now.Year.ToString("d"));
                    int m = int.Parse(DateTime.Now.Month.ToString("d"));
                    int d = int.Parse(DateTime.Now.Day.ToString("d"));
                    if (m == 1) m = 13;
                    if (m == 2) m = 14;*/
                    //int week = ((d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) / 7) % 5 + 1; 
                    int week = (int)DateTime.Today.DayOfWeek;
                    //int eday = DateTime.Now.DayOfYear;
                    //int i = (((eday + 3) / 7) % 5) + 1;
                    //switch (i)
                    switch (week)
                    {
                        case 1:
                            if (FKT_BJ.Test(FKTBJ.領取飛空庭齒輪))//_Xb13)
                            {
                                Say(pc, 121, "嗯?$R今天給您的，都已經轉交了嗎?$R;");
                                return;
                            }
                            if (CheckInventory(pc, 10028200, 1))
                            {
                                FKT_BJ.SetValue(FKTBJ.領取飛空庭齒輪, true);
                                //_Xb13 = true;
                                GiveItem(pc, 10028200, 1);
                                Say(pc, 121, "來看一下!是『飛空庭齒輪』啊!$R;" +
                                    "$R這是連接旋轉帆和引擎的$R重要部件!$R;");
                                Say(pc, 135, "收到了『飛空庭齒輪』!$R;");
                                return;
                            }
                            Say(pc, 121, "行李好像滿了!$R把行李減少後再來吧!$R;");
                            break;
                        case 2:
                            if (FKT_BJ.Test(FKTBJ.領取舵輪))//_Xb14)
                            {
                                Say(pc, 121, "嗯?$R今天給您的，都已經轉交了嗎?$R;");
                                return;
                            }
                            if (CheckInventory(pc, 10028900, 1))
                            {
                                FKT_BJ.SetValue(FKTBJ.領取舵輪, true);
                                //_Xb14 = true;
                                GiveItem(pc, 10028900, 1);
                                Say(pc, 121, "來看一下!是『舵輪』啊!$R;" +
                                    "$R這是調整飛空庭的部件$R當然控制台也裝有自動導航裝置$R是重要部件!$R;");
                                Say(pc, 135, "收到了『舵輪』!$R;");
                                return;
                            }
                            Say(pc, 121, "行李好像滿了!$R把行李減少後再來吧!$R;");
                            break;
                        case 3:
                            if (FKT_BJ.Test(FKTBJ.領取催化劑))//_Xb15)
                            {
                                Say(pc, 121, "嗯?$R今天給您的，都已經轉交了嗎?$R;");
                                return;
                            }
                            if (CheckInventory(pc, 10027600, 1))
                            {
                                FKT_BJ.SetValue(FKTBJ.領取催化劑, true);
                                //_Xb15 = true;
                                GiveItem(pc, 10027600, 1);
                                Say(pc, 121, "來看一下!是『催化劑』啊!$R;" +
                                    "$R這是把引擎中排出的水蒸氣還原$R是使引擎半永久性使用的部件$R;");
                                Say(pc, 135, "收到了『催化劑』!$R;");
                                return;
                            }
                            Say(pc, 121, "行李好像滿了!$R把行李減少後再來吧!$R;");
                            break;
                        case 4:
                            if (FKT_BJ.Test(FKTBJ.領取飛空庭引擎))//_Xb16)
                            {
                                Say(pc, 121, "嗯?$R今天給您的，都已經轉交了嗎?$R;");
                                return;
                            }
                            if (CheckInventory(pc, 10027900, 1))
                            {
                                FKT_BJ.SetValue(FKTBJ.領取飛空庭引擎, true);
                               // _Xb16 = true;
                                GiveItem(pc, 10027900, 1);
                                Say(pc, 121, "來看一下!這是『飛空庭引擎』啊!$R;" +
                                    "$R用壓縮的氫氣為發動原料$R是的機械時代的出土品!$R;");
                                Say(pc, 135, "收到了『飛空庭引擎』!$R;");
                                return;
                            }
                            Say(pc, 121, "行李好像滿了!$R把行李減少後再來吧!$R;");
                            break;
                        case 5:
                            if (FKT_BJ.Test(FKTBJ.領取飛空庭基臺))//_Xb12)
                            {
                                Say(pc, 121, "嗯?$R今天給您的，都已經轉交了嗎?$R;");
                                return;
                            }
                            if (CheckInventory(pc, 10027300, 1))
                            {
                                FKT_BJ.SetValue(FKTBJ.領取飛空庭基臺, true);
                                //_Xb12 = true;
                                GiveItem(pc, 10027300, 1);
                                Say(pc, 121, "來看一下!是『飛空庭基臺』啊!$R;" +
                                    "$R這是成為飛空庭機身的重要部件!$R;");
                                Say(pc, 135, "收到了『飛空庭基臺』!$R;");
                                return;
                            }
                            Say(pc, 121, "行李好像滿了!$R把行李減少後再來吧!$R;");
                            break;
                    }
                    break;
            }
        }
    }
}