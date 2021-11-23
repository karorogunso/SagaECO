using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10018100
{
    public class S11000414 : Event
    {
        public S11000414()
        {
            this.EventID = 11000414;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "哎呀…這裡是$R;" +
                "帕斯特市的入口$R;" +
                "$R爲了警備首先在這裡做入境審查$R;" +
                "什麽…雖然是形式性審查$R;" +
                "$P審查在裡面做$R;");
        }
        void 虎眼任务(ActorPC pc)
        {

            //EVT1100041401
            Say(pc, 131, "哎呀…辛苦了$R;" +
                "這一帶有「咕咕」像「古代咕咕雞」$R;" +
                "這樣的魔物在棲息呢$R;" +
                "$R不能被外表和名字騙阿$R;" +
                "不小心的話就出大事的!$R;" +
                "$P我以前也…$R;" +
                "阿!什麽都沒有啦!$R;" +
                "$R反正小心使禁物!$R;" +
                "保持警覺心是很重要的…$R;" +
                "$P我能說得就這麼多了$R;");
            //#6
            //_7a87 = true;
            //GOTO EVT1100041402
            //EVENTEND

            //EVT1100041402
            Say(pc, 131, "下一次要到軍艦島喔$R;" +
                "在飛空庭下面的家伙會說的$R;");
            //EVENTEND

        }
    }
}