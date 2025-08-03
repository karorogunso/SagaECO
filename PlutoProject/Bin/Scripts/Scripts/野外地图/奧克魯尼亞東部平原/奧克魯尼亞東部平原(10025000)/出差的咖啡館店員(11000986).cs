using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:出差的咖啡館店員(11000986) X:118 Y:135
namespace SagaScript.M10025000
{
    public class S11000986 : Event
    {
        public S11000986()
        {
            this.EventID = 11000986;
        }

        public override void OnEvent(ActorPC pc)
        {
            //Say(pc, 11000986, 302, "我是多麼♪$R;" +
            //                       "希望擁有屬於自己的出差咖啡館本店$R;" +
            //                       "的美麗動人的少女喔♪$R;" +
            //                       "$R要向著自己的夢想努力♪$R;", "出差的咖啡館店員");
            Say(pc, 302, "欢迎光临～～♪$R;", "咖啡馆店员");
            Say(pc, 302, "本店是阿克罗尼亚上空１０００ｍ的「天空咖啡厅」$R入场500G;", "酒馆店员");
            switch (Select(pc, "要进去吗？", "", "进去", "不进去"))
            {
                case 1:
                    if (pc.Gold >= 500)
                    {
                        pc.Gold -= 500;
                        Say(pc, 0, 131, "付500G。$R;", " ");
                        Say(pc, 302, "恩。$R;" +
                        "客人一名！$R;", "酒馆店员");
                        Warp(pc, 20081000, 11, 21);
                    }
                    else
                    {
                        Say(pc, 302, "对不起你身上没钱～～♪$R;", "酒馆店员");
                    }
                    break;
            }
        }
    }
}
