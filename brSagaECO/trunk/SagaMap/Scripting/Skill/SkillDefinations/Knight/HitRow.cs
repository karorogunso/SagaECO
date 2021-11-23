
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Knight
{
    /// <summary>
    /// 胳膊卸力（アームスラスト）
    /// </summary>
    public class HitRow : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (Skill.SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.RAPIER) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
                {
                    return 0;
                }
                else
                {
                    return -14;
                }
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 1000 * level;
            int rate = 20 + 5 * level;
            float factor = 1.8f;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
             if (SagaLib.Global.Random.Next(0, 99) < rate)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "HitRow", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //近命中
            int hit_melee_add = -40;
            if (skill.Variable.ContainsKey("HitRow_hit_melee"))
                skill.Variable.Remove("HitRow_hit_melee");
            skill.Variable.Add("HitRow_hit_melee", hit_melee_add);
            actor.Status.hit_melee_skill += (short)hit_melee_add;
                                        
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["HitRow_hit_melee"];
        
        }
        #endregion
    }
}
