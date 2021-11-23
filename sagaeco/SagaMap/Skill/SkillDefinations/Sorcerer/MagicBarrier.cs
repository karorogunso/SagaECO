using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    public class MagicBarrier:ISkill
    {
        bool MobUse;
        public MagicBarrier()
        {
            this.MobUse = false;
        }
        public MagicBarrier(bool MobUse)
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
                    life = 300000;
                    break;
                case 2:
                    life = 250000;
                    break;
                case 3:
                    life = 200000;
                    break;
                case 4:
                    life = 150000;
                    break;
                case 5:
                    life = 100000;
                    break;
            }

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 100, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PC)
                {
                    SkillHandler.RemoveAddition(act, "DevineBarrier");
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "MagicBarrier", life);
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
                    atk1 = 15;
                    atk2 = 5;
                    break;
                case 2:
                    atk1 = 20;
                    atk2 = 10;
                    break;
                case 3:
                    atk1 = 25;
                    atk2 = 10;
                    break;
                case 4:
                    atk1 = 30;
                    atk2 = 15;
                    break;
                case 5:
                    atk1 = 35;
                    atk2 = 15;
                    break;
            }

            if (skill.Variable.ContainsKey("MDef"))
                skill.Variable.Remove("MDef");
            skill.Variable.Add("MDef", atk1);
            actor.Status.mdef_skill += (short)atk1;
            if (skill.Variable.ContainsKey("MDefAdd"))
                skill.Variable.Remove("MDefAdd");
            skill.Variable.Add("MDefAdd", atk2);
            actor.Status.mdef_add_skill += (short)atk2;

            //actor.Buff.魔法防御力上昇 = true;
            actor.Buff.MDefUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["MDef"];
            actor.Status.mdef_skill -= (short)value;
            value = skill.Variable["MDefAdd"];
            actor.Status.mdef_add_skill -= (short)value;

            //actor.Buff.魔法防御力上昇 = false;
            actor.Buff.MDefUp = false;
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