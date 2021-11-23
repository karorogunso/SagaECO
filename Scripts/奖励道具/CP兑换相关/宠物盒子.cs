
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000097 : Event
    {
        public S910000097()
        {
            this.EventID = 910000097;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(CountItem(pc, 910000097) > 0)
            {
                PlaySound(pc, 2040, false, 100, 50);

                List<uint> ids = SagaDB.Partner.PartnerFactory.Instance.RankBPets;
                int ran = Global.Random.Next(0, 1000);
                if(ran < 20)
                    ids = SagaDB.Partner.PartnerFactory.Instance.RankSPets;
                if (ran < 120)
                    ids = SagaDB.Partner.PartnerFactory.Instance.RankAPets;
                if (ran < 2000)
                    ids = SagaDB.Partner.PartnerFactory.Instance.RankBPets;
                if (ids.Count >= 1)
                {
                    uint id = ids[Global.Random.Next(0, ids.Count - 1)];
                    TakeItem(pc, 910000097, 1);
                    GiveItem(pc, id, 1);
                }
            }
        }
    }
}

