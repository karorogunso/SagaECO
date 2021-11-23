using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004001
{
    public class S11000147 : Event
    {
        public S11000147()
        {
            this.EventID = 11000147;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Global.Random.Next(1, 2) == 1)
            {
                Say(pc, 11000145, 131, "看看我的肌肉，感觉如何？$R;");
                Say(pc, 11000146, 131, "您想靠您的肌肉出名啊？$R;");
                Say(pc, 11000147, 131, "哈哈哈哈哈哈！$R;");
            }
            else
            {

                Say(pc, 11000145, 131, "我们正在互相测量$R;" +
                    "对方手臂的粗细呢。$R;");
                Say(pc, 11000146, 131, "您也要量一量吗？$R;");
                Say(pc, 11000147, 131, "哈哈哈！$R;");

                switch (Select(pc, "您要继续听故事吗？", "", "听", "不听"))
                {
                    case 1:
                        Say(pc, 11000145, 131, "48!$R;");
                        Say(pc, 11000146, 131, "42.5!$R;");
                        Say(pc, 11000147, 131, "51!$R;");
                        Say(pc, 11000145, 131, "您说51？$R;");
                        Say(pc, 11000146, 131, "真的吗？$R;");
                        Say(pc, 11000147, 131, "哈哈哈！$R;");
                        Say(pc, 11000145, 131, "怎么做才可以变成那么壮呢？$R;");
                        Say(pc, 11000146, 131, "教教我吧！$R;");
                        Say(pc, 11000147, 131, "天天吃鸡蛋吧。$R;" +
                            "这样就差不多了$R;");
                        Say(pc, 11000145, 131, "就这样吗？$R;");
                        Say(pc, 11000146, 131, "原来方法这么简单啊$R;");
                        Say(pc, 11000147, 131, "呜呜呜呜~$R;" +
                            "我也可以有强壮的肌肉了，$R感动的都要哭出来了$R;");
                        break;
                    case 2:
                        break;
                }
            }
        }
    }
}