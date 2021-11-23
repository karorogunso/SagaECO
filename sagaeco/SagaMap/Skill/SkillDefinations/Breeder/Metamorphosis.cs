
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Network.Client;
namespace SagaMap.Skill.SkillDefinations.Breeder
{
    /// <summary>
    /// 变身
    /// </summary>
    public class Metamorphosis : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPartner partner = SkillHandler.Instance.GetPartner(sActor);
            ActorPC pc = (ActorPC)sActor;
            MapClient client = MapClient.FromActorPC(pc);
            if (pc.TranceID != 0)
            {
                pc.TranceID = 0;
            }
            else
            {
                if (partner != null)
                {
                    if (partner.rebirth || pc.Account.GMLevel> 200)
                        pc.TranceID = partner.BaseData.pictid;
                    else
                    {
                        SkillHandler.SendSystemMessage(pc, "宠物尚未转生，无法变身。");
                        pc.TranceID = 0;
                    }
                }
            }
            client.SendCharInfoUpdate();
            MapClient.FromActorPC(pc).SendRange();
        }
        #endregion
    }
}