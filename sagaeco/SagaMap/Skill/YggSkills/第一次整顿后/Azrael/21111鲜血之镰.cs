using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    public partial class S21111 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if(pc.HP < pc.MaxHP * 0.21)
            {
                SkillHandler.SendSystemMessage(pc, "HP不足以释放该【鲜血之镰】");
                return -150;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //获取锁定目标
            dActor = SkillHandler.Instance.GetdActor(sActor, args);
            if (dActor == null) return;

            //造成混合伤害
            float factor = 3f + 1.2f * level;
            int damage = SkillHandler.Instance.PhysicalAttack(sActor, new List<Actor> { dActor }, args, SkillHandler.DefType.Def, Elements.Dark, 0, factor, true);

            //附加流血DEBUFF
            int bleedingdamage = damage / 10;
            Bleeding bleeding = new Bleeding(null, sActor, dActor, 10000, bleedingdamage);
            SkillHandler.ApplyBuffAutoRenew(dActor,bleeding);

            //七魄.Show(sActor, dActor, "雀魄", 21906);
        }
    }
}
