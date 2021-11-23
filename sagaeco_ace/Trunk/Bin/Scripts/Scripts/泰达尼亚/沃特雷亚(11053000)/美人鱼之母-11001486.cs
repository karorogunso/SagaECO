using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001486-美人鱼之母- X:133 Y:152
namespace SagaScript.M11053000
{
    public class S11001486 : Event
    {
    public S11001486()
        {
            this.EventID = 11001486;
        }


        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 50067500) >= 1)
            {
                Say(pc, 0, "你那个是..!$R;"+
                            "珍珠脚链!可以送给我吗$R", "美人鱼之母");
                switch (Select(pc, "要送给她吗?", "", "才不愿意", "送给她吧"))
                {
                    case 1:
                        Say(pc, 0, "小气~$R;", "美人鱼之母");
                        break;
                    case 2:
                        Say(pc, 0, "哇!$R;"+
                                    "谢谢!这是我的谢礼!$R;", "美人鱼之母");
                        TakeItem(pc, 50067500, 1);
                        GiveItem(pc, 10061400, 1);
                        break;
                }
                
            }
            if (CountItem(pc, 10061702) >= 1&&CountItem(pc, 10061703) >= 1&&CountItem(pc, 10061750) >= 1)
            {
                Say(pc, 0, "这些贝壳...$R;" +
                           "可以给我吗..这里..$R" +
                           "有你需要的东西..$R", "美人鱼之母");
                switch (Select(pc, "要送给她吗?", "", "才不愿意", "送给她吧"))
                {
                    case 1:
                        Say(pc, 0, "小气~$R;", "美人鱼之母");
                        break;
                    case 2:
                        Say(pc, 0, "....谢谢$R;", "美人鱼之母");
                        TakeItem(pc, 10061702, 1);
                        TakeItem(pc, 10061703, 1);
                        TakeItem(pc, 10061750, 1);
                        GiveItem(pc, 30001600, 1);
                        break;
                }
            }


            //Say(pc, 0, "我们是不能和人类交配的!$R;");
            Say(pc, 0, "好想要珍珠脚链!$R;", "美人鱼之母");
        }
    }
}
