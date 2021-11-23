using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.ActorEventHandlers;

namespace SagaMap.Skill.SkillDefinations.X
{
    public class IceHeart2 : MobISkill
    {
        #region ISkill Members

        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 300, false, true);
            List<Actor> realAffected = new List<Actor>();
            ActorPC ec = new ActorPC();
            foreach (Actor act in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                    realAffected.Add(act);
            }
            foreach (Actor acs in realAffected)
            {
                if (acs.type == ActorType.PC)
                {
                    ActorPC ac = (ActorPC)acs;
                    if (ac.Job == PC_JOB.VATES || ac.Job == PC_JOB.DRUID || ac.Job == PC_JOB.CARDINAL || ac.Job == PC_JOB.BARD)
                        ec = ac;
                }
            }
            if (dActor.type == ActorType.PC)
            {
                if (ec == null)
                    ec = (ActorPC)dActor;
            }
            if (ec != null && sActor.type == ActorType.MOB)
            {
                ActorMob mob = (ActorMob)sActor;
                uint hate = ((MobEventHandler)mob.e).AI.Hate[dActor.ActorID] + 5000;
                ((MobEventHandler)mob.e).AI.Hate[ec.ActorID] = hate;
                ((MobEventHandler)mob.e).AI.NextSurelySkillID = 20011;
            }

            short[] pos = new short[2];
            pos[0] = ec.X;
            pos[1] = ec.Y;
            map.MoveActor(Map.MOVE_TYPE.START, sActor, pos, 20000, 20000, true);

            SagaMap.Skill.Additions.Global.Stun stun = new Stun(args.skill, ec, 5000);
            SkillHandler.ApplyAddition(ec, stun);

            
        }
        #endregion
    }
}
