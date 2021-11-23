using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// イクスパンジアーム
    /// </summary>
    public class IkspiariArmusing : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool cure = false;
            if (SagaLib.Global.Random.Next(0, 99) < 70)
            {
                cure = true;
            }
            if (cure)
            {
                RemoveAddition(sActor, "Poison");
                RemoveAddition(sActor, "MoveSpeedDown");
                RemoveAddition(sActor, "MoveSpeedDown2");
                RemoveAddition(sActor, "Stone");
                RemoveAddition(sActor, "Silence");
                RemoveAddition(sActor, "Stun");
                RemoveAddition(sActor, "Sleep");
                RemoveAddition(sActor, "Frosen");
                RemoveAddition(sActor, "Confuse");
            }
            float factor = 1.4f + 0.3f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
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
