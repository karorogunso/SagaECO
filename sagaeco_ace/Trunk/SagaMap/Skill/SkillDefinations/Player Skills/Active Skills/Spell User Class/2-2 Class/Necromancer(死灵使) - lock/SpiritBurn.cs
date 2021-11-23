
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
    public class SpiritBurn : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float spdmgpcnt = 0.25f + 0.15f * level;
            uint addSP = (uint)(dActor.MaxSP * spdmgpcnt);
            if (dActor.SP <= addSP)
                addSP = dActor.SP;
            dActor.SP -= addSP;
            SkillHandler.Instance.ShowVessel(dActor, 0, 0, (int)-addSP);
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        #endregion
    }
}