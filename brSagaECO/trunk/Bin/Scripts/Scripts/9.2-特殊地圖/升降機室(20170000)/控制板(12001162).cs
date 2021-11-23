using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20170000
{
    public class S12001162 : Event
    {
        public S12001162()
        {
            this.EventID = 12001162;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "控制面板的$R;" +
            "電源還連接著。$R;", "");
            switch (Select(pc, "要操作畫面么？", "", "不做", "按上方的按鈕", "按下方的按鈕"))
            {

                case 2:
                    ShowEffect(pc, 5289);

                    Say(pc, 0, 131, "嗶……。$R;" +
                    "$P……。$R;" +
                    "$P…………。$R;" +
                    "$P………………。$R;" +
                    "$P訪問成功。$R;" +
                    "開始傳送到塔尼亞界。$R;", " ");
                    ShowEffect(pc, 5314);
                    Wait(pc, 990);
                    Warp(pc, 11058000, 128, 157);
                    break;
                case 3:
                    if (pc.PossesionedActors.Count != 0)
                    {
                        Say(pc, 131, "解除凭依之后才能通过哦.");
                        return;
                    }
                    if (pc.Quest != null)
                        Say(pc, 131, "要交掉任务以后才能通过.");
                    else
                    {
                        ShowEffect(pc, 5289);

                        Say(pc, 0, 0, "嗶池……。$R;" +
                        "$P……。$R;" +
                        "$P…………。$R;" +
                        "$P………………。$R;" +
                        "$P進入成功。$R;" +
                        "要轉移到道米尼界嗎？$R;", "");
                        if (Select(pc, "要轉移嗎？", "", "次元転生ついて", "前往") == 2)
                        {
                            ShowEffect(pc, 5313);
                            Wait(pc, 990);
                            Warp(pc, 12058000, 127, 155);
                            SetHomePoint(pc, 12058000, 128, 155);
                        }
                    }
                    break;

            }
        }
    }
}