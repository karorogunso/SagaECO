using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:村裡的老人(11000070) X:189 Y:129
namespace SagaScript.M10023000
{
    public class S11000070 : Event
    {
        public S11000070()
        {
            this.EventID = 11000070;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<MUGEN_JND> MUGEN_JND_mask = pc.CMask["MUGEN_JND"];

            /*
            if (pc.Account.GMLevel >= 1)
            {
                //Call(EVT1100007030);
                return;
            }//*/

            if (CountItem(pc, 10010603) >= 1)
            {
                Say(pc, 131, "呵呵，很漂亮的戒指啊$R;" +
                    "$R您想把您的$R;" +
                    "『莫拉』戒指给我吗？$R;");
                switch (Select(pc, "要给他吗?", "", "不要", "给他"))
                {
                    case 1:
                        break;
                    case 2:
                        if (!MUGEN_JND_mask.Test(MUGEN_JND.莫拉))
                        {
                            SkillPointBonus(pc, 1);
                            TakeItem(pc, 10010603, 1);
                            MUGEN_JND_mask.SetValue(MUGEN_JND.莫拉, true);
                            PlaySound(pc, 4006, false, 100, 50);
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Say(pc, 131, "技能点数上升1点$R;");
                            Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                "这是答谢您的$R;" +
                                "$R再给我拿来戒指的话$R;" +
                                "我会给您更好的东西哦$R;");
                            return;
                        }
                        Say(pc, 131, "呵呵，真是谢谢呢$R;" +
                            "$R为表达谢意，想给您谢礼$R;" +
                            "给我留下充分的行李空间吧$R;");
                        switch (Select(pc, "行李空间充足吗？", "", "整理道具", "收礼物"))
                        {
                            case 1:
                                break;
                            case 2:
                                TakeItem(pc, 10010603, 1);
                                GiveRandomTreasure(pc, "MUGEN40");
                                //TREASURE MUGEN40
                                Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                    "这是答谢您的$R;" +
                                    "$R再给我拿来戒指的话$R;" +
                                    "我会给您更好的东西哦$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            if (CountItem(pc, 10010600) >= 1)
            {
                Say(pc, 131, "呵呵，很漂亮的戒指啊$R;" +
                    "$R您想把您的$R;" +
                    "『理拉』戒指给我吗？$R;");
                switch (Select(pc, "要给他吗?", "", "不要", "给他"))
                {
                    case 1:
                        break;
                    case 2:
                        if (!MUGEN_JND_mask.Test(MUGEN_JND.理拉))
                        {
                            SkillPointBonus(pc, 1);
                            TakeItem(pc, 10010600, 1);
                            MUGEN_JND_mask.SetValue(MUGEN_JND.理拉, true);
                            PlaySound(pc, 4006, false, 100, 50);
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Say(pc, 131, "技能点数上升1点$R;");
                            Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                "这是答谢您的$R;" +
                                "$R再给我拿来戒指的话$R;" +
                                "我会给您更好的东西唷$R;");
                            return;
                        }
                        Say(pc, 131, "呵呵，真是谢谢呢$R;" +
                            "$R为表达谢意，想给您谢礼$R;" +
                            "给我留下充分的行李空间吧$R;");
                        switch (Select(pc, "行李空间充足吗？", "", "整理道具", "收礼物"))
                        {
                            case 1:
                                break;
                            case 2:
                                TakeItem(pc, 10010600, 1);
                                GiveRandomTreasure(pc, "MUGEN35");
                                //TREASURE MUGEN35
                                Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                    "这是答谢您的$R;" +
                                    "$R再给我拿来戒指的话$R;" +
                                    "我会给您更好的东西哦$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            if (CountItem(pc, 10010502) >= 1)
            {
                Say(pc, 131, "呵呵，很漂亮的戒指啊$R;" +
                    "$R您想把您的$R;" +
                    "『阿古拉』戒指给我吗？$R;");
                switch (Select(pc, "要给他吗?", "", "不要", "给他"))
                {
                    case 1:
                        break;
                    case 2:
                        if (!MUGEN_JND_mask.Test(MUGEN_JND.阿古拉))
                        {
                            SkillPointBonus(pc, 1);
                            TakeItem(pc, 10010502, 1);
                            MUGEN_JND_mask.SetValue(MUGEN_JND.阿古拉, true);
                            PlaySound(pc, 4006, false, 100, 50);
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Say(pc, 131, "技能点数上升1点$R;");
                            Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                "这是答谢您的$R;" +
                                "$R再给我拿来戒指的话$R;" +
                                "我会给您更好的东西哦$R;");
                            return;
                        }
                        Say(pc, 131, "呵呵，真是谢谢呢$R;" +
                            "$R为表达谢意，想给您谢礼$R;" +
                            "给我留下充分的行李空间吧$R;");
                        switch (Select(pc, "行李空间充足吗？", "", "整理道具", "收礼物"))
                        {
                            case 1:
                                break;
                            case 2:
                                TakeItem(pc, 10010502, 1);
                                GiveRandomTreasure(pc, "MUGEN30");
                                //TREASURE MUGEN30
                                Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                    "这是答谢您的$R;" +
                                    "$R再给我拿来戒指的话$R;" +
                                    "我会给您更好的东西哦$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            if (CountItem(pc, 10010500) >= 1)
            {
                Say(pc, 131, "呵呵，很漂亮的戒指啊$R;" +
                    "$R您想把您的$R;" +
                    "『古尔拉』戒指给我吗？$R;");
                switch (Select(pc, "要给他吗?", "", "不要", "给他"))
                {
                    case 1:
                        break;
                    case 2:
                        if (!MUGEN_JND_mask.Test(MUGEN_JND.古爾拉))
                        {
                            SkillPointBonus(pc, 1);
                            TakeItem(pc, 10010500, 1);
                            MUGEN_JND_mask.SetValue(MUGEN_JND.古爾拉, true);
                            PlaySound(pc, 4006, false, 100, 50);
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                "这是答谢您的$R;" +
                                "$R再给我拿来戒指的话$R;" +
                                "我会给您更好的东西哦$R;");
                            return;
                        }
                        Say(pc, 131, "呵呵，真是谢谢呢$R;" +
                            "$R为表达谢意，想给您谢礼$R;" +
                            "给我留下充分的行李空间吧$R;");
                        switch (Select(pc, "行李空间充足吗？", "", "整理道具", "收礼物"))
                        {
                            case 1:
                                break;
                            case 2:
                                TakeItem(pc, 10010500, 1);
                                GiveRandomTreasure(pc, "MUGEN25");
                                //TREASURE MUGEN25
                                Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                    "这是答谢您的$R;" +
                                    "$R再给我拿来戒指的话$R;" +
                                    "我会给您更好的东西唷$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            if (CountItem(pc, 10010400) >= 1)
            {
                Say(pc, 131, "呵呵，很漂亮的戒指啊$R;" +
                    "$R您想把您的$R;" +
                    "『妮拉』戒指给我吗？$R;");
                switch (Select(pc, "要给他吗?", "", "不要", "给他"))
                {
                    case 1:
                        break;
                    case 2:
                        if (!MUGEN_JND_mask.Test(MUGEN_JND.妮拉))
                        {
                            SkillPointBonus(pc, 1);
                            TakeItem(pc, 10010400, 1);
                            MUGEN_JND_mask.SetValue(MUGEN_JND.妮拉, true);
                            PlaySound(pc, 4006, false, 100, 50);
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Say(pc, 131, "技能點數上升1點$R;");
                            Say(pc, 131, "呵呵，謝謝您呢$R;" +
                                "這是答謝您的$R;" +
                                "$R再給我拿來戒指的話$R;" +
                                "我會給您更好的東西唷$R;");
                            return;
                        }
                        Say(pc, 131, "呵呵，真是謝謝呢$R;" +
                            "$R為表達謝意，想給您謝禮$R;" +
                            "給我留下充分的行李空間吧$R;");
                        switch (Select(pc, "行李空間充足嗎？", "", "整理道具", "收禮物"))
                        {
                            case 1:
                                break;
                            case 2:
                                TakeItem(pc, 10010400, 1);
                                GiveRandomTreasure(pc, "MUGEN20");
                                //TREASURE MUGEN20
                                Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                    "这是答谢您的$R;" +
                                    "$R再给我拿来戒指的话$R;" +
                                    "我会给您更好的东西哦$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            if (CountItem(pc, 10010300) >= 1)
            {
                Say(pc, 131, "呵呵，很漂亮的戒指啊$R;" +
                    "$R您想把您的$R;" +
                    "『柯以拉』戒指给我吗？$R;");
                switch (Select(pc, "要给他吗?", "", "不要", "给他"))
                {
                    case 1:
                        break;
                    case 2:
                        if (!MUGEN_JND_mask.Test(MUGEN_JND.柯以拉))
                        {
                            SkillPointBonus(pc, 1);
                            TakeItem(pc, 10010300, 1);
                            MUGEN_JND_mask.SetValue(MUGEN_JND.柯以拉, true);
                            PlaySound(pc, 4006, false, 100, 50);
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Say(pc, 131, "技能点数上升1点$R;");
                            Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                "这是答谢您的$R;" +
                                "$R再给我拿来戒指的话$R;" +
                                "我会给您更好的东西哦$R;");
                            return;
                        }
                        Say(pc, 131, "呵呵，真是谢谢呢$R;" +
                            "$R为表达谢意，想给您谢礼$R;" +
                            "给我留下充分的行李空间吧$R;");
                        switch (Select(pc, "行李空间充足吗？", "", "整理道具", "收礼物"))
                        {
                            case 1:
                                break;
                            case 2:
                                TakeItem(pc, 10010300, 1);
                                GiveRandomTreasure(pc, "MUGEN15");
                                //TREASURE MUGEN15
                                Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                    "这是答谢您的$R;" +
                                    "$R再给我拿来戒指的话$R;" +
                                    "我会给您更好的东西哦$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            if (CountItem(pc, 10010200) >= 1)
            {
                Say(pc, 131, "呵呵，很漂亮的戒指啊$R;" +
                    "$R您想把您的$R;" +
                    "『卡拉』戒指给我吗？$R;");
                switch (Select(pc, "要给他吗?", "", "不要", "给他"))
                {
                    case 1:
                        break;
                    case 2:
                        if (!MUGEN_JND_mask.Test(MUGEN_JND.卡拉))
                        {
                            SkillPointBonus(pc, 1);
                            TakeItem(pc, 10010200, 1);
                            MUGEN_JND_mask.SetValue(MUGEN_JND.卡拉, true);
                            PlaySound(pc, 4006, false, 100, 50);
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Say(pc, 131, "技能点数上升1点$R;");
                            Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                "这是答谢您的$R;" +
                                "$R再给我拿来戒指的话$R;" +
                                "我会给您更好的东西唷$R;");
                            return;
                        }
                        Say(pc, 131, "呵呵，真是谢谢呢$R;" +
                            "$R为表达谢意，想给您谢礼$R;" +
                            "给我留下充分的行李空间吧$R;");
                        switch (Select(pc, "行李空间充足吗？", "", "整理道具", "收礼物"))
                        {
                            case 1:
                                break;
                            case 2:
                                TakeItem(pc, 10010200, 1);
                                GiveRandomTreasure(pc, "MUGEN10");
                                //TREASURE MUGEN10
                                Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                    "这是答谢您的$R;" +
                                    "$R再给我拿来戒指的话$R;" +
                                    "我会给您更好的东西哦$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            if (CountItem(pc, 10010100) >= 1)
            {
                Say(pc, 131, "呵呵，很漂亮的戒指啊$R;" +
                    "$R您想把您的$R;" +
                    "『基乌拉』戒指给我吗？$R;");
                switch (Select(pc, "要给他吗?", "", "不要", "给他"))
                {
                    case 1:
                        break;
                    case 2:
                        if (!MUGEN_JND_mask.Test(MUGEN_JND.基烏拉))
                        {
                            SkillPointBonus(pc, 1);
                            TakeItem(pc, 10010100, 1);
                            MUGEN_JND_mask.SetValue(MUGEN_JND.基烏拉, true);
                            PlaySound(pc, 4006, false, 100, 50);
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Say(pc, 131, "技能点数上升1点$R;");
                            Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                "这是答谢您的$R;" +
                                "$R再给我拿来戒指的话$R;" +
                                "我会给您更好的东西哦$R;");
                            return;
                        }
                        Say(pc, 131, "呵呵，真是谢谢呢$R;" +
                            "$R为表达谢意，想给您谢礼$R;" +
                            "给我留下充分的行李空间吧$R;");
                        switch (Select(pc, "行李空间充足吗？", "", "整理道具", "收礼物"))
                        {
                            case 1:
                                break;
                            case 2:
                                TakeItem(pc, 10010100, 1);
                                GiveRandomTreasure(pc, "MUGEN05");
                                //TREASURE MUGEN05
                                Say(pc, 131, "呵呵，谢谢您呢$R;" +
                                    "这是答谢您的$R;" +
                                    "$R再给我拿来戒指的话$R;" +
                                    "我会给您更好的东西哦$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            int a = Global.Random.Next(1, 2);
            if (a == 1)
                Say(pc, 131, "这花……$R;" +
                    "叫什么呢？$R;");

            else
                Say(pc, 131, "真是漂亮的花呢$R;");
            
        }
    }
}
