using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000793 : Event
    {
        public S11000793()
        {
            this.EventID = 11000793;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(pc.Fame>=20&&pc.Level>=31)
            {
                if (CountItem(pc, 10052000) >= 1)
                {
                    Say(pc, 131, "嗯？$R带来了『小天马』！$R;" +
                        "$R打算交换一个『微型爬爬虫』吗？$R;");
                    switch (Select(pc, "确定交换?", "", "交换", "才不要！"))
                    {
                        case 1:
                            TakeItem(pc, 10052000, 1);
                            GiveItem(pc, 10007597, 1);
                            break;
                        case 2:
                            break;
                    }
                    
                }
                else if (CountItem(pc, 10006650) >= 1)
                {
                    Say(pc, 131, "嗯？$R带来了『巴乌巴乌』！$R;" +
                        "$R打算交换一个『微型爬爬虫』吗？$R;");
                    switch (Select(pc, "确定交换?", "", "交换", "才不要！"))
                    {
                        case 1:
                            TakeItem(pc, 10006650, 1);
                            GiveItem(pc, 10007597, 1);
                            break;
                        case 2:
                            break;
                    }
                    
                }
                else if (CountItem(pc, 10018310) >= 1)
                {
                    Say(pc, 131, "嗯？$R带来了『洛克鸟』！$R;" +
                        "$R打算交换一个『微型爬爬虫』吗？$R;");
                    switch (Select(pc, "确定交换?", "", "交换", "才不要！"))
                    {
                        case 1:
                            TakeItem(pc, 10018310, 1);
                            GiveItem(pc, 10007597, 1);
                            break;
                        case 2:
                            break;
                    }
                    
                }
                else if (CountItem(pc, 10018310) >= 1)
                {
                    Say(pc, 131, "嗯？$R带来了『哞哞』！$R;" +
                        "$R打算交换一个『微型爬爬虫』吗？$R;");
                    switch (Select(pc, "确定交换?", "", "交换", "才不要！"))
                    {
                        case 1:
                            TakeItem(pc, 10018310, 1);
                            GiveItem(pc, 10007597, 1);
                            break;
                        case 2:
                            break;
                    }
                    
                }
                else if (CountItem(pc, 10048900) >= 1)
                {
                    Say(pc, 131, "嗯？$R带来了『汪汪』！$R;" +
                        "$R打算交换一个『微型爬爬虫』吗？$R;");
                    switch (Select(pc, "确定交换?", "", "交换", "才不要！"))
                    {
                        case 1:
                            TakeItem(pc, 10048900, 1);
                            GiveItem(pc, 10007597, 1);
                            break;
                        case 2:
                            break;
                    }
                    
                }
                Say(pc, 131, "呐！『微型爬爬虫』就交给你了！$R;" +
                                "$R要好好养大哦！$R;");
                return;
                //Say(pc, 131, "養大了可以進化的話$R你可以拜託牛牛草原的寵物飼養員$R;" +
                //    "$P他們會幫您把微型爬爬蟲$R馴服成『騎乘爬爬蟲』的$R;");
            }
            /*
            if (_5A87 && !_Xb00)
            {
                if (_5A88)
                {
                    if (CountItem(pc, 10052000) >= 1)
                    {
                        Say(pc, 131, "哇！$R帶來了約定的『帕格薩斯』啊！$R;" +
                            "$R和『微型爬爬蟲』交換嗎？$R;");
                        交換(pc);
                        return;
                    }
                }
                if (_5A89)
                {
                    if (CountItem(pc, 10006650) >= 1)
                    {
                        Say(pc, 131, "哇！$R帶來了約定的『巴烏巴烏』啊！$R;" +
                            "$R交換『微型爬爬蟲』嗎？$R;");
                        交換(pc);
                        return;
                    }
                }
                if (_5A90)
                {
                    if (CountItem(pc, 10018310) >= 1)
                    {
                        Say(pc, 131, "哇！$R帶來了約定的『洛克鳥』啊！$R;" +
                            "$R交換『微型爬爬蟲』嗎？$R;");
                        交換(pc);
                        return;
                    }
                }
                if (_5A91)
                {
                    if (CountItem(pc, 10052100) >= 1)
                    {
                        Say(pc, 131, "哇！$R帶來了約定的『牛牛』啊！$R;" +
                            "$R交換『微型爬爬蟲』嗎？$R;");
                        交換(pc);
                        return;
                    }
                }
                if (_5A92)
                {
                    if (CountItem(pc, 10048900) >= 1)
                    {
                        Say(pc, 131, "哇！$R帶來了約定的『汪汪』啊！$R;" +
                            "$R交換『微型爬爬蟲』嗎？$R;");
                        交換(pc);
                        return;
                    }
                }
                Say(pc, 131, "怎樣？$R已經找到那寵物了嗎？$R;");
                if (_6A11)
                {
                    return;
                }
                if (_5A88 || _5A89 || _5A90)
                {

                    switch (Select(pc, "找到寵物了嗎?", "           ", "我在認真地找呢！", "找不著"))
                    {
                        case 1:
                            Say(pc, 131, "等您$R;");
                            break;
                        case 2:
                            Say(pc, 131, "那樣？$R有點困難…$R;" +
                                "$P没辦法了$R給您交換別的吧$R;" +
                                "$R明白嗎？就一次！$R;");
                            switch (Select(pc, "要不要換過要交換的寵物?", "        ", "拜託了", "哦！没關係！"))
                            {
                                case 1:
                                    _6A11 = true;
                                    _5A88 = false;
                                    _5A89 = false;
                                    _5A90 = false;
                                    Say(pc, 131, "好啊！那麽…$R;");
                                    int b = Global.Random.Next(1, 2);
                                    switch (b)
                                    {
                                        case 1:
                                            _5A91 = true;
                                            Say(pc, 131, "和牛牛換怎樣？$R;" +
                                                "$R這次一定要加油哦！$R;");
                                            break;
                                        case 2:
                                            _5A92 = true;
                                            Say(pc, 131, "和汪汪交換怎樣？$R;" +
                                                "$R這次一定要加油哦！$R;");
                                            break;
                                    }
                                    break;
                                case 2:
                                    Say(pc, 131, "什麽時候都可以再過來！$R;");
                                    break;
                            }
                            break;
                    }
                }
                return;
            }
            if (_5A85 && !_5A86 && !_Xb00)
            {
                _5A86 = true;
                Say(pc, 131, "説這些爬爬蟲啊？$R;" +
                    "$R啊！好像是從牛牛草原上$R飼養員那裡聽來的？$R;" +
                    "$P對了!$R這孩子們就是要成為『騎乘爬爬蟲』的$R爬爬蟲新品種『微型爬爬蟲』!$R;" +
                    "$R最近開始養的，這些孩子還小呢$R;");
                //EVT1100079301
                switch (Select(pc, "『微型爬爬蟲』…", "          ", "給我一隻！", "什麽都不是"))
                {
                    case 1:
                        Say(pc, 131, "什麽？$R;" +
                            "$R什麽…這麽多$R是可以給您，但是…$R;" +
                            "$R嗯…這樣好了$R;" +
                            "$P我也不能收錢$R那用其他的寵物交換怎樣？$R;");
                        switch (Select(pc, "用寵物交換嗎?", "      ", "換！", "不換！"))
                        {
                            case 1:
                                string STR;
                                if (_5A88 || _5A89 || _5A90 || _5A91 || _5A92)
                                {
                                    if (_5A88)
                                    {
                                        _5A88 = true;
                                        STR = "帕格薩斯交換怎樣？";
                                    }
                                    if (_5A89)
                                    {
                                        _5A89 = true;
                                        STR = "巴烏巴烏交換怎樣？";
                                    }
                                    if (_5A90)
                                    {
                                        _5A90 = true;
                                        STR = "洛克鳥交換怎樣？";
                                    }
                                    if (_5A91)
                                    {
                                        _5A91 = true;
                                        STR = "牛牛交換怎樣？";
                                    }
                                    if (_5A92)
                                    {
                                        _5A92 = true;
                                        STR = "汪汪交換怎樣？";
                                    }
                                }
                                else
                                {
                                    Say(pc, 131, "對！那麽…$R;");
                                    int a = Global.Random.Next(1, 10);
                                    if (a == 1)
                                    {
                                        _5A88 = true;
                                        STR = "帕格薩斯交換怎樣？";
                                    }
                                    if (a == 2)
                                    {

                                        _5A89 = true;
                                        STR = "巴烏巴烏交換怎樣？";
                                    }
                                    if (a < 5)
                                    {
                                        _5A90 = true;
                                        STR = "洛克鳥交換怎樣？";
                                    }
                                    if (a < 8)
                                    {
                                        _5A91 = true;
                                        STR = "牛牛交換怎樣？";
                                    }
                                    if (a < 11)
                                    {
                                        _5A92 = true;
                                        STR = "汪汪交換怎樣？";
                                    }
                                }
                                Say(pc, 131, STR +
                                    "$R想養一養這傢伙$R所以現在存著錢呢$R;");
                                switch (Select(pc, "用寵物交換嗎?", "", "要交換", "算了"))
                                {
                                    case 1:
                                        _5A87 = true;
                                        Say(pc, 131, "那樣！那就拜託了！$R;");
                                        break;
                                    case 2:
                                        Say(pc, 131, "什麽時候都可以再過來！$R;");
                                        break;
                                }
                                break;
                            case 2:
                                Say(pc, 131, "什麽時候都可以再過來！$R;");
                                break;
                        }
                        break;
                }
                return;
            }
             */
            Say(pc, 131, "喂！天气不错吧？$R;");
            /*
            if (_5A86 && !_5A87 && !_Xb00)
            {
                switch (Select(pc, "『微型爬爬蟲』…", "          ", "給我一隻！", "什麽都不是"))
                {
                    case 1:
                        Say(pc, 131, "什麽？$R;" +
                            "$R什麽…這麽多$R是可以給您，但是…$R;" +
                            "$R嗯…這樣好了$R;" +
                            "$P我也不能收錢$R那用其他的寵物交換怎樣？$R;");
                        switch (Select(pc, "用寵物交換嗎?", "      ", "換！", "不換！"))
                        {
                            case 1:
                                string STR;
                                if (_5A88 || _5A89 || _5A90 || _5A91 || _5A92)
                                {
                                    if (_5A88)
                                    {
                                        _5A88 = true;
                                        STR = "帕格薩斯交換怎樣？";
                                    }
                                    if (_5A89)
                                    {
                                        _5A89 = true;
                                        STR = "巴烏巴烏交換怎樣？";
                                    }
                                    if (_5A90)
                                    {
                                        _5A90 = true;
                                        STR = "洛克鳥交換怎樣？";
                                    }
                                    if (_5A91)
                                    {
                                        _5A91 = true;
                                        STR = "牛牛交換怎樣？";
                                    }
                                    if (_5A92)
                                    {
                                        _5A92 = true;
                                        STR = "汪汪交換怎樣？";
                                    }
                                }
                                else
                                {
                                    Say(pc, 131, "對！那麽…$R;");
                                    int a = Global.Random.Next(1, 10);
                                    if (a == 1)
                                    {
                                        _5A88 = true;
                                        STR = "帕格薩斯交換怎樣？";
                                    }
                                    if (a == 2)
                                    {

                                        _5A89 = true;
                                        STR = "巴烏巴烏交換怎樣？";
                                    }
                                    if (a < 5)
                                    {
                                        _5A90 = true;
                                        STR = "洛克鳥交換怎樣？";
                                    }
                                    if (a < 8)
                                    {
                                        _5A91 = true;
                                        STR = "牛牛交換怎樣？";
                                    }
                                    if (a < 11)
                                    {
                                        _5A92 = true;
                                        STR = "汪汪交換怎樣？";
                                    }
                                }
                                Say(pc, 131, STR +
                                    "$R想養一養這傢伙$R所以現在存著錢呢$R;");
                                switch (Select(pc, "用寵物交換嗎?", "", "要交換", "算了"))
                                {
                                    case 1:
                                        _5A87 = true;
                                        Say(pc, 131, "那樣！那就拜託了！$R;");
                                        break;
                                    case 2:
                                        Say(pc, 131, "什麽時候都可以再過來！$R;");
                                        break;
                                }
                                break;
                            case 2:
                                Say(pc, 131, "什麽時候都可以再過來！$R;");
                                break;
                        }
                        break;
                }
                return;
            }
             */
        }

        void 交換(ActorPC pc)
        {
            /*
            Say(pc, 131, "如果擁有幾隻可交換的話$R就可以挑幾個我喜歡的小爬爬蟲!$R;" +
                "$P與小爬爬蟲交換吧$R不是現在帶來了又後悔吧？$R;");
            switch (Select(pc, "交換寵物?", "          ", "交換", "不換"))
            {
                case 1:
                    Say(pc, 131, "真的没關係嗎?$R;");
                    switch (Select(pc, "交換寵物?", "              ", "交換", "不換"))
                    {
                        case 1:
                            if (_5A88)
                            {
                                TakeItem(pc, 10052000, 1);
                                GiveItem(pc, 10007597, 1);
                            }
                            if (_5A89)
                            {
                                TakeItem(pc, 10006650, 1);
                                GiveItem(pc, 10007597, 1);
                            }
                            if (_5A90)
                            {
                                TakeItem(pc, 10018310, 1);
                                GiveItem(pc, 10007597, 1);
                            }
                            if (_5A91)
                            {
                                TakeItem(pc, 10052100, 1);
                                GiveItem(pc, 10007597, 1);
                            }
                            if (_5A92)
                            {
                                TakeItem(pc, 10048900, 1);
                                GiveItem(pc, 10007597, 1);
                            }
                            _Xb00 = true;
                            Say(pc, 131, "呐！收下『微型爬爬蟲』！$R;" +
                                "$R要好好養大哦！$R;");
                            Say(pc, 131, "養大了可以進化的話$R你可以拜託牛牛草原的寵物飼養員$R;" +
                                "$P他們會幫您把微型爬爬蟲$R馴服成『騎乘爬爬蟲』的$R;");
                            break;
                        case 2:
                            Say(pc, 131, "什麽時候都可以再過來！$R;");
                            break;
                    }
                    break;
                case 2:
                    Say(pc, 131, "什麽時候都可以再過來！$R;");
                    break;
            }
             */
        }
    }
}