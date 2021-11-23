using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004001
{
    public class S11000146 : Event
    {
        public S11000146()
        {
            this.EventID = 11000146;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Global.Random.Next(1, 2) == 1)
            {
                Say(pc, 11000145, 131, "看看我的肌肉，感覺如何？$R;");
                Say(pc, 11000146, 131, "您想靠您的肌肉出名啊？$R;");
                Say(pc, 11000147, 131, "哈哈哈哈哈哈！$R;");
            }
            else
            {

                Say(pc, 11000145, 131, "我們正在互相測量$R;" +
                    "對方手臂的粗細呢。$R;");
                Say(pc, 11000146, 131, "您也要量一量嗎？$R;");
                Say(pc, 11000147, 131, "哈哈哈！$R;");
                switch (Select(pc, "您要繼續聽故事嗎？", "", "聽", "不聽"))
                {
                    case 1:
                        Say(pc, 11000145, 131, "48!$R;");
                        Say(pc, 11000146, 131, "42.5!$R;");
                        Say(pc, 11000147, 131, "51!$R;");
                        Say(pc, 11000145, 131, "您說51？$R;");
                        Say(pc, 11000146, 131, "真的嗎？$R;");
                        Say(pc, 11000147, 131, "哈哈哈！$R;");
                        Say(pc, 11000145, 131, "怎麼做才可以變成那麼壯呢？$R;");
                        Say(pc, 11000146, 131, "教教我吧！$R;");
                        Say(pc, 11000147, 131, "天天吃雞蛋吧。$R;" +
                            "這樣就差不多了$R;");
                        Say(pc, 11000145, 131, "就這樣嗎？$R;");
                        Say(pc, 11000146, 131, "原來方法這麼簡單啊$R;");
                        Say(pc, 11000147, 131, "嗚嗚嗚嗚~$R;" +
                            "我也可以有強壯的肌肉了，$R感動的都要哭出來了$R;");
                        break;
                    case 2:
                        break;
                }
            }
        }
    }
}