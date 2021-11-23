
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 猛鳥回音（シュリルボイス）[接續技能]
    /// </summary>
    public class PetBirdAtkRowCircle2 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 30000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 200, false);
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "PetBirdAtkRowCircle", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(act, skill);
                    Confuse skill2 = new Confuse(args.skill, act, lifetime);
                    SkillHandler.ApplyAddition(act, skill2);
                }
            }
            
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //近命中
            int hit_melee_add = -(int)(6 * level);
            if (skill.Variable.ContainsKey("PetBirdAtkRowCircle_hit_melee"))
                skill.Variable.Remove("PetBirdAtkRowCircle_hit_melee");
            skill.Variable.Add("PetBirdAtkRowCircle_hit_melee", hit_melee_add);
            actor.Status.hit_melee_skill += (short)hit_melee_add;
                                        
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["PetBirdAtkRowCircle_hit_melee"];
        }
        #endregion
    }
}
