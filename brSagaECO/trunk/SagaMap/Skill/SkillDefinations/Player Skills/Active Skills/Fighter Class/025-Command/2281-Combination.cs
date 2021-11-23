
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 組合必殺（コンビネーション）
    /// </summary>
    public class Combination : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            uint ASHIBARAI_SkillID = 2136, UPPERCUT_SkillID = 2359, TACKLE_SkillID = 2137;
            ActorPC sActorPC = (ActorPC)sActor;
            if (sActorPC.Skills2.ContainsKey(ASHIBARAI_SkillID))
            {
                args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(ASHIBARAI_SkillID, sActorPC.Skills2[ASHIBARAI_SkillID].Level, 0));
            }
            else if (sActorPC.SkillsReserve.ContainsKey(ASHIBARAI_SkillID))
            {
                args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(ASHIBARAI_SkillID, sActorPC.SkillsReserve[ASHIBARAI_SkillID].Level, 0));
                
            }
            if (sActorPC.Skills2.ContainsKey(UPPERCUT_SkillID))
            {
                args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(UPPERCUT_SkillID, sActorPC.Skills2[UPPERCUT_SkillID].Level, 0));
            }
            else if (sActorPC.SkillsReserve.ContainsKey(UPPERCUT_SkillID))
            {
                args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(UPPERCUT_SkillID, sActorPC.SkillsReserve[UPPERCUT_SkillID].Level, 0));
            }

            if (sActorPC.Skills2.ContainsKey(TACKLE_SkillID))
            {
                args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(TACKLE_SkillID, sActorPC.Skills2[TACKLE_SkillID].Level, 0));
            }
            else if (sActorPC.SkillsReserve.ContainsKey(TACKLE_SkillID))
            {
                args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(TACKLE_SkillID, sActorPC.SkillsReserve[TACKLE_SkillID].Level, 0));
            }

        }
        #endregion
    }
}