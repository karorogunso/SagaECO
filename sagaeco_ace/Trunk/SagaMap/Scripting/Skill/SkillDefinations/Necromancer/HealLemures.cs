
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 治癒死靈（ネクロヒーリング）
    /// </summary>
    public class HealLemures : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            uint[] HP_add={0,550,880,1100,1320,1540};
            if (sActor.Status.Additions.ContainsKey("SummobLemures"))
            {
                SagaMap.Skill.SkillDefinations.Necromancer.SummobLemures.SummobLemuresBuff skill = (SagaMap.Skill.SkillDefinations.Necromancer.SummobLemures.SummobLemuresBuff)sActor.Status.Additions["SummobLemures"];
                if (skill.mob != null)
                {
                    args.affectedActors.Add(skill.mob);
                    skill.mob.HP += HP_add[level];
                    if (skill.mob.HP > skill.mob.MaxHP)
                    {
                        skill.mob.HP = skill.mob.MaxHP;
                    }
                    args.Init();
                    args.flag[0] |= SagaLib.AttackFlag.HP_HEAL | SagaLib.AttackFlag.NO_DAMAGE;

                    Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, skill.mob, true);
                }
            }
        }
        #endregion
    }
}