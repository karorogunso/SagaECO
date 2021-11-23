using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001064 : Event
    {
        public S11001064()
        {
            this.EventID = 11001064;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这是在摩戈岛『光之塔』里$R;" +
                "叫机干特的魔物$R;" +
                "$P不用怕$R;" +
                "$R我已经把它改造成善良的魔物了。$R;");
            ShowEffect(pc, 12001126, 4539);
            Wait(pc, 1000);
            ShowEffect(pc, 147, 21, 8047);
            Wait(pc, 200);
            ShowEffect(pc, 147, 20, 8047);
            Wait(pc, 200);
            ShowEffect(pc, 147, 19, 8047);
            Wait(pc, 200);
            ShowEffect(pc, 147, 18, 8047);
            Wait(pc, 200);
            ShowEffect(pc, 147, 17, 8047);
            Wait(pc, 200);
            ShowEffect(pc, 147, 18, 8047);
            Wait(pc, 200);
            ShowEffect(pc, 147, 19, 8047);
            Wait(pc, 200);
            ShowEffect(pc, 147, 20, 8047);
            Wait(pc, 200);
            ShowEffect(pc, 147, 21, 8047);
            Wait(pc, 200);
            ShowEffect(pc, 147, 20, 8047);
            Wait(pc, 1000);
            ShowEffect(pc, 11001064, 4539);
            Say(pc, 131, "哎呀，糟了，糟了$R;" +
                "突然怎么了，哪儿出现了问题？$R;" +
                "$P啊，知道了，是这里。$R;" +
                "都是开关出了问题$R;" +
                "$R现在没事了。$R;");
        }
    }
}