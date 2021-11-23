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
    public class S910000094 : Event
    {
        public S910000094()
        {
            this.EventID = 910000094;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000094) > 0)
            {
                //给使用者的搭档经验6000 实际给予搭档经验是按值的1/10算
                if (pc.Partner != null)//先判断是否有搭档存在
                {
                    TakeItem(pc, 910000094, 1);
                    SagaMap.Manager.ExperienceManager.Instance.ApplyPartnerLvExp(pc, 60000);
                }
            }
        }
    }
}