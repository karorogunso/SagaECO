
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 燃燒的精神（スピリットバーン）
    /// </summary>
    public class AtkSp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float[] factors = {0f, 0.4f, 0.55f, 0.7f, 0.85f, 1f };
            float factor = factors[level];
            uint SP_Spend = (uint)(dActor.MaxSP * factor);
            if (SP_Spend > dActor.SP)
            {
                dActor.SP = 0;
            }
            else
            {
                dActor.SP -= SP_Spend;
            }
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, dActor, true);
        }
        #endregion
    }
}