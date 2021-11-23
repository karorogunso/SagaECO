using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    ///  百鬼哭（百鬼哭）
    /// </summary>
    public class aHundredSpriteCry : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
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
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = new float[] { 0, 1.7f, 1.8f, 1.9f, 2.0f, 2.1f, 2.5f }[level];

            int[] attackTimes = { 0, 3, 3, 4, 4, 5, 10 };
            if (sActor is ActorPC)
            {
                int lv = 0;
                ActorPC pc = sActor as ActorPC;
                if (pc.Skills3.ContainsKey(1117))
                {
                    lv = pc.Skills3[1117].Level;
                    factor += (0.3f + 0.1f * lv);
                    //ジリオンブレイド
                    SkillHandler.Instance.SetNextComboSkill(sActor, 2534);
                }
            }
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < attackTimes[level]; i++)
                dest.Add(dActor);
            args.delayRate = 4.5f;
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}
