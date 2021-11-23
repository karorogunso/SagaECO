using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000778 : Event
    {
        public S11000778()
        {
            this.EventID = 11000778;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "用新鮮水果做成的果汁！$R;" +
                "$R在帕斯特生産的水果$R;" +
                "做成的果汁怎樣?$R;");

            switch (Select(pc, "做什麼?", "", "榨果汁（收費）", "看秘方", "什麽都不做"))
            {
                case 1:
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
                       || pc.Job == PC_JOB.GUNNER
                       || pc.Job == PC_JOB.WIZARD
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
                        Synthese(pc, 2023, 1);
                        return;
                    }
                    Synthese(pc, 2023, 2);
                    break;
                case 2:
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
                        || pc.Job == PC_JOB.GUNNER
                        || pc.Job == PC_JOB.WIZARD
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
                        Say(pc, 131, "用10個『蘋果』$R;" +
                            "可以做『100%蘋果果汁』$R;" +
                            "$R真簡單吧$R;");
                        return;
                    }
                    Say(pc, 131, "用10個『蘋果』$R;" +
                        "可以做『100%蘋果果汁』$R;" +
                        "$R當然除了蘋果$R;" +
                        "還有用甜梅、橙或櫻桃也可以！$R;");
                    break;
            }
        }
    }
}