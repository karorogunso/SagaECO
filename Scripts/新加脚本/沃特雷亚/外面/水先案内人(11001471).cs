using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001471 : Event
    {
        public S11001471()
        {
            this.EventID = 11001471;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "乗ってく？$R;" +
            "天まで続く塔の島に連れてったげる♪$R;", "水先案内人");
            switch (Select(pc, "乗ってく？", "", "行かない", "行く", "船を貸してもらう"))
            {

                case 2:
                    Warp(pc, 11058000, 125, 244);
                    break;
                case 3:
                    Say(pc, 131, "……え？別にいいけど、$R;" +
                    "ちゃんと返してね～！$R;", "水先案内人");
                    Warp(pc, 11075000, 20, 11);
                    break;

            }
        }
    }
}


    



