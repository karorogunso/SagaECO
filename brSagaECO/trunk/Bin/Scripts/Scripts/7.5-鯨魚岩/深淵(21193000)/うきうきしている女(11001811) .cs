using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001811 : Event
    {
        public S11001811()
        {
            this.EventID = 11001811;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "今天是跟他的第一次約會的說！$R;" +
            "$R…雖然稍微早了一點來到。$R;" +
            "$P不過這等待的時間也$R;" +
            "感覺很愉快呢！$R;" +
            "$P今日要一起去哪兒呢？$R;" +
            "很期待啊♪$R;", "很興奮興奮的女子");

            // 03/09/2015
            /*
            Say(pc, 0, "今日は彼と初デートなの！$R;" +
            "$R…少し早めに来ちゃったけど。$R;" +
            "$Pでもこうやって待ってる時間も$R;" +
            "ウキウキして楽しいのよね！$R;" +
            "$P今日はどこに連れて行ってくれるのかな？$R;" +
            "楽しみっ♪$R;", "うきうきしている女");
            */
        }
    }

        
    }


