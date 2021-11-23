
using System;
using System.Collections.Generic;
using SagaMap.Skill.Additions.Global;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    /// <summary>
    /// 黑暗火焰（ダークブレイズ）
    /// </summary>
    public class EventSelfDarkStorm : ISkill
    {
        bool MobUse;
        public EventSelfDarkStorm()
        {
            this.MobUse = false;
        }
        public EventSelfDarkStorm(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 5;
            }
            float factor = 2.5f + 0.5f * level;
            short range = (short)((100 * level) + 50);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, range, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                    if(act.Darks == 1)
                    {
                        Manager.MapManager.Instance.GetMap(sActor.MapID).SendEffect(dActor, 5202);
                        DuckFire df = new DuckFire(args.skill, sActor, act, 10000, SkillHandler.Instance.CalcDamage(false, sActor, act, args, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 50, 0.2f));
                        SkillHandler.ApplyAddition(sActor, df);
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SagaLib.Elements.Dark, factor);
        }
        class DuckFire : DefaultBuff
        {
            public DuckFire(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage)
                : base(skill, sActor, dActor, "DuckFire", lifetime, 1000, damage)
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
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (dActor.HP > 0 && !dActor.Buff.Dead)
                    {
                        SkillHandler.Instance.CauseDamage(sActor, dActor, damage);
                        SkillHandler.Instance.ShowVessel(dActor, damage);
                        SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID), dActor, 5141);
                    }
                }
                catch (Exception ex)
                {
                   SagaLib.Logger.ShowError(ex);
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}