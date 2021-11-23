
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 雙手槍命中率提升（２丁拳銃命中率上昇）
    /// </summary>
    public class GunHitUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN)
                    {
                        active = true;
                    }
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "GunHitUp", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }        
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            int[] Hit_Ranged_Add = {0, 3, 6, 10, 15, 20 };

            //遠命中
            int hit_ranged_add = (int)(Hit_Ranged_Add[level]);
            if (skill.Variable.ContainsKey("GunHitUp_hit_ranged"))
                skill.Variable.Remove("GunHitUp_hit_ranged");
            skill.Variable.Add("GunHitUp_hit_ranged", hit_ranged_add);
            actor.Status.hit_ranged_skill += (short)hit_ranged_add;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //遠命中
            actor.Status.hit_ranged_skill -= (short)skill.Variable["GunHitUp_hit_ranged"];
        }
        #endregion
    }
}

