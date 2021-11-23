using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations
{
    class S20003 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            雷鸣怒号 curse = new 雷鸣怒号(sActor, dActor, level);
            curse.Activate();
        }
        class 雷鸣怒号 : MultiRunTask
        {
            Actor sActor;//攻击者  
            Actor dActor;//目标
            byte count = 0;
            float factor1;
            float factor2;
            float factor3;
            Map map;
            public 雷鸣怒号(Actor sActor, Actor dActor,byte level)
            {
                this.sActor = sActor;
                this.dActor = dActor;
                dueTime = 0;
                period = 1000;
                factor1 = 2f + 0.5f * level;
                factor2 = 2f + 0.5f * level;
                factor3 = 3f + 1f * level;
                map = SkillHandler.GetActorMap(sActor);
            }
            public override void CallBack()
            {
                try
                {
                    if (count > 2)
                        Deactivate();

                    //第一次伤害
                    if (count == 0)
                        SkillHandler.Instance.DoDamage(false, sActor, dActor, null, SkillHandler.DefType.MDef, Elements.Wind, 50, factor1);

                    //第二次伤害
                    if (count == 1)
                    { 
                        List<Actor> targets = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, dActor, 100, true);
                        foreach (var item in targets)
                            SkillHandler.Instance.DoDamage(false, sActor, dActor, null, SkillHandler.DefType.MDef, Elements.Wind, 50, factor2);
                    }

                    //第三次伤害
                    if (count == 2)
                    {
                        List<Actor> targets = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, dActor, 200, true);
                        foreach (var item in targets)
                        {
                            SkillHandler.Instance.DoDamage(false, sActor, dActor, null, SkillHandler.DefType.MDef, Elements.Wind, 50, factor3);

                            //几率麻痹
                            if (SkillHandler.Instance.CanAdditionApply(sActor, item, SkillHandler.DefaultAdditions.Paralyse, 10))
                            {
                                Paralyse skill = new Paralyse(null, item, 5000, 0);
                                SkillHandler.ApplyBuffAutoRenew(item, skill);
                            }
                        }
                        Deactivate();
                    }
                    count++;
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
            }
        }
    }
}
