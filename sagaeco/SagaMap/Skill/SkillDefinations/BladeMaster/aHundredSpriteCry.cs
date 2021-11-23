using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    ///  百鬼哭（百鬼哭）
    /// </summary>
    public class aHundredSpriteCry : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }

        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            aHundredSpriteCry1 sc = new aHundredSpriteCry1(args.skill, sActor, dActor,2400, 0,args);
            SkillHandler.ApplyAddition(dActor, sc);
        }
        #endregion
        class aHundredSpriteCry1 : DefaultBuff
        {
            int count = 0;
            public aHundredSpriteCry1(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage,SkillArg arg)
                : base(skill, sActor, dActor, "aHundredSpriteCry1", lifetime, 200, damage,arg)
            {

                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate2 += this.TimerUpdate;

            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {

            }

            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill, SkillArg arg, int damage)
            {

                
                int[] maxcounts = { 0, 5, 5, 6, 6, 7 };
                int maxcount = maxcounts[skill.skill.Level];
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < maxcount)
                    {
                        float[] factor = { 0, 1.7f, 1.8f, 1.7f, 1.8f, 1.7f, 1.5f };

                        if (dActor.HP > 0 && !dActor.Buff.Dead)
                        {
                            damage = SkillHandler.Instance.CalcDamage(true, sActor, dActor, arg, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor[skill.skill.Level]);
                            SkillHandler.Instance.CauseDamage(sActor, dActor, damage);
                            SkillHandler.Instance.ShowVessel(dActor, damage);
                            SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID), dActor, 8041);
                        }
                        count++;
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
