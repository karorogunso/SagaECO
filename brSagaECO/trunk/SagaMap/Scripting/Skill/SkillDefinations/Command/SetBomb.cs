using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 定時炸彈（セットボム）
    /// </summary>
    public class SetBomb : ISkill
    {
        #region ISkill Members

        uint itemID = 10022307;
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                if (SkillHandler.Instance.CountItem(sActor, itemID) > 0)
                {
                    SkillHandler.Instance.TakeItem(sActor, itemID, 1);
                    return 0;
                }
                else
                {
                    return -2;
                }
            }
            else
            {
                return -14;
            }

        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, 1.0f);
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(2378, level, 3000));
        }
        #endregion
    }
}
