
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 黑暗領域（ダークライト）
    /// </summary>
    public class DarkLight : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 15000 + 5000 * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 100, true);
            foreach (Actor act in affected)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, act, "DarkLight", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(act, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //近命中
            int hit_melee_add = -(int)(actor.Status.hit_melee * (0.2f + 0.05f * level));
            if (skill.Variable.ContainsKey("DarkLight_hit_melee"))
                skill.Variable.Remove("DarkLight_hit_melee");
            skill.Variable.Add("DarkLight_hit_melee", hit_melee_add);
            actor.Status.hit_melee_skill += (short)hit_melee_add;
                           
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["DarkLight_hit_melee"];
           
        }
        #endregion
    }
}
