
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 燃燒的靈魂（ソウルバーン）
    /// </summary>
    public class SoulBurn : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float mpdmgpcnt = 0.25f + 0.15f * level;
            uint addMP = (uint)(dActor.MaxMP * mpdmgpcnt);
            if (dActor.MP <= addMP)
                addMP = dActor.MP;
            dActor.MP -= addMP;
            SkillHandler.Instance.ShowVessel(dActor, 0, (int)-addMP, 0);
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
    }
}