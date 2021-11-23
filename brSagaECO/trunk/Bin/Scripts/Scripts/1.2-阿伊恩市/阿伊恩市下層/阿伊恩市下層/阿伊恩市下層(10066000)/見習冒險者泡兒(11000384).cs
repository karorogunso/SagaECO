using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000384 : Event
    {
        public S11000384()
        {
            this.EventID = 11000384;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            /*
            //PARAM ME.WORK0 = GETQID
            if (//ME.WORK0 = 12010017
            )
            {
                //EVT1100038407
                Say(pc, 131, "啊，是在做我拜託的事嗎？$R;" +
                    "醫生說了『諾頓的妙藥』$R;" +
                    "能夠醫治我媽媽的病呢。$R;" +
                    "$P可是，藥店裡沒有$R;" +
                    "也不知道在哪裡才可以找到$R;" +
                    "該怎麼辦呢?$R;" +
                    "$R求求您！求您救救我媽！$R;");
                //EVENTEND
                return;
            }
             */
            if (CountItem(pc, 10048006) >= 1)
            {
                if (!mask.Test(AYEFlags.交還泡兒的禮券第一次))//_2a87)
                {
                    Say(pc, 131, "啊，是我做的票。$R;" +
                        "謝謝您給我帶來藥草。$R;");
                    if (CheckInventory(pc, 10025210, 1))
                    {
                        mask.SetValue(AYEFlags.交還泡兒的禮券第一次, true);
                        //_2a87 = true;
                        TakeItem(pc, 10048006, 1);
                        GiveItem(pc, 10025210, 1);
                        Say(pc, 131, "這是在外面玩耍的時候發現的。$R;" +
                            "金光閃閃的，漂亮吧？$R;" +
                            "$R是我的寶物呢。收下吧。$R;");
                        return;
                    }
                    Say(pc, 131, "要給您好東西$R;" +
                        "整理好行李以後，再來吧$R;");
                    return;
                }
                if (!mask.Test(AYEFlags.交還泡兒的禮券第二次))//_2a88)
                {
                    SkillPointBonus(pc, 1);
                    TakeItem(pc, 10048006, 1);
                    mask.SetValue(AYEFlags.交還泡兒的禮券第二次, true);
                    //_2a88 = true;
                    Say(pc, 131, "啊，是我做的票。$R;" +
                        "謝謝您給我帶來藥草。$R;" +
                        "$R這是給您的謝禮。$R;");
                    Wait(pc, 2000);
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 2000);
                    Say(pc, 131, "技能點數升一點$R;");
                    return;
                }
                TakeItem(pc, 10048006, 1);
                Say(pc, 131, "啊，是我做的票。$R;" +
                    "謝謝您給我帶來藥草。$R;" +
                    "我給您揉肩吧。$R;" +
                    "$P啪啪啪啪！$R;" +
                    "啪啪啪！$R;" +
                    "$R怎麼樣？舒服吧？$R;");
                return;
            }
            if (mask.Test(AYEFlags.交還泡兒的禮券第一次))//_2a87)
            {
                Say(pc, 131, "謝謝您給我帶來藥草。$R;" +
                    "媽媽一定會好起來的。$R;" +
                    "$P這是媽媽做的防具。$R;" +
                    "$R好堅固的！$R;" +
                    "剩的不多了，便宜賣給您吧。$R;");
                switch (Select(pc, "要買什麼樣的呢？", "", "男孩衣服", "女孩衣服", "不要"))
                {
                    case 1:
                        OpenShopBuy(pc, 109);
                        break;
                    case 2:
                        OpenShopBuy(pc, 110);
                        break;
                }
                return;
            }
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                if (pc.Level < 11)
                {
                    Say(pc, 131, "我要成為冒險者$R;" +
                        "$R什麼?$R;" +
                        "您也是冒險者嗎？$R;" +
                        "看起來不像啊。$R;");
                    return;
                }
                if (pc.Level < 16)
                {
                    Say(pc, 131, "冒險有意思嗎？$R;" +
                        "我也想像您一樣去冒險阿$R;");
                    return;
                }
                if (pc.Level < 21)
                {
                    Say(pc, 131, "或許，您見過我爸爸吧？$R;" +
                        "$R我爸也是冒險者呢。$R;" +
                        "如果見過的話，請告訴我好嗎？$R;");
                    return;
                }
                if (pc.Level < 26)
                {
                    Say(pc, 131, "您看起來很強呢。$R;" +
                        "到現在為止抓了多少魔物?$R;" +
                        "$R我也可以像您那樣嗎？$R;");
                    return;
                }
                if (pc.Level < 31)
                {
                    Say(pc, 131, "您是……$R;" +
                        pc.Name +
                        "?$R;" +
                        "咖啡館老闆經常說起您呢$R;");
                    return;
                }
                if (pc.Level < 36 || !mask.Test(AYEFlags.泡兒的對話))//_2a86)
                {
                    mask.SetValue(AYEFlags.泡兒的對話, true);
                    //_2a86 = true;
                    Say(pc, 131, "我也想和您一樣$R;" +
                        pc.Name +
                            "姐姐！$R;" +
                        "可以叫您姐姐嗎？$R;");
                    return;
                }
                if (pc.Level < 39)
                {
                    Say(pc, 131, pc.Name +
                        "姐姐！$R;" +
                        "$R總有一天$R;" +
                        "我會和姐姐您一樣的$R;");
                    return;
                }
                Say(pc, 131, pc.Name +
                        "姐姐！$R;" +
                    "怎麼辦？$R;" +
                    "媽媽不舒服呢。$R;" +
                    "$P需要的藥材$R;" +
                    "雖然已經和咖啡館說了，$R;" +
                    "但是可能因為報酬太少，$R;" +
                    "沒人肯接這任務呢。$R;" +
                    "$R媽媽若是去世的話$R;" +
                    "我怎麼辦啊？$R;");
                return;
            }
            if (pc.Level < 11)
            {
                Say(pc, 131, "我要成為冒險者$R;" +
                    "$R什麼?$R;" +
                    "您也是冒險者嗎？$R;" +
                    "看起來不像啊。$R;");
                return;
            }
            if (pc.Level < 16)
            {
                Say(pc, 131, "冒險有意思嗎？$R;" +
                    "我也想像您一樣去冒險阿$R;");
                return;
            }
            if (pc.Level < 21)
            {
                Say(pc, 131, "或許，您見過我爸爸吧？$R;" +
                    "$R我爸也是冒險者呢。$R;" +
                    "如果見過的話，請告訴我好嗎？$R;");
                return;
            }
            if (pc.Level < 26)
            {
                Say(pc, 131, "您看起來很強呢。$R;" +
                    "到現在為止抓了多少魔物?$R;" +
                    "$R我也可以像您那樣嗎？$R;");
                return;
            }
            if (pc.Level < 31)
            {
                Say(pc, 131, "您是……$R;" +
                    pc.Name +
                    "?$R;" +
                    "咖啡館老闆經常說起您呢$R;");
                return;
            }
            if (pc.Level < 36 || !mask.Test(AYEFlags.泡兒的對話))//_2a86)
            {
                mask.SetValue(AYEFlags.泡兒的對話, true);
                //_2a86 = true;
                Say(pc, 131, "我也想和您一樣$R;" +
                    pc.Name +
                    "哥$R;" +
                    "可以叫您哥哥嗎？$R;");
                return;
            }
            if (pc.Level < 39)
            {
                Say(pc, 131, pc.Name +
                    "哥！$R;" +
                    "總有一天$R;" +
                    "我會和哥哥您一樣的$R;");
                return;
            }
            Say(pc, 131, pc.Name +
                "哥…$R;" +
                "怎麼辦？$R;" +
                "媽媽不舒服呢。$R;" +
                "$P需要的藥材$R;" +
                "雖然已經和咖啡館說了，$R;" +
                "但是可能因為報酬太少，$R;" +
                "沒人肯接這任務呢。$R;" +
                "$R媽媽若是去世的話$R;" +
                "我怎麼辦啊？$R;");
        }
    }
}