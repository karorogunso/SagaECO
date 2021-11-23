using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Map;
using SagaMap.Network.Client;
using SagaMap.Scripting;
using SagaMap.Manager;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class S90000212 : SagaMap.Scripting.Item
    {
        public S90000212()
        {
            Init(90000212, delegate(ActorPC pc)
            {
                ShowEffect(pc, 4023);
                if (CheckMapFlag(pc.SaveMap, MapFlags.Dominion) != CheckMapFlag(pc.MapID, MapFlags.Dominion))
                {
                    Say(pc, 131, "目前您的存储点在泰达尼亚世界.$R无法使用这个道具.", "时空之钥E");
                    return;
                }
                else
                {
                    if (CheckMapFlag(pc.MapID, MapFlags.Dominion))
                    {
                        Say(pc, 131, "角色在多米尼翁世界.$R无法使用这个道具", "时空之钥E");
                        return;
                    }
                    else
                    {
                        int a = 0;
                        a = Global.Random.Next(1, 30);
                        if(a==30)
                        {
                            Say(pc, 0, 0, "时空之钥E开始散发出奇怪的"+
                                          "白色气息...");
                            Say(pc, 0, 0, "....不应该是这样的...$R;", "女孩子的声音");
                            Say(pc, 0, 0, "!?$R;", pc.Name);
                            //ShowEffect(pc, 4555);
                            switch (Select(pc, "好像有点危险,要怎么办呢?", "", "立刻摧毁这个时空之钥E","听天由命"))
                            {
                                case 1:
                                    TakeItem(pc, 16000300, 1);
                                    Say(pc, 0, 0, "时空之钥E被摧毁了$R;");
                                    break;
                                case 2:
                                    TakeItem(pc, 16000300, 1);
                                    Say(pc, 0, 0, "呜啊啊啊啊!!!!$R;",pc.Name);
                                    ShowEffect(pc, 5604);
                                    Warp(pc, 20015001, 9, 7);
                                    break;
                            }
                            return;
                        }
                        switch (Select(pc, "要去哪儿呢?", "", "阿克罗波利斯上层中心", "城市传送", "地图传送", "指定NPC", "斗技场", "通天塔", "玛依玛依岛","ECO城",  "取消"))
                        {
                            case 1:
                                TakeItem(pc, 16000300, 1);
                                Warp(pc, 10023000, 132, 151);
                                break;
                            case 2:
                                switch (Select(pc, "要去哪儿呢?", "", "法伊斯特市", "唐卡島", "摩戈", "诺森长廊", "阿克罗尼亚东部吊桥", "阿克罗尼亚南部吊桥", "阿克罗尼亚西部吊桥", "阿克罗尼亚北部吊桥", "东方海角", "法伊斯特街道(飞空匠人)", "诺森海滨长廊", "军舰岛", "谜团", "艾恩萨乌斯", "艾尔·夏尔（下层）", "取消"))
                                {
                                    case 1:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10057000, 9, 123);
                                        break;
                                    case 2:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10062000, 71, 201);
                                        break;
                                    case 3:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10060000, 47, 138);
                                        break;
                                    case 4:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10065000, 52, 127);
                                        break;
                                    case 5:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10023100, 239, 127);
                                        break;
                                    case 6:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10023300, 127, 245);
                                        break;
                                    case 7:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10023200, 10, 127);
                                        break;
                                    case 8:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10023400, 127, 9);
                                        break;
                                    case 9:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10018100, 202, 68);
                                        break;
                                    case 10:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10017000, 148, 210);
                                        break;
                                    case 11:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10065000, 52, 195);
                                        break;
                                    case 12:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10035000, 245, 2);
                                        break;
                                    case 13:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10054000, 186, 138);
                                        break;
                                    case 14:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10063100, 123, 147);
                                        break;
                                    case 15:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 11024000, 52, 126);
                                        break;
                                    case 16:
                                        break;
                                }
                                break;
                            case 3:
                                switch (Select(pc, "要去哪儿呢?", "", "阿克罗尼亚丛林", "斯诺普山道", "北方海角", "北方中央山脉", "通往永恒的北方极地", "阿克罗尼亚海岸", "不死皇城", "鬼之侵岩", "南方海角", "阿克罗尼亚北部平原", "阿克罗尼亚东部平原", "阿克罗尼亚西部平原", "阿克罗尼亚南部平原", "果树森林", "杀人蜂山路", "不死岛", "南部地牢3F", "南部地牢2F", "南部地牢1F", "南牢码头", "东牢", "冰坑b1f", "北牢", "东牢", "光塔1f", "大陆洞b1f", "取消"))
                                {

                                    case 1:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10015000, 152, 143);
                                        break;
                                    case 2:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10003000, 81, 249);
                                        break;
                                    case 3:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10001000, 84, 35);
                                        break;
                                    case 4:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10050000, 54, 253);
                                        break;
                                    case 5:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10051000, 92, 127);
                                        break;
                                    case 6:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10034000, 4, 222);
                                        break;
                                    case 7:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10069000, 145, 208);
                                        break;
                                    case 8:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10061000, 188, 251);
                                        break;
                                    case 9:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10046000, 197, 251);
                                        break;
                                    case 10:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10014000, 128, 252);
                                        break;
                                    case 11:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10025000, 4, 127);
                                        break;
                                    case 12:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10022000, 252, 128);
                                        break;
                                    case 13:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10031000, 157, 252);
                                        break;
                                    case 14:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10030000, 217, 250);
                                        break;
                                    case 15:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10020000, 252, 158);
                                        break;
                                    case 16:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10019000, 253, 169);
                                        break;
                                    case 17:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 20022000, 128, 129);
                                        break;
                                    case 18:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 20021000, 124, 144);
                                        break;
                                    case 19:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 20020000, 127, 129);
                                        break;
                                    case 20:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 20023000, 150, 44);
                                        break;
                                    case 21:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 20090000, 95, 2);
                                        break;
                                    case 22:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 20010000, 63, 12);
                                        break;
                                    case 23:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 20014000, 241, 12);
                                        break;
                                    case 24:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 20090000, 112, 253);
                                        break;
                                    case 25:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 20140000, 37, 52);
                                        break;
                                    case 26:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 20000000, 63, 23);
                                        break;
                                    case 27:
                                        break;
                                }
                                break;
                            case 4:
                                switch (Select(pc, "要去哪儿呢?", "", "情人教堂", "圣女的家", "遗广", "贤者之家", "妈妈(法伊斯特)", "取消"))
                                {
                                    case 1:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 30122000, 11, 20);
                                        break;
                                    case 2:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 30090001, 4, 6);
                                        break;
                                    case 3:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 20080012, 25, 26);
                                        break;
                                    case 4:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 30022000, 3, 4);
                                        break;
                                    case 5:
                                        TakeItem(pc, 16000300, 1);
                                        Warp(pc, 10057000, 64, 140);
                                        break;
                                    case 6:
                                        break;
                                }
                                break;
                            case 5:
                                TakeItem(pc, 16000300, 1);
                                Warp(pc, 20080000, 25, 26);
                                break;
                            case 6:
                                TakeItem(pc, 16000300, 1);
                                Warp(pc, 10058000, 127, 160);
                                break;
                            case 7:
                                TakeItem(pc, 16000300, 1);
                                Warp(pc, 10059000, 68, 147);
                                break;
                            case 8:
                                {
                                    switch (Select(pc, "要去哪儿呢?", "", "ECO城", "口内渊", "深渊·谜", "深渊", "取消"))
                                    {
                                        case 1:
                                            TakeItem(pc, 16000300, 1);
                                            Warp(pc, 11027000, 252, 7);
                                            break;
                                        case 2:
                                            TakeItem(pc, 16000300, 1);
                                            Warp(pc, 21190000, 30, 30);
                                            break;
                                        case 3:
                                            TakeItem(pc, 16000300, 1);
                                            Warp(pc, 21195000, 68, 67);
                                            break;
                                        case 4:
                                            TakeItem(pc, 16000300, 1);
                                            Warp(pc, 21193000, 127, 240);
                                            break;
                                        case 5:
                                            return;
                                    }
                                    break;
                                }
                            //case 9:
                            //    {
                            //        switch (Select(pc, "要去哪儿呢?", "", "剧情用方舟大厅", "-----------------------------", "远古方舟大厅1", "远古方舟大厅2", "远古方舟大厅3", "远古方舟大厅4", "远古方舟大厅5", "远古方舟大厅6", "远古方舟大厅7", "远古方舟大厅8", "远古方舟大厅9", "远古方舟大厅10", "-----------------------------", "地图案1", "地图案2", "地图案3", "地图案4", "地图案5", "地图案6", "EABOSS地图", "财宝房间", "退出"))
                            //        {
                            //            case 1:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 50161000, 64, 107);
                            //                    break;
                            //                }
                            //            case 2:
                            //                {
                            //                    break;
                            //                }
                            //            case 3:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 30210000, 64, 107);
                            //                    break;
                            //                }
                            //            case 4:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 30210001, 64, 107);
                            //                    break;
                            //                }
                            //            case 5:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 30210002, 64, 107);
                            //                    break;
                            //                }
                            //            case 6:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 30210003, 64, 107);
                            //                    break;
                            //                }
                            //            case 7:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 30210004, 64, 107);
                            //                    break;
                            //                }
                            //            case 8:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 30210005, 64, 107);
                            //                    break;
                            //                }
                            //            case 9:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 30210006, 64, 107);
                            //                    break;
                            //                }
                            //            case 10:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 30210007, 64, 107);
                            //                    break;
                            //                }
                            //            case 11:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 30210008, 64, 107);
                            //                    break;
                            //                }
                            //            case 12:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 30210009, 64, 107);
                            //                    break;
                            //                }
                            //            case 13:
                            //                {
                            //                    break;
                            //                }
                            //            case 14:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 80200000, 7, 3);
                            //                    break;
                            //                }
                            //            case 15:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 80210000, 5, 4);
                            //                    break;
                            //                }
                            //            case 16:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 80220000, 11, 4);
                            //                    break;
                            //                }
                            //            case 17:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 80230000, 10, 10);
                            //                    break;
                            //                }
                            //            case 18:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 80240000, 10, 10);
                            //                    break;
                            //                }
                            //            case 19:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 80250000, 6, 5);
                            //                    break;
                            //                }
                            //            case 20:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 80260000, 32, 31);
                            //                    break;
                            //                }
                            //            case 21:
                            //                {
                            //                    TakeItem(pc, 16000300, 1);
                            //                    Warp(pc, 80270000, 32, 31);
                            //                    break;
                            //                }
                            //            case 22:
                            //                {
                            //                    return;
                            //                }
                            //        }
                            //    }
                            //    break;
                            //case 10:
                                //{
                                //    switch (Select(pc, "时空之钥E", "", "危险的森林", "取消"))
                                //    {
                                //        case 1:
                                //            {
                                //                //10022900
                                //                Say(pc, 0, "这是特殊的地图,$R这需要大量金钱.$R$R需要10000G, 购买吗?", "危险的森林");
                                //                switch (Select(pc, "时空之钥E", "", "去啊!", "再想想..."))
                                //                {
                                //                    case 1:
                                //                        {
                                //                            if (pc.Gold > 10000)
                                //                            {
                                //                                Warp(pc, 10032002, 40, 40);
                                //                                GiveItem(pc, 10022900, 1);
                                //                                pc.Gold = pc.Gold - 10000;
                                //                            }
                                //                            else
                                //                            {
                                //                                ShowEffect(pc, 4023);
                                //                                Say(pc, 0, "钱!!!!!!!!$R不够啊QAQ!", "危险的森林");
                                //                            }
                                //                            return;
                                //                        }
                                //                    case 2:
                                //                        {
                                //                            return;
                                //                        }
                                //                }
                                //                break;
                                //            }
                                //        case 2:
                                //            return;
                                //    }
                                //    break;
                                //}
                            case 9:
                                {
                                    return;
                                }
                        }
                        
                    }
                }
                
            });
        }
    }
}
