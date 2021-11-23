using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Shaman
{
    public class WindShield:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //RemoveAddition(dActor, "HolyShield");
            //RemoveAddition(dActor, "EarthShield");
            //RemoveAddition(dActor, "FireShield");
            //RemoveAddition(dActor, "WaterShield");
            int amount = args.skill.Level * 5;
            ShieldWind skill = new ShieldWind(args.skill, dActor, 10000, amount);
            SkillHandler.ApplyAddition(dActor, skill);
        }

        #endregion
    }
}
