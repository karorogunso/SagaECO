using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004001
{
    public class S11000140 : Event
    {
        public S11000140()
        {
            this.EventID = 11000140;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Global.Random.Next(1,2) == 1)
            Say(pc, 11000140, 131, "這是瑪歐斯的聚集地$R;" +
                "$R是沒有人知道的閉鎖空間唷。$R;");

            Say(pc, 11000140, 131, "想找長老？$R;" +
                "$R他在裡面，您找他有什麼事嗎？$R;");
        }
    }
}