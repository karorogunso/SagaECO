using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:銀(11001172) X:132 Y:97
namespace SagaScript.M80061062
{
    public class S11003367 : Event
    {
        public S11003367()
        {
            this.EventID = 11003367;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11003367, 131, "完全搞不清楚情况就莫名其妙变成$R;" +
                                   "这个样子了,意识很清晰,有一种不可思议$R;" +
                                   "的感觉$R;" +
                                   "$R;" +
                                   "但...麻烦的是不知道为何无法前往上层阶梯?$R;", "诺森王国的市民");
        }
    }
}