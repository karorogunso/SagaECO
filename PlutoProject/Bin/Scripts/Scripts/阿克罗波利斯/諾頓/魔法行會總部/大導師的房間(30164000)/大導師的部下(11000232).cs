using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30164000
{
    public class S11000232 : Event
    {
        public S11000232()
        {
            this.EventID = 11000232;
        }


        public override void OnEvent(ActorPC pc)
        {
            BitMask<Reset> mask = pc.CMask["Reset"];
            BitMask<NDFlags> NDFlags_mask = pc.CMask["NDFlags"];

            if (pc.Account.GMLevel >= 100)
            {
                switch (Select(pc, "找大导师有什么事情吗？", "", "技能的幻觉", "技能点重设", "属性点重设", "没有特别的事情"))
                {
                    case 1:
                        洗一轉技能點(pc);
                        break;
                    case 2:
                        洗技能點(pc);
                        break;
                    case 3:
                        系屬性點(pc);
                        break;
                }
                return;
            }

            if (NDFlags_mask.Test(NDFlags.大导师第一次对话))
            {
                Say(pc, 131, "这里是魔法行会总部$R;" +
                    "综合管理魔法师的地方$R;" +
                    "$R来的好…$R;");
                return;
            }


            if (!mask.Test(Reset.洗過技能點) && !mask.Test(Reset.洗過屬性點))
            {
                switch (Select(pc, "找大导师有什么事情吗？", "", "技能点重设", "属性点重设", "没有特别的事情"))
                {
                    case 1:
                        洗技能點(pc);
                        break;
                    case 2:
                        系屬性點(pc);
                        break;
                }
            }

            if (!mask.Test(Reset.洗過技能點) && mask.Test(Reset.洗過屬性點))
            {
                switch (Select(pc, "找大导师有什么事情吗？", "", "技能点重设", "没有特别的事情"))
                {
                    case 1:
                        洗技能點(pc);
                        break;
                }
            }

            if (mask.Test(Reset.洗過技能點) && !mask.Test(Reset.洗過屬性點))
            {
                switch (Select(pc, "找大导师有什么事情吗？", "", "属性点重设", "没有特别的事情"))
                {
                    case 1:
                        系屬性點(pc);
                        break;
                }
            }
        }

        void 洗技能點(ActorPC pc)
        {
            if (pc.Race == PC_RACE.DEM)
            {
                return;
            }
            BitMask<Reset> mask = pc.CMask["Reset"];

            if (pc.Skills.Count == 0 &&
                pc.Skills2.Count == 0)
            {
                Say(pc, 131, "您不是没有学过技能吗？$R;" +
                    "$R没有学过，怎么能说忘记呢$R;" +
                    "快走吧$R;");
                return;
            }

            Say(pc, 131, "什么？$R;" +
                "$R想把所有的技能删除，重新学习？$R;");
            switch (Select(pc, "重设技能吗？", "", "重设", "不重设"))
            {
                case 1:
                    Say(pc, 131, "重设技能会带给您很多麻烦$R;" +
                        "不能多次进行。$R;" +
                        "$R确定要做吗？$R;");
                    switch (Select(pc, "重设技能吗？", "", "重设", "不重设"))
                    {
                        case 1:
                            mask.SetValue(Reset.洗過技能點, true);
                            //_2A43 = true;
                            ResetSkill(pc, 1);
                            ResetSkill(pc, 2);
                            //SKILLRESET_ALL
                            Say(pc, 131, "那也不错$R;" +
                                "大导师，请借给我力量吧$R;");
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "把所有的技能都忘了$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 2:
                    break;
            }
        }

        void 系屬性點(ActorPC pc)
        {
            if (pc.Race == PC_RACE.DEM)
            {
                return;
            }
            BitMask<Reset> mask = pc.CMask["Reset"];

            Say(pc, 131, "什么？$R;" +
                "$R想回到出生状态$R;" +
                "重新分配奖励点数吗？$R;");
            switch (Select(pc, "重设状态吗？", "", "重设", "不重设"))
            {
                case 1:
                    Say(pc, 131, "重设状态会带您给很多麻烦$R;" +
                        "不能多次进行。$R;" +
                        "$R确定要做吗？$R;");
                    switch (Select(pc, "重设状态吗？", "", "重设", "不重设"))
                    {
                        case 1:

                            mask.SetValue(Reset.洗過屬性點, true);
                            //_2A47 = true;
                            ResetStatusPoint(pc);
                            //STATUSRESET
                            Say(pc, 131, "那也不错$R;" +
                                "大导师，请借给我力量吧$R;");
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "状态恢复初始值了$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 2:
                    break;
            }
        }

        void 洗一轉技能點(ActorPC pc)
        {
            if (pc.Race == PC_RACE.DEM)
            {
                return;
            }

            int a = (int)pc.Level * 1000;

            if (pc.Skills.Count == 0 &&
                pc.Skills2.Count == 0)
            {
                Say(pc, 131, "您不是没有学过技能吗？$R;" +
                    "$R没有学过，怎么能说忘记呢$R;" +
                    "快走吧$R;");
                return;
            }

            Say(pc, 131, "那么当做使用费需要$R;" +
                a + "金币$R;" +
                "没问题吧？$R;");

            switch (Select(pc, "怎么办呢？", "", "支付", "不支付"))
            {
                case 1:
                    if (pc.Gold < a)
                    {
                        Say(pc, 131, "钱不够呀$R;");
                        return;
                    }

                    Say(pc, 131, "那也不错$R;" +
                        "大导师，请借给我力量吧$R;");
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 4000);
                    ResetSkill(pc, 1);
                    //SKILLRESET_ONE
                    if (pc.Level == 0)
                    {
                        return;
                    }
                    pc.Gold -= a;
                    Say(pc, 131, "支付了" + a + "金币$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}