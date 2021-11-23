using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S14032 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.MP == pc.MaxMP) return -12;
            if (pc.Status.Additions.ContainsKey("魔力引导CD"))
            {
                Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("『魔力引导』冷却剩余" + (((OtherAddition)pc.Status.Additions["魔力引导CD"]).RestLifeTime / 1000).ToString() + "秒");
                return -30;
            }
            
            SkillHandler.Instance.ShowEffectOnActor(pc, 4297);
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int c = (int)(sActor.MaxMP - sActor.MP) / 3;
            int mp = (int)(sActor.MaxMP - sActor.MP);
            SkillHandler.Instance.ShowVessel(sActor, 0, -mp);
            sActor.MP = sActor.MaxMP;
            if (sActor.type==ActorType.PC)
            {
                ActorPC Me = sActor as ActorPC;
                if (Me.Status.Additions.ContainsKey("魔法少女") && Me.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) && Me.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
                    c = 0;
            }
            
            if (c > 0)
                sActor.EP += (uint)c;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            SkillHandler.Instance.ShowEffectOnActor(sActor, 5083);

            OtherAddition cd = new OtherAddition(null, sActor, "魔力引导CD", 60000);
            cd.OnAdditionEnd += (s, e) =>
            {
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("『魔力引导』冷却完毕。");
            };
            SkillHandler.ApplyAddition(sActor, cd);
        }
        #endregion
    }
}
