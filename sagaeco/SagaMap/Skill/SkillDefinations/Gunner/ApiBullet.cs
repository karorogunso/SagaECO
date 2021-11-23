
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 徹甲彈（徹甲弾）
    /// </summary>
    public class ApiBullet : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.5f+0.5f*level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            if(sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                pc.TInt["徹甲彈目標"] = (int)dActor.ActorID;
                pc.TInt["徹甲彈目標傷害加成"] = 10 + 20 * level;
            }
            SkillHandler.Instance.Homicidal(sActor, 1);
        }
        #endregion
    }
}