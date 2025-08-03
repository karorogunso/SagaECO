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
                Say(pc, 131, "啊，是在做我拜托的事吗？$R;" +
                    "医生说了『北国的灵药』$R;" +
                    "能夠医治我妈妈的病呢。$R;" +
                    "$P可是，药店里没有$R;" +
                    "也不知道在哪里才可以找到$R;" +
                    "该怎么办呢?$R;" +
                    "$R求求您！求您救救我妈妈！$R;");
                //EVENTEND
                return;
            }
             */
            if (CountItem(pc, 10048006) >= 1)
            {
                if (!mask.Test(AYEFlags.交還泡兒的禮券第一次))//_2a87)
                {
                    Say(pc, 131, "啊，是我做的票。$R;" +
                        "谢谢您给我带来药草。$R;");
                    if (CheckInventory(pc, 10025210, 1))
                    {
                        mask.SetValue(AYEFlags.交還泡兒的禮券第一次, true);
                        //_2a87 = true;
                        TakeItem(pc, 10048006, 1);
                        GiveItem(pc, 10025210, 1);
                        Say(pc, 131, "这是在外面玩耍的时候发现的。$R;" +
                            "金光闪闪的，漂亮吧？$R;" +
                            "$R是我的宝物呢。收下吧。$R;");
                        return;
                    }
                    Say(pc, 131, "要给您好东西$R;" +
                        "整理好行李以后，再来吧$R;");
                    return;
                }
                if (!mask.Test(AYEFlags.交還泡兒的禮券第二次))//_2a88)
                {
                    SkillPointBonus(pc, 1);
                    TakeItem(pc, 10048006, 1);
                    mask.SetValue(AYEFlags.交還泡兒的禮券第二次, true);
                    //_2a88 = true;
                    Say(pc, 131, "啊，是我做的票。$R;" +
                        "谢谢您给我带来药草。$R;" +
                        "$R这是给您的谢礼。$R;");
                    Wait(pc, 2000);
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 2000);
                    Say(pc, 131, "技能点数升一点$R;");
                    return;
                }
                TakeItem(pc, 10048006, 1);
                Say(pc, 131, "啊，是我做的票。$R;" +
                    "谢谢您给我带来药草。$R;" +
                    "我给您揉肩吧。$R;" +
                    "$P啪啪啪啪！$R;" +
                    "啪啪啪！$R;" +
                    "$R怎么样？舒服吧？$R;");
                return;
            }
            if (mask.Test(AYEFlags.交還泡兒的禮券第一次))//_2a87)
            {
                Say(pc, 131, "谢谢您给我带来药草。$R;" +
                    "妈妈一定会好起来的。$R;" +
                    "$P这是妈妈做的防具。$R;" +
                    "$R好坚固的！$R;" +
                    "剩的不多了，便宜卖给您吧。$R;");
                switch (Select(pc, "要买什么样的呢？", "", "男孩衣服", "女孩衣服", "不要"))
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
                    Say(pc, 131, "我要成为冒险者$R;" +
                        "$R什么?$R;" +
                        "您也是冒险者吗？$R;" +
                        "看起来不像啊。$R;");
                    return;
                }
                if (pc.Level < 16)
                {
                    Say(pc, 131, "冒险有意思吗？$R;" +
                        "我也想像您一样去冒险阿$R;");
                    return;
                }
                if (pc.Level < 21)
                {
                    Say(pc, 131, "或许，您见过我爸爸吧？$R;" +
                        "$R我爸也是冒险者呢。$R;" +
                        "如果见过的话，请告诉我好吗？$R;");
                    return;
                }
                if (pc.Level < 26)
                {
                    Say(pc, 131, "您看起来很强呢。$R;" +
                        "到现在为止抓了多少魔物?$R;" +
                        "$R我也可以像您那样吗？$R;");
                    return;
                }
                if (pc.Level < 31)
                {
                    Say(pc, 131, "您是……$R;" +
                        pc.Name +
                        "?$R;" +
                        "酒馆老板经常说起您呢$R;");
                    return;
                }
                if (pc.Level < 36 || !mask.Test(AYEFlags.泡兒的對話))//_2a86)
                {
                    mask.SetValue(AYEFlags.泡兒的對話, true);
                    //_2a86 = true;
                    Say(pc, 131, "我也想和您一样$R;" +
                        pc.Name +
                            "姐姐！$R;" +
                        "可以叫您姐姐吗？$R;");
                    return;
                }
                if (pc.Level < 39)
                {
                    Say(pc, 131, pc.Name +
                        "姐姐！$R;" +
                        "$R总有一天$R;" +
                        "我会和姐姐您一样的$R;");
                    return;
                }
                Say(pc, 131, pc.Name +
                        "姐姐！$R;" +
                    "怎么办？$R;" +
                    "妈妈不舒服呢。$R;" +
                    "$P需要的药材$R;" +
                    "虽然已经和酒馆说了，$R;" +
                    "但是可能因为报酬太少，$R;" +
                    "没人肯接这任务呢。$R;" +
                    "$R妈妈若是去世的话$R;" +
                    "我怎么办啊？$R;");
                return;
            }
            if (pc.Level < 11)
            {
                Say(pc, 131, "我要成为冒险者$R;" +
                    "$R什么?$R;" +
                    "您也是冒险者吗？$R;" +
                    "看起来不像啊。$R;");
                return;
            }
            if (pc.Level < 16)
            {
                Say(pc, 131, "冒险有意思吗？$R;" +
                    "我也想像您一样去冒险阿$R;");
                return;
            }
            if (pc.Level < 21)
            {
                Say(pc, 131, "或许，您见过我爸爸吧？$R;" +
                    "$R我爸也是冒险者呢。$R;" +
                    "如果见过的话，请告诉我好吗？$R;");
                return;
            }
            if (pc.Level < 26)
            {
                Say(pc, 131, "您看起来很强呢。$R;" +
                    "到现在为止抓了多少魔物?$R;" +
                    "$R我也可以像您那样吗？$R;");
                return;
            }
            if (pc.Level < 31)
            {
                Say(pc, 131, "您是……$R;" +
                    pc.Name +
                    "?$R;" +
                    "酒馆老板经常说起您呢$R;");
                return;
            }
            if (pc.Level < 36 || !mask.Test(AYEFlags.泡兒的對話))//_2a86)
            {
                mask.SetValue(AYEFlags.泡兒的對話, true);
                //_2a86 = true;
                Say(pc, 131, "我也想和您一样$R;" +
                    pc.Name +
                    "哥$R;" +
                    "可以叫您哥哥吗？$R;");
                return;
            }
            if (pc.Level < 39)
            {
                Say(pc, 131, pc.Name +
                    "哥！$R;" +
                    "总有一天$R;" +
                    "我会和哥哥您一样的$R;");
                return;
            }
            Say(pc, 131, pc.Name +
                "哥…$R;" +
                "怎么办？$R;" +
                "妈妈不舒服呢。$R;" +
                "$P需要的药材$R;" +
                "虽然已经和酒馆说了，$R;" +
                "但是可能因为报酬太少，$R;" +
                "没人肯接这任务呢。$R;" +
                "$R妈妈若是去世的话$R;" +
                "我怎么办啊？$R;");
        }
    }
}