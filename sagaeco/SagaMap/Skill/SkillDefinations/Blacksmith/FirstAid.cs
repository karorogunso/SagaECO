
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    /// <summary>
    /// 緊急治療（ファーストエイド）
    /// </summary>
    public class FirstAid : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            uint hp_recovery = (uint)(dActor.MaxHP * (0.06f + 0.04f * level));
            if (dActor.HP + hp_recovery <= dActor.MaxHP)
            {
                if (!dActor.Status.Additions.ContainsKey("Sacrifice"))
                dActor.HP += hp_recovery;
            }
            else
            {
                dActor.HP = dActor.MaxHP;
            }
            args.affectedActors.Add(dActor);
            args.Init();
            if (args.flag.Count > 0)
            {
                args.flag[0] |= SagaLib.AttackFlag.HP_HEAL | SagaLib.AttackFlag.NO_DAMAGE ;
            }
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, dActor, true);
        }
        #endregion
    }
}