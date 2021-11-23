using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001773 : Event
    {
        public S11001773()
        {
            this.EventID = 11001773;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "お前はなんでこっちの$R;" +
            "世界に来たんだ？$R;", "ヨルン");

            Say(pc, 11001774, 131, "なんでって……お前も$R;" +
            "聞いただろ、今この世界が$R;" +
            "大変なことになってるって。$R;", "トール");

            Say(pc, 131, "聞いたけどよ、俺達にとっちゃ$R;" +
            "正直関係のない話だろ？$R;", "ヨルン");

            Say(pc, 11001774, 131, "なっ！？$R;" +
            "……っふ、やっぱタイタニア$R;" +
            "種族なんて自分達のことしか$R;" +
            "考えていないんだな。$R;", "トール");

            Say(pc, 131, "あぁん？$R;" +
            "癇に障るねぇその言い方。$R;" +
            "$R俺は他の奴とは違うんだよ！$R;" +
            "気になんなきゃこんなとこまで$R;" +
            "来ないっつーんだよ……。$R;", "ヨルン");

            Say(pc, 11001774, 131, "……素直じゃない奴だな、お前。$R;", "トール");

            Say(pc, 112, "うるせぇよ。$R;", "ヨルン");
           }

           }
                        
                }
            
            
        
     
    