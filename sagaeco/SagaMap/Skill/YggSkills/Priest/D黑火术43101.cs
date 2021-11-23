using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 黑火：大黑火 3转版本
    /// </summary>
    public class S43101 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Dark)
                {
                    sActor.EP += 500;
                    if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                }
            }
            float factor = 5f;
            List<Actor> affected = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 500, true);
            SkillHandler.Instance.ShowEffect(map, sActor, args.x, args.y, 4471);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                    if (sActor.Status.Additions.ContainsKey("属性契约"))
                    {
                        if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Dark)
                        {
                            if (act.Darks == 1)
                            {
                                int damage = SkillHandler.Instance.CalcDamage(false, sActor, act, args, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 50, 1f);
                                Burning burn = new Burning(args.skill, act, 10000, damage);
                                SkillHandler.ApplyAddition(act, burn);
                                act.Darks = 0;
                            }
                        }
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SagaLib.Elements.Dark, factor);
        }
    }
}