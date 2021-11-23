using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;
using SagaDB.Item;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31047 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.5f;
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            args.delayRate = 1f;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < 10; i++)
            {
                dest.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);
            SkillHandler.Instance.ShowEffectByActor(sActor, 4133);
            Activator2 sc = new Activator2(sActor, dActor, args, factor);
            sc.Activate();
        }
        private class Activator2 : MultiRunTask
        {
            Actor caster;
            Actor dActor;
            Map map;
            SkillArg arg;
            int maxcount = 40;
            int count = 0;
            List<Actor> dactors = new List<Actor>();
            float factor;
            public Activator2(Actor caster, Actor dactor, SkillArg args,float factor)
            {
                this.caster = caster;
                this.dActor = dactor;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 70;
                this.dueTime = 0;
                this.arg = args;
                this.factor = factor;

                List<Actor> targets = map.GetActorsArea(caster, 150, true);
                foreach (var item in targets)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                    {
                        dactors.Add(item);
                    }
                }

                if (dactors.Count > 1)
                    maxcount = 15;
            }
            public override void CallBack()
            {

                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < maxcount)
                    {
                        Actor i = dactors[SagaLib.Global.Random.Next(0, dactors.Count - 1)];
                        if (i.HP > 0 && !i.Buff.Dead)
                        {
                            SkillHandler.Instance.DoDamage(true, caster, i, arg, SkillHandler.DefType.Def, Elements.Neutral, 0, factor);
                            SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(i.MapID), i, 8041);
                        }
                        count++;
                    }
                    else
                    {
                        this.Deactivate();
                    }
                        
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                    this.Deactivate();
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }

    }
}
