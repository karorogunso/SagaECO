using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    public class EnergyBarrier:ISkill
    {
         bool MobUse;
        public EnergyBarrier()
        {
            this.MobUse = false;
        }
        public EnergyBarrier(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int life = 0;
            if (MobUse)
            {
                level = 5;
            }
            switch (level)
            {
                case 1:
                    life = 600000;
                    break;
                case 2:
                    life = 500000;
                    break;
                case 3:
                    life = 400000;
                    break;
                case 4:
                    life = 300000;
                    break;
                case 5:
                    life = 200000;
                    break;
            }

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 100, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type== ActorType.PC)
                {
                    SkillHandler.RemoveAddition(act, "DevineBarrier");
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "EnergyBarrier", life);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(act, skill);
                }
            }
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int atk1 = 0, atk2 = 0;
            switch (skill.skill.Level)
            {
                case 1:
                    atk1 = 3;
                    atk2 = 5;
                    break;
                case 2:
                    atk1 = 3;
                    atk2 = 10;
                    break;
                case 3:
                    atk1 = 6;
                    atk2 = 10;
                    break;
                case 4:
                    atk1 = 6;
                    atk2 = 15;
                    break;
                case 5:
                    atk1 = 9;
                    atk2 = 15;
                    break;
            }

            if (skill.Variable.ContainsKey("Def"))
                skill.Variable.Remove("Def");
            skill.Variable.Add("Def", atk1);
            actor.Status.def_skill += (short)atk1;
            if (skill.Variable.ContainsKey("DefAdd"))
                skill.Variable.Remove("DefAdd");
            skill.Variable.Add("DefAdd", atk2);
            actor.Status.def_add_skill += (short)atk2;

            actor.Buff.DefAddUp = true;
            actor.Buff.DefUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["Def"];
            actor.Status.def_skill -= (short)value;
            value = skill.Variable["DefAdd"];
            actor.Status.def_add_skill -= (short)value;

            actor.Buff.DefAddUp = false;
            actor.Buff.DefUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
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