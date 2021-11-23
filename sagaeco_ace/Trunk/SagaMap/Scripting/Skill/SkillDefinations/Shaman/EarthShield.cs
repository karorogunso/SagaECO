using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Shaman
{
    public class EarthShield:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int life = 0;
            switch (level)
            {
                case 1:
                    life = 20000;
                    break;
                case 2:
                    life = 40000;
                    break;
                case 3:
                    life = 60000;
                    break;
                case 4:
                    life = 100000;
                    break;
                case 5:
                    life = 150000;
                    break;
            }
            //RemoveAddition(dActor, "HolyShield");
            //RemoveAddition(dActor, "FireShield");
            //RemoveAddition(dActor, "WaterShield");
            //RemoveAddition(dActor, "WindShield");
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "EarthShield", life);
            //if (skill.Variable.ContainsKey("FireShield"))
            //    skill.Variable.Remove("FireShield");
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int atk1;
            atk1 = skill.skill.Level * 5;
            if (skill.Variable.ContainsKey("EarthShield"))
                skill.Variable.Remove("EarthShield");
            skill.Variable.Add("EarthShield", atk1);
            actor.Elements[SagaLib.Elements.Earth] += atk1;

            actor.Buff.体の土属性上昇 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["EarthShield"];
            actor.Elements[SagaLib.Elements.Earth] -= (short)value;
            
            actor.Buff.体の土属性上昇 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
