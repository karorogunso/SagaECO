using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 光之武器：加攻击加会心力
    /// </summary>
    public class S43006 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(pc.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Holy)
                {
                    return 0;
                }
                return -2;
            }
            else
            {
                return -2;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 60000;
            int atksup = 35;
            int crisup = 20;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> targets = map.GetActorsArea(dActor, 300, true);
            ActorPC me = (ActorPC)sActor;
            foreach (var item in targets)
            {
                if (item != null)
                {
                    if (item.type == ActorType.PC)
                    {
                        ActorPC target = (ActorPC)item;
                        if ((target.Party == me.Party && me.Party != null) || (target == me))
                        {
                            SkillHandler.Instance.ShowEffectOnActor(target, 5177);
                            AtkUp buff1 = new AtkUp(args.skill, target, lifetime, atksup);
                            HitCriUp buff2 = new HitCriUp(args.skill, target, lifetime, crisup);
                            SkillHandler.ApplyAddition(target, buff1);
                            SkillHandler.ApplyAddition(target, buff2);
                        }
                    }
                }
            }
        }
        #endregion
        class WeaponHolyBuff : DefaultBuff
        {
            int type;
            Actor target;
            public WeaponHolyBuff(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int type, SkillArg arg)
                : base(skill, sActor, dActor, "WeaponHolyBuff", lifetime, 60000, type, arg)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.type = type;
                target = dActor;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
                switch (type)
                {
                    case 5://光
                        int max_atk1_add = (int)(actor.Status.max_atk1 * 0.2f);
                        if (skill.Variable.ContainsKey("光之武器最大物理攻击力上升1"))
                            skill.Variable.Remove("光之武器最大物理攻击力上升1");
                        skill.Variable.Add("光之武器最大物理攻击力上升1", max_atk1_add);
                        actor.Status.max_atk1_skill += (short)max_atk1_add;

                        int max_atk2_add = (int)(actor.Status.max_atk2 * 0.2f);
                        if (skill.Variable.ContainsKey("光之武器最大物理攻击力上升2"))
                            skill.Variable.Remove("光之武器最大物理攻击力上升2");
                        skill.Variable.Add("光之武器最大物理攻击力上升2", max_atk2_add);
                        actor.Status.max_atk2_skill += (short)max_atk2_add;

                        int max_atk3_add = (int)(actor.Status.max_atk3 * 0.2f);
                        if (skill.Variable.ContainsKey("光之武器最大物理攻击力上升3"))
                            skill.Variable.Remove("光之武器最大物理攻击力上升3");
                        skill.Variable.Add("光之武器最大物理攻击力上升3", max_atk3_add);
                        actor.Status.max_atk3_skill += (short)max_atk3_add;

                        int max_matk_add = (int)(actor.Status.max_matk * 0.2f);
                        if (skill.Variable.ContainsKey("光之武器最大魔法攻击力上升"))
                            skill.Variable.Remove("光之武器最大魔法攻击力上升");
                        skill.Variable.Add("光之武器最大魔法攻击力上升", max_matk_add);
                        actor.Status.max_matk_skill += (short)max_matk_add;

                        Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("受到了来自『光之武器』的祝福，最大攻击力上升了！");
                        break;
                    default:
                        return;
                }
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
                Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("光之武器消失了。");
                if (skill.Variable.ContainsKey("光之武器最大物理攻击力上升1"))
                {
                    actor.Status.max_atk1_skill -= (short)skill.Variable["光之武器最大物理攻击力上升1"];
                    skill.Variable.Remove("光之武器最大物理攻击力上升1");
                }
                if (skill.Variable.ContainsKey("光之武器最大物理攻击力上升2"))
                {
                    actor.Status.max_atk2_skill -= (short)skill.Variable["光之武器最大物理攻击力上升2"];
                    skill.Variable.Remove("光之武器最大物理攻击力上升2");
                }
                if (skill.Variable.ContainsKey("光之武器最大物理攻击力上升3"))
                {
                    actor.Status.max_atk3_skill -= (short)skill.Variable["光之武器最大物理攻击力上升3"];
                    skill.Variable.Remove("光之武器最大物理攻击力上升3");
                }
                if (skill.Variable.ContainsKey("光之武器最大魔法攻击力上升"))
                {
                    actor.Status.max_matk_skill -= (short)skill.Variable["光之武器最大魔法攻击力上升"];
                    skill.Variable.Remove("光之武器最大魔法攻击力上升");
                }

            }

        }
    }
}
