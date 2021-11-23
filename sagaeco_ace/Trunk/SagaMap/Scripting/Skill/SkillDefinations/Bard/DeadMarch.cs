using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Mob;
namespace SagaMap.Skill.SkillDefinations.Bard
{
    /// <summary>
    /// 死亡進行曲（デッドマーチ）
    /// </summary>
    public class DeadMarch: ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (Skill.SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.STRINGS) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                return 0;
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 5 + 5 * level;
            float factor = 1.0f + 0.5f * level;
            int lifetime = 4000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 350, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SagaLib.Elements.Holy, factor);
            foreach (Actor act in realAffected)
            {
                if (SkillHandler.Instance.isBossMob(act))
                {
                    continue;
                }
                if (act == sActor)
                {
                    continue;
                }                
                if (SkillHandler.Instance.CanAdditionApply(sActor,act, SkillHandler.DefaultAdditions.Stun, rate))
                {
                    Additions.Global.Stun skill1 = new SagaMap.Skill.Additions.Global.Stun(args.skill, act, lifetime);
                    SkillHandler.ApplyAddition(act, skill1);
                }
                if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.鈍足, rate))
                {
                    Additions.Global.鈍足 skill2 = new SagaMap.Skill.Additions.Global.鈍足(args.skill, act, lifetime);
                    SkillHandler.ApplyAddition(act, skill2);
                }
                if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Silence, rate))
                {
                    Additions.Global.Silence skill3 = new SagaMap.Skill.Additions.Global.Silence(args.skill, act, lifetime);
                    SkillHandler.ApplyAddition(act, skill3);
                }
                if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.CannotMove, rate))
                {
                    Additions.Global.CannotMove skill4 = new SagaMap.Skill.Additions.Global.CannotMove(args.skill, act, lifetime);
                    SkillHandler.ApplyAddition(act, skill4);
                }
                if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Confuse, rate))
                {
                    Additions.Global.Confuse skill5 = new SagaMap.Skill.Additions.Global.Confuse(args.skill, act, lifetime);
                    SkillHandler.ApplyAddition(act, skill5);
                }
            }
        }
        #endregion
    }
}
