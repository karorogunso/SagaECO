using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:帕斯特街道(10017000) NPC基本資訊:飛空庭匠人(11000786) X:148 Y:212
namespace SagaScript.M10017000
{
    public class S11000786 : Event
    {
        public S11000786()
        {
            this.EventID = 11000786;
        }


        public override void OnEvent(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];

            if (pc.Account.GMLevel >= 100)
            {
                if (Select(pc, "要怎麼做呢？", "", "進入管理模式", "算了") == 1)
                {
                    管理用(pc);
                    return;
                }
            }
            //貓靈茜子相關
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.收到碎紙) &&
                !Neko_05_cmask.Test(Neko_05.去哈爾列爾利的飛空庭))
            {
                Say(pc, 0, 131, "真的非常感恩？!$R;" +
                    "$R「客人」!「客人」!$R來!快點!往唐卡吧!!$R回去媽媽在的地方吧$R;", "行李裡面的哈爾列爾利");
                return;
            }
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.告知需寻找工匠) &&
                !Neko_05_cmask.Test(Neko_05.收到碎紙))
            {
                Neko_05_cmask.SetValue(Neko_05.收到碎紙, true);
                Say(pc, 11000786, 111, "…瞌睡…瞌睡……撲通!!$R;");
                Say(pc, 11000786, 131, "是?是!!是!!$R;" +
                    "$R讓他保管飛空庭的部件嗎?$R;");
                Say(pc, 0, 131, "剛剛在睡覺吧?$R;" +
                    "$R我想問一些有關飛空庭引擎的問題…$R;", "行李內的哈爾列爾利");
                Say(pc, 11000786, 131, "什麼話啊!$R沒睡!沒睡啊!!$R;" +
                    "$R事情太多才有點累…$R…是誰?…誰在行李裡面啊?$R;");
                Say(pc, 0, 131, "我是莉塔的兒子$R石像「哈爾列爾利」!$R;" +
                    "$R我想問一些有關飛空庭引擎的問題…$R;", "行李裡面的哈爾列爾利");
                Say(pc, 0, 131, "…哈爾列爾利和飛空庭工匠的對話$R繼續…$R;");
                Say(pc, 11000786, 131, "…嗯!還是那樣啊!$R;" +
                    "$R啊!知道了!$R是圓頂材料的問題啊!$R;" +
                    "$P把乳化劑的混合比例改變的話$R黏度會上升，就可以承受圓頂的內壓$R;" +
                    "$R您把秘方記在紙上吧$R這樣做就可以了$R;" +
                    "$P把可以參考的，都一起寫給您吧$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, 131, "『莉塔的哈爾列爾利』收到$R『碎紙』!$R;");
                Say(pc, 0, 131, "真的非常感恩？!$R;" +
                    "$R「客人」!「客人」!$R來!快點!往唐卡吧!!$R回去媽媽在的地方吧$R;", "行李裡面的哈爾列爾利");
                return;
            }//*/

            if (fgarden.Test(FGarden.得到飛空庭鑰匙) && CountItem(pc, 10022700) == 0)
            {
                Say(pc, 131, "什麼?$R弄丟了『飛空庭鑰匙』?$R;" +
                    "$R真是不小心啊$R鑰匙很重要的，要好好保管喔!$R;");
                if (CheckInventory(pc, 10022700, 1))
                {
                    GiveItem(pc, 10022700, 1);
                    Say(pc, 131, "來!給您『飛空庭鑰匙』$R;" +
                        "$R這次可不要弄丟了!$R;");
                }
                else
                {
                    Say(pc, 131, "行李好像都滿了喔?$R;" +
                        "$R把行李減少後再來吧!$R;");
                }
                return;
            }

            if (SInt["FGarden_Potion"] < 50000 && !fgarden.Test(FGarden.得到飛空庭鑰匙))
            {
                傑利科藥水不夠(pc);
                return;
            }

            if (fgarden.Test(FGarden.還飛空庭旋轉帆超重))
            {
                還飛空庭旋轉帆(pc);
                return;
            }

            if (SInt["FGarden_Potion"] > 49000 && !fgarden.Test(FGarden.得到飛空庭鑰匙))
            {
                傑利科藥水夠了(pc);
                return;
            }
            Say(pc, 131, "您好!$R您的飛空庭最近運行的好嗎?$R;");
        }

        void 管理用(ActorPC pc)
        {
            //EVT1100078669
            switch (Select(pc, "管理者模式", "", "增加傑利科藥水", "減少傑利科藥水", "什麼都不做"))
            {
                case 1:
                    SInt["FGarden_Potion"] += int.Parse(InputBox(pc, "輸入要增加的數量", InputType.ItemCode));
                    break;
                case 2:
                    int count = int.Parse(InputBox(pc, "輸入要減少的數量", InputType.ItemCode));
                    if (SInt["FGarden_Potion"] > count)
                        SInt["FGarden_Potion"] -= count;
                    else
                        SInt["FGarden_Potion"] = 0;
                    break;
            }
        }

        void 傑利科藥水夠了(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            if (!fgarden.Test(FGarden.第一次和飛空庭匠人說話))
            {
                fgarden.SetValue(FGarden.第一次和飛空庭匠人說話, true);
                fgarden.SetValue(FGarden.得知飛空庭材料, true);
                Say(pc, 131, "喔喔!都忙到沒魂了！太忙了！$R;" +
                    "$R要幫製作『飛空庭』話，別跟我說…$R;" +
                    "$P我說真的$R;" +
                    "$R有飛空庭從唐卡去諾頓途中墜落了$R;" +
                    "$P因為諾頓島那邊的$R;" +
                    "天氣經常不好，所以是禁飛區$R;" +
                    "$R不應該讓客機來啊…$R;" +
                    "$P修理時，一定需要『傑利科藥水』的$R;" +
                    "自己一個人收集困難$R所以找了大家幫忙!$R;" +
                    "$P雖然轉眼間材料都收集到了$R;" +
                    "真的太感動了，$R;" +
                    "所以答應了為大家造『飛空庭』…$R;" +
                    "$R看!$R;" +
                    "$P就因為這樣$R;" +
                    "所以正趕製耽誤進度的『飛空庭』喔$R;" +
                    "$P『飛空庭』是飛上天空的庭園!$R;" +
                    "就像名字一樣，是庭園在飛!$R;" +
                    "$R在瑪衣瑪衣島上的猴麵包樹$R;" +
                    "到了300年，就會產生浮力$R;" +
                    "在上面裝上機械時代的發掘引擎$R;" +
                    "$P然後在那引擎軸上，$R;" +
                    "安裝能調整猴麵包樹的旋轉帆！$R;" +
                    "飛空庭就完成了!$R;" +
                    "$P在後面的就是『飛空庭』!$R;" +
                    "想像一下它在天空中翱翔的情景吧!$R;" +
                    "$R不是很漂亮嗎…$R;" +
                    "$P真是!$R;" +
                    "我是唐卡的沃頓，是飛空庭的製作師$R;" +
                    "$R在國內被稱為『飛空庭師』$R;" +
                    "是唐卡大師最崇高的稱號$R;");
                Say(pc, 131, "……$R;" +
                    "$P…………$R;" +
                    "$P………………$R;" +
                    "$P莫非…$R;" +
                    "您也想擁有飛空庭?$R;" +
                    "$P那倒是可以理解$R;" +
                    "喔喔…哎呀!$R;" +
                    "$R那我也幫您製作飛空庭吧!$R;" +
                    "$P給我拿來飛空庭的部件的話$R;" +
                    "我就給您組裝!$R;" +
                    "$R需要的部件都收集好的話$R;" +
                    "飛空庭就完成了!$R;" +
                    "$P需要的部件是…$R;" +
                    "$R首先是『飛空庭基臺』$R;" +
                    "然後是『飛空庭引擎』$R;" +
                    "$P還有『飛空庭旋轉帆』6張!$R;" +
                    "$R把這些都收集到的話$R;" +
                    "就成為『飛空庭旋轉帆套裝』!$R;" +
                    "$P還有『飛空庭齒輪』!$R;" +
                    "$R『舵輪』和『催化劑』也收集到的話$R;" +
                    "飛空庭就完成了!$R;");
            }
            else
            {
                if (!fgarden.Test(FGarden.得知飛空庭材料))
                {
                    Say(pc, 131, "真的謝謝!$R幸虧有您5萬瓶『傑利科藥水』$R全部都收集到了!$R;" +
                       "$P真的非常感恩！$R;" +
                       "$R用這個，引擎都修好了$R所以飛到諾頓…$R;" +
                       "$P哎阿!差點忘了!$R;" +
                       "$R説好要給您製作飛空庭的吧!!$R;" +
                       "$P我會製作最好的飛空庭的，相信我！$R;" +
                       "$R把飛空庭的部件拿來給我$R我會幫您組裝的!$R;" +
                       "$R需要的部件都收集好的話$R飛空庭就會完成的!$R;");
                    Say(pc, 131, "需要的部件是…$R;" +
                        "$R首先是『飛空庭基臺』$R;" +
                        "$P還有『飛空庭旋轉帆』6張!$R;" +
                        "$R把這些都收集到的話$R就成為『飛空庭旋轉帆套裝』!$R;" +
                        "$P還有『飛空庭齒輪』!$R『舵輪』和『催化劑』也收集到的話$R飛空庭就完成了!$R;");
                    fgarden.SetValue(FGarden.得知飛空庭材料, true);
                }
                else
                {
                    製作飛空庭(pc);
                }
            }
        }

        void 製作飛空庭(ActorPC pc)
        {
            BitMask<FGardenParts> parts = pc.AMask["FGardenParts"];
            Say(pc, 131, "拿來了什麼部件?$R;");
            switch (Select(pc, "怎麼做呢?", "", "給他『飛空庭基臺』", "給他『舵輪』", "給他『飛空庭引擎』", "給他『催化劑』", "給他『飛空庭旋轉帆』", "給他『飛空庭旋轉帆套裝』", "給他『飛空庭齒輪』", "看看都收集了多少部件", "放棄"))
            {
                case 1:
                    if (parts.Test(FGardenParts.Foundation))
                    {
                        Say(pc, 131, "部件已經接收了?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10027300) >= 1)
                        {
                            Say(pc, 131, "是『飛空庭基臺』啊!$R;" +
                                "$R我來幫您保管好嗎?$R;");
                            switch (Select(pc, "怎麼做呢", "", "讓他保管『飛空庭基臺』", "不讓他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10027300, 1);
                                    parts.SetValue(FGardenParts.Foundation, true);
                                    Say(pc, 131, "我會好好保管『飛空庭基臺』的！$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "隨時再來吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像沒有部件喔?$R;");
                    }
                    break;
                case 2:
                    if (parts.Test(FGardenParts.Steer))
                    {
                        Say(pc, 131, "部件已經接收了?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10028900) >= 1)
                        {
                            Say(pc, 131, "是『舵輪』啊!$R;" +
                                "$R讓我來保管好嗎?$R;");
                            switch (Select(pc, "怎麼做呢?", "", "讓他保管『舵輪』", "不讓他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10028900, 1);
                                    parts.SetValue(FGardenParts.Steer, true);
                                    Say(pc, 131, "我會好好保管『舵輪』的!$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "隨時再來吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像沒有部件喔?$R;");
                    }
                    break;
                case 3:
                    if (parts.Test(FGardenParts.Engine))
                    {
                        Say(pc, 131, "部件已經接收了?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10027900) >= 1)
                        {
                            Say(pc, 131, "是『飛空庭引擎$R;" +
                                "$R我來幫您保管好嗎?$R;");
                            switch (Select(pc, "怎麼做呢", "", "讓他保管『飛空庭引擎』", "不讓他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10027900, 1);
                                    parts.SetValue(FGardenParts.Engine, true);
                                    Say(pc, 131, "我會好好保管『飛空庭引擎』的！$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "隨時再來吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像沒有部件喔?$R;");
                    }

                    break;
                case 4:
                    if (parts.Test(FGardenParts.Catalyst))
                    {
                        Say(pc, 131, "部件已經接收了?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10027600) >= 1)
                        {
                            Say(pc, 131, "是『催化劑』啊!$R;" +
                                "$R讓我來保管好嗎?$R;");
                            switch (Select(pc, "怎麼做呢?", "", "讓他保管『催化劑』", "不讓他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10027600, 1);
                                    parts.SetValue(FGardenParts.Catalyst, true);
                                    Say(pc, 131, "我會好好保管『催化劑』的!$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "隨時再來吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像沒有部件喔?$R;");
                    }
                    break;
                case 5:
                    if (parts.Test(FGardenParts.SailComplete))
                    {
                        Say(pc, 131, "已經收集了6個旋轉帆了嗎?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10028000) >= 1)
                        {
                            Say(pc, 131, "是『飛空庭旋轉帆』啊!$R;" +
                                "$R讓我來保管好嗎?$R;");
                            switch (Select(pc, "怎麼做呢?", "", "讓他保管『飛空庭旋轉帆』", "不讓他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10028000, 1);
                                    if (!parts.Test(FGardenParts.Sail1))
                                    {
                                        parts.SetValue(FGardenParts.Sail1, true);
                                        Say(pc, 131, "是1個『飛空庭旋轉帆』啊!$R我來保管吧$R;");
                                    }
                                    else if (parts.Test(FGardenParts.Sail1) && !parts.Test(FGardenParts.Sail2))
                                    {
                                        parts.SetValue(FGardenParts.Sail2, true);
                                        Say(pc, 131, "是第2張『飛空庭旋轉帆』啊!$R我來保管吧;");
                                    }
                                    else if (parts.Test(FGardenParts.Sail2) && !parts.Test(FGardenParts.Sail3))
                                    {
                                        parts.SetValue(FGardenParts.Sail3, true);
                                        Say(pc, 131, "是第3張『飛空庭旋轉帆』啊!$R我來保管吧;");
                                    }
                                    else if (parts.Test(FGardenParts.Sail3) && !parts.Test(FGardenParts.Sail4))
                                    {
                                        parts.SetValue(FGardenParts.Sail4, true);
                                        Say(pc, 131, "是第4張『飛空庭旋轉帆』啊!$R我來保管吧!$R;");
                                    }
                                    else if (parts.Test(FGardenParts.Sail4) && !parts.Test(FGardenParts.Sail5))
                                    {
                                        parts.SetValue(FGardenParts.Sail5, true);
                                        Say(pc, 131, "是第5張『飛空庭旋轉帆』啊!$R我來保管吧$R;");
                                    }
                                    else if (parts.Test(FGardenParts.Sail5) && !parts.Test(FGardenParts.SailComplete))
                                    {
                                        parts.SetValue(FGardenParts.SailComplete, true);
                                        Say(pc, 131, "是第6張『飛空庭旋轉帆』啊!$R我來保管吧$R;" +
                                            "$R恭喜您!$R『飛空庭旋轉帆』全都收集好了！$R;");
                                    }
                                    break;
                                case 2:
                                    Say(pc, 131, "隨時再來吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像沒有部件喔?$R;");
                    }
                    break;
                case 6:
                    if (parts.Test(FGardenParts.SailComplete))
                    {
                        Say(pc, 131, "部件已經接收了?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10028100) >= 1)
                        {
                            Say(pc, 131, "是『飛空庭旋轉帆套裝』啊!$R;" +
                                "$R讓我來保管好嗎?$R;");
                            switch (Select(pc, "怎麼做呢?", "", "讓他保管『飛空庭旋轉帆套裝』", "不讓他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10028100, 1);
                                    parts.SetValue(FGardenParts.SailComplete, true);
                                    Say(pc, 131, "我會好好保管『飛空庭旋轉帆套裝』的!$R;");
                                    還飛空庭旋轉帆(pc);
                                    break;
                                case 2:
                                    Say(pc, 131, "隨時再來吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像沒有部件喔?$R;");
                    }
                    break;
                case 7:
                    if (parts.Test(FGardenParts.Wheel))
                    {
                        Say(pc, 131, "部件已經接收了?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10028200) >= 1)
                        {
                            Say(pc, 131, "是『飛空庭齒輪』啊!$R;" +
                                "$R讓我來保管好嗎?$R;");
                            switch (Select(pc, "怎麼做呢?", "", "讓他保管『飛空庭齒輪』", "不讓他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10028200, 1);
                                    parts.SetValue(FGardenParts.Wheel, true);
                                    Say(pc, 131, "我會好好保管『飛空庭齒輪』的!$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "隨時再來吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像沒有部件喔?$R;");
                    }
                    break;
                case 9:
                    return;
            }
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            if (!fgarden.Test(FGarden.得到飛空庭鑰匙) &&
                parts.Test(FGardenParts.Foundation) &&
                parts.Test(FGardenParts.Engine) &&
                parts.Test(FGardenParts.SailComplete) &&
                parts.Test(FGardenParts.Steer) &&
                parts.Test(FGardenParts.Wheel) &&
                parts.Test(FGardenParts.Catalyst))
            {
                Say(pc, 131, "終於都收集好了!$R;" +
                    "$P恭喜您!$R您的 「飛空庭」完成了!$R;" +
                    "$P這個給您吧!$R;");
                Say(pc, 131, "拿到了『飛空庭鑰匙』!$R;");
                Say(pc, 131, "使用這道具的話$R您可以在使用的地方，$R召喚您的飛空庭$R;" +
                    "$P飛空庭裡有自動導航系統$R只要收到『飛空庭鑰匙』的訊號$R就會飛到您上面的!$R;" +
                    "$P然後，點擊飛空庭的話$R會放下來「飛空庭繩子」$R選擇「往飛空庭移動」$R就可以進入飛空庭裡!$R;" +
                    "$P您可以選擇「整理繩子」$R來整理「飛空庭繩子」$R;" +
                    "$P進去飛空庭以後，有不懂的話$R點擊一下「舵輪」$R或房間的「操作盤」試試看吧$R;" +
                    "$P『飛空庭鑰匙』的功能是$R召喚自己飛空庭時，使用的道具$R所以沒有飛空庭的人，就算用了$R也無法召喚飛空庭的$R;" +
                    "$P最後要說的是$R現在對飛空庭的飛空規則管理很嚴格$R;" +
                    "$R為避免飛空庭被用作軍事用途$R和保護運送隊的權益$R商人行會那邊也有壓力$R還有一些難言之隱$R;" +
                    "$P在奧克魯尼亞世界裡$R如果沒有得到特別許可$R只可以在「飛空庭機場地區」裡$R召喚飛空庭$R;" +
                    "$R啊!$R;");

                if (pc.FGarden == null)//如果當前帳號還沒創建過飛空庭
                    pc.FGarden = new SagaDB.FGarden.FGarden(pc);//創建新的飛空庭
                GiveItem(pc, 10022700, 1);
                fgarden.SetValue(FGarden.得到飛空庭鑰匙, true);
            }
            SendFGardenCreateMaterial(pc, parts);
        }

        void 還飛空庭旋轉帆(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            BitMask<FGardenParts> parts = pc.AMask["FGardenParts"];
            if (parts.Test(FGardenParts.Sail1))
            {
                Say(pc, 131, "嗯?$R我保管的『飛空庭旋轉帆』$R要還給您嗎?$R;");
                switch (Select(pc, "怎麼做呢?", "", "拿回『飛空庭旋轉帆』", "不需要"))
                {
                    case 1:
                        ushort count = 0;
                        if (parts.Test(FGardenParts.Sail5))
                            count = 5;
                        else if (parts.Test(FGardenParts.Sail4))
                            count = 4;
                        else if (parts.Test(FGardenParts.Sail3))
                            count = 3;
                        else if (parts.Test(FGardenParts.Sail2))
                            count = 2;
                        else if (parts.Test(FGardenParts.Sail1))
                            count = 1;
                        if (CheckInventory(pc, 10028000, count))
                        {
                            GiveItem(pc, 10028000, count);
                        }
                        else
                        {
                            fgarden.SetValue(FGarden.還飛空庭旋轉帆超重, true);
                            Say(pc, 131, "行李都滿了$R減少行李後再來吧!$R;");
                        }
                        break;
                    case 2:
                        Say(pc, 131, "不需要?$R那好!那我來處理吧$R;");
                        fgarden.SetValue(FGarden.還飛空庭旋轉帆超重, false);
                        break;
                }
            }
        }

        void 傑利科藥水不夠(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            if (!fgarden.Test(FGarden.第一次和飛空庭匠人說話))
            {
                Say(pc, 131, "這個真是犯愁啊!$R;" +
                    "$R有飛空庭從唐卡去諾頓途中墜落了$R;" +
                    "$P因為諾頓島那邊的$R;" +
                    "天氣經常不好，所以是禁飛區$R;" +
                    "$R不應該讓客機來啊…$R;" +
                    "我是唐卡的沃頓，是飛空庭的製作師$R;" +
                    "$R在國內被稱為『飛空庭師』$R;" +
                    "是唐卡大師最崇高的稱號$R;" +
                    "$P『飛空庭』是飛上天空的庭園!$R;" +
                    "就像名字一樣，是庭園在飛!$R;" +
                    "$R在瑪衣瑪衣島上的猴麵包樹$R;" +
                    "到了300年，就會產生浮力$R;" +
                    "在上面裝上機械時代的發掘引擎$R;" +
                    "$P然後在那引擎軸上，$R;" +
                    "安裝能調整猴麵包樹的旋轉帆！$R;" +
                    "飛空庭就完成了!$R;" +
                    "$P本來要修理飛空庭的$R;" +
                    "可是引擎的汽油全都漏了$R;" +
                    "$R不好意思$R;" +
                    "可以幫我收集一些道具嗎?$R;" +
                    "$P製作液體氫氣，所需要的原料$R;" +
                    "是『傑利科藥水』5萬瓶$R;" +
                    "$R只要充電一次$R;" +
                    "下次開始，只要引擎待機時補充氫氣$R;" +
                    "就可以半永久性的運作!$R;" +
                    "$P對了!$R;" +
                    "嗯嗯…$R;" +
                    "$P對了!$R;" +
                    "收集到『傑利科藥水』5萬瓶的話$R;" +
                    "給您製作飛空庭當作報酬!$R;" +
                    "怎麼樣?$R;" +
                    "$P什麼?$R;" +
                    "$R哈哈哈哈!$R;" +
                    "我製作的飛空庭非常完美的!!$R;");
                fgarden.SetValue(FGarden.第一次和飛空庭匠人說話, true);
            }
            else
            {
                switch (Select(pc, " 怎麼做呢?", "", "收集傑利科藥水", "現在有幾瓶傑利科藥水?", "什麼都不做"))
                {
                    case 1:
                        int count = 0;
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10000104)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交給了{0}個傑利科藥水", count));
                            SInt["FGarden_Potion"] += count;
                        }
                        break;
                    case 2:
                        Say(pc, 131, "現在是…$R;" +
                            SInt["FGarden_Potion"] + "個傑利科藥水啊!$R;");
                        break;
                }
            }
        }
    }
}

