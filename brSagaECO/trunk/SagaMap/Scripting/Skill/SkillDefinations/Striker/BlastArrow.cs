using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 暴風之箭（ブラストアロー）
    /// </summary>
    public class BlastArrow : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.7f + 0.5f * level;
            uint StrikeArrowSkillID =2130;
            ActorPC pc=(ActorPC) sActor ;
            if (pc.Skills2.ContainsKey(StrikeArrowSkillID))
            {
                factor = 1.6f + 0.5f * level;
            }
            if (pc.SkillsReserve.ContainsKey(StrikeArrowSkillID))
            {
                factor = 1.6f + 0.5f * level;
            }
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 100, null);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    affected.Add(i);
                }
            }

            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}
