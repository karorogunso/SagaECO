using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S42002 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("属性契约"))
            {
                return 0;
            }
            else
            {
                return -2;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
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
                            if (me.Status.Additions.ContainsKey("属性契约"))
                            {
                                switch (((OtherAddition)(me.Status.Additions["属性契约"])).Variable["属性契约"])
                                {
                                    case 1://火
                                        ShieldFire sf = new ShieldFire(args.skill, target, 45000, 20);
                                        SkillHandler.ApplyAddition(target, sf);
                                        break;
                                    case 2://水
                                        ShieldWater sw = new ShieldWater(args.skill, target, 45000, 20);
                                        SkillHandler.ApplyAddition(target, sw);
                                        break;
                                    case 3://风
                                        ShieldWind swi = new ShieldWind(args.skill, target, 45000, 20);
                                        SkillHandler.ApplyAddition(target, swi);
                                        break;
                                    case 4://地
                                        ShieldEarth se = new ShieldEarth(args.skill, target, 45000, 20);
                                        SkillHandler.ApplyAddition(target, se);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
