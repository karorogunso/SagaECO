using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S11123 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("流光爆裂CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.ShowEffectByActor(sActor, 2501);


            OtherAddition skill2 = new OtherAddition(null, sActor, "流光爆裂CD", 120000);
            skill2.OnAdditionEnd += (s, e) =>
            {
                SkillHandler.Instance.ShowEffectOnActor(sActor, 4287);
                SkillHandler.SendSystemMessage(sActor, "『流光爆裂』已准备就绪！");
            };
            SkillHandler.ApplyAddition(sActor, skill2);



            流光爆裂 skill = new 流光爆裂(sActor, level);
            skill.Activate();

        }
        class 流光爆裂 : MultiRunTask
        {
            Map map;
            Actor caster;
            byte level;
            byte count = 0, maxcount = 7;
            float factor = 4.5f;
            float factor2 = 4f;
            public 流光爆裂(Actor actor,byte level)
            {
                caster = actor;
                this.level = level;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                period = 500;
                factor = 3f + level * 1f;
                factor2 = factor * 1.5f;
            }
            public override void CallBack()
            {
                try
                {
                    count++;
                    if (count < maxcount)
                    {
                        List<Actor> targets = map.GetActorsArea(caster, 400, false);
                        foreach (var item in targets)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                if (item.Status.Additions.ContainsKey("Stun"))
                                {
                                    int damage = SkillHandler.Instance.CalcDamage(true, caster, item, null, SkillHandler.DefType.Def, Elements.Dark, 50, factor2);
                                    damage += SkillHandler.Instance.CalcDamage(false, caster, item, null, SkillHandler.DefType.Def, Elements.Dark, 50, factor2);
                                    SkillHandler.Instance.CauseDamage(caster, item, damage);
                                    SkillHandler.Instance.ShowVessel(item, damage);
                                }
                                else
                                {
                                    int damage = SkillHandler.Instance.CalcDamage(true, caster, item, null, SkillHandler.DefType.Def, Elements.Dark, 50, factor2);
                                    damage += SkillHandler.Instance.CalcDamage(false, caster, item, null, SkillHandler.DefType.Def, Elements.Dark, 50, factor2);
                                    SkillHandler.Instance.CauseDamage(caster, item, damage);
                                    SkillHandler.Instance.ShowVessel(item, damage);
                                }
                            }
                        }
                    }
                    else
                        Deactivate();
                }
                catch (Exception ex)
                {
                    Deactivate();
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
