using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    class IceInfernal: MobISkill
    {
        #region ISkill Members

        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if(sActor.type == ActorType.MOB)
            {
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                ActorMob mob = (ActorMob)sActor;
                ActorEventHandlers.MobEventHandler mobe = ((ActorEventHandlers.MobEventHandler)mob.e);
                List<uint> ids = new List<uint>();
                if(mobe.AI.Hate.Count> 0)
                {
                    foreach (uint aid in mobe.AI.Hate.Keys)
                    {
                        ids.Add(aid);
                    }
                    uint id = ids[SagaLib.Global.Random.Next(0, ids.Count)];
                    Actor da = map.GetActor(id);
                    if (da != null)
                        dActor = da;
                }
            }
            return;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 5f;
            ActorSkill actorS = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 300, null);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    Additions.Global.Freeze freeze = new Additions.Global.Freeze(args.skill, i, 5000);
                    SkillHandler.ApplyAddition(i, freeze);
                    affected.Add(i);
                }
            }
            
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Water, factor);
        }
        #endregion
    }
}
