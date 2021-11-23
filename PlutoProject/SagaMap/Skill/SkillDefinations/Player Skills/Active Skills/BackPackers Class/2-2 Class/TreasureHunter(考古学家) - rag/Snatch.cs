
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.TreasureHunter
{
    /// <summary>
    /// 偷天換日（スナッチ）
    /// </summary>
    public class Snatch : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.8f + 0.2f * level;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Skills3.ContainsKey(992) || pc.DualJobSkill.Exists(x => x.ID == 992))
                {

                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 992))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 992).Level;

                    //这里取主职的剑圣等级
                    var mainlv = 0;
                    if (pc.Skills3.ContainsKey(992))
                        mainlv = pc.Skills3[992].Level;

                    //这里取等级最高的剑圣等级用来做居合的倍率加成
                    factor += 0.1f * Math.Max(duallv, mainlv);
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            int rate = 3 * level;
            if (SagaLib.Global.Random.Next(0, 99) < rate && !dActor.Status.Additions.ContainsKey("Snatch"))
            {
                if (dActor.type == ActorType.MOB)
                {
                    ActorMob mob = (ActorMob)dActor;
                    if(!SkillHandler.Instance.isBossMob(mob))
                    {
                        //偷東西!!
                        DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Snatch", int.MaxValue);
                        skill.OnAdditionStart += this.StartEventHandler;
                        skill.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(dActor, skill);
                    }
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
