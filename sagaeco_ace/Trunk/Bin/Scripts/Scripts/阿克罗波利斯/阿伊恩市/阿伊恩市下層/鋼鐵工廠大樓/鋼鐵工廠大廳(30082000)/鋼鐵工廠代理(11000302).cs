using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30082000
{
    public class S11000302 : Event
    {
        public S11000302()
        {
            this.EventID = 11000302;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (!mask.Test(AYEFlags.與鋼鐵工廠老闆對話))//_0c38)
            {
                switch (Select(pc, "欢迎，这裡是钢铁工厂！", "", "制造武器", "强化装备", "强化装备的注意事项", "洽谈", "什么也不做"))
                {
                    case 1:
                        switch (Select(pc, "做什么呢？", "", "制造武器", "制造防具", "炼制金属", "制造『箭』", "不做"))
                        {
                            case 1:
                                switch (Select(pc, "想制作什么?", "", "制作武器", "制作魔杖", "制作弓", "放弃"))
                                {
                                    case 1:
                                        Synthese(pc, 2010, 10);
                                        break;
                                    case 2:
                                        Synthese(pc, 2021, 5);
                                        break;
                                    case 3:
                                        Synthese(pc, 2034, 5);
                                        break;
                                }
                                break;
                            case 2:
                                Synthese(pc, 2017, 5);
                                break;
                            case 3:
                                Synthese(pc, 2051, 3);
                                break;
                            case 4:
                                Synthese(pc, 2035, 5);
                                break;
                        }
                        break;
                    case 2:
                        強化說明(pc);
                        break;
                    case 3:
                        Say(pc, 131, "强化武器会给道具带来负担的喔。$R;" +
                            "一个装备最多可以强化『十次』。$R;" +
                            "强化越多次就越容易损毁，$R;" +
                            "所以要注意啊。$R;" +
                            "$P但是在强化时破碎了，$R;" +
                            "手续费也是不会退还的阿。$R;" +
                            "敬请留意$R;" +
                            "但是强化一两次是不会破碎的，$R;" +
                            "请放心。$R;" +
                            "$P啊！还有要强化的装备$R是要脱下来的，$R小心点给我吧$R;");
                        break;
                    case 4:
                        Say(pc, 131, "老板只会给有名的冒险者$R;" +
                            "介绍任务阿$R;" +
                            "$P……您$R;" +
                            pc.Name + "?$R;" +
                            "$P……好像还不够资格$R;" +
                            "没听过呢！$R;" +
                            "多工作，累积更多经验再来吧！$R;");
                        break;
                }
                return;
            }
            switch (Select(pc, "欢迎，这里是钢铁工厂！", "", "任务服务台", "买东西", "制造武器", "强化装备", "强化装备的注意事项", "什么也不做"))
            {
                case 1:
                    HandleQuest(pc, 45);
                    break;
                case 2:
                    OpenShopBuy(pc, 97);
                    break;
                case 3:
                    switch (Select(pc, "做什么呢？", "", "制造武器", "制造防具", "炼制金属", "制造『箭』", "不做"))
                    {
                        case 1:
                            switch (Select(pc, "想制作什么?", "", "制作武器", "制作魔杖", "制作弓", "放弃"))
                            {
                                case 1:
                                    Synthese(pc, 2010, 10);
                                    break;
                                case 2:
                                    Synthese(pc, 2021, 5);
                                    break;
                                case 3:
                                    Synthese(pc, 2034, 5);
                                    break;
                            }
                            break;
                        case 2:
                            Synthese(pc, 2017, 5);
                            break;
                        case 3:
                            Synthese(pc, 2051, 3);
                            break;
                        case 4:
                            Synthese(pc, 2035, 5);
                            break;
                    }
                    break;
                case 4:
                    強化說明(pc);
                    break;
                case 5:
                    Say(pc, 131, "强化武器会给道具带来负担的喔。$R;" +
                        "一个装备最多可以强化『十次』。$R;" +
                        "强化越多次就越容易损毁，$R;" +
                        "所以要注意啊。$R;" +
                        "$P但是在强化时破碎了，$R;" +
                        "手续费也是不会退还的啊。$R;" +
                        "敬请留意$R;" +
                        "但是强化一两次是不会破碎的，$R;" +
                        "请放心。$R;" +
                        "$P啊！还有要强化的装备$R是要脱下来的，$R小心点给我吧$R;");
                    break;
            }
        }

        void 強化說明(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];

            if (!mask.Test(AYEFlags.聽取強化是說明))//_2a67)
            {
                mask.SetValue(AYEFlags.聽取強化是說明, true);
                //_2a67 = true;
                Say(pc, 131, "您是第一次使用$R;" +
                    "我们这里的强化装备服务吗？$R;" +
                    "那就简单说明一下，如何？$R;");
                switch (Select(pc, "要听说明吗？", "", "听", "不听"))
                {
                    case 1:
                        Say(pc, 131, "强化装备是提高武器$R;" +
                            "或者防具性能的技术。$R;" +
                            "是我们公司独有的技术呢$R;" +
                            "$P想得到这服务$R;" +
                            "必须满足以下几个条件。$R;" +
                            "$R首先要有适合催化的『道具』$R;" +
                            "$P另外，要向我们公司$R;" +
                            "支付『手续费5000金币』啊$R;" +
                            "$P要强化的装备$R是要脱下来的，$R小心点给我吧$R;" +
                            "催化道具的不同，$R;" +
                            "强化结果也不同。$R;" +
                            "$R请准备好$R;" +
                            "与装备适合的催化道具吧。$R;");
                        if (pc.Fame < 10)
                        {
                            Say(pc, 131, "既然已经来到这里了$R;" +
                                "就送您一个催化道具吧。$R;" +
                                "$P…$R;" +
                                "$P客人，您好像是初心者呢。$R;" +
                                "现在装备还够用$R;" +
                                "还不需要强化装备吧？$R;" +
                                "以后多点去冒险，$R;" +
                                "真的需要强化装备的时候$R;" +
                                "再来吧。$R;" +
                                "$R到时再送您礼物。$R;");
                            return;
                        }
                        break;
                    case 2:
                        if (!mask.Test(AYEFlags.得到生命結晶)&& pc.Fame > 9)//_Xa13 
                        {
                            Say(pc, 131, "既然已经来到这里了$R;" +
                                "就送您一个催化道具吧。$R;");
                            if (CheckInventory(pc, 90000043, 1))
                            {
                                mask.SetValue(AYEFlags.得到生命結晶, true);
                                //_Xa13 = true;
                                GiveItem(pc, 90000043, 1);
                                Say(pc, 131, "这是『生命的结晶』。$R;" +
                                    "使用这个的话$R;" +
                                    "可以制造提升最大HP的『防具』啊$R;" +
                                    "$R试一下吧$R;");
                                return;
                            }
                            Say(pc, 131, "整理行李后，减少道具$R;" +
                                "再来吧。$R;");
                            return;
                        }
                        break;
                }
                return;
            }
            if (!mask.Test(AYEFlags.得到生命結晶) && pc.Fame > 9)//_Xa13
            {
                Say(pc, 131, "既然已经来到这里了$R;" +
                    "就送您一个催化道具吧。$R;");
                if (CheckInventory(pc, 90000043, 1))
                {
                    mask.SetValue(AYEFlags.得到生命結晶, true);
                    //_Xa13 = true;
                    GiveItem(pc, 90000043, 1);
                    Say(pc, 131, "这是『生命的结晶』。$R;" +
                        "使用这个的话$R;" +
                        "可以制造提升最大HP的『防具』啊$R;" +
                        "$R试一下吧$R;");
                    return;
                }
                Say(pc, 131, "整理行李后，减少道具$R;" +
                    "再来吧。$R;");
                return;
            }
            Say(pc, 131, "每强化一次$R;" +
                "就要5000金币手续费啊$R;" +
                "$R费用会自动转账，$R;" +
                "只要确认身上的钱就可以了。$R;");
            if (pc.Gold < 5000)
                Say(pc, 131, "这服务需要手续费5000金币$R;" +
                    "您现在的现金，好像不太够呢。$R;");
            else if (CountItem(pc, 90000043) == 0 &&
                CountItem(pc, 90000044) == 0 &&
                CountItem(pc, 90000045) == 0 &&
                CountItem(pc, 90000046) == 0)
                Say(pc, 131, "没有拿催化道具呢。$R;");
            else if (!ItemEnhance(pc))
                Say(pc, 131, "没拿材料道具呢。$R;");
        }
    }
}