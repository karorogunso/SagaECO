using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M11058000
{
    public class S11001470 : Event
    {
        public S11001470()
        {
            this.EventID = 11001470;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "要乘搭嗎？$R;" +
           "我們會帶你到達我們的島嶼♪$R;", "水先案内人");
            switch (Select(pc, "要乘搭嗎？", "", "不要前往", "前往", "把船借船借來用一下"))
            // 02/09/2015 by hoshinokanade
            /*  Say(pc, 131, "乗ってく？$R;" +
            "あたしたちの島に連れてったげる♪$R;", "水先案内人");
            switch (Select(pc, "乗ってく？", "", "行かない", "行く", "船を貸してもらう"))
            */
            {

                case 2:
                    Warp(pc, 11053000, 19, 230);
                    break;
                case 3:
                    Say(pc, 131, "……誒？其他也可以的說、$R;" +
                    "要好好地回來喔～！$R;", "水先案内人");
                    Warp(pc, 11075000, 20, 11);
                    break;
                    /* Say(pc, 131, "……え？別にいいけど、$R;" + 
                    "ちゃんと返してね～！$R;", "水先案内人");
                    Warp(pc, 11075000, 20, 11);
                    break;
                    */
            }
         




        }
    }
}