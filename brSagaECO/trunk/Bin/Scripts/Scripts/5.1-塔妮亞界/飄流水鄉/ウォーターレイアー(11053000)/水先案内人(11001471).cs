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
            Say(pc, 131, "要乘搭嗎？$R;" +
            "我們會帶你到達通天塔之島♪$R;", "水先案内人");
            switch (Select(pc, "要乘搭嗎？", "", "不要前往", "前往", "把船借來用一下"))

            //
            /*Say(pc, 131, "乗ってく？$R;" +
            "天まで続く塔の島に連れてったげる♪$R;", "水先案内人");
            switch (Select(pc, "乗ってく？", "", "行かない", "行く", "船を貸してもらう"))
            */
            {

                case 2:
                    Warp(pc, 11058000, 125, 244);
                    break;
                case 3:
                    Say(pc, 131, "……誒？其他的話也可以的說、$R;" +
                                        "要好好地回來喔～！$R;", "水先案内人");
                    Warp(pc, 11075000, 18, 8);
                    break;
                    /*
                        Say(pc, 131, "……え？別にいいけど、$R;" +
                        "ちゃんと返してね～！$R;", "水先案内人");
                        Warp(pc, 11075000, 20, 11);
                        break;
                     */

            }
        }
    }
}


    



