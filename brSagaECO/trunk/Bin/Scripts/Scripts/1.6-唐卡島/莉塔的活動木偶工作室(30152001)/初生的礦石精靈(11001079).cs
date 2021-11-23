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
                Say(pc, 255, "閃閃！閃閃！$R;");
                Say(pc, 11001075, 131, "對不起，$R;" +
                    "能不能給我『沙金』呢？$R;" +
                    "$R這個孩子只吃沙金。$R;" +
                    "不過沙金很貴重的，$R;");
                switch (Select(pc, "怎麼辦呢？", "", "不給", "給他吧"))
                {
                    case 1:
                        break;
                    case 2:
                        if (CheckInventory(pc, 10011501, 1))
                        {
                            TakeItem(pc, 10015000, 1);
                            GiveItem(pc, 10011501, 1);
                            Say(pc, 255, "來~！$R;");
                            Say(pc, 11001075, 131, "哈哈，向您表示感謝$R;" +
                                "請收下！$R;");
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 0, 131, "收到了『含有不明物體的寶石』$R;");
                            return;
                        }
                        Say(pc, 255, "吱！吱咦！！$R;");
                        Say(pc, 11001075, 131, "綠礦石精靈？怎麼了？$R;" +
                            "$R嗯？減輕行李？$R;" +
                            "嗯，雖然不太清楚$R;" +
                            "不過還是試試看，減輕行李吧$R;");
                        break;
                }
                return;
            }
            if (pc.Marionette != null)
            {
                Say(pc, 255, "噗噗噗…$R;" +
                    "$R閃閃亮亮的漂亮粉末呀$R;");
                return;
            }
            Say(pc, 11001075, 131, "來，把嘴張開，啊，試試$R;");
            Say(pc, 255, "啐！$R;");
            Say(pc, 11001075, 131, "真不好意思…$R;" +
                "綠礦石精靈的嬰兒食品$R味道到底是怎樣的呢？$R;");
        }
    }
}