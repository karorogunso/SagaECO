
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// 英雄の加護
    /// </summary>
    public class HerosProtection : ISkill
    {
        #region ISkill Members
        
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Status.Additions.ContainsKey("HerosProtection"))
                return -30;
            else
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 180000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (map == null)
                return;
            List<Actor> targets = map.GetActorsArea(sActor, 250, true);
            foreach (Actor i in targets)
            {
                if (i.type != ActorType.PC)
                    continue;
                if (!i.Status.Additions.ContainsKey("HerosProtection"))
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, i, "HerosProtection", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(i, skill);
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);

            actor.Buff.StateOfMonsterKillerChamp = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);

            actor.Buff.StateOfMonsterKillerChamp = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        #endregion
    }
}
