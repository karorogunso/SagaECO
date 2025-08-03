using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30079000
{
    public class S11000396 : Event
    {
        public S11000396()
        {
            this.EventID = 11000396;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (mask.Test(AYEFlags.職業裝任務結束))//_4a44)
            {
                Say(pc, 131, "哦哦哦！这个呀！$R;" +
                    "哦，您的武器在说话呢。$R;" +
                    "$R诉说您流的汗水和激烈的战斗$R;" +
                    "$P真了不起吧！$R;" +
                    "您说是不是啊，小姑娘?$R;");
                Say(pc, 11000294, 131, "是，老爷爷，$R;" +
                    "我也是这么想的$R;" +
                    "$R下一位。$R;");
                return;
            }
            if (pc.Job == PC_JOB.SWORDMAN
              || pc.Job == PC_JOB.BLADEMASTER
              || pc.Job == PC_JOB.BOUNTYHUNTER
              || pc.Job == PC_JOB.FENCER
              || pc.Job == PC_JOB.KNIGHT
              || pc.Job == PC_JOB.DARKSTALKER
              || pc.Job == PC_JOB.SCOUT
              || pc.Job == PC_JOB.ASSASSIN
              || pc.Job == PC_JOB.COMMAND
              || pc.Job == PC_JOB.ARCHER
              || pc.Job == PC_JOB.STRIKER
              || pc.Job == PC_JOB.GUNNER)
            {
                战士系职业分支判断(pc);
                return;
            }
            if (pc.Job == PC_JOB.WIZARD
                || pc.Job == PC_JOB.SORCERER
                || pc.Job == PC_JOB.SAGE
                || pc.Job == PC_JOB.SHAMAN
                || pc.Job == PC_JOB.ELEMENTER
                || pc.Job == PC_JOB.ENCHANTER
                || pc.Job == PC_JOB.VATES
                || pc.Job == PC_JOB.DRUID
                || pc.Job == PC_JOB.BARD
                || pc.Job == PC_JOB.WARLOCK
                || pc.Job == PC_JOB.GAMBLER
                || pc.Job == PC_JOB.NECROMANCER)
            {
                法师系开始(pc);
                return;
            }
            Say(pc, 131, "现在的战士真是不行啊，$R;" +
                "练一会儿就说累，$R;" +
                "$R真是看不下去了$R;" +
                "我以前也没有这样啊$R;" +
                "$P以前好好的……$R;" +
                "您说是不是啊，小姑娘?$R;");
            Say(pc, 11000294, 131, "是，老爷爷，$R;" +
                "我也是这么想的$R;" +
                "$R下一位。$R;");
        }
        void 战士系职业分支判断(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (mask.Test(AYEFlags.劍士系職業裝))//_2a96)
            {
                /*
                if (_4a43)
                {
                    剑士系完成(pc);
                    return;
                }
                //60010411, 1);
                if (a//ME.WORK0 > 0
                )
                {
                    _4a43 = true;
                    TakeItem(pc, 60010411, 1);
                    Say(pc, 131, "呵呵，您看起来经历了很多呢。$R;" +
                        "给我武器让我看一看吧$R;" +
                        "$R那我看看啊。$R;" +
                        "$P呵呵，武器告诉我$R;" +
                        "$R您是个真正的战士呢。$R;" +
                        "$P真的很厉害.$R;" +
                        "最近像您这样的可真少见呢。$R;" +
                        "$R合我的心意$R;");
                    剑士系完成(pc);
                    return;
                }
                */
                Say(pc, 131, "$R用它证明您的力量和能力吧$R;" +
                    "$P这个武器会记录您的战斗，$R;" +
                    "$R它会告诉我您的能力$R;");
                return;
            }

            if (mask.Test(AYEFlags.騎士系職業裝))//_2a97)
            {
                /*
                if (_4a43)
                {
                    骑士系完成(pc);
                    return;
                }
                //60060250, 1);
                if (a//ME.WORK0 > 0
                )
                {
                    _4a43 = true;
                    TakeItem(pc, 60060250, 1);
                    Say(pc, 131, "呵呵，您看起来经历了很多呢。$R;" +
                        "给我武器让我看一看吧$R;" +
                        "$R那我看看啊。$R;" +
                        "$P呵呵，武器告诉我$R;" +
                        "$R您是个真正的战士呢。$R;" +
                        "$P真的很厉害$R;" +
                        "最近像您这样的可真少见呢。$R;" +
                        "$R合我的心意$R;");
                    骑士系完成(pc);
                    return;
                }
                */
                Say(pc, 131, "用这个证明您的力量和能力吧$R;" +
                    "$P这个武器会记录您的战斗，$R;" +
                    "$R它会告诉我您的能力$R;");
                return;
            }
            if (mask.Test(AYEFlags.盜賊系職業裝))//_2a98)
            {
                /*
                if (_4a43)
                {
                    盗贼系完成(pc);
                    return;
                }
                //60010050, 1);
                if (a//ME.WORK0 > 0
                )
                {
                    _4a43 = true;
                    TakeItem(pc, 60010050, 1);
                    Say(pc, 131, "呵呵，您看起来经历了很多呢。$R;" +
                        "给我武器让我看一看吧$R;" +
                        "$R那我看看啊。$R;" +
                        "$P呵呵，武器告诉我$R;" +
                        "$R您是个真正的战士呢。$R;" +
                        "$P真的很厉害.$R;" +
                        "最近像您这样的可真少见呢。$R;" +
                        "$R合我的心意$R;");
                    盗贼系完成(pc);
                    return;
                }
                 */
                Say(pc, 131, "用这个证明您的力量和能力吧$R;" +
                    "$P这个武器会记录您的战斗，$R;" +
                    "$R它会告诉我您的能力唷$R;");
                return;
            }
            if (mask.Test(AYEFlags.弓箭手系職業裝))//_2a99)
            {
                /*
                if (_4a43)
                {
                    弓箭手系完成(pc);
                    return;
                }
                //60090150, 1);
                if (a//ME.WORK0 > 0
                )
                {
                    _4a43 = true;
                    TakeItem(pc, 60090150, 1);
                    Say(pc, 131, "呵呵，您看起来经历了很多呢。$R;" +
                        "给我武器让我看一看吧$R;" +
                        "$R那我看看啊。$R;" +
                        "$P呵呵，武器告诉我$R;" +
                        "$R您是个真正的战士呢。$R;" +
                        "$P真的很厉害.$R;" +
                        "最近像您这样的可真少见呢。$R;" +
                        "$R合我的心意$R;");
                    弓箭手系完成(pc);
                    return;
                }
                */
                Say(pc, 131, "用这个证明您的力量和能力吧$R;" +
                    "$P这个武器会记录您的战斗，$R;" +
                    "$R它会告诉我您的能力$R;");
                return;
            }
            Say(pc, 131, "现在的战士真是不行啊，$R;" +
                "练一会儿就说累，$R;" +
                "$R真是看不下去了$R;" +
                "我以前也没有这样啊$R;" +
                "$P还是以前好啊$R;");
            switch (Select(pc, "是不是以前好呢？", "", "不是", "是的"))
            {
                case 1:
                    if (pc.Level < 45)
                    {
                        Say(pc, 131, "您这个等级低的小鬼，$R哪有资格说这样的话阿?$R;");
                        return;
                    }
                    if (pc.Job == PC_JOB.SWORDMAN
                        || pc.Job == PC_JOB.BLADEMASTER
                        || pc.Job == PC_JOB.BOUNTYHUNTER)
                    {
                        Say(pc, 131, "您是真正的战士吗？$R;" +
                            "那就拿着这个$R;" +
                            "$R用它证明您的力量和能力吧$R;" +
                            "$P这个武器会记录您的战斗，$R;" +
                            "$R它会告诉我您的能力$R;");
                        if (CheckInventory(pc, 60010411, 1))
                        {
                            mask.SetValue(AYEFlags.劍士系職業裝, true);
                            //_2a96 = true;
                            GiveItem(pc, 60010411, 1);
                            return;
                        }
                        Say(pc, 131, "行李太多了$R;" +
                            "去整理一下，再来吧$R;");
                        return;
                    }
                    if (pc.Job == PC_JOB.FENCER
                        || pc.Job == PC_JOB.KNIGHT
                        || pc.Job == PC_JOB.DARKSTALKER)
                    {
                        Say(pc, 131, "您是真正的战士吗？$R;" +
                            "那就拿着这个$R;" +
                            "$R用它证明您的力量和能力吧$R;" +
                            "$P这个武器会记录您的战斗，$R;" +
                            "$R它会告诉我您的能力$R;");
                        if (CheckInventory(pc, 60060250, 1))
                        {
                            mask.SetValue(AYEFlags.騎士系職業裝, true);
                            //_2a97 = true;
                            GiveItem(pc, 60060250, 1);
                            return;
                        }
                        Say(pc, 131, "行李太多了$R;" +
                            "去整理一下，再来吧$R;");
                        return;
                    }
                    if (pc.Job == PC_JOB.SCOUT
                        || pc.Job == PC_JOB.ASSASSIN
                        || pc.Job == PC_JOB.COMMAND)
                    {
                        Say(pc, 131, "您是真正的战士吗？$R;" +
                            "那就拿着这个$R;" +
                            "$R用它证明您的力量和能力吧$R;" +
                            "$P这个武器会记录您的战斗，$R;" +
                            "$R它会告诉我您的能力$R;");
                        if (CheckInventory(pc, 60010050, 1))
                        {
                            mask.SetValue(AYEFlags.盜賊系職業裝, true);
                            //_2a98 = true;
                            GiveItem(pc, 60010050, 1);
                            return;
                        }
                        Say(pc, 131, "行李太多了$R;" +
                            "去整理一下，再来吧$R;");
                        return;
                    }
                    if (pc.Job == PC_JOB.ARCHER
                        || pc.Job == PC_JOB.STRIKER
                        || pc.Job == PC_JOB.GUNNER)
                    {
                        Say(pc, 131, "您是真正的战士吗？$R;" +
                            "那就拿着这个$R;" +
                            "$R用它证明您的力量和能力吧$R;" +
                            "$P这个武器会记录您的战斗，$R;" +
                            "$R它会告诉我您的能力$R;");
                        if (CheckInventory(pc, 60090150, 1))
                        {
                            mask.SetValue(AYEFlags.弓箭手系職業裝, true);
                            //_2a99 = true;
                            GiveItem(pc, 60090150, 1);
                            return;
                        }
                        Say(pc, 131, "行李太多了$R;" +
                            "去整理一下，再来吧$R;");
                        return;
                    }
                    Say(pc, 131, "您又不是战士，装懂！$R;");
                    break;
                case 2:
                    Say(pc, 131, "真是可怜啊！$R;" +
                        "现在的年轻人太弱了。$R;" +
                        "真想遇见个强悍得$R;" +
                        "可以一手把我的武器$R;" +
                        "弄断的傢伙呢。$R;" +
                        "$P那我就能送他$R;" +
                        "我那个悲壮的……$R;");
                    break;
            }
        }
        void 剑士系完成(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                if (CheckInventory(pc, 50061850, 1))
                {
                    Say(pc, 131, "这是我以前使用的$R;" +
                        "悲壮的防具$R;" +
                        "$R送给你吧！$R;");
                    mask.SetValue(AYEFlags.職業裝任務結束, true);
                    GiveItem(pc, 50061850, 1);
                    return;
                }
                Say(pc, 131, "这是我以前使用的$R;" +
                    "悲壮的防具$R;" +
                    "$R送给您吧！$R;" +
                    "去把行李整理一下，再来吧！$R;");
                return;
            }
            if (CheckInventory(pc, 50013050, 1))
            {
                Say(pc, 131, "这是我以前使用的$R;" +
                    "悲壮的防具$R;" +
                    "$R送给你吧！$R;");
                mask.SetValue(AYEFlags.職業裝任務結束, true);
                GiveItem(pc, 50013050, 1);
                return;
            }
            Say(pc, 131, "这是我以前使用的$R;" +
                "悲壮的防具$R;" +
                "$R送给您吧！$R;" +
                "去把行李整理一下，再来吧！$R;");
        }
        void 骑士系完成(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                if (CheckInventory(pc, 50061950, 1))
                {
                    Say(pc, 131, "这是我以前使用的$R;" +
                        "悲壮的防具$R;" +
                        "$R送给您吧！$R;");
                    mask.SetValue(AYEFlags.職業裝任務結束, true);
                    GiveItem(pc, 50061950, 1);
                    return;
                }
                Say(pc, 131, "这是我以前使用的$R;" +
                    "悲壮的防具$R;" +
                    "$R送给您吧！$R;" +
                    "去把行李整理一下，再来吧！$R;");
                return;
            }
            if (CheckInventory(pc, 50013150, 1))
            {
                Say(pc, 131, "这是我以前使用的$R;" +
                    "悲壮的防具$R;" +
                    "$R送给您吧！$R;");
                mask.SetValue(AYEFlags.職業裝任務結束, true);
                GiveItem(pc, 50013150, 1);
                return;
            }
            Say(pc, 131, "这是我以前使用的$R;" +
                "悲壮的防具$R;" +
                "$R送给您吧！$R;" +
                "去把行李整理一下，再来吧！$R;");
        }
        void 盗贼系完成(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (CheckInventory(pc, 50025350, 1))
            {
                Say(pc, 131, "这是我以前使用的$R;" +
                    "悲壮的防具$R;" +
                    "$R送给您吧！$R;");
                mask.SetValue(AYEFlags.職業裝任務結束, true);
                GiveItem(pc, 50025350, 1);
                return;
            }
            Say(pc, 131, "这是我以前使用的$R;" +
                "悲壮的防具$R;" +
                "$R送给您吧！$R;" +
                "去把行李整理一下，再来吧！$R;");
        }
        void 弓箭手系完成(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                if (CheckInventory(pc, 50023252, 1))
                {
                    Say(pc, 131, "这是我以前使用的$R;" +
                        "悲壮的防具$R;" +
                        "$R送给您吧！$R;");
                    mask.SetValue(AYEFlags.職業裝任務結束, true);
                    GiveItem(pc, 50023252, 1);
                    return;
                }
                Say(pc, 131, "这是我以前使用的$R;" +
                    "悲壮的防具$R;" +
                    "$R送给您吧！$R;" +
                    "去把行李整理一下，再来吧！$R;");
                return;
            }
            if (CheckInventory(pc, 50023251, 1))
            {
                Say(pc, 131, "这是我以前使用的$R;" +
                    "悲壮的防具$R;" +
                    "$R送给您吧！$R;");
                mask.SetValue(AYEFlags.職業裝任務結束, true);
                GiveItem(pc, 50023251, 1);
                return;
            }
            Say(pc, 131, "这是我以前使用的$R;" +
                "悲壮的防具$R;" +
                "$R送给您吧！$R;" +
                "去把行李整理一下，再来吧！$R;");
        }
        void 法师系开始(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (mask.Test(AYEFlags.法師系職業裝))//_4a45)
            {
                /*
                if (_4a43)
                {
                    法师系职业判断(pc);
                    return;
                }
                //60070164, 1);
                if (a//ME.WORK0 > 0
                )
                {
                    _4a43 = true;
                    TakeItem(pc, 60070164, 1);
                    Say(pc, 131, "呵呵，您看起来经历了很多呢。$R;" +
                        "给我武器让我看一看吧$R;" +
                        "$R那我看看啊。$R;" +
                        "$P呵呵，武器告诉我$R;" +
                        "$R您是个真正的战士呢。$R;" +
                        "$P真的很厉害$R;" +
                        "最近像您这样的可真少见呢。$R;" +
                        "$R合我的心意$R;");
                    法师系职业判断(pc);
                    return;
                }
                */
                Say(pc, 131, "用这个证明您的力量和能力吧$R;" +
                    "$P这个武器会记录您的战斗，$R;" +
                    "$R它会告诉我您的能力$R;");
                return;
            }
            Say(pc, 131, "现在的魔法师真是不行啊，$R;" +
                "练一会儿就说累，$R;" +
                "$R真是看不下去阿$R;" +
                "我以前也没有这样啊$R;" +
                "还是以前好啊$R;");
            switch (Select(pc, "是不是以前好呢？", "", "不是", "是的"))
            {
                case 1:
                    if (pc.Level < 45)
                    {
                        Say(pc, 131, "您这个等级低的小鬼，$R哪有资格说这样的话啊?$R;");
                        return;
                    }
                    Say(pc, 131, "您说您是真正的魔法师吗？$R;" +
                        "那就拿着这个$R;" +
                        "$R用它证明您的力量和能力吧$R;" +
                        "$P这个武器会记录您的战斗，$R;" +
                        "$R它会告诉我您的能力$R;");
                    if (CheckInventory(pc, 60070164, 1))
                    {
                        mask.SetValue(AYEFlags.法師系職業裝, true);
                        //_4a45 = true;
                        GiveItem(pc, 60070164, 1);
                        return;
                    }
                    Say(pc, 131, "想给您礼物，但是行李太多了，$R;" +
                        "去整理一下，再回来吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "真是可怜啊！$R;" +
                        "现在的年轻人太弱了。$R;" +
                        "真想遇见个强悍得$R;" +
                        "可以一手把我的武器$R;" +
                        "弄断的傢伙呢。$R;" +
                        "$P那我就能送他$R;" +
                        "我那个悲壮的……$R;");
                    break;
            }
        }

        void 法师系职业判断(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (pc.Job == PC_JOB.WIZARD
               || pc.Job == PC_JOB.SORCERER
               || pc.Job == PC_JOB.SAGE)
            {
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    if (CheckInventory(pc, 50012550, 1))
                    {
                        Say(pc, 131, "这是我以前使用的$R;" +
                            "悲壮的防具$R;" +
                            "$R送给你吧！$R;");
                        mask.SetValue(AYEFlags.職業裝任務結束, true);
                        GiveItem(pc, 50012550, 1);
                        return;
                    }
                    Say(pc, 131, "这是我以前使用的$R;" +
                        "悲壮的防具$R;" +
                        "$R送给你吧！$R;" +
                        "去把行李整理一下，再来吧！$R;");
                    return;
                }
                if (CheckInventory(pc, 50062250, 1))
                {
                    Say(pc, 131, "这是我以前使用的$R;" +
                        "悲壮的防具$R;" +
                        "$R送给你吧！$R;");
                    mask.SetValue(AYEFlags.職業裝任務結束, true);
                    GiveItem(pc, 50062250, 1);
                    return;
                }
                Say(pc, 131, "这是我以前使用的$R;" +
                    "悲壮的防具$R;" +
                    "$R送给你吧！$R;" +
                    "去把行李整理一下，再来吧！$R;");
                return;
            }
            if (pc.Job == PC_JOB.SHAMAN
               || pc.Job == PC_JOB.ELEMENTER
               || pc.Job == PC_JOB.ENCHANTER)
            {
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    if (CheckInventory(pc, 50012750, 1))
                    {
                        Say(pc, 131, "这是我以前使用的$R;" +
                            "悲壮的防具$R;" +
                            "$R送给您吧！$R;");
                        mask.SetValue(AYEFlags.職業裝任務結束, true);
                        GiveItem(pc, 50012750, 1);
                        return;
                    }
                    Say(pc, 131, "这是我以前使用的$R;" +
                        "悲壮的防具$R;" +
                        "$R送给您吧！$R;" +
                        "去把行李整理一下，再来吧！$R;");
                    return;
                }
                if (CheckInventory(pc, 50062350, 1))
                {
                    Say(pc, 131, "这是我以前使用的$R;" +
                        "悲壮的防具$R;" +
                        "$R送给您吧！$R;");
                    GiveItem(pc, 50062350, 1);
                    return;
                }
                Say(pc, 131, "这是我以前使用的$R;" +
                    "悲壮的防具$R;" +
                    "$R送给您吧！$R;" +
                    "去把行李整理一下，再来吧！$R;");
                return;
            }
            if (pc.Job == PC_JOB.VATES
              || pc.Job == PC_JOB.DRUID
              || pc.Job == PC_JOB.BARD)
            {
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    if (CheckInventory(pc, 50013450, 1))
                    {
                        Say(pc, 131, "这是我以前使用的$R;" +
                            "悲壮的防具$R;" +
                            "$R送给您吧！$R;");
                        mask.SetValue(AYEFlags.職業裝任務結束, true);
                        GiveItem(pc, 50013450, 1);
                        return;
                    }
                    Say(pc, 131, "这是我以前使用的$R;" +
                        "悲壮的防具$R;" +
                        "$R送给您吧！$R;" +
                        "去把行李整理一下，再来吧！$R;");
                    return;
                }
                if (CheckInventory(pc, 50062450, 1))
                {
                    Say(pc, 131, "这是我以前使用的$R;" +
                        "悲壮的防具$R;" +
                        "$R送给您吧！$R;");
                    mask.SetValue(AYEFlags.職業裝任務結束, true);
                    GiveItem(pc, 50062450, 1);
                    return;
                }
                Say(pc, 131, "这是我以前使用的$R;" +
                    "悲壮的防具$R;" +
                    "$R送给您吧！$R;" +
                    "去把行李整理一下，再来吧！$R;");
                return;
            }
            if (pc.Job == PC_JOB.WARLOCK
               || pc.Job == PC_JOB.GAMBLER
               || pc.Job == PC_JOB.NECROMANCER)
            {
                if (pc.Gender == PC_GENDER.FEMALE)
                {
                    if (CheckInventory(pc, 50062551, 1))
                    {
                        Say(pc, 131, "这是我以前使用的$R;" +
                            "悲壮的防具$R;" +
                            "$R送给您吧！$R;");
                        mask.SetValue(AYEFlags.職業裝任務結束, true);
                        GiveItem(pc, 50062551, 1);
                        return;
                    }
                    Say(pc, 131, "这是我以前使用的$R;" +
                        "悲壮的防具$R;" +
                        "$R送给您吧！$R;" +
                        "去把行李整理一下，再来吧！$R;");
                    return;
                }
                if (CheckInventory(pc, 50013551, 1))
                {
                    Say(pc, 131, "这是我以前使用的$R;" +
                        "悲壮的防具$R;" +
                        "$R送给您吧！$R;");
                    mask.SetValue(AYEFlags.職業裝任務結束, true);
                    GiveItem(pc, 50013551, 1);
                    return;
                }
                Say(pc, 131, "这是我以前使用的$R;" +
                    "悲壮的防具$R;" +
                    "$R送给您吧！$R;" +
                    "去把行李整理一下，再来吧！$R;");
                return;
            }
        }
    }
}