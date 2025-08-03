using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 科學祝福
    /// </summary>
    public class MobHolyfeather : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 12000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 200, true);
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.MOB)
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "MobHolyfeather", lifetime, 3000);
                    skill.OnAdditionStart += this.StartEvent;
                    skill.OnAdditionEnd += this.EndEvent;
                    skill.OnUpdate += this.TimerUpdate;
                    SkillHandler.ApplyAddition(act, skill);
                }
            }
        }
        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void TimerUpdate(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            uint hpadd = 800;
            if (!actor.Buff.NoRegen)
                actor.HP += hpadd;
            if (actor.HP > actor.MaxHP)
            {
                actor.HP = actor.MaxHP;
            }
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
        }
        #endregion
    }
}