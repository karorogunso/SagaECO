using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// 火光蟲（フレアスティング）
    /// </summary>
    public class FlareSting :ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float[] factors = { 0f, 2.25f, 2.50f, 2.75f,3.0f,3.25f };
            float factor = factors[level];
            SkillHandler.Instance.PhysicalAttack (sActor, dActor, args, SagaLib.Elements.Dark, factor);
            int rate = 25 + 5 * level;
            if (args.hp.Count > 0)
            {
                if (SagaLib.Global.Random.Next(0, 99) < rate && args.hp[0] > 0 && !SkillHandler.Instance.isBossMob(dActor))
                {
                    AutoCastInfo info = new AutoCastInfo();
                    info.delay = 500;
                    info.level = level;
                    info.skillID = 2404;
                    args.autoCast.Add(info);
                }
            }
        }
        #endregion
    }
}
