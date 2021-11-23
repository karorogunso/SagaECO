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
    public class S11000856 : Event
    {
        public S11000856()
        {
            this.EventID = 11000856;

        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "不好意思$R;" +
                "問您一件事情行嗎？$R;" +
                "$R到「光之塔」的飛空庭，$R;" +
                "是在這兒乘坐嗎？$R;");
            switch (Select(pc, "到「光之塔」的飛空庭，$R是在這兒乘坐嗎？", "", "是的", "不是"))
            {
                case 1:
                    Say(pc, 131, "原來如此！$R;" +
                        "那些傢伙們到底在幹什麼呢？$R;" +
                        "$R讓人等這麼久！$R;" +
                        "$P哎呀…謝謝您告訴我呀$R;");
                    break;
                case 2:
                    Say(pc, 131, "是嗎？真的嗎？$R;" +
                        "哎呀…我好像走錯地方了…$R;" +
                        "$R哎呀…怎麼辦才好呢$R;");
                    break;
            }
        }
    }
}