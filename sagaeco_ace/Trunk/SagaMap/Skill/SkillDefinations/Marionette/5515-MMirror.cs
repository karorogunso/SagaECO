
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Marionette
{
    /// <summary>
    /// 小丘比分身
    /// </summary>
    public class MMirror : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 60000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MMirror", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Slave.Add(map.SpawnMob(26160006, (short)(actor.X + SagaLib.Global.Random.Next(-1, 1)), (short)(actor.Y + SagaLib.Global.Random.Next(-1, 1)), 2500, actor));
            actor.Slave.Add(map.SpawnMob(26160006, (short)(actor.X + SagaLib.Global.Random.Next(-1, 1)), (short)(actor.Y + SagaLib.Global.Random.Next(-1, 1)), 2500, actor));
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            var mobs=from ActorMob m in (from Actor a in actor.Slave where a.type== ActorType.MOB select a)
                     where m.MobID ==26160006
                     select m;
            foreach (ActorMob m in mobs)
            {
                m.ClearTaskAddition();
                map.DeleteActor(m);
            }
        }
        #endregion
    }
}
