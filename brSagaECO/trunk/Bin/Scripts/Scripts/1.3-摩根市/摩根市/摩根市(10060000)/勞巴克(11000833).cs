using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000833 : Event
    {
        public S11000833()
        {
            this.EventID = 11000833;

        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000064, 361, "這是帕斯持的寵物神仙給的，$R;" +
                "發育狀態很好阿。$R;" +
                "$P摩根的氣候不好、$R;" +
                "食物也不算豐富$R;" +
                "$R如果在更好的環境下成長$R;" +
                "那該多好…$R;" +
                "$R對不起了，勞巴克$R;");
            Say(pc, 11000833, 361, "哞哞~哞哞~$R;");
        }
    }
}