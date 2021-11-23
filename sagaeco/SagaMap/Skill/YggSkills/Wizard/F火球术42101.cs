using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 火球术：3×3火属性魔法单段攻击，附带灼烧
    /// </summary>
    public class S42101 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 3.5f;
            float factor_burn = 2f;

            if(sActor.type == ActorType.PARTNER)
            {
                factor = 15f;
                factor_burn = 2f;
            }

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> targets = map.GetActorsArea(dActor, 200, true);
            List<Actor> dactors = new List<Actor>();
            SkillHandler.Instance.ShowEffectOnActor(dActor, 7917);
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    SkillHandler.Instance.ShowEffectOnActor(item, 7756);
                    if (sActor.type == ActorType.MOB && item.Status.Additions.ContainsKey("冰霜之焰") && SagaLib.Global.Random.Next(0, 100) < 10)
                    {
                        int heal = SkillHandler.Instance.CalcDamage(false, sActor, item, null, SkillHandler.DefType.MDef, Elements.Fire, 0, factor);
                        item.HP += (uint)heal;
                        if (item.HP > item.MaxHP)
                            item.HP = item.MaxHP;
                        SkillHandler.Instance.ShowVessel(item, -heal);
                        OtherAddition skill = new OtherAddition(null, item, "免疫寒冰之夜", 10000);
                        skill.OnAdditionStart += (s, e) =>
                        {
                            SkillHandler.SendSystemMessage(item, "开始免疫『寒冰之夜』的冰冻状态了。");
                            item.Buff.ShieldWater = true;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                        };
                        skill.OnAdditionEnd += (s, e) =>
                        {
                            SkillHandler.SendSystemMessage(item, "免疫『寒冰之夜』的冰冻状态解除了。");
                            item.Buff.ShieldWater = false;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                        };
                        SkillHandler.ApplyBuffAutoRenew(item, skill);
                    }
                    else
                    {
                        SkillHandler.Instance.DoDamage(false, sActor, item, null, SkillHandler.DefType.MDef, Elements.Fire, 50, factor);
                        int brunDamage = SkillHandler.Instance.CalcDamage(false, sActor, item, null, SkillHandler.DefType.MDef, Elements.Fire, 50, factor_burn);
                        Burning burn = new Burning(args.skill, item, 6000, brunDamage);
                        SkillHandler.ApplyAddition(item, burn);
                        if(sActor.type == ActorType.PARTNER && ((ActorPartner)sActor).rebirth && SagaLib.Global.Random.Next(0, 100) < 10)
                        {
                            Stun stun = new Stun(null, item, 3000);
                            SkillHandler.ApplyAddition(item, stun);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
