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
            Say(pc, 131, "用新鲜水果做成的果汁！$R;" +
                "$R在法伊斯特生产的水果$R;" +
                "做成的果汁怎样?$R;");

            switch (Select(pc, "做什么?", "", "榨果汁（收费）", "看秘方", "什么都不做"))
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
                        Say(pc, 131, "用10个『苹果』$R;" +
                            "可以做『百分之百的天然果汁』$R;" +
                            "$R真简单吧$R;");
                        return;
                    }
                    Say(pc, 131, "用10个『苹果』$R;" +
                        "可以做『百分之百的天然果汁』$R;" +
                        "$R当然除了苹果$R;" +
                        "还有用甜梅、橙或樱桃也可以！$R;");
                    break;
            }
        }
    }
}