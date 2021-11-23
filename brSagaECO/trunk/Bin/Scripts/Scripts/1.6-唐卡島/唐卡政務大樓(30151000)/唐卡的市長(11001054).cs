using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30151000
{
    public class S11001054 : Event
    {
        public S11001054()
        {
            this.EventID = 11001054;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            Say(pc, 131, "歡迎，$R;" +
                "冒險者，歡迎光臨$R;" +
                "$R今天有何貴幹？$R;");
            switch (Select(pc, "給我介紹介紹吧", "", "清潔計劃", "想註冊飛空庭", "街燈的燈壞了", "警備機器人好煩", "什麼也不做"))
            {
                case 1:
                    //GOTO EVT1100105408;
                    break;
                case 2:

                    if (!fgarden.Test(FGarden.唐卡注册飞空庭))
                    {
                        if (fgarden.Test(FGarden.得到飛空庭鑰匙)) 
                        {
                            Say(pc, 131, "註冊需要50000金幣唷$R;");
                            switch (Select(pc, "怎麼辦呢?", "", "付50000金幣", "放棄"))
                            {
                                case 1:
                                    if (pc.Gold > 49999)
                                    {
                                        if (CheckInventory(pc, 31110709, 1))
                                        {
                                            fgarden.SetValue(FGarden.唐卡注册飞空庭, true);
                                            GiveItem(pc, 31110709, 1);
                                            pc.Gold -= 50000;
                                            PlaySound(pc, 2060, false, 100, 50);
                                            Say(pc, 131, "付了50000金幣！$R;");
                                            Say(pc, 131, "名字…分明…$R;" +
                                                pc.Name + "是吧?$R;" +
                                                "$P註冊結束了$R;" +
                                                "現在您的飛空庭以$R;" +
                                                "唐卡這裡為根據地喔$R;" +
                                                "$R這是給您$R;" +
                                                "代替證明書的唐卡國旗。$R;" +
                                                "$R請收下$R;" +
                                                "$P這裡定為根據地，可以自由利用$R;" +
                                                "飛空庭大工廠的各種服務唷$R;" +
                                                "$R那麼拜託了。$R;");
                                            return;
                                        }
                                        Say(pc, 131, "註冊後會給您物品，$R;" +
                                            "$R請先把行李減輕吧$R;");
                                        return;
                                    }
                                    Say(pc, 131, "嗯~錢不夠呀。$R;");
                                    break;
                                case 2:
                                    break;
                            }
                            return;
                        }
                        Say(pc, 131, "嗯~看樣子您好像沒有飛空庭呀$R;");
                        return;
                    }
                    Say(pc, 131, "嗯，已經註冊了$R;");
                    break;
                case 3:
                    Say(pc, 131, "謝謝您告訴我$R;" +
                        "以後會插進去的。$R;");
                    break;
                case 4:
                    Say(pc, 131, "哈哈哈！$R;" +
                        "聽起來真煩！$R;" +
                        "$R我也天天被質問業務上的事情$R;" +
                        "不會害您的，好好對它吧。$R;");
                    break;
                case 5:
                    break;
            }
        }
    }
}