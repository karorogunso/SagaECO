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

            Say(pc, 131, "哎呀…这里是$R;" +
                "法伊斯特的入口$R;" +
                "$R为了警备首先在这里做入境审查$R;" +
                "什么…虽然是形式性审查$R;" +
                "$P审查在里面做$R;");
        }
        void 虎眼任务(ActorPC pc)
        {

            //EVT1100041401
            Say(pc, 131, "哎呀…辛苦了$R;" +
                "这一带有「咕咕」像「蛇鸡」$R;" +
                "这样的魔物在栖息呢$R;" +
                "$R不能被外表和名字骗阿$R;" +
                "不小心的话就出大事的!$R;" +
                "$P我以前也…$R;" +
                "啊!什么都没有啦!$R;" +
                "$R反正千万别大意!$R;" +
                "保持警觉心是很重要的…$R;" +
                "$P我能说得就这么多了$R;");
            //#6
            //_7a87 = true;
            //GOTO EVT1100041402
            //EVENTEND

            //EVT1100041402
            Say(pc, 131, "下一次要到军舰岛喔$R;" +
                "在飞空庭下面的家伙会说的$R;");
            //EVENTEND

        }
    }
}