using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30020004
{
    public class S11000588 : Event
    {
        public S11000588()
        {
            this.EventID = 11000588;
        }

        public override void OnEvent(ActorPC pc)
        {

            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (Job2X_12_mask.Test(Job2X_12.收集石油))
            {
                Say(pc, 131, "古鲁杜叔叔是对汪汪非常亲切的人!$R;");
                return;
            }

            if (!Job2X_12_mask.Test(Job2X_12.轉職開始))//_3A63)
            {
                return;
            }

            if (Job2X_12_mask.Test(Job2X_12.收集黃麥粉) && !Job2X_12_mask.Test(Job2X_12.給予黃麥粉))//_3A71 && !_3A76)
            {
                Say(pc, 11000583, 131, "为什么那么慢啊!!$R;" +
                    "$R汪汪它们…汪汪它们…$R在饿着肚子呢!!$R;" +
                    "$P「巨麦粉」是制作宠物食物的时候$R必不可少的材料!$R;");
                Say(pc, 11000588, 131, "古鲁杜叔叔…为什么那么生气啊?$R平时是那么亲切...$R;");
                Say(pc, 11000583, 131, "啊…不是那个…那个…那$R嗯…嗯…$R呼…小鬼就安静点$R;" +
                    "$R嗯…?$R;");

                if (CountItem(pc, 10001100) >= 10)
                {
                    TakeItem(pc, 10001100, 10);
                    Job2X_12_mask.SetValue(Job2X_12.給予黃麥粉, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集石油, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集黃麥粉, false);
                    //_3A76 = true;
                    //_3A72 = true;
                    //_3A71 = false;
                    Say(pc, 11000583, 131, "嗯「巨麦粉」10份$R确实是拿过来了!$R;" +
                        "$R按照顾客们的订购$R准确的把商品转交$R对商人来说是最重要的事情$R;");
                    Say(pc, 11000583, 131, "那下次要转交的地方是…$R喂…不要一直发牢骚!$R;" +
                        "$P替我给艾恩萨乌斯的普兰茨$R转交10份「石油」吧$R;" +
                        "$R我稍后也会过去的…$R那就拜托了$R;");
                    return;
                }
                Say(pc, 11000583, 131, "!!$R这不是「巨麦粉」10个啊!!$R重新拿过来吧!$R;");
                return;
            }
            Say(pc, 11000583, 131, "给艾恩萨乌斯的普兰茨$R转交10份「油」吧$R;" +
                "$R拜託了$R;");
        }
    }
}