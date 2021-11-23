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

            Say(pc, 159, "歡迎光臨$R;");

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
            switch (Select(pc, "怎麼回事呢？", "", "問怎麼回事", "什麼也不做"))
            {
                case 1:
                    Say(pc, 131, "要聽我的事嗎？$R;" +
                        "您真是一個好孩子…$R;" +
                        "$P也不是什麼大不了的事情…$R;" +
                        "$R其實今天早晨剪了頭髮呀$R;" +
                        "$P您看看！$R是不是剪的太短了$R;" +
                        "看看這，太短了！$R;" +
                        "$R這樣怎辦呢…真是的$R;" +
                        "搞成這樣，這個月的粉絲投票上，$R肯定會輸給弟弟的$R;" +
                        "$P只要有長髮的「護髮劑」，$R就可以改變髮型…$R;");

                    if (CountItem(pc, 10000608) > 0)
                    {
                        switch (Select(pc, "怎麼辦呢？", "", "什麼也不做", "給他護髮劑"))
                        {
                            case 1:
                                break;
                            case 2:
                                TakeItem(pc, 10000608, 1);
                                mask.SetValue(MorgFlags.給予護髮劑, true);
                                //_6a80 = true;
                                Say(pc, 131, "真的是給我的嗎？$R;" +
                                    "您真親切阿$R;" +
                                    "$R那我現在就用囉$R;");
                                Wait(pc, 1000);
                                ShowEffect(pc, 11000829, 4112);
                                Wait(pc, 1000);
                                Say(pc, 131, "哦，不錯$R;" +
                                    "長的挺好的嗎？我很滿意！$R;");

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

            Say(pc, 131, "謝謝！以表謝意$R;" +
                "技能點數當作禮物送給您吧！$R;");

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
                switch (Select(pc, "接受哪種技能點數呢？", "", "暫時想一想", "基本職業", "專門職業", "技術職業"))
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
                        Say(pc, 131, "基本職業的$R;" +
                            "技能點數上升1了$R;");
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
                        Say(pc, 131, "專門職業的$R;" +
                            "技能點數上升1了$R;");
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
                        Say(pc, 131, "技術職業的$R;" +
                            "技能點數上升1了$R;");
                        break;
                }
                return;
            }
            switch (Select(pc, "接受技能點數嗎？", "", "暫時想一想", "接受！"))
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
                    Say(pc, 131, "基本職業的$R;" +
                        "技能點數上升1了$R;");
                    break;
            }
        }
    }
}