using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M12035000
{
    public class S11001629 : Event
    {
        public S11001629()
        {
            this.EventID = 11001629;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "唉……、傷腦筋了。$R;" +
            "$R還想要到西部要塞去避難，$R;" +
            "結果來到這裏……。$R;" +
            "$R沒想到跟我預想的道路$R;" +
            "弄錯了……。$R;", "難民婦女");
            int selection;
            do
            {
                selection = Select(pc, "試著詢問什麽？", "", "關于西部要塞", "避難？","關於『ＤＥＭ』","還是算了");
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "呃……？西部要塞嗎？$R;" +
                        "$R西部要塞在$R;" +
                        "這個大陸的最西端$R;" +
                        "是道米尼族最後的根據地。$R;" +
                        "$P要是能坐著那邊的飛空庭$R;" +
                        "的話，馬上就能到。$R;" +
                        "$R但是發生了橫斷整個$R;" +
                        "大陸的强烈氣流$R;" +
                        "所以就沒法用飛空庭飛行了……$R;" +
                        "$P如果妳要去$R;" +
                        "西部要塞的話，$R;" +
                        "雖然我想說讓我們跟你一起$R;" +
                        "……$R;" +
                        "$R但是這裏貌似也很安全，$R;" +
                        "就暫時現在這裏住一會兒吧。$R;", "難民の女性");
                        break;
                    case 2:
                        Say(pc, 131, "是啊……。$R;" +
                        "$R在最近ＤＥＭ的攻擊$R;" +
                         "變得更强烈了。$R;" +
                         "因此帶著妹妹從住着的城市那裏$R;" +
                          "來避難了。$R;", "難民婦女");
                        break;
                    case 3:
                        Say(pc, 131, "難到你什麽都不知道嗎？。$R;" +
                        "$RＤＥＭ，又稱作機械族$R;" +
                        "就要支配這個國家，道米尼世界了。$R;" +
                        "$P那些傢伙都冷酷無比，又殘酷。$R;" +
                        "我們的父母都被那些傢伙給……$R;", "難民の女性");
                        break;
                }
            } while (selection != 4);
        }
    }
}


        
   


