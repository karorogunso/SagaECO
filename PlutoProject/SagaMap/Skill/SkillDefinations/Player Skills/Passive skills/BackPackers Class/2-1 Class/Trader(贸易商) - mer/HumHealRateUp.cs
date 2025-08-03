
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Trader
{
    /// <summary>
    /// 傭兵回復率UP（傭兵回復率上昇）
    /// </summary>
    public class HumHealRateUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPet pet = SkillHandler.Instance.GetPet(sActor);
            if (pet == null)
            {
                return;
            }
            if (SkillHandler.Instance.CheckMobType(pet, "HUMAN"))
            {
                bool active = true;
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, pet, "HumHealRateUp", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(pet, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            //HP恢復率
            short[] HumHealRateUps = { 0, 14, 16, 20, 25, 33 };
            int aspd_add = (int)(HumHealRateUps[level]);
            if (skill.Variable.ContainsKey("PetAtkupSelf_aspd"))
                skill.Variable.Remove("PetAtkupSelf_aspd");
            skill.Variable.Add("PetAtkupSelf_aspd", aspd_add);
            actor.Status.hp_recover_skill += HumHealRateUps[level];

        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //攻擊速度
            actor.Status.aspd_skill -= (short)skill.Variable["PetAtkupSelf_aspd"];
        }
        #endregion
    }
}

