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
    public class S910000095 : Event
    {
        public S910000095()
        {
            this.EventID = 910000095;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000095) > 0)
            {
                //给使用者的搭档经验8000 实际给予搭档经验是按值的1/10算
                if (pc.Partner != null)//先判断是否有搭档存在
                {
                    TakeItem(pc, 910000095, 1);
                    SagaMap.Manager.ExperienceManager.Instance.ApplyPartnerLvExp(pc, 80000);
                }
            }
        }
    }
}