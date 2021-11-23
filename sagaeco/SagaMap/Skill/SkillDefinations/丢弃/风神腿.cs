using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.X
{
    class Fengshenlegs : MobISkill
    {
        #region ISkill Members

        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            if (dActor.Status.Additions.ContainsKey("Parry"))
            {
                Skill.Additions.Global.Stun stun = new Stun(args.skill, dActor, 5000);
                SkillHandler.ApplyAddition(dActor, stun);
                EffectArg arg = new EffectArg();
                arg.effectID = 5133;
                arg.actorID = dActor.ActorID;
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, dActor, true);
                SkillHandler.Instance.FixAttack(sActor, dActor, args, Elements.Neutral, 888);
            }
            else
            {
                SkillHandler.Instance.FixAttack(sActor, dActor, args, Elements.Neutral, 100);
            }
        }
        #endregion
    }
}
