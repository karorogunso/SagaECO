using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 星辰亂舞（ディスラプション）
    /// </summary>
    public class BeatUp : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(sActor, args))
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
            BeatUp1 bu = new BeatUp1(args.skill, sActor, dActor, 2000, 0, args);
            SkillHandler.ApplyAddition(sActor, bu);
        }
        #endregion
        class BeatUp1 : DefaultBuff
        {
            int count = 0;
            public BeatUp1(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage, SkillArg arg)
                : base(skill, sActor, dActor, "BeatUp1", lifetime, 400, damage, arg)
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
                int maxcount = 3;
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < maxcount)
                    {
                        float factor = 1.2f + 0.3f * arg.skill.Level;
                        Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
                        List<Actor> actors = map.GetActorsArea(sActor, 200, false);
                        foreach (Actor item in actors)
                        {
                            if (item.HP > 0 && !item.Buff.Dead)
                            {
                                damage = SkillHandler.Instance.CalcDamage(true, sActor, item, arg, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor);
                                SkillHandler.Instance.CauseDamage(sActor, item, damage);
                                SkillHandler.Instance.ShowVessel(item, damage);
                                SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(item.MapID), item, 8041);
                            }
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
