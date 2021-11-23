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
                switch (Select(pc, "欢迎来到飞空庭大工厂", "", "木材加工", "什么也不做"))
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
                switch (Select(pc, "欢迎来到飞空庭大工厂", "", "木材加工", "改造飞空庭", "什么也不做"))
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
               switch (Select(pc, "新的飞行帆制作？", "", "不制作", "制作！"))
               {
                   case 1:
                       break;
                   case 2:
                       Say(pc, 131, "OK！$R;" +
                           "根据心情来更换飞行帆$R;" +
                           "是件非常棒的事！$R;" +
                           "$R那么、马上开始$R;" +
                           "著手制作吧！$R;" +
                           "$P价格、制作要9999万G！$R;");
                       Select(pc, "怎么办？", "", "盯", "无视", "冷目相看");
                       Say(pc, 131, "啊！$R;" +
                           "果然第二次就吓不倒你了啊。$R;" +
                           "$R刚才的是从材料收集到$R;" +
                           "装配全部都在这边做的价格。$R;" +
                           "$R客人你自己搜集材料$R;" +
                           "来做的话只需要5000G。$R;");
                       switch (Select(pc, "怎么办？", "", "考虑一下", "5000G"))
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
                                   Say(pc, 131, "那么开始受理了！$R;" +
                                       "负责人就是我雪列娜。$R;" +
                                       "$R有任何问题的话$R;" +
                                       "就到我这里来报告吧$R;" +
                                       "$P那么赶紧开始收集$R;" +
                                       "飞行帆所需要的材料。$R;" +
                                       "$R需要收集的道具有……$R;" +
                                       "$P　飞空庭甲板　1个$R;" +
                                       "　涡轮引擎　1个$R;" +
                                       "　汽笛　1个$R;" +
                                       "　飞行用帆　2个$R;" +
                                       "　飞行用大帆　2个$R;" +
                                       "$P道具预存在大工厂。$R;" +
                                       "$R需要的道具全部收集齐全后$R;" +
                                       "就立即进入飞行帆装配作业！$R;");
                                   return;
                               }
                               Say(pc, 131, "金钱好像不够哦……。$R;");
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
                if (CountItem(pc, 10049500) >= 1)//30060000
                {
                    Say(pc, 131, "这是……$R;"+
                                 "推进器?$R;"+
                                 "……天哪$R;"+
                                 "是这个东西的话,稍作加工就可以做出$R;"+
                                 "奇迹一般的飞空帆$R;"+
                                 "甚至连摩戈炭都不需要!$R;"+
                                 "如何!要试试吗?");
                    switch (Select(pc, "怎么办呢？", "", "不要", "怎么有拒绝的理由!"))
                    {
                        case 1:
                            return;
                        case 2:
                            Say(pc, 131, "交给我吧!");
                            Fade(pc, FadeType.Out, FadeEffect.Black);
                            PlaySound(pc, 2210, false, 100, 50);
                            Wait(pc, 2000);
                            PlaySound(pc, 2210, false, 100, 50);
                            Wait(pc, 2000);
                            PlaySound(pc, 2210, false, 100, 50);
                            Wait(pc, 2000);
                            Fade(pc, FadeType.In, FadeEffect.Black);
                            TakeItem(pc, 10049500, 1);
                            GiveItem(pc, 30060000,1);
                            Say(pc, 131, "好$R;" +
                                 "完成了!$R;" +
                                 "赶紧拿去试试吧?");
                            fgarden.SetValue(FGarden.飞空庭改造完成, true);
                            fgarden.SetValue(FGarden.给予飞空翅膀, true);
                            //推進器(pc);
                            break;
                    }
                }
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
                Say(pc, 131, "辛苦了$R;" +
                    "收集完道具了吗？$R;");
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
                        Say(pc, 131, "后背有点痒啊$R;");
                        Say(pc, 131, "啊，那是推进器！$R;" +
                            "$R肯定想搭乘飞空庭了。$R;");
                        switch (Select(pc, "怎么办呢？", "", "才不是呢，我不坐", "乘坐"))
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
                switch (Select(pc, "怎么办呢？", "", "交付『飞空庭甲板』", "交付『涡轮引擎』", "交付『汽笛』", "交付『飞行用帆』", "交付『飞行用大帆』", "确认部件收集情況。", "放弃"))
                {
                    case 1:
                        if (fgarden.Test(FGarden.委托飞空庭甲板))
                        {
                            Say(pc, 131, "那个道具已经委托了吗？$R;");
                            return;
                        }
                        if (CountItem(pc, 10029100) < 1)
                        {
                            Say(pc, 131, "好像没有道具呀$R;");
                            return;
                        }
                        Say(pc, 131, "这是『飞空庭甲板』吗？$R;" +
                            "怎么找到这个的，不错嘛。$R;");
                        switch (Select(pc, "怎么办呢？", "", "交付", "不交付"))
                        {
                            case 1:
                                TakeItem(pc, 10029100, 1);
                                fgarden.SetValue(FGarden.委托飞空庭甲板, true);
                                Say(pc, 131, "交付了『飛空庭甲板』$R;");
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 2:
                        if (fgarden.Test(FGarden.委托涡轮引擎))
                        {
                            Say(pc, 131, "那个道具已经委托了吗？$R;");
                            return;
                        }
                        if (CountItem(pc, 10027750) < 1)
                        {
                            Say(pc, 131, "好像没有道具呀$R;");
                            return;
                        }
                        Say(pc, 131, "这是『涡轮引擎』呀，$R;" +
                            "怎么找到的，不错呀。$R;");
                        switch (Select(pc, "怎么办呢？", "", "交付『涡轮引擎』", "不委托"))
                        {
                            case 1:
                                TakeItem(pc, 10027750, 1);
                                fgarden.SetValue(FGarden.委托涡轮引擎, true);
                                Say(pc, 131, "交付『涡轮引擎』了$R;");
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 3:
                        if (fgarden.Test(FGarden.委托汽笛))
                        {
                            Say(pc, 131, "那个道具已经交付了$R;");
                            return;
                        }
                        if (CountItem(pc, 10018600) < 1)
                        {
                            Say(pc, 131, "好像没有道具呀$R;");
                            return;
                        }
                        Say(pc, 131, "这是『汽笛』呀，$R;" +
                            "怎么找到的，不错呀。$R;");
                        switch (Select(pc, "怎么办呢？", "", "交付『汽笛』", "不交付"))
                        {
                            case 1:
                                TakeItem(pc, 10018600, 1);
                                fgarden.SetValue(FGarden.委托汽笛, true);
                                Say(pc, 131, "交付『汽笛』了$R;");
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 4:
                        if (fgarden.Test(FGarden.完全委托飞行用帆))
                        {
                            Say(pc, 131, "那个道具已经交付了$R;");
                            return;
                        }
                        if (CountItem(pc, 10028700) < 1)
                        {
                            Say(pc, 131, "好像没有道具呀$R;");
                            return;
                        }
                        Say(pc, 131, "这是『飞行用帆』呀$R;" +
                            "怎么找到的，不错呀。$R;");
                        switch (Select(pc, "怎么办呢？", "", "交付『飞行用帆』", "不交付"))
                        {
                            case 1:
                                if (fgarden.Test(FGarden.委托飞行用帆))
                                {
                                    TakeItem(pc, 10028700, 1);
                                    fgarden.SetValue(FGarden.完全委托飞行用帆, true);
                                    Say(pc, 131, "交付『飞行用帆』了$R;");
                                    return;
                                }
                                TakeItem(pc, 10028700, 1);
                                fgarden.SetValue(FGarden.委托飞行用帆, true);
                                Say(pc, 131, "交付『飞行用帆』了$R;");
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 5:
                        if (fgarden.Test(FGarden.完全委托飞行用大帆))
                        {
                            Say(pc, 131, "那个道具已经交付了吗？$R;");
                            return;
                        }
                        if (CountItem(pc, 10028800) < 1)
                        {
                            Say(pc, 131, "好像没有道具呀$R;");
                            return;
                        }
                        Say(pc, 131, "这是『飞行用大帆』呀$R;" +
                            "怎么找到的，不错呀。$R;");
                        switch (Select(pc, "怎么办呢？", "", "交付『飞行用大帆』", "不交付"))
                        {
                            case 1:
                                if (fgarden.Test(FGarden.委托飞行用大帆))
                                {
                                    TakeItem(pc, 10028800, 1);
                                    fgarden.SetValue(FGarden.完全委托飞行用大帆, true);
                                    Say(pc, 131, "完全交付了『飛行用大帆』了$R;");
                                    return;
                                }
                                TakeItem(pc, 10028800, 1);
                                fgarden.SetValue(FGarden.委托飞行用大帆, true);
                                Say(pc, 131, "交付『飞行用大帆』了$R;");
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 6:
                        GiveItem(pc, 10020104, 1);
                        Say(pc, 131, "做一个清单吧$R;" +
                            "这样才好随时确认$R;");
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
                    "加固工程结束啰。$R;" +
                    "没经过允许，我就先看了。$R;" +
                    "真的太漂亮了。$R;" +
                    "$P这里卖一些家具，看看吧。$R;" +
                    "$R那里的哈尔列尔利也在卖喔$R;" +
                    "$P下一步准备一些$R;" +
                    "飞行帆的材料吧。$R;" +
                    "$R要收集的道具有$R;" +
                    "$P飞空庭甲板1个$R;" +
                    "涡轮引擎1个$R;" +
                    "汽笛1个$R;" +
                    "飞行用帆2个$R;" +
                    "飞行用大帆2个哦$R;" +
                    "$P道具委托大工厂了$R;" +
                    "$R需要的道具收集完后，$R就开始组装飞行用帆吧。$R;");
                return;
            }
            #endregion

            if (fgarden.Test(FGarden.铁板收集完毕))
            {
                Say(pc, 131, "工厂正在作业，$R;" +
                    "$R需要一些时间，$R;" +
                    "您先坐在椅子上听一听$R;" +
                    "$R『阿克罗尼亚航空法』的讲座吧。$R;" +
                    "$P讲座由$CO木偶制作大师蒂阿$CD大师负责。$R;" +
                    "$R木偶制作大师蒂阿，拜托了。$R;");
                Say(pc, 131, "OK！木偶制作大师雪列娜$R;" +
                    "到这里来吧。$R;");
                return;
            }

            if (fgarden.Test(FGarden.接受改造飞空庭订单))
            {
                if (CountItem(pc, 10038616) >= 30)
                {
                    fgarden.SetValue(FGarden.铁板收集完毕, true);
                    TakeItem(pc, 10038616, 30);
                    Say(pc, 131, "累了吧？$R;" +
                        "30张『铁板』已经收集好了。$R;" +
                        "$R好，正好$R;" +
                        "木偶制作大师们正好$R;" +
                        "刚刚结束作业准备呢。$R;" +
                        "$R那么马上开始加固工程吧$R;");
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 0, 131, "给了30张『铁板』$R;");
                    Say(pc, 131, "木偶制作大师是我们大工厂的骄傲喔，$R;" +
                        "他们会竭尽全力$R;" +
                        "改造您的飞空庭。$R;" +
                        "$R改造需要一些时间，$R;" +
                        "您先坐在椅子上听一听$R;" +
                        "『阿克罗尼亚航空法』的讲座吧。$R;" +
                        "$P讲座由$CO木偶制作大师蒂阿$CD先生负责。$R;" +
                        "$R蒂阿，拜托了。$R;");
                    Say(pc, 131, "OK！雪列娜$R;" +
                        "喂,你,到这里来吧。$R;");
                    return;
                }
                Say(pc, 131, "累了吧？$R;" +
                    "让我看看收集了多少？$R;" +
                    "$P您现在为了飞空庭本体$R;" +
                    "加固工程的需要$R;" +
                    "$R收集30张『铁板』中$R;" +
                    "$P制作铁板需要很多『铁块』$R;" +
                    "$R岩石、铁矿石岩等材料$R这些可以从魔物那取得，$R;" +
                    "要努力收集呀。$R;");
                return;
            }

            #region 接受订单

            Say(pc, 131, "改造飞空庭？$R;" +
                "$R真是好久没有接到过的订单呀$R;" +
                "木偶制作大师们该兴奋了，哈哈$R;" +
                "$R先说明一下吧$R;" +
                "$P改造飞空庭，$R;" +
                "就可以用自己的飞空庭，$R;" +
                "去各个港口唷$R;" +
                "可以随意去不同地方，$R;" +
                "对冒险者来说很方便的，对吧！$R;" +
                "$P改造只需9999万金喔！$R;");
            Select(pc, "怎么办呢?", "", "怎么办？", "尖叫", "晕过去了");
            Say(pc, 131, "稍等一下阿$R;" +
                "我是开玩笑呢！$R;" +
                "$R我知道您付不起$R;" +
                "$P这是我收集材料组装制做完成的价钱，$R;" +
                "$R客人如果自己收集材料来代工，$R我只收1万金唷$R;");
            int SEL;
            do
            {
                SEL = Select(pc, "怎么办呢?", "", "先想想", "9999万金币", "1万金币");
                switch (SEL)
                {
                    case 2:
                        Say(pc, 131, "呵呵呵！$R;" +
                            "好好笑啊$R;");
                        break;
                    case 3:
                        if (pc.Gold < 10000)
                        {
                            Say(pc, 131, "金币不足呀……$R;");
                            return;
                        }
                        fgarden.SetValue(FGarden.接受改造飞空庭订单, true);
                        pc.Gold -= 10000;
                        Say(pc, 131, "那么就接受改造订单了。$R;" +
                            "负责人是我雪列娜$R;" +
                            "$R有什么不清楚的$R;" +
                            "跟我联系吧$R;" +
                            "$P那么先来说明$R;" +
                            "改造工程的顺序，$R;" +
                            "$P先进行飞空庭本体的加固工程喔$R;" +
                            "$R然后制做飞行用帆$R;" +
                            "并把飞行用帆安装在飞空庭上$R;" +
                            "工程算结束了。$R;" +
                            "$P这样就可以$R;" +
                            "在空中自由翱翔了$R;" +
                            "$P客人请亲自去收集$R;" +
                            "加固工程需要的材料和$R;" +
                            "飞行用帆的材料喔$R;" +
                            "$R不要太担心$R;" +
                            "一定会收集到的$R;" +
                            "$P那么先从$R;" +
                            "加固工程需要的材料开始…$R;" +
                            "首先收集的道具有……$R;" +
                            "$P『$CO铁板$CD』30张！$R;" +
                            "$R有点重哦，请小心收集$R;" +
                            "$P制作铁板需要很多『$CO铁块$CD』喔$R;" +
                            "$R岩石、铁矿石岩等材料$R;" +
                            "可以从魔物那取得，$R;" +
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
            Say(pc, 131, "现在道具全部收集好了。$R;" +
                "$R辛苦了，$R;" +
                "$P来，现在我们开始组装，$R;" +
                "结束之前，请等一会儿$R;" +
                "$R会给您做一个漂亮的飞行帆的。$R;");

            if (GetItemCount(pc, ContainerType.BODY) > 90)
            {
                Say(pc, 131, "给您道具，$R;" +
                    "先减少行李再过来吧。$R;");
                return;
            }
            Say(pc, 131, "装饰想怎么做呢？$R;" +
                "$R准备了火箭系列和$R魔法翅膀系列两种。$R;");
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Say(pc, 131, "功能都一样！$R;" +
                    "女孩子比较喜欢魔法翅膀系列吧$R;");
            }
            else
            {
                Say(pc, 131, "功能都一样！$R;" +
                    "男人比较喜欢火箭系列吧$R;");
            }
            switch (Select(pc, "选择哪种呢？", "", "火箭系列", "魔法翅膀系列"))
            {
                case 1:
                    GiveItem(pc, 30060100, 1);
                    GiveItem(pc, 10016700, 10);
                    fgarden.SetValue(FGarden.给予飞空翅膀, true);
                    Say(pc, 131, "OK$R;" +
                        "$R火箭系列是吗？$R;" +
                        "那么在这里等一下。$R;");
                    Fade(pc, FadeType.Out, FadeEffect.Black);
                    PlaySound(pc, 2210, false, 100, 50);
                    Wait(pc, 2000);
                    PlaySound(pc, 2210, false, 100, 50);
                    Wait(pc, 2000);
                    PlaySound(pc, 2210, false, 100, 50);
                    Wait(pc, 2000);
                    Fade(pc, FadeType.In, FadeEffect.Black);
                    Say(pc, 131, "完成了！$R;" +
                        "飞行帆完成了！$R;");
                    PlaySound(pc, 4006, false, 100, 50);
                    Say(pc, 131, "得到了『火箭飞行帆』$R;");
                    Say(pc, 131, "要安装请在飞空庭上面点击。$R;" +
                        "$R更换翅膀就可以了$R;" +
                        "$P安装飞行帆时，$R;" +
                        "『舵轮』目录上$R;" +
                        "会增加『飞空庭出发』$R;" +
                        "$R然后选择想去的地方就可以了。$R;" +
                        "$P听完讲座，您一定知道，$R;" +
                        "想飞行需要大量『摩戈炭』吧$R;" +
                        "$R这是我给您的礼物。$R;");
                    Say(pc, 131, "得到了『摩戈炭』$R;");
                    Say(pc, 131, "来，赶紧安装吧。$R;");
                    break;
                case 2:
                    GiveItem(pc, 30060200, 1);
                    GiveItem(pc, 10016700, 10);
                    fgarden.SetValue(FGarden.给予飞空翅膀, true);
                    Say(pc, 131, "OK$R;" +
                        "$R魔法翅膀系列合适吧？$R;" +
                        "那么在这里稍等一下吧。$R;");
                    Fade(pc, FadeType.Out, FadeEffect.Black);
                    PlaySound(pc, 2210, false, 100, 50);
                    Wait(pc, 2000);
                    PlaySound(pc, 2210, false, 100, 50);
                    Wait(pc, 2000);
                    PlaySound(pc, 2210, false, 100, 50);
                    Wait(pc, 2000);
                    Fade(pc, FadeType.In, FadeEffect.Black);
                    Say(pc, 131, "完成了！$R;" +
                        "飞行帆完成了！$R;");
                    PlaySound(pc, 4006, false, 100, 50);
                    Say(pc, 131, "得到了『翅膀飞行帆』$R;");
                    Say(pc, 131, "要安装请在飞空庭上面点击。$R;" +
                        "$R更换翅膀就可以了$R;" +
                        "$P安装飞行帆时，$R;" +
                        "『舵轮』目录上$R;" +
                        "$R会增加『飞空庭出发』$R;" +
                        "然后选择想去的地方就可以了。$R;" +
                        "$P听完讲座，您一定知道，$R;" +
                        "想飞行需要大量『摩戈炭』吧$R;" +
                        "$R这是我给您的礼物。$R;");
                    Say(pc, 131, "得到了『摩戈炭』$R;");
                    Say(pc, 131, "来，赶紧安装吧。$R;");
                    break;
            }
        }

        void 推進器(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            Say(pc, 131, "记住这一点，$R;" +
                "一但安装推进器，$R;" +
                "飞空庭就不能恢复原状了。$R;");
            switch (Select(pc, "怎么办呢？", "", "有点难决定呀", "好吧，不管了"))
            {
                case 1:
                    break;
                case 2:
                    Say(pc, 131, "最后再问一次$R;" +
                        "真的，真的没关系吗？$R;" +
                        "可能会失去推进器喔。$R;" +
                        "$R我跟您再确认一次$R;" +
                        "因为真的恢复不了原状啊。$R;" +
                        "$P如果这样也无所谓的话…$R;" +
                        "那么请选择『安装推进器』吧。$R;");
                    switch (Select(pc, "怎么办呢？", "", "放弃了", "安装推进器"))
                    {
                        case 1:
                            break;
                        case 2:
                            Say(pc, 131, "知道了！$R;" +
                                "$R把到现在为止收集的飞行帆还给您。$R;");

                            if (fgarden.Test(FGarden.委托飞空庭甲板))
                            {
                                if (!CheckInventory(pc,10029100,1))
                                {
                                    Say(pc, 131, "给您道具，$R;" +
                                        "先减少行李再过来吧。$R;");
                                    return;
                                }

                                GiveItem(pc, 10029100, 1);
                                fgarden.SetValue(FGarden.委托飞空庭甲板, false);
                                Say(pc, 131, "『飞空庭甲板』$R;");
                            }
                            if (fgarden.Test(FGarden.委托涡轮引擎))
                            {
                                if (!CheckInventory(pc,10027750,1))
                                {
                                    Say(pc, 131, "给您道具，$R;" +
                                        "先减少行李再过来吧。$R;");
                                    return;
                                }
                                GiveItem(pc, 10027750, 1);
                                fgarden.SetValue(FGarden.委托涡轮引擎, false);
                                Say(pc, 131, "交还了『涡轮引擎』$R;");
                            }
                            if (fgarden.Test(FGarden.委托汽笛))
                            {
                                if (!CheckInventory(pc,10018600,1))
                                {
                                    Say(pc, 131, "给您道具，$R;" +
                                        "先减少行李再过来吧。$R;");
                                    return;
                                }
                                GiveItem(pc, 10018600, 1);
                                fgarden.SetValue(FGarden.委托汽笛, false);
                                Say(pc, 131, "交还了『汽笛』$R;");
                            }
                            if (fgarden.Test(FGarden.委托飞行用帆))
                            {
                                if (!CheckInventory(pc,10028700,1))
                                {
                                    Say(pc, 131, "给您道具，$R;" +
                                        "先减少行李再过来吧。$R;");
                                    return;
                                }
                                GiveItem(pc, 10028700, 1);
                                fgarden.SetValue(FGarden.委托飞行用帆, false);
                                Say(pc, 131, "交还了『飞行用的帆』$R;");
                            }
                            if (fgarden.Test(FGarden.完全委托飞行用帆))
                            {
                                if (!CheckInventory(pc, 10028700, 1))
                                {
                                    Say(pc, 131, "给您道具，$R;" +
                                        "先减少行李再过来吧。$R;");
                                    return;
                                }
                                GiveItem(pc, 10028700, 1);
                                fgarden.SetValue(FGarden.完全委托飞行用帆, false);
                                Say(pc, 131, "交还了『飞行用的帆』$R;");
                            }
                            if (fgarden.Test(FGarden.委托飞行用大帆))
                            {
                                if (!CheckInventory(pc, 10028800, 1))
                                {
                                    Say(pc, 131, "给您道具，$R;" +
                                        "先减少行李再过来吧。$R;");
                                    return;
                                }
                                GiveItem(pc, 10028800, 1);
                                fgarden.SetValue(FGarden.委托飞行用大帆, false);
                                Say(pc, 131, "交还了『飞行用的大帆』$R;");
                            }
                            if (fgarden.Test(FGarden.完全委托飞行用大帆))
                            {
                                if (!CheckInventory(pc, 10028800, 1))
                                {
                                    Say(pc, 131, "给您道具，$R;" +
                                        "先减少行李再过来吧。$R;");
                                    return;
                                }
                                GiveItem(pc, 10028800, 1);
                                fgarden.SetValue(FGarden.完全委托飞行用大帆, false);
                                Say(pc, 131, "交还了『飞行用的大帆』$R;");
                            }
                            if (GetItemCount(pc, ContainerType.BODY) > 90)
                            {
                                Say(pc, 131, "给您道具，$R;" +
                                    "先减少行李再过来吧。$R;");
                                return;
                            }
                            Say(pc, 131, "那么请在这里等等吧$R;");
                            Fade(pc, FadeType.Out, FadeEffect.Black);
                            PlaySound(pc, 2210, false, 100, 50);
                            Wait(pc, 2000);
                            PlaySound(pc, 2210, false, 100, 50);
                            Wait(pc, 2000);
                            PlaySound(pc, 2210, false, 100, 50);
                            Wait(pc, 2000);
                            Fade(pc, FadeType.In, FadeEffect.Black);
                            Say(pc, 131, "完成了！$R;" +
                                "飞行用的帆完成了。$R;");
                            PlaySound(pc, 4006, false, 100, 50);
                            Say(pc, 131, "得到了『飞翔帆推进器』$R;");
                            //Say(pc, 131, "要安装请在飞空庭上面点击。$R;" +
                            //    "$R然后换翅膀就可以了$R;" +
                            //    "$P安装飞行帆，$R;" +
                            //    "『舵轮』目录上会增加$R;" +
                            //    "$R『飞空庭出发』喔$R;" +
                            //    "然后选择想去的地方就可以了。$R;" +
                            //    "$P只要有听讲座，您就知道，$R;" +
                            //    "想飞行需要『摩戈炭』呀$R;" +
                            //    "这是我给您的礼物哦。$R;");
                            //Say(pc, 131, "得到了『摩戈炭』$R;");
                            Say(pc, 131, "来，赶紧安装吧。$R;");
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
