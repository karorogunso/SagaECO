
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Bard
{
    /// <summary>
    /// 合唱（リラクゼーション）
    /// </summary>
    public class HMSPRateUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (Skill.SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.STRINGS) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                return 0;
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 50000 + 10000 * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 150, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PC || act.type == ActorType.PET)
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "HMSPRateUp", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(act, skill);
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.hp_recover_skill += 12;
            actor.Status.mp_recover_skill += 12;
            actor.Status.sp_recover_skill += 12;
            actor.Buff.HP回復率上昇 = true;
            actor.Buff.SP回復率上昇 = true;
            actor.Buff.MP回復率上昇 = true;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.hp_recover_skill -= 12;
            actor.Status.mp_recover_skill -= 12;
            actor.Status.sp_recover_skill -= 12;
            actor.Buff.HP回復率上昇 = false;
            actor.Buff.SP回復率上昇 = false;
            actor.Buff.MP回復率上昇 = false;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
