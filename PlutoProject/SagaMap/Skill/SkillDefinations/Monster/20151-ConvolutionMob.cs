
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 大车轮 (大車輪)怪物用
    /// </summary>
    public class ConvolutionMob : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 8.7f;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 200, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.鈍足, 50))
                    {
                        //这里并不知道顿足的持续时间, 先暂时设定为本技能1级时持续1秒, 每级增加0.25秒 满级顿足 2.25秒
                        Additions.Global.鈍足 skill = new SagaMap.Skill.Additions.Global.鈍足(args.skill, dActor, (int)(750 + 250 * level));
                        SkillHandler.ApplyAddition(dActor, skill);
                    }
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
