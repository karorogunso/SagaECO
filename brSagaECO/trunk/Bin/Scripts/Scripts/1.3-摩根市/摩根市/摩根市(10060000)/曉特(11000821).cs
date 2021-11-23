using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000821 : Event
    {
        public S11000821()
        {
            this.EventID = 11000821;

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<MorgFlags> mask = pc.CMask["MorgFlags"];

            Say(pc, 131, "咕嚕嚕嚕~$R;");
            Say(pc, 255, "好像是餓了$R;", "");

            if (CountItem(pc, 10006550) > 0)
            {
                switch (Select(pc, "怎麼辦呢？", "", "什麼也不做", "給美味的肉串燒燒"))
                {
                    case 1:
                        break;

                    case 2:
                        TakeItem(pc, 10006550, 1);
                        Say(pc, 131, "咕碌！$R;");
                        Say(pc, 255, "曉特很享受的吃了「美味的肉串燒燒」$R;", "");
                        Say(pc, 131, "汪汪！！$R;");

                        if (mask.Test(MorgFlags.獲得基本職業) || 
                            mask.Test(MorgFlags.獲得技術職業) || 
                            mask.Test(MorgFlags.獲得專門職業))//_6a63 || _6a64 || _6a65)
                        {
                            return;
                        }

                        Say(pc, 255, "看樣子為了答謝我，給我什麼東西吧$R;", "");

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
                            switch (Select(pc, "接受哪種技能點數呀？", "", "暫時想一想", "基本職業", "專門職業", "技術職業"))
                            {
                                case 1:
                                    break;
                                case 2:
                                    mask.SetValue(MorgFlags.獲得基本職業, true);
                                    //_6a63 = true;
                                    Wait(pc, 1000);
                                    PlaySound(pc, 3087, false, 100, 50);
                                    ShowEffect(pc, 4131);
                                    Wait(pc, 1000);
                                    SkillPointBonus(pc, 1);
                                    Say(pc, 131, "基本職業的技能點數上升1了$R;");
                                    break;
                                case 3:
                                    mask.SetValue(MorgFlags.獲得專門職業, true);
                                    //_6a64 = true;
                                    Wait(pc, 1000);
                                    PlaySound(pc, 3087, false, 100, 50);
                                    ShowEffect(pc, 4131);
                                    Wait(pc, 1000);
                                    SkillPointBonus2X(pc, 1);
                                    //SKILLPBONUS2ND 1
                                    Say(pc, 131, "專門職業的技能點數上升1了$R;");
                                    break;
                                case 4:
                                    mask.SetValue(MorgFlags.獲得技術職業, true);
                                    //_6a65 = true;
                                    Wait(pc, 1000);
                                    PlaySound(pc, 3087, false, 100, 50);
                                    ShowEffect(pc, 4131);
                                    Wait(pc, 1000);
                                    SkillPointBonus2T(pc, 1);
                                    //SKILLPBONUS3RD 1
                                    Say(pc, 131, "技術職業的技能點數上升1了$R;");
                                    break;
                            }
                            return;
                        }
                        switch (Select(pc, "接受技能點數嗎？", "", "暫時想一想", "接受"))
                        {
                            case 1:
                                break;
                            case 2:
                                mask.SetValue(MorgFlags.獲得基本職業, true);
                                //_6a63 = true;
                                Wait(pc, 1000);
                                PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4131);
                                Wait(pc, 1000);
                                SkillPointBonus(pc, 1);
                                Say(pc, 131, "基本職業的技能點數上升1了$R;");
                                break;
                        }
                        break;
                }
            }
        }
    }
}