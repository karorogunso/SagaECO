using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東方海角(10018001) NPC基本信息:大地之精靈(11000935) X:43 Y:95
namespace SagaScript.M10018001
{
    public class S11000935 : Event
    {
        public S11000935()
        {
            this.EventID = 11000935;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;

            Say(pc, 11000935, 131, "您好!$R;" +
                                   "$R我是尼奥，$R;" +
                                   "是大地之精灵喔!$R;" +
                                   "$P有什么事吗?$R;", "大地之精灵");

            selection = Select(pc, "想问什么呢?", "", "什么叫「精灵」", "您在做什么?", "什么也不问");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000935, 131, "是这样吗?$R;" +
                                               "$R我是掌管「地」之力的精灵。$R;" +
                                               "$P知道「地属性」吗?$R;" +
                                               "$P这个世界存在著各种「属性」。$R;" +
                                               "$P有火/水/风/地$R;" +
                                               "还有光和暗属性唷!$R;" +
                                               "$P属性大部分都突显在地区。$R;" +
                                               "$R偶尔也在魔物、武器或防具上。$R;" +
                                               "$P炎热的地方$R;" +
                                               "「火」属性较强，$R;" +
                                               "$R寒冷的地方$R;" +
                                               "「水」属性较强$R;" +
                                               "$P颳风的地方$R;" +
                                               "「风」属性较强，$R;" +
                                               "$R绿荫的地方$R;" +
                                               "「地」属性较强。$R;" +
                                               "$P它们之间没有力量的相互关系。$R;" +
                                               "$R但是各属性有专门的职业，$R;" +
                                               "你直接去问他们吧。$R;", "大地之精灵");
                        break;

                    case 2:
                        Say(pc, 11000935, 131, "掌管属性的精灵周围，$R;" +
                                               "会有相同属性的「水晶」哦!$R;" +
                                               "$P下次来的时候找找看吧?$R;" +
                                               "$P可以利用从水晶里，$R;" +
                                               "抽取出来的「召唤石」，$R;" +
                                               "来灌注「某种」属性!$R;" +
                                               "$P需要的话，欢迎随时过来。$R;" +
                                               "$P掌管其他属性的精灵，$R;" +
                                               "也拥有同样的能力喔!$R;", "大地之精灵");
                        break;

                    case 3:
                        Say(pc, 11000935, 131, "是吗?$R;" +
                                               "需要的话，随时过来吧!$R;", "大地之精灵");
                        return;
                }

                selection = Select(pc, "想问什么呢?", "", "什么叫「精灵」", "您在做什么?", "什么也不问");
            }
        }
    }
}
