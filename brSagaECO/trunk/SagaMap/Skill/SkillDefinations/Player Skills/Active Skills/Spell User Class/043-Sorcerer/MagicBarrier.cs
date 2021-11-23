using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// マジックバリア
    /// </summary>
    public class MagicBarrier : ISkill
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
            if (MobUse)
                level = 5;

            int life = 60000 + 120000 * level;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 250, true);
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

            actor.Buff.MagicDefUp = true;
            actor.Buff.MagicDefRateUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["MDef"];
            actor.Status.mdef_skill -= (short)value;
            value = skill.Variable["MDefAdd"];
            actor.Status.mdef_add_skill -= (short)value;

            actor.Buff.MagicDefUp = false;
            actor.Buff.MagicDefRateUp = false;
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