using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Vates
{
    public class HolyShield:ISkill
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
            //RemoveAddition(dActor, "EarthShield");
            //RemoveAddition(dActor, "FireShield");
            //RemoveAddition(dActor, "WaterShield");
            //RemoveAddition(dActor, "WindShield");
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "HolyShield", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int atk1;
            atk1 = skill.skill.Level * 5;
            if (skill.Variable.ContainsKey("HolyShield"))
                skill.Variable.Remove("HolyShield");
            skill.Variable.Add("HolyShield", atk1);
            actor.Elements[SagaLib.Elements.Holy] += atk1;

            actor.Buff.体の光属性上昇 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["HolyShield"];
            actor.Elements[SagaLib.Elements.Holy] -= (short)value;

            actor.Buff.体の光属性上昇 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        //public void RemoveAddition(Actor actor, String additionName)
        //{
        //    if (actor.Status.Additions.ContainsKey(additionName))
        //    {
        //        Addition addition = actor.Status.Additions[additionName];
        //        actor.Status.Additions.Remove(additionName);
        //        if (addition.Activated)
        //        {
        //            addition.AdditionEnd();
        //        }
        //        addition.Activated = false;
        //    }
        //}
        #endregion
    }
}
