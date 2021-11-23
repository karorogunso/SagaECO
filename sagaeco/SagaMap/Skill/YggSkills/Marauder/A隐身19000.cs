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
    public class S19000:ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Invisible inv = new Invisible(args.skill, sActor, 10000);
            SkillHandler.ApplyAddition(sActor, inv);

            /*for (int i = 0; i < 100; i++)
            {
                Activator2 sc = new Activator2(sActor, dActor, args);
                sc.Activate();
            }
            for (int i = 0; i < 100; i++)
            {
                Freeze fa = new Freeze(args.skill, sActor, 10000, 30);
                SkillHandler.ApplyAddition(sActor, fa);
                Stone s = new Stone(args.skill, sActor, 10000, 30);
                SkillHandler.ApplyAddition(sActor, s);
                DefUp su = new DefUp(args.skill, sActor, 10000, 30);
                SkillHandler.ApplyAddition(sActor, su);
                MDefUp dsu = new MDefUp(args.skill, sActor, 10000, 30);
                SkillHandler.ApplyAddition(sActor, dsu);
                DEFDOWN sd = new DEFDOWN(args.skill, sActor, 10000, 30);
                SkillHandler.ApplyAddition(sActor, sd);
                MDEFDOWN msd = new MDEFDOWN(args.skill, sActor, 10000, 30);
                SkillHandler.ApplyAddition(sActor, msd);

                List<string> name = new List<string>();
                foreach (var item in sActor.Status.Additions.Keys)
                {
                    name.Add(item);
                }
                foreach (var item in name)
                {
                    SkillHandler.RemoveAddition(sActor, item);
                }
            }*///BUFF压力测试脚本

        }
        private class Activator2 : MultiRunTask
        {
            Actor sActor;
            Actor dActor;
            Map map;
            SkillArg args;
            int maxcount = 300;
            int count = 0;
            public Activator2(Actor caster, Actor dactor, SkillArg args)
            {
                this.sActor = caster;
                this.dActor = dactor;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 10;
                this.dueTime = 0;
                this.args = args;
            }
            public override void CallBack()
            {

                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < maxcount)
                    {
                        Freeze fa = new Freeze(args.skill, sActor, 10000, 30);
                        SkillHandler.ApplyAddition(sActor, fa);
                        Stone s = new Stone(args.skill, sActor, 10000, 30);
                        SkillHandler.ApplyAddition(sActor, s);
                        DefUp su = new DefUp(args.skill, sActor, 10000, 30);
                        SkillHandler.ApplyAddition(sActor, su);
                        MDefUp dsu = new MDefUp(args.skill, sActor, 10000, 30);
                        SkillHandler.ApplyAddition(sActor, dsu);
                        DEFDOWN sd = new DEFDOWN(args.skill, sActor, 10000, 30);
                        SkillHandler.ApplyAddition(sActor, sd);
                        MDEFDOWN msd = new MDEFDOWN(args.skill, sActor, 10000, 30);
                        SkillHandler.ApplyAddition(sActor, msd);

                        List<string> name = new List<string>();
                        foreach (var item in sActor.Status.Additions.Keys)
                        {
                            name.Add(item);
                        }
                        foreach (var item in name)
                        {
                            SkillHandler.RemoveAddition(sActor, item);
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
