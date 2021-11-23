using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Skill;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 魂魄献祭：消耗n点魂，自身进入hp无法恢复状态，并提升攻防。
    /// </summary>
    public class S43206 : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 20000;
            int atksup = (int)(sActor.Status.max_matk_bs * 0.6f);
            int defaddsup = 100;
            SkillHandler.Instance.ShowEffectOnActor(sActor, 5355);
            SkillHandler.Instance.ShowEffectOnActor(sActor, 4169);
            SacrificeBuff sb = new SacrificeBuff(args.skill, sActor, dActor, lifetime, (int)(sActor.MaxHP * 0.1f));
            SkillHandler.ApplyAddition(sActor, sb);
            HPRecDown debuff1 = new HPRecDown(args.skill, sActor, lifetime, sActor.Status.hp_recover_bs);
            SkillHandler.ApplyAddition(sActor, debuff1);
            //AtkMinUp buff1 = new AtkMinUp(args.skill, sActor, lifetime, atksup);
            MAtkMinUp buff2 = new MAtkMinUp(args.skill, sActor, lifetime, atksup);
            DefAddUp buff3 = new DefAddUp(args.skill, sActor, lifetime, defaddsup);
            MDefAddUp buff4 = new MDefAddUp(args.skill, sActor, lifetime, defaddsup);
            //SkillHandler.ApplyAddition(sActor, buff1);
            SkillHandler.ApplyAddition(sActor, buff2);
            SkillHandler.ApplyAddition(sActor, buff3);
            SkillHandler.ApplyAddition(sActor, buff4);
        }
        class SacrificeBuff : DefaultBuff
        {
            public SacrificeBuff(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage)
                : base(skill, sActor, dActor, "Sacrifice", (int)(lifetime * (1f + Math.Max((dActor.Status.debuffee_bonus / 100), -0.9f))), 3000, damage)
            {
                this.OnUpdate2 += this.TimerUpdate;
            }
            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill, SkillArg arg, int damage)
            {
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (sActor.HP > 0 && !dActor.Buff.Dead)
                    {
                        SkillHandler.Instance.CauseDamage(sActor, sActor, damage);
                        SkillHandler.Instance.ShowVessel(sActor, damage);
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
    }
}