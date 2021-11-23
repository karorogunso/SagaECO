using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001788 : Event
    {
        public S11001788()
        {
            this.EventID = 11001788;
        }

        public override void OnEvent(ActorPC pc)
        {
            //int selection;
            //if (CountItem(pc, 10066400) >= 1 && CountItem(pc, 10067000) >= 1 && CountItem(pc, 10066600) >= 1)
            //{
            //    if (pc.ECoin >= 20000)
            //    {
            //        if (CountItem(pc, 10066700) >= 1 && CountItem(pc, 10066800) >= 1)//依代の鉄布 天霊珠 
            //        {
            //            if (Select(pc, "换么？", "", "换", "不换") == 1)
            //            {
            //                selection = Global.Random.Next(1, 3);
            //                switch (selection)
            //                {
            //                    case 1:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066700, 1);
            //                        TakeItem(pc, 10066800, 1);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10067900, 1);//片手剣
            //                        return;

            //                    case 2:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066700, 1);
            //                        TakeItem(pc, 10066800, 1);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10068100, 1);//短剣
            //                        return;
            //                    case 3:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066700, 1);
            //                        TakeItem(pc, 10066800, 1);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10069500, 1);//投擲
            //                        return;
            //                }
            //            }
            //        }
            //        if (CountItem(pc, 10066700) >= 1 && CountItem(pc, 10066900) >= 1)//依代の鉄布 夢幻の焔 
            //        {
            //            if (Select(pc, "换么？", "", "换", "不换") == 1)
            //            {
            //                selection = Global.Random.Next(1, 3);
            //                switch (selection)
            //                {
            //                    case 1:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066700, 1);
            //                        TakeItem(pc, 10066900, 1);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10068300, 1);//片手斧
            //                        return;
            //                    case 2:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066700, 1);
            //                        TakeItem(pc, 10066900, 1);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10068000, 1);//細剣
            //                        return;
            //                    case 3:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066700, 1);
            //                        TakeItem(pc, 10066900, 1);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10069100, 1);//銃
            //                        return;

            //                }
            //            }
            //        }

            //        if (CountItem(pc, 10066800) >= 1 && CountItem(pc, 10066900) >= 1)//天霊珠 夢幻の焔 
            //        {
            //            if (Select(pc, "换么？", "", "换", "不换") == 1)
            //            {
            //                selection = Global.Random.Next(1, 3);
            //                switch (selection)
            //                {
            //                    case 1:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066800, 1);
            //                        TakeItem(pc, 10066900, 1);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10068500, 1);//片手棒
            //                        return;
            //                    case 2:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066800, 1);
            //                        TakeItem(pc, 10066900, 1);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10068700, 1);//杖
            //                        return;
            //                    case 3:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066800, 1);
            //                        TakeItem(pc, 10066900, 1);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10069000, 1);//弩
            //                        return;

            //                }
            //            }
            //        }

            //        if (CountItem(pc, 10066700) >= 2)//依代の鉄布 依代の鉄布 
            //        {
            //            if (Select(pc, "换么？", "", "换", "不换") == 1)
            //            {
            //                selection = Global.Random.Next(1, 3);
            //                switch (selection)
            //                {
            //                    case 1:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066700, 2);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10068600, 1);//両手棒
            //                        return;
            //                    case 2:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066700, 2);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10068200, 1);//爪
            //                        return;
            //                    case 3:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066700, 2);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10069200, 1);//ライフル
            //                        return;

            //                }
            //            }
            //        }

            //        if (CountItem(pc, 10066800) >= 2)//天霊珠 天霊珠 
            //        {
            //            if (Select(pc, "换么？", "", "换", "不换") == 1)
            //            {
            //                selection = Global.Random.Next(1, 3);
            //                switch (selection)
            //                {
            //                    case 1:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066800, 2);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10068400, 1);//両手斧
            //                        return;

            //                    case 2:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066800, 2);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10069300, 1);//鞭
            //                        return;
            //                    case 3:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066800, 2);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10068900, 1);//弓
            //                        return;

            //                }
            //            }
            //        }
            //        if (CountItem(pc, 10066900) >= 2)//夢幻の焔 夢幻の焔 
            //        {
            //            if (Select(pc, "换么？", "", "换", "不换") == 1)
            //            {
            //                selection = Global.Random.Next(1, 3);
            //                switch (selection)
            //                {
            //                    case 1:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066900, 2);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10069400, 1);//楽器
            //                        return;

            //                    case 2:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066900, 2);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10068800, 1);//本
            //                        return;
            //                    case 3:
            //                        TakeItem(pc, 10066400, 1);
            //                        TakeItem(pc, 10067000, 1);
            //                        TakeItem(pc, 10066600, 1);
            //                        TakeItem(pc, 10066900, 2);
            //                        pc.ECoin -= 20000;
            //                        GiveItem(pc, 10069600, 1);//カード
            //                        return;
            //                }
            //            }
            //        }
            //        Say(pc, 0, "材料不足呢!$R;", "ローウェン");
            //        return;
            //    }
            //    Say(pc, 0, "ecoin好像不够呢!$R;", "ローウェン");
            //    return;
            //}







            int selection;
            if (CountItem(pc, 10066400) >= 1 && CountItem(pc, 10066600) >= 1 && CountItem(pc, 10067000) >= 1)
            {
                
                if (pc.ECoin >= 20000)
                {
                    
                    Say(pc, 0, "彼此都有自己要守护的东西$R;" +
                               "这些东西$R;" +
                               "应该对你有用吧$R;" +
                               "我也要拿我需要的东西", "哈根");
                    Say(pc, 0, "那么,你想怎么挑$R;" +
                               "反正都是些垃圾$R;" +
                               "你给我拿点对我更有用的东西...$P;" +
                               "(哈根对着这个诡异的空间四周看了看)$P;"+
                               "那我就花点时间给你搞搞分类$R;", "哈根");
                    switch(Select(pc, "交换古代武器吗？", "", "完全随机交换","分类交换" ,"不换"))
                    {
                        case 1:
                            selection = Global.Random.Next(1, 18);
                            switch (selection)
                            {
                            case 1:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10067900, 1);//单手剑
                                break;
                            case 2:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10068100, 1);//匕首
                                break;
                            case 3:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10069500, 1);//投枪
                                break;
                            case 4:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10068300, 1);//单手斧
                                break;
                            case 5:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10068000, 1);//细剑
                                break;
                            case 6:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10069100, 1);//手枪
                                break;
                            case 7:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10068500, 1);//单手锤
                                break;
                            case 8:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10068700, 1);//魔杖
                                break;
                            case 9:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10069000, 1);//弩
                                break;
                            case 10:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10068600, 1);//双手锤
                                break;
                            case 11:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10068200, 1);//爪
                                break;
                            case 12:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10069200, 1);//步枪
                                break;
                            case 13:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10068400, 1);//双手斧
                                break;
                            case 14:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10069300, 1);//鞭子
                                break;
                            case 15:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10068900, 1);//弓
                                break;
                            case 16:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10069400, 1);//乐器
                                break;
                            case 17:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10068800, 1);//书
                                break;
                            case 18:
                                TakeItem(pc, 10066400, 1);
                                TakeItem(pc, 10067000, 1);
                                TakeItem(pc, 10066600, 1);
                                pc.ECoin -= 20000;
                                GiveItem(pc, 10069600, 1);//卡
                                break;
                            }
                            break;
                        case 2:
                            //int selection;
                            if (CountItem(pc, 10066400) >= 1 && CountItem(pc, 10067000) >= 1 && CountItem(pc, 10066600) >= 1)
                            {
                                if (pc.ECoin >= 20000)
                                {

                                    Say(pc, 0, "那么,我说明一下$R;" +
                                               "第一组是单手剑,短剑,投掷武器$R;" +
                                               "这组需要神灵附身的铁布和天灵珠$R;" +
                                               "第二组是单手斧,细剑,还有手枪$R;" +
                                               "这组需要神灵附身的铁布和梦幻之焰$R;" , "哈根");
                                    Say(pc, 0, "第三组是单手锤,魔杖,以及弩$R;" +
                                               "这组需要梦幻之焰和天灵珠$R;" +
                                               "第四组是双手锤,爪,以及步枪$R;" +
                                               "这组需要两个神灵附身的铁布$R;", "哈根");
                                    Say(pc, 0, "第五组是双手斧,鞭子,以及弓$R;" +
                                               "这组需要两个天灵珠$R;" +
                                               "第六组是琴,书,以及..卡$R;" +
                                               "这组需要两个梦幻之焰$R;", "哈根");
                                    switch (Select(pc, "交换古代武器吗？", "", "第一组", "第二组", "第三组", "第四组", "第五组", "第六组", "不换"))
                                    {
                                        case 1:
                                            if (CountItem(pc, 10066700) >= 1 && CountItem(pc, 10066800) >= 1)//依代の鉄布 天霊珠 
                                            {
                                                if (Select(pc, "换么？", "", "换", "不换") == 1)
                                                {
                                                    selection = Global.Random.Next(1, 3);
                                                    switch (selection)
                                                    {
                                                        case 1:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066700, 1);
                                                            TakeItem(pc, 10066800, 1);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10067900, 1);//片手剣
                                                            return;

                                                        case 2:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066700, 1);
                                                            TakeItem(pc, 10066800, 1);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10068100, 1);//短剣
                                                            return;
                                                        case 3:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066700, 1);
                                                            TakeItem(pc, 10066800, 1);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10069500, 1);//投擲
                                                            return;
                                                    }
                                                }
                                            }
                                            Say(pc, 0, "没材料就别浪费我时间啦$R;", "哈根");
                                            break;
                                        case 2:
                                            if (CountItem(pc, 10066700) >= 1 && CountItem(pc, 10066900) >= 1)//依代の鉄布 夢幻の焔 
                                            {
                                                if (Select(pc, "换么？", "", "换", "不换") == 1)
                                                {
                                                    selection = Global.Random.Next(1, 3);
                                                    switch (selection)
                                                    {
                                                        case 1:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066700, 1);
                                                            TakeItem(pc, 10066900, 1);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10068300, 1);//片手斧
                                                            return;
                                                        case 2:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066700, 1);
                                                            TakeItem(pc, 10066900, 1);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10068000, 1);//細剣
                                                            return;
                                                        case 3:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066700, 1);
                                                            TakeItem(pc, 10066900, 1);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10069100, 1);//銃
                                                            return;

                                                    }
                                                }
                                            }
                                            Say(pc, 0, "没材料就别浪费我时间啦$R;", "哈根");
                                            break;
                                        case 3:
                                            if (CountItem(pc, 10066800) >= 1 && CountItem(pc, 10066900) >= 1)//天霊珠 夢幻の焔 
                                            {
                                                if (Select(pc, "换么？", "", "换", "不换") == 1)
                                                {
                                                    selection = Global.Random.Next(1, 3);
                                                    switch (selection)
                                                    {
                                                        case 1:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066800, 1);
                                                            TakeItem(pc, 10066900, 1);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10068500, 1);//片手棒
                                                            return;
                                                        case 2:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066800, 1);
                                                            TakeItem(pc, 10066900, 1);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10068700, 1);//杖
                                                            return;
                                                        case 3:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066800, 1);
                                                            TakeItem(pc, 10066900, 1);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10069000, 1);//弩
                                                            return;

                                                    }
                                                }
                                            }
                                            Say(pc, 0, "没材料就别浪费我时间啦$R;", "哈根");
                                            break;
                                        case 4:
                                            if (CountItem(pc, 10066700) >= 2)//依代の鉄布 依代の鉄布 
                                            {
                                                if (Select(pc, "换么？", "", "换", "不换") == 1)
                                                {
                                                    selection = Global.Random.Next(1, 3);
                                                    switch (selection)
                                                    {
                                                        case 1:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066700, 2);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10068600, 1);//両手棒
                                                            return;
                                                        case 2:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066700, 2);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10068200, 1);//爪
                                                            return;
                                                        case 3:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066700, 2);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10069200, 1);//ライフル
                                                            return;

                                                    }
                                                }
                                            }
                                            Say(pc, 0, "没材料就别浪费我时间啦$R;", "哈根");
                                            break;
                                        case 5:
                                            if (CountItem(pc, 10066800) >= 2)//天霊珠 天霊珠 
                                            {
                                                if (Select(pc, "换么？", "", "换", "不换") == 1)
                                                {
                                                    selection = Global.Random.Next(1, 3);
                                                    switch (selection)
                                                    {
                                                        case 1:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066800, 2);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10068400, 1);//両手斧
                                                            return;

                                                        case 2:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066800, 2);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10069300, 1);//鞭
                                                            return;
                                                        case 3:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066800, 2);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10068900, 1);//弓
                                                            return;

                                                    }
                                                }
                                            }
                                            Say(pc, 0, "没材料就别浪费我时间啦$R;", "哈根");
                                            break;
                                        case 6:
                                            if (CountItem(pc, 10066900) >= 2)//夢幻の焔 夢幻の焔 
                                            {
                                                if (Select(pc, "换么？", "", "换", "不换") == 1)
                                                {
                                                    selection = Global.Random.Next(1, 3);
                                                    switch (selection)
                                                    {
                                                        case 1:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066900, 2);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10069400, 1);//楽器
                                                            return;

                                                        case 2:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066900, 2);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10068800, 1);//本
                                                            return;
                                                        case 3:
                                                            TakeItem(pc, 10066400, 1);
                                                            TakeItem(pc, 10067000, 1);
                                                            TakeItem(pc, 10066600, 1);
                                                            TakeItem(pc, 10066900, 2);
                                                            pc.ECoin -= 20000;
                                                            GiveItem(pc, 10069600, 1);//カード
                                                            return;
                                                    }
                                                }
                                            }
                                            Say(pc, 0, "没材料就别浪费我时间啦$R;", "哈根");
                                            break;
                                        default:
                                            break;
                                    }
                                    //Say(pc, 0, "材料不足呢!$R;", "哈根");
                                    return;
                                }
                                Say(pc, 0, "ecoin好像不够呢!$R;", "哈根");
                                return;
                            }
                            break;
                        default:
                            break;  
                     }
                    
                        
                        
                    Say(pc, 0, "...梅露提...$R;" +
                               "不用多久了..$R;", "哈根");
                    return;
                }
                Say(pc, 0, "喂喂,ecoin好像不够啊!$R;", "哈根");
                return;
            }
            Say(pc, 0, "哎…我在干嘛？$R;" +
                        "$P我现在啊、正在找好的素材$R;" +
                        "忙得很呢。$R;" +
                        "$R有什么话,以后再说吧。$R;", "ハガン");
            Say(pc, 0, "什么,这些东西上哪去找?$R;" +
                        "虽然这些东西在三个种族各自的世界也有$R;" +
                        "你在深渊深处应该能找到那些迷失的人们$R;" +
                        "他们也经常能拿出一些难以置信的东西$R;" +
                        "试着去问问吧$R;", "哈根");
            //Say(pc, 0, "ん…何か用か？$R;" +
            //"$P俺は今、良質の素材を探すので$R;" +
            //"忙しいんだ。$R;" +
            //"$R話があるなら、後にしてくれ。$R;", "ハガン");
        }
    }
}