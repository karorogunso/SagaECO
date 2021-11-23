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
            Say(pc, 131, "這是在摩根島『光之塔』裡$R;" +
                "叫機幹特的魔物$R;" +
                "$P不用怕$R;" +
                "$R我已經把它改造成善良的魔物了。$R;");
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
            Say(pc, 131, "哎呀，好了，好了$R;" +
                "突然怎麼了，哪兒出現了問題？$R;" +
                "$P啊，知道了，是這裡。$R;" +
                "都是開關出了問題$R;" +
                "$R現在沒事了。$R;");
        }
    }
}