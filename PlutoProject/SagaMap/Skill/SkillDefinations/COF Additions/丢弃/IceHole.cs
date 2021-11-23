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
    class IceHole : MobISkill
    {
        #region ISkill Members

        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 1000, false, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                    realAffected.Add(act);
            }
            foreach (Actor a in realAffected)
            {
                Skill.Additions.Global.MoveSpeedDown 钝足 = new MoveSpeedDown(args.skill, a, 5000);
                SkillHandler.ApplyAddition(a, 钝足);
                EffectArg arg = new EffectArg();
                arg.effectID = 5078;
                arg.actorID = a.ActorID;
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, a, true);
            }
            if (sActor.type == ActorType.MOB)
            {
                ((SagaMap.ActorEventHandlers.MobEventHandler)((ActorMob)sActor).e).AI.NextSurelySkillID = 20006;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 1000, false, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                    realAffected.Add(act);
            }
            //SkillHandler.Instance.MagicAttack(sActor,realAffected,args, Elements.Water,2f);
            foreach (Actor act in realAffected)
            {
                Skill.Additions.Global.Freeze freeze = new Freeze(args.skill, act, 5000);
                SkillHandler.ApplyAddition(act, freeze);
                EffectArg arg = new EffectArg();
                arg.effectID = 5284;
                arg.actorID = act.ActorID;
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, act, true);
            }
            
        }
        #endregion
    }
}
