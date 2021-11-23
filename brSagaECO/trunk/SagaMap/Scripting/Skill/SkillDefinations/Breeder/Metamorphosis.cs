
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Network.Client;
namespace SagaMap.Skill.SkillDefinations.Breeder
{
    /// <summary>
    /// メタモルフォーゼ（メタモルフォーゼ）
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
            ActorPet pet = SkillHandler.Instance.GetPet(sActor);
            ActorPC pc = (ActorPC)sActor;
            MapClient client = MapClient.FromActorPC(pc);
            if (pc.TranceID != 0)
            {
                pc.TranceID = 0;
            }
            else
            {
                if (pet != null)
                {
                    if(!SkillHandler.Instance.IsRidePet(pet))
                    {
                        pc.TranceID = pet.BaseData.pictid;
                    }
                }
            }
            client.SendCharInfoUpdate();
        }
        #endregion
    }
}