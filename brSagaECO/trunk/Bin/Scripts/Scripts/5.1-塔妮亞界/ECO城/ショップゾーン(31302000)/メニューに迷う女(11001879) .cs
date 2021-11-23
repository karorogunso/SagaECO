using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001879 : Event
    {
        public S11001879()
        {
            this.EventID = 11001879;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "う～ん、どれも捨てがたい$R;" +
            "メニューねっ！$R;" +
            "$R全部食べてみたいけど$R;" +
            "一人じゃ食べきれないし……。$R;" +
            "$Rう～ん……。$R;", "メニューに迷う女");
}
}

        
    }


