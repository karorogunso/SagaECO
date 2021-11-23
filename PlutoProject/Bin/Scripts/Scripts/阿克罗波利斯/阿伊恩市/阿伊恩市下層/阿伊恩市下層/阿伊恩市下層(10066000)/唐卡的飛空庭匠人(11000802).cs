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
                Say(pc, 121, "您好!我是从唐卡来的$R飞空庭工匠的「飞空庭师」$R;" +
                    "$P好像这次航空规则完善了$R看阿克罗尼亚都这么和平$R应该是理所当然的$R;" +
                    "$P所以我为了飞空庭的普及$R从唐卡派遣过来的$R;");
                if (pc.Level > 29 && pc.Fame > 9)
                {
                    Say(pc, 121, "我现在$R把飞空庭的部件都发给大家!$R当然是免费的!$R;" +
                        "$P飞空庭部件虽然珍贵$R但很重，很难搬动$R所以领取的时候会比较慢$R所以想要的话，要遵守秩序喔$R;" +
                        "$P嗯？为什么是免费的?$R;" +
                        "$R嗯!先说好喔$R所有飞空庭必须以唐卡为据点$R;" +
                        "$P是为了要防止有人不在唐卡登记$R偷偷的制造飞空庭$R;" +
                        "$R当然装备或改造飞空庭会牵涉利益$R所以先想清楚啊$R;" +
                        "$P我们每天会发送库存部件一次$R所以每天过来一次吧!$R;" +
                        "$P还有，并不是所有的部件都给喔$R因为运输费太贵了!$R;" +
                        "$R在这大陆上能简单制作的$P就自己制作看看吧$R;");
                    mask.SetValue(AYEFlags.允許領取, true);
                    //_6A13 = true;
                }
                else
                {
                    Say(pc, 121, "嗯…$R;" +
                        "$R再深入的消息，$R等您变得更强后$R再告诉您吧$R;");
                    return;
                }
            }
            Say(pc, 121, "需要飞空庭的部件吗?$R;" +
                "$R啊!因为部件太重$R收到以后可能无法动弹的$R所以把行李减少后，再来比吧$R;");
            switch (Select(pc, "接受飞空庭的部件吗?", "", "接受", "放弃"))
            {
                case 1:
                    if (pc.Account.GMLevel >= 100)
                    {
                        switch (Select(pc, "管理者模式(除错 注意！)", "", "第1周", "第2周", "第3周", "第4周", "第5周", "取消"))
                        {
                            case 1:
                                if (FKT_BJ.Test(FKTBJ.領取飛空庭基臺))//_Xb12)
                                {
                                    Say(pc, 121, "嗯?$R今天给您的，都已经转交了吗?$R;");
                                    return;
                                }
                                if (CheckInventory(pc, 10027300, 1))
                                {
                                    FKT_BJ.SetValue(FKTBJ.領取飛空庭基臺, true);
                                    //_Xb12 = true;
                                    GiveItem(pc, 10027300, 1);
                                    Say(pc, 121, "来看一下!是『飞空庭底座』啊!$R;" +
                                        "$R这是成为飞空庭机身的重要部件!$R;");
                                    Say(pc, 135, "收到了『飞空庭底座』!$R;");
                                    return;
                                }
                                Say(pc, 121, "行李好像满了!$R把行李减少后再来吧!$R;");
                                break;
                            case 2:
                                if (FKT_BJ.Test(FKTBJ.領取飛空庭齒輪))//_Xb13)
                                {
                                    Say(pc, 121, "嗯?$R今天给您的，都已经转交了吗?$R;");
                                    return;
                                }
                                if (CheckInventory(pc, 10028200, 1))
                                {
                                    FKT_BJ.SetValue(FKTBJ.領取飛空庭齒輪, true);
                                    //_Xb13 = true;
                                    GiveItem(pc, 10028200, 1);
                                    Say(pc, 121, "来看一下!是『飞空庭的绞车』啊!$R;" +
                                        "$R这是连接旋转帆和引擎的$R重要部件!$R;");
                                    Say(pc, 135, "收到了『飞空庭的绞车』!$R;");
                                    return;
                                }
                                Say(pc, 121, "行李好像满了!$R把行李减少后再来吧!$R;");
                                break;
                            case 3:
                                if (FKT_BJ.Test(FKTBJ.領取舵輪))//_Xb14)
                                {
                                    Say(pc, 121, "嗯?$R今天给您的，都已经转交了吗?$R;");
                                    return;
                                }
                                if (CheckInventory(pc, 10028900, 1))
                                {
                                    FKT_BJ.SetValue(FKTBJ.領取舵輪, true);
                                    //_Xb14 = true;
                                    GiveItem(pc, 10028900, 1);
                                    Say(pc, 121, "来看一下!是『操舵轮』啊!$R;" +
                                        "$R这是调整飞空庭的部件$R当然控制台也装有自动导航装置$R是重要部件!$R;");
                                    Say(pc, 135, "收到了『操舵轮』!$R;");
                                    return;
                                }
                                Say(pc, 121, "行李好像满了!$R把行李减少后再来吧!$R;");
                                break;
                            case 4:
                                if (FKT_BJ.Test(FKTBJ.領取催化劑))//_Xb15)
                                {
                                    Say(pc, 121, "嗯?$R今天给您的，都已经转交了吗?$R;");
                                    return;
                                }
                                if (CheckInventory(pc, 10027600, 1))
                                {
                                    FKT_BJ.SetValue(FKTBJ.領取催化劑, true);
                                    //_Xb15 = true;
                                    GiveItem(pc, 10027600, 1);
                                    Say(pc, 121, "来看一下!是『触媒』啊!$R;" +
                                        "$R这是把引擎中排出的水蒸气还原$R是使引擎半永久性使用的部件$R;");
                                    Say(pc, 135, "收到了『触媒』!$R;");
                                    return;
                                }
                                Say(pc, 121, "行李好像满了!$R把行李减少后再来吧!$R;");
                                break;
                            case 5:
                                if (FKT_BJ.Test(FKTBJ.領取飛空庭引擎))//_Xb16)
                                {
                                    Say(pc, 121, "嗯?$R今天给您的，都已经转交了吗?$R;");
                                    return;
                                }
                                if (CheckInventory(pc, 10027900, 1))
                                {
                                    FKT_BJ.SetValue(FKTBJ.領取飛空庭引擎, true);
                                    // _Xb16 = true;
                                    GiveItem(pc, 10027900, 1);
                                    Say(pc, 121, "来看一下!这是『飞空庭引擎』啊!$R;" +
                                        "$R用压缩的氢气为发动原料$R是的机械时代的出土品!$R;");
                                    Say(pc, 135, "收到了『飞空庭引擎』!$R;");
                                    return;
                                }
                                Say(pc, 121, "行李好像满了!$R把行李减少后再来吧!$R;");
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
                                Say(pc, 121, "嗯?$R今天给您的，都已经转交了吗?$R;");
                                return;
                            }
                            if (CheckInventory(pc, 10028200, 1))
                            {
                                FKT_BJ.SetValue(FKTBJ.領取飛空庭齒輪, true);
                                //_Xb13 = true;
                                GiveItem(pc, 10028200, 1);
                                Say(pc, 121, "来看一下!是『飞空庭的绞车』啊!$R;" +
                                    "$R这是连接旋转帆和引擎的$R重要部件!$R;");
                                Say(pc, 135, "收到了『飞空庭的绞车』!$R;");
                                return;
                            }
                            Say(pc, 121, "行李好像满了!$R把行李减少后再来吧!$R;");
                            break;
                        case 2:
                            if (FKT_BJ.Test(FKTBJ.領取舵輪))//_Xb14)
                            {
                                Say(pc, 121, "嗯?$R今天给您的，都已经转交了吗?$R;");
                                return;
                            }
                            if (CheckInventory(pc, 10028900, 1))
                            {
                                FKT_BJ.SetValue(FKTBJ.領取舵輪, true);
                                //_Xb14 = true;
                                GiveItem(pc, 10028900, 1);
                                Say(pc, 121, "来看一下!是『操舵轮』啊!$R;" +
                                    "$R这是调整飞空庭的部件$R当然控制台也装有自动导航装置$R是重要部件!$R;");
                                Say(pc, 135, "收到了『操舵轮』!$R;");
                                return;
                            }
                            Say(pc, 121, "行李好像满了!$R把行李减少后再来吧!$R;");
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
                                Say(pc, 121, "嗯?$R今天给您的，都已经转交了吗?$R;");
                                return;
                            }
                            if (CheckInventory(pc, 10027900, 1))
                            {
                                FKT_BJ.SetValue(FKTBJ.領取飛空庭引擎, true);
                               // _Xb16 = true;
                                GiveItem(pc, 10027900, 1);
                                Say(pc, 121, "来看一下!这是『飞空庭引擎』啊!$R;" +
                                    "$R用压缩的氢气为发动原料$R是的机械时代的出土品!$R;");
                                Say(pc, 135, "收到了『飞空庭引擎』!$R;");
                                return;
                            }
                            Say(pc, 121, "行李好像满了!$R把行李减少后再来吧!$R;");
                            break;
                        case 5:
                            if (FKT_BJ.Test(FKTBJ.領取飛空庭基臺))//_Xb12)
                            {
                                Say(pc, 121, "嗯?$R今天给您的，都已经转交了吗?$R;");
                                return;
                            }
                            if (CheckInventory(pc, 10027300, 1))
                            {
                                FKT_BJ.SetValue(FKTBJ.領取飛空庭基臺, true);
                                //_Xb12 = true;
                                GiveItem(pc, 10027300, 1);
                                Say(pc, 121, "来看一下!是『飞空庭底座』啊!$R;" +
                                    "$R这是成为飞空庭机身的重要部件!$R;");
                                Say(pc, 135, "收到了『飞空庭底座』!$R;");
                                return;
                            }
                            Say(pc, 121, "行李好像满了!$R把行李减少后再来吧!$R;");
                            break;
                    }
                    break;
            }
        }
    }
}