using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
   /// <summary>
    /// 遠距離迴避
    /// </summary>

    public class LAvoUp: ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }


        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (sActor.type == ActorType.PC)
            {
                active = true;
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "LAVOUp", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }

        
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value;
            value = 4 + 4 * skill.skill.Level;


            if (skill.Variable.ContainsKey("LAVOUp-HitUp"))
                skill.Variable.Remove("LAVOUp-HitUp");
            skill.Variable.Add("LAVOUp-HitUp", value);
            actor.Status.hit_ranged_skill += (short)value;

            value = 2 + 4 * skill.skill.Level;

            if (skill.Variable.ContainsKey("LAVOUp-VoUp"))
                skill.Variable.Remove("LAVOUp-VoUp");
            skill.Variable.Add("LAVOUp-VoUp", value);
            actor.Status.avoid_ranged_skill += (short)value;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            if (actor.type == ActorType.PC)
            {
                int value = skill.Variable["LAVOUp-HitUp"];
                actor.Status.hit_ranged_skill -= (short)value;
                value = skill.Variable["LAVOUp-VoUp"];
                actor.Status.avoid_ranged_skill -= (short)value;
            }
        }

        #endregion




    }
}
