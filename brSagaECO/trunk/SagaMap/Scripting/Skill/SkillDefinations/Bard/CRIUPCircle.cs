
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Bard
{
    /// <summary>
    /// 樂與怒演奏（ロックンロール）
    /// </summary>
    public class CRIUPCircle : ISkill
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
                if (act.type == ActorType.PC || act.type == ActorType.PET)
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "CRIUPCircle", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(act, skill);
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //會心一擊率
            int aspd_add = (int)(0.05f * level);
            if (skill.Variable.ContainsKey("CRIUPCircle_aspd"))
                skill.Variable.Remove("CRIUPCircle_aspd");
            skill.Variable.Add("CRIUPCircle_aspd", aspd_add);
            actor.Status.cri_skill += (short)aspd_add;
    
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //會心一擊率
            actor.Status.cri_skill  -= (short)skill.Variable["CRIUPCircle_aspd"];
              
        }
        #endregion
    }
}
