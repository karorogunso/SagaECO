using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// 団子
    /// </summary>
    public class Dango : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = -0.9f;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, factor);
            RemoveAddition(dActor, "Poison");
            RemoveAddition(dActor, "鈍足");
            RemoveAddition(dActor, "Stone");
            RemoveAddition(dActor, "Silence");
            RemoveAddition(dActor, "Stun");
            RemoveAddition(dActor, "Sleep");
            RemoveAddition(dActor, "Frosen");
            RemoveAddition(dActor, "Confuse");
        }
        public void RemoveAddition(Actor actor, String additionName)
        {
            if (actor.Status.Additions.ContainsKey(additionName))
            {
                Addition addition = actor.Status.Additions[additionName];
                actor.Status.Additions.Remove(additionName);
                if (addition.Activated)
                {
                    addition.AdditionEnd();
                }
                addition.Activated = false;
            }
        }

        #endregion
    }
}
