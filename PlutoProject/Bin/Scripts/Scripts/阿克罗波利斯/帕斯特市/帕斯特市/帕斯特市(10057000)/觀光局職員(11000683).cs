using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000683 : Event
    {
        public S11000683()
        {
            this.EventID = 11000683;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "欢迎光临农业国家法伊斯特$R;" +
                "需要什么帮助?$R;");
            switch (Select(pc, "要给您带路吗？", "", "不用了", "管理局", "农夫行会总部", "绿盾军本部", "商店区", "学校"))
            {
                case 2:
                    Say(pc, 131, "法伊斯特管理局在这里$R;" +
                        "$R他们会给年轻的冒险家$R;" +
                        "委托简单的事情的$R;");
                    break;
                case 3:
                    Say(pc, 131, "这里是农夫行会总部吧$R;" +
                        "不仅是农夫行会$R;" +
                        "$R也有生产系行会分会$R;" +
                        "好好利用吧！$R;");
                    Navigate(pc, 174, 100);
                    break;
                case 4:
                    Say(pc, 131, "绿盾军的本部就是那个建筑$R;" +
                        "$R绿盾军如其名$R;" +
                        "是负责这个国家防御和$R;" +
                        "城市安全的国家！$R;" +
                        "$P就连喜欢小狗的人聚在一起$R;" +
                        "他们也管的！$R;");
                    Navigate(pc, 28, 100);
                    break;
                case 5:
                    Say(pc, 131, "是酒馆和武器商区的集中地$R;" +
                        "非常繁华$R;" +
                        "带您到商店区的入口吧$R;" +
                        "请随着箭头方向走$R;");
                    Navigate(pc, 141, 103);
                    break;
                case 6:
                    Say(pc, 131, "挂着大钟的建筑物就是学校$R;" +
                        "谁都可以上学的$R;" +
                        "去看看吧！$R;");
                    Navigate(pc, 147, 119);
                    break;
            }
        }
    }
}