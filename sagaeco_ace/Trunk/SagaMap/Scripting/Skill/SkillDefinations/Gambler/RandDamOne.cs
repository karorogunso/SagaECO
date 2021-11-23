
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gambler
{
    /// <summary>
    /// 強運（一か八か）
    /// </summary>
    public class RandDamOne : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.0f + 0.5f * level;
            int rate = 85 - 5 * level;
            if (SagaLib.Global.Random.Next(0, 99) < rate)
            {
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            }
            else
            {
                sActor.HP = 1;
                int lifetime = 1000 + 1000 * level;
                Confuse skill = new Confuse(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill);
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            }
        }
        #endregion
    }
}