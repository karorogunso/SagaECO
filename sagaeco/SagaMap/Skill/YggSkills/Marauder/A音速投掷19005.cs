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
    public class S19005 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2f;
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            args.delayRate = ((3500 - sActor.Status.aspd * 2) / 1000) * 0.8f;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < 10; i++)
            {
                dest.Add(dActor);
            }
            /*if(sActor.type ==  ActorType.PC)
            {
                if (((ActorPC)sActor).Mode != PlayerMode.NORMAL)
                {
                    硬直 s2 = new 硬直(args.skill, sActor, 1000);
                    SkillHandler.ApplyAddition(sActor, s2);
                }
            }*/
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
            int maxcount = 10;
            int count = 0;
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
            }
            public override void CallBack()
            {

                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < maxcount)
                    {
                        if (dActor.HP > 0 && !dActor.Buff.Dead)
                        {
                            SkillHandler.Instance.DoDamage(true, caster, dActor, arg, SkillHandler.DefType.Def, Elements.Neutral, 0, factor);
                            SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID), dActor, 8041);
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
