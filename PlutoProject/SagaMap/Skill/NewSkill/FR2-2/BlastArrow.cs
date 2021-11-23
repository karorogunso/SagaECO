using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.Network.Client;
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
            return SkillHandler.Instance.CheckPcBowAndArrow(sActor);
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PcArrowDown(sActor);
            float factor = 2.3f + 0.5f * level;
            if (sActor is ActorPC)
            {
                ActorPC pc = sActor as ActorPC;
                //不管是主职还是副职, 只要习得剑圣技能, 都会导致combo成立, 这里一步就行了
                if (pc.Skills.ContainsKey(2130) || pc.DualJobSkill.Exists(x => x.ID == 2130))
                {
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 2130))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 2130).Level;

                    //这里取主职的剑圣等级
                    var mainlv = 0;
                    if (pc.Skills.ContainsKey(2130))
                        mainlv = pc.Skills[2130].Level;

                    //这里取等级最高的剑圣等级用来做居合的倍率加成
                    factor += (float)(0.15f + 0.15 * Math.Max(duallv, mainlv));


                }
            }
            List<Actor> actors = Manager.MapManager.Instance.GetMap(sActor.MapID).GetActorsArea(dActor, 100, false);
            List<Actor> affected = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                    affected.Add(item);
            }
            affected.Add(dActor);
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.STAB;
            args.delayRate = 4.5f;
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
