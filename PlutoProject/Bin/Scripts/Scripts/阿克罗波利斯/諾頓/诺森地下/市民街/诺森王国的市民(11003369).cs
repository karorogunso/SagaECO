using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:銀(11001172) X:132 Y:97
namespace SagaScript.M80061062
{
    public class S11003369 : Event
    {
        public S11003369()
        {
            this.EventID = 11003369;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11003368, 131, "喂喂$R;", "诺森王国的市民");
            Say(pc, 11003369, 131, "嗯?$R;", "诺森王国的市民");
            Say(pc, 11003368, 131, "总觉得最近是不是很少见到一般人?$R;", "诺森王国的市民");
            Say(pc, 11003369, 131, "一般人?我们也是一般人吧?$R;", "诺森王国的市民");
            Say(pc, 11003368, 131, "不对不对,是有身体的人啊$R;", "诺森王国的市民");
            Say(pc, 11003369, 131, "啊啊,这样啊$R;" +
                                    "$R;" +
                                    "但是,若是向那些人告知我们存在$R;" +
                                    "的话,不是就有解决这个事态的人$R;" +
                                    "出现的可能吗$R;", "诺森王国的市民");
            Say(pc, 11003368, 131, "不行的,这里的存在不能被知道$R;" +
                                   "大导师大人不是这样说过吗$R;", "诺森王国的市民");
            Say(pc, 11003369, 131, "毕竟有跟上面的人交流的机会$R;" +
                                   "虽然我也觉得不是很好就是了$R;", "诺森王国的市民");
            Say(pc, 11003368, 131, "上面的人也是$R;" +
                                   "也只有一部分人知道这里的情况$R;", "诺森王国的市民");
            Say(pc, 11003368, 131, "说的也是$R;", "诺森王国的市民");
        }
    }
}