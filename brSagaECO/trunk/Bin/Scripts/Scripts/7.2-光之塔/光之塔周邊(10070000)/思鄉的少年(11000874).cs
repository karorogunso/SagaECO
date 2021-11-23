using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10070000
{
    public class S11000874 : Event
    {
        public S11000874()
        {
            this.EventID = 11000874;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<LightTower> LightTower_mask = pc.CMask["LightTower"];

            if (LightTower_mask.Test(LightTower.基本職業) ||
                LightTower_mask.Test(LightTower.專門職業) ||
                LightTower_mask.Test(LightTower.技術職業))
            {
                Say(pc, 255, "那座塔的上面還有那傢伙呀$R;" +
                    "小心點阿…$R;");
                return;
            }

            Say(pc, 255, "……$R;");

            if (LightTower_mask.Test(LightTower.給予懷錶))
            {
                Say(pc, 255, "謝謝……$R;" +
                    "為了答謝您，給您汽笛…$R;");
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
                    switch (Select(pc, "接受哪種的技能點數呢？", "", "暫時想一想", "基本職業", "專門職業", "技術職業"))
                    {
                        case 1:
                            break;

                        case 2:
                            LightTower_mask.SetValue(LightTower.基本職業, true);
                            //_6a60 = true;
                            Wait(pc, 1000);
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 1000);
                            SkillPointBonus(pc, 1);
                            Say(pc, 131, "基本職業的$R;" +
                                "技能點數上升了1了$R;");
                            break;

                        case 3:
                            LightTower_mask.SetValue(LightTower.專門職業, true);
                            //_6a61 = true;
                            Wait(pc, 1000);
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 1000);
                            SkillPointBonus2X(pc, 1);
                            //SKILLPBONUS2ND 1
                            Say(pc, 131, "專門職業的$R;" +
                                "技能點數上升了1了$R;");
                            break;

                        case 4:
                            LightTower_mask.SetValue(LightTower.技術職業, true);
                            //_6a62 = true;
                            Wait(pc, 1000);
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 1000);
                            SkillPointBonus2T(pc, 1);
                            //SKILLPBONUS3RD 1
                            Say(pc, 131, "技術職業的$R;" +
                                "技能點數上升了1了$R;");
                            break;
                    }
                    return;
                }

                switch (Select(pc, "要接受技能點數嗎？", "", "暫時想一想", "接受"))
                {
                    case 1:
                        break;

                    case 2:
                        LightTower_mask.SetValue(LightTower.基本職業, true);
                        //_6a60 = true;
                        Wait(pc, 1000);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 1000);
                        SkillPointBonus(pc, 1);
                        Say(pc, 131, "基本職業的$R;" +
                            "技能點數上升了1了$R;");
                        break;
                }
                return;
            }

            if (CountItem(pc, 10024680) > 0)
            {
                Say(pc, 255, "給摩根……$R;" +
                    "的那……個女人吧……$R;");
            }

            GiveItem(pc, 10024680, 1);
            Say(pc, 255, "把這個…給她...$R;");
        }
    }
}