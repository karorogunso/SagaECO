using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:銀(11001172) X:132 Y:97
namespace SagaScript.M80061060
{
    public class S11003370 : Event
    {
        public S11003370()
        {
            this.EventID = 11003370;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11003370, 131, "哈,取回了原来身体的而兴奋到$R;" +
                                    "强行离开这里的朋友们现在在做什么呢?$R;"+
                                    "$R;"+
                                    "强行出去的话,听说精神崩坏,无法$R;" +
                                    "保护自己什么的...$R;", "诺森王国的市民");
        }
    }
}