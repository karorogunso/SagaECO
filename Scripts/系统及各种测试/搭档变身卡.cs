
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaMap.Network.Client;
using SagaMap.Skill;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000106 : Event
    {
        public S910000106()
        {
            this.EventID = 910000106;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000106) >= 1)
            {
                ActorPartner partner = SkillHandler.Instance.GetPartner(pc);
                MapClient client = MapClient.FromActorPC(pc);
                if(partner == null)
                {
                    Say(pc, 0, "要装备搭档才能使用。");
                }
                if (pc.TranceID != 0)
                {
                    pc.TranceID = 0;
                }
                else
                {
                    if (partner != null)
                    {
                        pc.TranceID = partner.BaseData.pictid;
                        TakeItem(pc, 910000106, 1);
                        SagaMap.Skill.Additions.Global.OtherAddition skill = new SagaMap.Skill.Additions.Global.OtherAddition(null, pc, "搭档变身BUFF", 1800000);
                        skill.OnAdditionEnd += (s, e) =>
                        {
                            pc.TranceID = 0;
                            client.SendCharInfoUpdate();
                        };
                        SkillHandler.ApplyAddition(pc, skill);
                    }
                }
                client.SendCharInfoUpdate();
            }
        }
    }
}

