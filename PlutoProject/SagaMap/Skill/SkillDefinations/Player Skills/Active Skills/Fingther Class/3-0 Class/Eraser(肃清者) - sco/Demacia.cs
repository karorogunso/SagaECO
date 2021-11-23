using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Eraser
{
    /// <summary>
    /// マーシレスシャドウ
    /// </summary>
    public class Demacia : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Status.Additions.ContainsKey("Demacia"))
                return -30;
            else
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] lifetime = { 0, 10000, 15000, 15000, 15000, 20000 };
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "Demacia", lifetime[level]);
            SkillHandler.ApplyAddition(sActor, skill);
            float factor = 1.3f + 0.7f * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 150, true);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
                if (SkillHandler.Instance.CanAdditionApply(sActor, i, SkillHandler.DefaultAdditions.Confuse, 10))
                {
                    Additions.Global.Stiff skill2 = new SagaMap.Skill.Additions.Global.Stiff(args.skill, i, 2500);
                    SkillHandler.ApplyAddition(i, skill);
                }
            }

            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
