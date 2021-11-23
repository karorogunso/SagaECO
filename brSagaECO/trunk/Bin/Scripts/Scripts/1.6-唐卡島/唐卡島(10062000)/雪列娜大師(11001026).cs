using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001026 : Event
    {
        public S11001026()
        {
            this.EventID = 11001026;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            
            /*if (a//ME.IS_CAPA_PAYL_OVER = 1
            && !_2b31)
            {
                Say(pc, 131, "這東西很重吧$R;" +
                    "只要到唐卡市倉庫的話$R;" +
                    "可以把行李寄過去唷$R;");
                _2b31 = true;

                OpenWareHouse(pc, WarehousePlace.Tonka);
                //WAREHOUSE 08
                return;
            }*/

            if (!fgarden.Test(FGarden.唐卡注册飞空庭))
            {
                switch (Select(pc, "歡迎來到飛空庭大工廠", "", "木材加工", "什麼也不做"))
                {
                    case 1:
                        Synthese(pc, 2020, 3);
                        break;
                    case 2:
                        break;
                }
            }
            else
            {
                switch (Select(pc, "歡迎來到飛空庭大工廠", "", "木材加工", "改造飛空庭", "什麼也不做"))
                {
                    case 1:
                        Synthese(pc, 2020, 3);
                        break;
                    case 2:
                        飛空艇改造(pc);
                        break;
                    case 3:
                        break;
                }
            }
        }
        
        void 飛空艇改造(ActorPC pc)
        {
           BitMask<FGarden> fgarden = pc.AMask["FGarden"];
           if (fgarden.Test(FGarden.给予飞空翅膀))
           {
               switch (Select(pc, "新的飛行帆製作？", "", "不製作", "製作！"))
               {
                   case 1:
                       break;
                   case 2:
                       Say(pc, 131, "OK！$R;" +
                           "根據心情來更換飛行帆$R;" +
                           "是件非常棒的事！$R;" +
                           "$R那麼、馬上開始$R;" +
                           "著手製作吧！$R;" +
                           "$P價格、製作要9999萬G！$R;");
                       Select(pc, "怎麼辦？", "", "盯", "無視", "冷目相看");
                       Say(pc, 131, "啊！$R;" +
                           "果然第二次就嚇不倒你了啊。$R;" +
                           "$R剛才的是從材料收集到$R;" +
                           "裝配全部都在這邊做的價格。$R;" +
                           "$R客人你自己搜集材料$R;" +
                           "來做的話只需要5000G。$R;");
                       switch (Select(pc, "怎麼辦？", "", "考慮一下", "5000G"))
                       {
                           case 1:
                               break;
                           case 2:
                               if (pc.Gold > 4999)
                               {
                                   fgarden.SetValue(FGarden.委托飞空庭甲板, false);
                                   fgarden.SetValue(FGarden.委托汽笛, false);
                                   fgarden.SetValue(FGarden.委托涡轮引擎, false);
                                   fgarden.SetValue(FGarden.委托飞行用帆, false);
                                   fgarden.SetValue(FGarden.完全委托飞行用帆, false);
                                   fgarden.SetValue(FGarden.委托飞行用大帆, false);
                                   fgarden.SetValue(FGarden.完全委托飞行用大帆, false);
                                   fgarden.SetValue(FGarden.飞空庭改造完成, false);
                                   fgarden.SetValue(FGarden.给予飞空翅膀, false);
                                   pc.Gold -= 5000;
                                   Say(pc, 131, "那麼確實受理了！$R;" +
                                       "承擔者就是我雪列娜。$R;" +
                                       "$R有任何問題的話$R;" +
                                       "就到我這裡來報告吧$R;" +
                                       "$P那麼趕緊開始收集$R;" +
                                       "飛行帆所需要的材料。$R;" +
                                       "$R需要收集的道具有……$R;" +
                                       "$P　飛空庭甲板　1個$R;" +
                                       "　渦輪引擎　1個$R;" +
                                       "　汽笛　1個$R;" +
                                       "　飛行用帆　2個$R;" +
                                       "　飛行用大帆　2個$R;" +
                                       "$P道具預存在大工廠。$R;" +
                                       "$R需要的道具全部收集齊全后$R;" +
                                       "就立即進入飛行帆裝配作業！$R;");
                                   return;
                               }
                               Say(pc, 131, "金錢好像不夠哦……。$R;");
                               break;
                       }
                       break;
               }
               return;
           }

           if (fgarden.Test(FGarden.飞空庭改造完成))
           {
               完成(pc);
               return;
           }

            if (fgarden.Test(FGarden.开始收集改造部件))
            {
                Say(pc, 131, "辛苦了$R;" +
                    "收集完道具了嗎？$R;");
                fgarden.SetValue(FGarden.凯提推进器, false);
                fgarden.SetValue(FGarden.凯提推进器_光, false);
                fgarden.SetValue(FGarden.凯提推进器_暗, false);
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10049500 ||
                       pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10049550 ||
                       pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10049551)
                    {
                        switch (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID)
                        {
                            case 10049500:
                                fgarden.SetValue(FGarden.凯提推进器, true);
                                break;
                            case 10049550:
                                fgarden.SetValue(FGarden.凯提推进器_光, true);
                                break;
                            case 10049551:
                                fgarden.SetValue(FGarden.凯提推进器_暗, true);
                                break;
                        }
                        Say(pc, 131, "後背有點癢啊$R;");
                        Say(pc, 131, "啊，那是推進器！$R;" +
                            "$R肯定想搭乘飛空庭了。$R;");
                        switch (Select(pc, "怎麼辦呢？", "", "才不是呢，我不坐", "乘坐"))
                        {
                            case 1:
                                break;
                            case 2:
                                推進器(pc);
                                break;
                        }
                        return;
                    }
                }

                #region 飞空庭改造零件
                switch (Select(pc, "怎麼辦呢？", "", "交出了『飛空庭甲板』", "交出了『渦輪引擎』", "交出了『汽笛』", "交出了『飛行用帆』", "交出了『飛行用大帆』", "確認部件收集情況。", "放棄"))
                {
                    case 1:
                        if (fgarden.Test(FGarden.委托飞空庭甲板))
                        {
                            Say(pc, 131, "那個道具已經委託了嗎？$R;");
                            return;
                        }
                        if (CountItem(pc, 10029100) < 1)
                        {
                            Say(pc, 131, "好像沒有道具呀$R;");
                            return;
                        }
                        Say(pc, 131, "這是『飛空庭甲板』嗎？$R;" +
                            "怎麼找到這個的，不錯嘛。$R;");
                        switch (Select(pc, "怎麼辦呢？", "", "委託", "不委託"))
                        {
                            case 1:
                                TakeItem(pc, 10029100, 1);
                                fgarden.SetValue(FGarden.委托飞空庭甲板, true);
                                Say(pc, 131, "完全委託了『飛空庭甲板』$R;");
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 2:
                        if (fgarden.Test(FGarden.委托涡轮引擎))
                        {
                            Say(pc, 131, "那個道具已經委託了嗎？$R;");
                            return;
                        }
                        if (CountItem(pc, 10027750) < 1)
                        {
                            Say(pc, 131, "好像沒有道具呀$R;");
                            return;
                        }
                        Say(pc, 131, "這是『渦輪引擎』呀，$R;" +
                            "怎麼找到的，不錯呀。$R;");
                        switch (Select(pc, "怎麼辦呢？", "", "委託『渦輪引擎』", "不委託"))
                        {
                            case 1:
                                TakeItem(pc, 10027750, 1);
                                fgarden.SetValue(FGarden.委托涡轮引擎, true);
                                Say(pc, 131, "完全委託『渦輪引擎』了$R;");
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 3:
                        if (fgarden.Test(FGarden.委托汽笛))
                        {
                            Say(pc, 131, "那個道具已經委託了嗎？$R;");
                            return;
                        }
                        if (CountItem(pc, 10018600) < 1)
                        {
                            Say(pc, 131, "好像沒有道具呀$R;");
                            return;
                        }
                        Say(pc, 131, "這是『汽笛』呀，$R;" +
                            "怎麼找到的，不錯呀。$R;");
                        switch (Select(pc, "怎麼辦呢？", "", "委託『汽笛』", "不委託"))
                        {
                            case 1:
                                TakeItem(pc, 10018600, 1);
                                fgarden.SetValue(FGarden.委托汽笛, true);
                                Say(pc, 131, "完全委託了『汽笛』$R;");
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 4:
                        if (fgarden.Test(FGarden.完全委托飞行用帆))
                        {
                            Say(pc, 131, "那個道具已經委託了嗎？$R;");
                            return;
                        }
                        if (CountItem(pc, 10028700) < 1)
                        {
                            Say(pc, 131, "好像沒有道具呀$R;");
                            return;
                        }
                        Say(pc, 131, "這是『飛行用帆』呀$R;" +
                            "怎麼找到的，不錯呀。$R;");
                        switch (Select(pc, "怎麼辦呢？", "", "委託『飛行用帆』", "不委託"))
                        {
                            case 1:
                                if (fgarden.Test(FGarden.委托飞行用帆))
                                {
                                    TakeItem(pc, 10028700, 1);
                                    fgarden.SetValue(FGarden.完全委托飞行用帆, true);
                                    Say(pc, 131, "完全委託『飛行用帆』了$R;");
                                    return;
                                }
                                TakeItem(pc, 10028700, 1);
                                fgarden.SetValue(FGarden.委托飞行用帆, true);
                                Say(pc, 131, "委託『飛行用帆』了$R;");
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 5:
                        if (fgarden.Test(FGarden.完全委托飞行用大帆))
                        {
                            Say(pc, 131, "那個道具已經委託了嗎？$R;");
                            return;
                        }
                        if (CountItem(pc, 10028800) < 1)
                        {
                            Say(pc, 131, "好像沒有道具呀$R;");
                            return;
                        }
                        Say(pc, 131, "這是『飛行用大帆』呀$R;" +
                            "怎麼找到的，不錯呀。$R;");
                        switch (Select(pc, "怎麼辦呢？", "", "委託『飛行用大帆』", "不委託"))
                        {
                            case 1:
                                if (fgarden.Test(FGarden.委托飞行用大帆))
                                {
                                    TakeItem(pc, 10028800, 1);
                                    fgarden.SetValue(FGarden.完全委托飞行用大帆, true);
                                    Say(pc, 131, "完委託『飛行用大帆』了$R;");
                                    return;
                                }
                                TakeItem(pc, 10028800, 1);
                                fgarden.SetValue(FGarden.委托飞行用大帆, true);
                                Say(pc, 131, "委託『飛行用大帆』了$R;");
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 6:
                        GiveItem(pc, 10020104, 1);
                        Say(pc, 131, "做一個清單吧$R;" +
                            "這樣可以隨時確認唷$R;");
                        return;
                    case 7:
                        return;
                }
#endregion

                if (!fgarden.Test(FGarden.飞空庭改造完成) && 
                    fgarden.Test(FGarden.接受改造飞空庭订单) && 
                    fgarden.Test(FGarden.铁板收集完毕) &&
                    fgarden.Test(FGarden.委托飞空庭甲板) &&
                    fgarden.Test(FGarden.委托汽笛) &&
                    fgarden.Test(FGarden.委托涡轮引擎) &&
                    fgarden.Test(FGarden.委托飞行用帆) &&
                    fgarden.Test(FGarden.完全委托飞行用帆) &&
                    fgarden.Test(FGarden.委托飞行用大帆) &&
                    fgarden.Test(FGarden.完全委托飞行用大帆))
                {
                    完成(pc);
                    return;
                }
                return;
            }

            #region 听完飞行规则
            if (fgarden.Test(FGarden.听完飞空庭飞行规则))
            {
                fgarden.SetValue(FGarden.开始收集改造部件, true);
                Say(pc, 131, "辛苦了$R;" +
                    "加固工程結束囉。$R;" +
                    "沒經過允許，我就先看了。$R;" +
                    "真的太漂亮了。$R;" +
                    "$P這裡賣一些家具，看看吧。$R;" +
                    "$R那裡的哈爾列爾利也在賣喔$R;" +
                    "$P下一步準備一些$R;" +
                    "飛行帆的材料吧。$R;" +
                    "$R要收集的道具有$R;" +
                    "$P飛空庭甲板1個$R;" +
                    "渦輪引擎1個$R;" +
                    "汽笛1個$R;" +
                    "飛行用帆2個$R;" +
                    "飛行用大帆2個唷$R;" +
                    "$P道具委託大工廠了$R;" +
                    "$R需要的道具收集完後，$R就開始組裝飛行用帆吧。$R;");
                return;
            }
            #endregion

            if (fgarden.Test(FGarden.铁板收集完毕))
            {
                Say(pc, 131, "工廠正在作業，$R;" +
                    "$R需要一些時間，$R;" +
                    "您先坐在椅子上聽一聽$R;" +
                    "$R『奧克魯尼亞航空法』的講座吧。$R;" +
                    "$P講座由$CO木偶製作大師蒂阿$CD大師負責。$R;" +
                    "$R木偶製作大師蒂阿，拜託了。$R;");
                Say(pc, 131, "OK！木偶製作大師雪列娜$R;" +
                    "到這裡來吧。$R;");
                return;
            }

            if (fgarden.Test(FGarden.接受改造飞空庭订单))
            {
                if (CountItem(pc, 10038616) >= 30)
                {
                    fgarden.SetValue(FGarden.铁板收集完毕, true);
                    TakeItem(pc, 10038616, 30);
                    Say(pc, 131, "累了吧？$R;" +
                        "30張『鐵板』已經收集好了。$R;" +
                        "$R好，正好$R;" +
                        "木偶製作大師們正好$R;" +
                        "剛剛結束作業準備呢。$R;" +
                        "$R那麼馬上開始加固工程吧$R;");
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 0, 131, "給了30張『鐵板』$R;");
                    Say(pc, 131, "木偶製作大師是我們大工廠的驕傲喔，$R;" +
                        "他們會竭盡全力$R;" +
                        "改造您的飛空庭唷。$R;" +
                        "$R改造需要一些時間，$R;" +
                        "您先坐在椅子上聽一聽$R;" +
                        "『奧克魯尼亞航空法』的講座吧。$R;" +
                        "$P講座由$CO木偶製作大師蒂阿$CD先生負責。$R;" +
                        "$R木偶製作大師蒂阿，拜託了。$R;");
                    Say(pc, 131, "OK！木偶製作大師雪列娜$R;" +
                        "到這裡來吧。$R;");
                    return;
                }
                Say(pc, 131, "累了吧？$R;" +
                    "讓我看看收集了多少？$R;" +
                    "$P您現在為了飛空庭本體$R;" +
                    "加固工程的需要$R;" +
                    "$R收集30張『鐵板』中$R;" +
                    "$P製作鐵板需要很多『鐵塊』$R;" +
                    "$R岩石、鐵礦石岩等材料$R這些可以從魔物那取得唷，$R;" +
                    "要努力收集呀。$R;");
                return;
            }

            #region 接受订单

            Say(pc, 131, "改造飛空庭？$R;" +
                "$R真是好久沒有接到過的訂單呀$R;" +
                "木偶製作大師們該興奮了，哈哈$R;" +
                "$R先說明一下吧$R;" +
                "$P改造飛空庭，$R;" +
                "就可以用自己的飛空庭，$R;" +
                "去各個港口唷$R;" +
                "可以隨意去不同地方，$R;" +
                "對冒險者來說很方便的，對吧！$R;" +
                "$P改造只需9999萬金喔！$R;");
            Select(pc, "怎麼辦呢?", "", "怎麼辦？", "尖叫", "暈過去了");
            Say(pc, 131, "稍等一下阿$R;" +
                "我是開玩笑呢！$R;" +
                "$R我知道您付不起$R;" +
                "$P這是我收集材料組裝製做完成的價錢，$R;" +
                "$R客人如果自己收集材料來代工，$R我只收1萬金唷$R;");
            int SEL;
            do
            {
                SEL = Select(pc, "怎麼辦呢?", "", "先想想", "9999萬金幣", "1萬金幣");
                switch (SEL)
                {
                    case 2:
                        Say(pc, 131, "呵呵呵！$R;" +
                            "好好笑啊$R;");
                        break;
                    case 3:
                        if (pc.Gold < 10000)
                        {
                            Say(pc, 131, "金幣不足呀……$R;");
                            return;
                        }
                        fgarden.SetValue(FGarden.接受改造飞空庭订单, true);
                        pc.Gold -= 10000;
                        Say(pc, 131, "那麼就接受改造訂單了。$R;" +
                            "負責人是我雪列娜$R;" +
                            "$R有什麼不清楚的$R;" +
                            "跟我聯系吧$R;" +
                            "$P那麼先來說明$R;" +
                            "改造工程的順序，$R;" +
                            "$P先進行飛空庭本體的加固工程喔$R;" +
                            "$R然後製做飛行用帆$R;" +
                            "並把飛行用帆安裝在飛空庭上$R;" +
                            "工程算結束了。$R;" +
                            "$P這樣就可以$R;" +
                            "在空中自由翱翔了$R;" +
                            "$P客人請親自去收集$R;" +
                            "加固工程需要的材料和$R;" +
                            "飛行用帆的材料喔$R;" +
                            "$R不要太擔心$R;" +
                            "一定會收集到的$R;" +
                            "$P那麼先從$R;" +
                            "加固工程需要的材料開始…$R;" +
                            "首先收集的道具有……$R;" +
                            "$P『$CO鐵板$CD』30張！$R;" +
                            "$R有點重唷，請小心收集$R;" +
                            "$P製作鐵板需要很多『$CO鐵塊$CD』喔$R;" +
                            "$R岩石、鐵礦石岩等材料$R;" +
                            "可以從魔物那取得唷，$R;" +
                            "要努力收集呀。$R;");
                        break;
                }
            } while (SEL == 2);
            #endregion
        }

        void 完成(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            fgarden.SetValue(FGarden.飞空庭改造完成, true);
            Say(pc, 131, "現在道具全部收集好了。$R;" +
                "$R辛苦了，$R;" +
                "$P來，現在我們開始組裝，$R;" +
                "結束之前，請等一會兒唷$R;" +
                "$R會給您做一個漂亮的飛行帆的。$R;");

            if (GetItemCount(pc, ContainerType.BODY) > 90)
            {
                Say(pc, 131, "給您道具，$R;" +
                    "先減少行李再過來吧。$R;");
                return;
            }
            Say(pc, 131, "裝飾想怎麼做呢？$R;" +
                "$R準備了火箭系列和$R魔法翅膀系列兩種。$R;");
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Say(pc, 131, "功能都一樣！$R;" +
                    "女士比較喜歡魔法翅膀系列呀$R;");
            }
            else
            {
                Say(pc, 131, "功能都一樣！$R;" +
                    "男士比較喜歡火箭系列呀$R;");
            }
            switch (Select(pc, "選擇哪種呢？", "", "火箭系列", "魔法翅膀系列"))
            {
                case 1:
                    GiveItem(pc, 30060100, 1);
                    GiveItem(pc, 10016700, 10);
                    fgarden.SetValue(FGarden.给予飞空翅膀, true);
                    Say(pc, 131, "OK$R;" +
                        "$R火箭系列是嗎？$R;" +
                        "那麼在這裡等一下。$R;");
                    Fade(pc, FadeType.Out, FadeEffect.Black);
                    PlaySound(pc, 2210, false, 100, 50);
                    Wait(pc, 2000);
                    PlaySound(pc, 2210, false, 100, 50);
                    Wait(pc, 2000);
                    PlaySound(pc, 2210, false, 100, 50);
                    Wait(pc, 2000);
                    Fade(pc, FadeType.In, FadeEffect.Black);
                    Say(pc, 131, "完成了！$R;" +
                        "飛行帆完成了！$R;");
                    PlaySound(pc, 4006, false, 100, 50);
                    Say(pc, 131, "得到了『火箭飛行帆』$R;");
                    Say(pc, 131, "要安裝請在飛空庭上面點擊W。$R;" +
                        "$R更換翅膀就可以了$R;" +
                        "$P安裝飛行帆時，$R;" +
                        "『舵輪』目錄上$R;" +
                        "會增加『飛空庭出發』$R;" +
                        "$R然後選擇想去的地方就可以了。$R;" +
                        "$P聽完講座，您一定知道，$R;" +
                        "想飛行需要大量『摩根炭』吧$R;" +
                        "$R這是我給您的禮物唷。$R;");
                    Say(pc, 131, "得到了『摩根炭』$R;");
                    Say(pc, 131, "來，趕緊安裝吧。$R;");
                    break;
                case 2:
                    GiveItem(pc, 30060200, 1);
                    GiveItem(pc, 10016700, 10);
                    fgarden.SetValue(FGarden.给予飞空翅膀, true);
                    Say(pc, 131, "OK$R;" +
                        "$R魔法翅膀系列合適吧？$R;" +
                        "那麼在這裡稍等一下吧。$R;");
                    Fade(pc, FadeType.Out, FadeEffect.Black);
                    PlaySound(pc, 2210, false, 100, 50);
                    Wait(pc, 2000);
                    PlaySound(pc, 2210, false, 100, 50);
                    Wait(pc, 2000);
                    PlaySound(pc, 2210, false, 100, 50);
                    Wait(pc, 2000);
                    Fade(pc, FadeType.In, FadeEffect.Black);
                    Say(pc, 131, "完成了！$R;" +
                        "飛行帆完成了！$R;");
                    PlaySound(pc, 4006, false, 100, 50);
                    Say(pc, 131, "得到了『翅膀飛行帆』$R;");
                    Say(pc, 131, "要安裝請在飛空庭上面點擊W。$R;" +
                        "$R更換翅膀就可以了$R;" +
                        "$P安裝飛行帆時，$R;" +
                        "『舵輪』目錄上$R;" +
                        "$R會增加『飛空庭出發』$R;" +
                        "然後選擇想去的地方就可以了。$R;" +
                        "$P聽完講座，您一定知道，$R;" +
                        "想飛行需要大量『摩根炭』吧$R;" +
                        "$R這是我給您的禮物唷。$R;");
                    Say(pc, 131, "得到了『摩根炭』$R;");
                    Say(pc, 131, "來，趕緊安裝吧。$R;");
                    break;
            }
        }

        void 推進器(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            Say(pc, 131, "記住這一點，$R;" +
                "一但安裝推進器，$R;" +
                "飛空庭就不能回復原狀了唷。$R;");
            switch (Select(pc, "怎麼辦呢？", "", "有點難決定呀", "好吧，不管了"))
            {
                case 1:
                    break;
                case 2:
                    Say(pc, 131, "最後再問一次$R;" +
                        "真的，真的沒關係嗎？$R;" +
                        "可能會失去推進器喔。$R;" +
                        "$R我跟您再確認一次$R;" +
                        "因為真的恢復不了原狀呀。$R;" +
                        "$P如果這樣也無所謂的話…$R;" +
                        "那麼請選擇『安裝推進器』吧。$R;");
                    switch (Select(pc, "怎麼辦呢？", "", "放棄了", "安裝推進器"))
                    {
                        case 1:
                            break;
                        case 2:
                            Say(pc, 131, "知道了！$R;" +
                                "$R把到現在為止收集的飛行帆還給您。$R;");

                            if (fgarden.Test(FGarden.委托飞空庭甲板))
                            {
                                if (!CheckInventory(pc,10029100,1))
                                {
                                    Say(pc, 131, "給您道具，$R;" +
                                        "先減少行李再過來吧。$R;");
                                    return;
                                }

                                GiveItem(pc, 10029100, 1);
                                fgarden.SetValue(FGarden.委托飞空庭甲板, false);
                                Say(pc, 131, "『飛空庭甲板』$R;");
                            }
                            if (fgarden.Test(FGarden.委托涡轮引擎))
                            {
                                if (!CheckInventory(pc,10027750,1))
                                {
                                    Say(pc, 131, "給您道具，$R;" +
                                        "先減少行李再過來吧。$R;");
                                    return;
                                }
                                GiveItem(pc, 10027750, 1);
                                fgarden.SetValue(FGarden.委托涡轮引擎, false);
                                Say(pc, 131, "交還了『渦輪引擎』$R;");
                            }
                            if (fgarden.Test(FGarden.委托汽笛))
                            {
                                if (!CheckInventory(pc,10018600,1))
                                {
                                    Say(pc, 131, "給您道具，$R;" +
                                        "先減少行李再過來吧。$R;");
                                    return;
                                }
                                GiveItem(pc, 10018600, 1);
                                fgarden.SetValue(FGarden.委托汽笛, false);
                                Say(pc, 131, "交還了『汽笛』$R;");
                            }
                            if (fgarden.Test(FGarden.委托飞行用帆))
                            {
                                if (!CheckInventory(pc,10028700,1))
                                {
                                    Say(pc, 131, "給您道具，$R;" +
                                        "先減少行李再過來吧。$R;");
                                    return;
                                }
                                GiveItem(pc, 10028700, 1);
                                fgarden.SetValue(FGarden.委托飞行用帆, false);
                                Say(pc, 131, "交還了『飛行用帆』$R;");
                            }
                            if (fgarden.Test(FGarden.完全委托飞行用帆))
                            {
                                if (!CheckInventory(pc, 10028700, 1))
                                {
                                    Say(pc, 131, "給您道具，$R;" +
                                        "先減少行李再過來吧。$R;");
                                    return;
                                }
                                GiveItem(pc, 10028700, 1);
                                fgarden.SetValue(FGarden.完全委托飞行用帆, false);
                                Say(pc, 131, "交還了『飛行用帆』$R;");
                            }
                            if (fgarden.Test(FGarden.委托飞行用大帆))
                            {
                                if (!CheckInventory(pc,10028800,1))
                                {
                                    Say(pc, 131, "給您道具，$R;" +
                                        "先減少行李再過來吧。$R;");
                                    return;
                                }
                                GiveItem(pc, 10028800, 1);
                                fgarden.SetValue(FGarden.委托飞行用大帆, false);
                                Say(pc, 131, "交還了『飛行用大帆』$R;");
                            }
                            if (fgarden.Test(FGarden.完全委托飞行用大帆))
                            {
                                if (!CheckInventory(pc, 10028800, 1))
                                {
                                    Say(pc, 131, "給您道具，$R;" +
                                        "先減少行李再過來吧。$R;");
                                    return;
                                }
                                GiveItem(pc, 10028800, 1);
                                fgarden.SetValue(FGarden.完全委托飞行用大帆, false);
                                Say(pc, 131, "交還了『飛行用大帆』$R;");
                            }
                            if (GetItemCount(pc, ContainerType.BODY) > 90)
                            {
                                Say(pc, 131, "給您道具，$R;" +
                                    "先減少行李再過來吧。$R;");
                                return;
                            }
                            Say(pc, 131, "那麼請在這裡等等吧$R;");
                            Fade(pc, FadeType.Out, FadeEffect.Black);
                            PlaySound(pc, 2210, false, 100, 50);
                            Wait(pc, 2000);
                            PlaySound(pc, 2210, false, 100, 50);
                            Wait(pc, 2000);
                            PlaySound(pc, 2210, false, 100, 50);
                            Wait(pc, 2000);
                            Fade(pc, FadeType.In, FadeEffect.Black);
                            Say(pc, 131, "完成了！$R;" +
                                "飛行用帆完成了。$R;");
                            PlaySound(pc, 4006, false, 100, 50);
                            Say(pc, 131, "得到了『飛翔帆推進器』$R;");
                            Say(pc, 131, "要安裝請在飛空庭上面點擊W。$R;" +
                                "$R然後換翅膀就可以了$R;" +
                                "$P安裝飛行帆，$R;" +
                                "『舵輪』目錄上會增加$R;" +
                                "$R『飛空庭出發』喔$R;" +
                                "然後選擇想去的地方就可以了。$R;" +
                                "$P只要有聽講座，您就知道，$R;" +
                                "想飛行需要『摩根炭』呀$R;" +
                                "這是我給您的禮物唷。$R;");
                            Say(pc, 131, "得到了『摩根炭』$R;");
                            Say(pc, 131, "來，趕緊安裝吧。$R;");
                            if (fgarden.Test(FGarden.凯提推进器))
                            {
                                TakeItem(pc, 10049500, 1);
                                GiveItem(pc, 30060000, 1);
                                GiveItem(pc, 10016700, 10);
                                fgarden.SetValue(FGarden.飞空庭改造完成, true);
                                fgarden.SetValue(FGarden.给予飞空翅膀, true);
                                return;
                            }
                            if (fgarden.Test(FGarden.凯提推进器_光))
                            {
                                TakeItem(pc, 10049550, 1);
                                GiveItem(pc, 30060000, 1);
                                GiveItem(pc, 10016700, 10);
                                fgarden.SetValue(FGarden.飞空庭改造完成, true);
                                fgarden.SetValue(FGarden.给予飞空翅膀, true);
                                return;
                            }
                            if (fgarden.Test(FGarden.凯提推进器_暗))
                            {
                                TakeItem(pc, 10049551, 1);
                                GiveItem(pc, 30060000, 1);
                                GiveItem(pc, 10016700, 10);
                                fgarden.SetValue(FGarden.飞空庭改造完成, true);
                                fgarden.SetValue(FGarden.给予飞空翅膀, true);
                                return;
                            }
                            break;
                    }
                    break;
            }
        }
    }
}
