
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap.ActorEventHandlers;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Farmasist
{
    /// <summary>
    /// 草隱陷阱（グラストラップ）
    /// </summary>
    public class GrassTrap : Trap
    {
        public GrassTrap()
            : base(true, 100, PosType.sActor)
        {

        }
        public override void BeforeProc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            LifeTime = 17000 - 2000 * level;
        }
        public override void ProcSkill(Actor sActor, Actor mActor, ActorSkill actor, SkillArg args, Map map, int level, float factor)
        {
            factor = 0.33f;
            float HP_Lost = (float)(mActor.HP * factor);
            int maxdmg = 1000 + 1000 * level;
            if (HP_Lost > maxdmg)
            {
                HP_Lost = maxdmg;
            }
            SkillHandler.Instance.FixAttack(sActor, mActor, args, sActor.WeaponElement, HP_Lost);
        }
    }
}