using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Network.Client;
namespace SagaScript.M30210000
{
    public class S910000096 : Event
    {
        public S910000096()
        {
            this.EventID = 910000096;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000096) > 0)
            {
                //给使用者的搭档经验10000 实际给予搭档经验是按值的1/10算
                if (pc.Partner != null)//先判断是否有搭档存在
                {
                    TakeItem(pc, 910000096, 1);
                    SagaMap.Manager.ExperienceManager.Instance.ApplyPartnerLvExp(pc, 100000);
                }
            }
        }
    }
}