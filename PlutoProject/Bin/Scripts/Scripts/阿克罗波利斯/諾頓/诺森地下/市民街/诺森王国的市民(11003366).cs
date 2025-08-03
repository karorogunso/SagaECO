using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:銀(11001172) X:132 Y:97
namespace SagaScript.M80061062
{
    public class S11003366 : Event
    {
        public S11003366()
        {
            this.EventID = 11003366;
        }

        public override void OnEvent(ActorPC pc)
        {
            int a = 0;
            a = Global.Random.Next(1, 2);
            switch(a)
            {
                case 1:
                    Say(pc, 11003366, 131, "哎..哎哎!?$R;"+
                                   "你好像不是诺森的住民$R;" +
                                   "为什么会在这里啊?$R;", "诺森王国的市民");
                    Say(pc, 11003366, 131, "但是...嘛...$R;" +
                                   "难得都来到这种地方了$R;" +
                                   "去市民街看看吧$R;" +
                                   "$R;"+
                                   "虽然有点冷就是了$R;", "诺森王国的市民");
                    break;
                case 2:
                    Say(pc, 11003366, 131, "欢迎光临!诺森的市民街$R;"+
                                           "$R;"+
                                           "尽管都被冰冻就是了$R;" +
                                           "请慢慢参观吧$R;", "诺森王国的市民");
                    Say(pc, 11003366, 131, "什么$R;" +
                                           "身体很奇怪?$R;" +
                                           "这只是小事$R;" +
                                           "太在意的话是没法在这里生活的$R;", "诺森王国的市民");
                    break;
            }
            
        }
    }
}