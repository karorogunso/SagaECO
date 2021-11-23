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
    /// 圣母之祈福：加防御加抗性，加光防御属性 抗性未实装
    /// </summary>
    public class S43005:ISkill
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
            int defsup = 15;
            int shield = 20;
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
                            SkillHandler.Instance.ShowEffectOnActor(target, 5178);
                            DefUp buff1 = new DefUp(args.skill, target, lifetime, defsup);
                            MDefUp buff2 = new MDefUp(args.skill, target, lifetime, defsup);
                            ShieldHoly buff3 = new ShieldHoly(args.skill, target, lifetime, shield);
                            SkillHandler.ApplyAddition(dActor, buff1);
                            SkillHandler.ApplyAddition(dActor, buff2);
                            SkillHandler.ApplyAddition(dActor, buff3);
                        }
                    }
                }
            }
        }
        #endregion
        class ShieldHolyBuff : DefaultBuff
        {
            int type;
            Actor target;
            public ShieldHolyBuff(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int type, SkillArg arg)
                : base(skill, sActor, dActor, "ShieldHolyBuff", lifetime, 60000, type, arg)
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
                        int def_add = (int)(actor.Status.def_bs * (0.15f));
                        if (skill.Variable.ContainsKey("圣母之祈福物理防御"))
                            skill.Variable.Remove("圣母之祈福物理防御");
                        skill.Variable.Add("圣母之祈福物理防御", def_add);
                        actor.Status.def_skill += (short)def_add;

                        int mdef_add = (int)(actor.Status.mdef_bs * (0.15f));
                        if (skill.Variable.ContainsKey("圣母之祈福魔法防御"))
                            skill.Variable.Remove("圣母之祈福魔法防御");
                        skill.Variable.Add("圣母之祈福魔法防御", mdef_add);
                        actor.Status.mdef_skill += (short)mdef_add;

                        Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("受到了来自『圣母之祈福』的祝福，防御力上升了！");
                        break;
                    default:
                        return;
                }
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
                Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("圣母之祈福消失了。");
                if (skill.Variable.ContainsKey("圣母之祈福物理防御"))
                {
                    actor.Status.def_skill -= (short)skill.Variable["圣母之祈福物理防御"];
                    skill.Variable.Remove("圣母之祈福物理防御");
                }
                if (skill.Variable.ContainsKey("圣母之祈福魔法防御"))
                {
                    actor.Status.mdef_skill -= (short)skill.Variable["圣母之祈福魔法防御"];
                    skill.Variable.Remove("圣母之祈福魔法防御");
                }
            }

        }
    }
}
