
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Harvest
{
    /// <summary>
    /// ツイステッドプラント
    /// </summary>
    public class TwistedPlant : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if(dActor.Status.Additions.ContainsKey("PlantShield"))
            {
                return -14;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 15000 + 15000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "PlantShield", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.PlantShield = actor.MaxHP;
            actor.Buff.三转植物寄生 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.PlantShield = 0;
            actor.Buff.三转植物寄生 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
