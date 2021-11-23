using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000790 : Event
    {
        public S11000790()
        {
            this.EventID = 11000790;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NNCYFlags> mask = pc.CMask["NNCY"];
            if (!mask.Test(NNCYFlags.爬爬蟲拉丁第一次對話) && pc.Level > 30 && pc.Fame > 19)//_5A85
            {
                mask.SetValue(NNCYFlags.爬爬蟲拉丁第一次對話, true);
                //_5A85 = true;
                Say(pc, 131, "♪$R;");
                Say(pc, 131, "可愛吧?$R;" +
                    "$R牠叫「爬爬蟲拉丁」$R;" +
                    "是可以載著主人到處走的騎乘寵物唷$R;");
                Say(pc, 131, "由「微型爬爬蟲」訓練成的$R騎乘寵物，就是這小子了$R;" +
                    "$R「微型爬爬蟲」力量大，$R所以可以載人和很多行李唷$R;" +
                    "$P帕斯特市的「皮爾」最近在分發呢$R我也去拿了一隻$R我要把它訓練成騎乘寵物喔！$R;");
                int a;
                a = Select(pc, "要問有關騎乘動物的嗎??", "", "跟一般寵物有什麼分別?", "怎樣分配的?", "怎樣才可以得到?", "沒什麼要問的");
                while (a != 4)
                {
                    switch (a)
                    {
                        case 1:
                            Say(pc, 131, "不論如何，可以騎乘，$R就是跟一般寵物最大的差別囉$R;" +
                                "$R騎乘方法很簡單，$R只要雙擊寵物的圖示就可以了$R;" +
                                "$P乘坐的時候$R只要操控「騎乘寵物」就可以了$R這樣一來，移動和打鬥$R都是由「騎乘寵物」完成$R;" +
                                "$P受攻擊時，$R受傷的也是「騎乘寵物」$R;" +
                                "$R不過，「騎乘寵物」$R打敗魔物的經驗值$R以及掉落的道具$R您就不能得到了。要記住啊！$R;");
                            break;
                        case 2:
                            Say(pc, 131, "「騎乘寵物」在您騎乘的時候$R跟魔物打鬥的話$R寵物自己也會成長唷$R;" +
                                "$R這一點跟一般寵物一樣$R;" +
                                "$P當「騎乘寵物」無法走動時$R把牠拋棄，親密度會減1喔$R;" +
                                "$R使用「四葉草糖果」$R親密度恢復程度$R跟其他寵物一樣喔$R;");
                            break;
                        case 3:
                            Say(pc, 131, "每一種寵物得到的方法都不同喔$R但要得到「爬爬蟲拉丁」$R;" +
                                "只要把您細心飼養的$R「微型爬爬蟲」送到這來$R;" +
                                "$P我會把牠養育成$R「爬爬蟲拉丁」唷$R;" +
                                "$P「爬爬蟲拉丁」$R雖然可以運載很多行李$R但腿比較短$R;" +
                                "$P只要您的寵物在「微型爬爬蟲」時期$R好好訓練$R當牠長成「爬爬蟲拉丁」時$R也可以敏捷地行動呀$R;");
                            break;
                    }
                    a = Select(pc, "要問有關騎乘動物的嗎??", "", "跟一般寵物有什麼分別?", "怎樣分配的?", "怎樣才可以得到?", "沒什麼要問的");
                }
                return;
            }
            Say(pc, 131, "哞哞$R;");
        }
    }
}