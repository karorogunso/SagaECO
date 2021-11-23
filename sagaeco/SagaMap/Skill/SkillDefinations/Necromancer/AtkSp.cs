
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
            if (sActor.HP > sActor.MaxHP * 0.35f)
                return 0;
            else return -2;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.HP > sActor.MaxHP * 0.35f)
            {
                uint deductHP = (uint)(sActor.MaxHP * 0.35f);
                sActor.HP -= deductHP;
                uint addSP = (uint)(sActor.MaxSP * 0.5f);
                sActor.SP += addSP;
                if (sActor.SP > sActor.MaxSP)
                    sActor.SP = sActor.MaxSP;
                SkillHandler.Instance.ShowVessel(sActor, (int)deductHP, 0, (int)-addSP);
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            }
        }
        #endregion
    }
}