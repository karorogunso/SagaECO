
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.TreasureHunter
{
    /// <summary>
    /// 鞭子拖拉（キャッチ）
    /// </summary>
    public class Catch : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.ROPE) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                if (dActor.type == ActorType.MOB)
                {
                    if(SkillHandler.Instance.isBossMob((ActorMob)dActor))
                    {
                        return -14;
                    }
                }
                return 0;
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.FixAttack(sActor, dActor, args, SagaLib.Elements.Neutral, 1f);
            int lifetime = 1000 ;
            Stiff dskill = new Stiff(args.skill, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, dskill);
        }
        #endregion
    }
}