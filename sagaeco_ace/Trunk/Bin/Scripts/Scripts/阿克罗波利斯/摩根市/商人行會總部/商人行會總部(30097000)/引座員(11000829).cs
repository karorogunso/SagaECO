using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30097000
{
    public class S11000829 : Event
    {
        public S11000829()
        {
            this.EventID = 11000829;

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<MorgFlags> mask = pc.CMask["MorgFlags"];

            Say(pc, 159, "欢迎光临$R;");

            if (mask.Test(MorgFlags.接受基本職業) ||
                mask.Test(MorgFlags.接受專門職業) ||
                mask.Test(MorgFlags.接受技術職業))
            {
                return;
            }

            if (mask.Test(MorgFlags.給予護髮劑))
            {
                接受技能點(pc);
                return;
            }

            Say(pc, 112, "呀…$R;");
            switch (Select(pc, "怎么回事呢？", "", "问怎么回事", "什么也不做"))
            {
                case 1:
                    Say(pc, 131, "要听我的事吗？$R;" +
                        "您真是一个好孩子…$R;" +
                        "$P也不是什么大不了的事情…$R;" +
                        "$R其实今天早晨剪了头发呀$R;" +
                        "$P您看看！$R是不是剪的太短了$R;" +
                        "看看这，太短了！$R;" +
                        "$R这样怎办呢…真是的$R;" +
                        "搞成这样，这个月的粉丝投票上，$R肯定会输给弟弟的$R;" +
                        "$P只要有长发的「护发剂」，$R就可以改变发型…$R;");

                    if (CountItem(pc, 10000608) > 0)
                    {
                        switch (Select(pc, "怎么办呢？", "", "什么也不做", "给他护发剂"))
                        {
                            case 1:
                                break;
                            case 2:
                                TakeItem(pc, 10000608, 1);
                                mask.SetValue(MorgFlags.給予護髮劑, true);
                                //_6a80 = true;
                                Say(pc, 131, "真的是给我的吗？$R;" +
                                    "您真亲切阿$R;" +
                                    "$R那我现在就用啰$R;");
                                Wait(pc, 1000);
                                ShowEffect(pc, 11000829, 4112);
                                Wait(pc, 1000);
                                Say(pc, 131, "哦，不错$R;" +
                                    "长的挺好的吗？我很满意！$R;");

                                接受技能點(pc);
                                break;
                        }
                        return;
                    }
                    break;
                case 2:
                    break;
            }
        }

        void 接受技能點(ActorPC pc)
        {
            BitMask<MorgFlags> mask = pc.CMask["MorgFlags"];

            Say(pc, 131, "谢谢！以表谢意$R;" +
                "技能点数当作礼物送给您吧！$R;");

            if (pc.Job != PC_JOB.NOVICE &&
                pc.Job != PC_JOB.SWORDMAN &&
                 pc.Job != PC_JOB.FENCER &&
                 pc.Job != PC_JOB.SCOUT &&
                 pc.Job != PC_JOB.ARCHER &&
                 pc.Job != PC_JOB.WIZARD &&
                 pc.Job != PC_JOB.SHAMAN &&
                 pc.Job != PC_JOB.VATES &&
                 pc.Job != PC_JOB.WARLOCK &&
                 pc.Job != PC_JOB.TATARABE &&
                 pc.Job != PC_JOB.FARMASIST &&
                 pc.Job != PC_JOB.RANGER &&
                 pc.Job != PC_JOB.MERCHANT)
            {
                switch (Select(pc, "接受哪种技能点数呢？", "", "暂时想一想", "基本职业", "进阶职业", "保留职业"))
                {
                    case 1:
                        break;
                    case 2:
                        mask.SetValue(MorgFlags.接受基本職業, true);
                        //_6a77 = true;
                        Wait(pc, 1000);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 1000);
                        SkillPointBonus(pc, 1);
                        Say(pc, 131, "基本职业的$R;" +
                            "技能点数上升1了$R;");
                        break;
                    case 3:

                        mask.SetValue(MorgFlags.接受專門職業, true);
                        //_6a78 = true;
                        Wait(pc, 1000);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 1000);
                        SkillPointBonus2X(pc, 1);
                        //SKILLPBONUS2ND 1
                        Say(pc, 131, "进阶职业的$R;" +
                            "技能点数上升1了$R;");
                        break;
                    case 4:

                        mask.SetValue(MorgFlags.接受技術職業, true);
                        //_6a79 = true;
                        Wait(pc, 1000);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 1000);
                        SkillPointBonus2T(pc, 1);
                        //SKILLPBONUS3RD 1
                        Say(pc, 131, "保留职业的$R;" +
                            "技能点数上升1了$R;");
                        break;
                }
                return;
            }
            switch (Select(pc, "接受技能点数吗？", "", "暂时想一想", "接受！"))
            {
                case 1:
                    break;
                case 2:

                    mask.SetValue(MorgFlags.接受基本職業, true);
                    //_6a77 = true;
                    Wait(pc, 1000);
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 1000);
                    SkillPointBonus(pc, 1);
                    Say(pc, 131, "基本职业的$R;" +
                        "技能点数上升1了$R;");
                    break;
            }
        }
    }
}