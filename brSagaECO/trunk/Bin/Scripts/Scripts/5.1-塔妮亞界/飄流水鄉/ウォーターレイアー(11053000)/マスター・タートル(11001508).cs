using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001508 : Event
    {
        public S11001508()
        {
            this.EventID = 11001508;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 190, "呼呼呼。$R;" +
            "美人魚醬~`那個♪$R;", "マスター・タートル");

            Say(pc, 190, "哦哦！$R;", "マスター・タートル");

            Say(pc, 0, 190, "（…咕～、咕嚕嚕）$R;", " ");

            Say(pc, 190, "哦哦$R;" +
            "感覺肚子餓了呢……。$R;", "マスター・タートル");

            Say(pc, 190, "香噴噴的『肉まん』$R;" +
            "想要吃……。$R;", "マスター・タートル");
        }


    }
}


