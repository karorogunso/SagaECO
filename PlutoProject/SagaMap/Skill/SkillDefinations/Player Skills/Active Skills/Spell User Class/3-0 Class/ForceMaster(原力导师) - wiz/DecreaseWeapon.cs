using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    public class DecreaseWeapon : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if(args.skill.Level==2|| args.skill.Level == 4)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
                {
                    return 0;
                }
                return -12;
            }
            else
            {
                if (!SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
                {
                    if (dActor.ActorID == sActor.ActorID)
                        return 0;
                    else
                    {
                        if(dActor.type == ActorType.PC && sActor.type==ActorType.PC)
                        {
                            ActorPC spc = (ActorPC)sActor;
                            ActorPC dpc = (ActorPC)dActor;
                            if(spc.Party!=null&& dpc.Party != null)
                            {
                                if(dpc.Buff.Dead)
                                {
                                    return -11;
                                }
                                else if (dpc.PossessionTarget != 0)
                                {
                                    return -4;
                                }
                                else if (spc.Party.ID== dpc.Party.ID)
                                {
                                    return 0;
                                }
                            }
                            return -12;
                        }
                        return -12;
                    }
                }
                return -12;
            }
            return -12;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 40000 + 40000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DecreaseWeapon", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
            if(dActor.ActorID==sActor.ActorID)
            {
                Map map= map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                EffectArg arg2 = new EffectArg();
                arg2.effectID = 5137;
                arg2.actorID = sActor.ActorID;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, sActor, true);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.WeaponNatureElementUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.WeaponNatureElementUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
