using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaMap.Skill.Additions.Global;
using SagaLib;
using SagaMap;
using SagaMap.Mob;
using SagaDB.Skill;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 闪电链：6×2风属性单次魔法攻击，附加盲目 范围对象判定有问题 待修正
    /// </summary>
    class S16302 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.3f;
            int maxc = 10;
            if (sActor.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Wind)
                {
                    factor = 2.7f;
                    maxc = 10;
                    sActor.EP += 300;
                }
            }
            sActor.EP += 500;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);

            if (sActor.Status.Additions.ContainsKey("元素解放"))
            {
                factor = 3f;
                maxc = 20;
            }
            Activator timer = new Activator(sActor, args, factor, maxc);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<Actor> dactors = new List<Actor>();
            int countMax = 7, count = 0;
            float factor;
            public Activator(Actor caster, SkillArg args,float fa,int maxc)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 100;
                this.dueTime = 100;
                this.countMax = maxc;
                factor = fa;

                List<Actor> targets = map.GetActorsArea(caster, 1500, true);
                foreach (var item in targets)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                    {
                        dactors.Add(item);
                    }
                }
            }
            public override void CallBack()
            {
                //ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        Actor i = dactors[SagaLib.Global.Random.Next(0, dactors.Count - 1)];
                        if (i == null) return;
                        SkillHandler.Instance.ShowEffect(map, i, 5601);
                        

                        SkillHandler.AttackResult res = SkillHandler.AttackResult.Hit;
                        int damaga = SkillHandler.Instance.CalcDamage(true, caster, i, skill, SkillHandler.DefType.MDef, Elements.Wind, 50, factor, out res);
                        if (i.Status.Additions.ContainsKey("雷电链后续"))
                            damaga = (int)(damaga * 0.15f);
                        SkillHandler.Instance.CauseDamage(caster, i, damaga);
                        SkillHandler.Instance.ShowVessel(i, damaga, 0, 0, res);
                        count++;
                        OtherAddition skill2 = new OtherAddition(null, i, "雷电链后续", 1000);
                        SkillHandler.ApplyBuffAutoRenew(i, skill2);
                    }
                    else
                    {
                        this.Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                }
                //解开同步锁
                //ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}
