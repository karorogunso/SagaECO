using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30152001
{
    public class S11001079 : Event
    {
        public S11001079()
        {
            this.EventID = 11001079;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10015000) >= 1)
            {
                Say(pc, 255, "闪闪！闪闪！$R;");
                Say(pc, 11001075, 131, "对不起，$R;" +
                    "能不能给我『砂金』呢？$R;" +
                    "$R这个孩子只吃砂金。$R;" +
                    "不过沙金很贵重的，$R;");
                switch (Select(pc, "怎么办呢？", "", "不给", "给他吧"))
                {
                    case 1:
                        break;
                    case 2:
                        if (CheckInventory(pc, 10011501, 1))
                        {
                            TakeItem(pc, 10015000, 1);
                            GiveItem(pc, 10011501, 1);
                            Say(pc, 255, "来~！$R;");
                            Say(pc, 11001075, 131, "哈哈，向您表示感谢$R;" +
                                "请收下！$R;");
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 0, 131, "收到了『含有不明物体的宝石』$R;");
                            return;
                        }
                        Say(pc, 255, "吱！吱咦！！$R;");
                        Say(pc, 11001075, 131, "矿石精灵？怎么了？$R;" +
                            "$R嗯？减轻行李？$R;" +
                            "嗯，虽然不太清楚$R;" +
                            "不过还是试试看，减轻行李吧$R;");
                        break;
                }
                return;
            }
            if (pc.Marionette != null)
            {
                Say(pc, 255, "噗噗噗…$R;" +
                    "$R闪闪亮亮的漂亮粉末呀$R;");
                return;
            }
            Say(pc, 11001075, 131, "来，把嘴张开，啊，试试$R;");
            Say(pc, 255, "啐！$R;");
            Say(pc, 11001075, 131, "真不好意思…$R;" +
                "矿石精灵的婴儿食品$R味道到底是怎样的呢？$R;");
        }
    }
}