using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 雷击术：3×3风属性魔法多段攻击，附加盲目
    /// </summary>
    public class S42301 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("雷击术CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> targets = map.GetActorsArea(dActor, 100, true);
            List<Actor> dactors = new List<Actor>();
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    attack sc = new attack(args.skill, sActor, item, 1200, 0, args);
                    SkillHandler.ApplyAddition(item, sc);
                }
            }
            if (sActor.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Wind)
                    sActor.EP += 500;
            }
            if (sActor.Status.Additions.ContainsKey("元素解放"))
            {
                DefaultBuff cd = new DefaultBuff(sActor, "雷击术CD", 5000);
                SkillHandler.ApplyAddition(sActor, cd);
            }
        }
        #endregion

        class attack : DefaultBuff
        {
            int count = 0;
            public attack(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage, SkillArg arg)
                : base(skill, sActor, dActor, "雷击术", lifetime, 200, damage, arg)
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
                float factor = 2.4f;
                if (sActor.Status.Additions.ContainsKey("属性契约"))
                {
                    if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Wind)
                    {
                        factor = 3.4f;
                        maxcount = 5;
                    }
                }
                if (sActor.Status.Additions.ContainsKey("元素解放"))
                {
                    factor = 5.5f;
                    maxcount = 5;
                }
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                try
                {
                    if (count < maxcount)
                    {
                        if (dActor.HP > 0 && !dActor.Buff.Dead)
                        {
                            if (factor > 5f)
                            {
                                Paralyse f = new Paralyse(this.skill, dActor, 3000, 20);
                                SkillHandler.ApplyAddition(dActor, f);
                            }
                            SkillHandler.AttackResult res = SkillHandler.AttackResult.Hit;
                            damage = SkillHandler.Instance.CalcDamage(true, sActor, dActor, arg, SkillHandler.DefType.MDef, Elements.Wind, 0, factor, out res);
                            SkillHandler.Instance.CauseDamage(sActor, dActor, damage);
                            SkillHandler.Instance.ShowVessel(dActor, damage, 0, 0, res);
                            SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(dActor.MapID), dActor, 8041);
                        }
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
                //ClientManager.LeaveCriticalArea();
            }
        }
    }
}
