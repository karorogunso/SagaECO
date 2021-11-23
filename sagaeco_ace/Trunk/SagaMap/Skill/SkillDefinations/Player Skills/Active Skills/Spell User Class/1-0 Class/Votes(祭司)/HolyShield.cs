using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Vates
{
    public class HolyShield : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.RemoveAddition(dActor, "EarthShield");
            SkillHandler.RemoveAddition(dActor, "FireShield");
            SkillHandler.RemoveAddition(dActor, "WaterShield");
            SkillHandler.RemoveAddition(dActor, "WindShield");
            SkillHandler.RemoveAddition(dActor, "DarkShield");
            int life = new int[] { 0, 15000, 35000, 60000, 100000, 150000 }[level];
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "HolyShield", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int atk1 = skill.skill.Level * 5;
            if (skill.Variable.ContainsKey("HolyShield"))
                skill.Variable.Remove("HolyShield");
            skill.Variable.Add("HolyShield", atk1);
            actor.Status.elements_skill[SagaLib.Elements.Holy] += atk1;

            actor.Buff.BodyHolyElementUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["HolyShield"];
            actor.Status.elements_skill[SagaLib.Elements.Holy] -= (short)value;

            actor.Buff.BodyHolyElementUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
