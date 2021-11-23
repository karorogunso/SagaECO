
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Bard
{
    /// <summary>
    /// 古典演奏（クラシック）
    /// </summary>
    public class HPSPMPUPCircle : ISkill
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
            int lifetime = 6000 + 2000 * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 150, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type== ActorType.PC || act.type== ActorType.PET)
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "HPSPMPUPCircle", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(act, skill);
                }
            }            
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level=skill.skill.Level;
            actor.MaxHP += (uint)(50 + 50 * level);
            actor.MaxMP  += (uint)(50 + 50 * level);
            actor.MaxSP += (uint)(50 * level);
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            actor.MaxHP -= (uint)(50 + 50 * level);
            actor.MaxMP -= (uint)(50 + 50 * level);
            actor.MaxSP -= (uint)(50 * level);
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
        }
        #endregion
    }
}
