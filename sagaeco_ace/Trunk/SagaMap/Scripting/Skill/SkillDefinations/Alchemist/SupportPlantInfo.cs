
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 植物教育（植物授業）
    /// </summary>
    public class SupportPlantInfo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 15000 + 15000 * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 550, false);
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PC)
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "PlantInfo", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(act, skill);
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
        }
        #endregion
    }
}

