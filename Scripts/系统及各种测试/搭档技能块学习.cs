
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
    public class S90000529 : Event
    {
        public S90000529()
        {
            this.EventID = 90000529;
        }

        public override void OnEvent(ActorPC pc)
        {
            SagaDB.Partner.ActCubeData acd = SagaDB.Partner.PartnerFactory.Instance.GetCubeItemID((uint)pc.TInt["技能块ItemID"]);
            if (pc.Partner != null && CountItem(pc,(uint)pc.TInt["技能块ItemID"]) >= 1)
            {
                if (Select(pc, "确定要给搭档学习【" + acd.cubename + "】吗？", "", "是的", "算了") == 1)
                {
                    MapClient client = MapClient.FromActorPC(pc);
                    client.OnPartnerCubeLearn((uint)pc.TInt["技能块ItemID"]);
                }
            }
        }
    }
}

