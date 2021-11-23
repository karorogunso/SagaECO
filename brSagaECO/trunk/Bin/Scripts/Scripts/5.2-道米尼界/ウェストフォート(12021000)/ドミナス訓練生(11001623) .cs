using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001623 : Event
    {
        public S11001623()
        {
            this.EventID = 11001623;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "……。$R;" +
                "$P…………。$R;" +
                "$P………………。$R;" +
                "$P（……糟糕！$R;" +
                "現在、正在訓練中不能說出話來。）$R;" +
                "$R（讓我回避下、教官……）$R;", "ドミナス訓練生");

            Say(pc, 11001622, 111, "（……ギロリ。）$R;", "グンバール教官");

            Say(pc, 131, "（嗶咕咻！）$R;" +
            "$R（……不、會被$R;" +
            "軍曹罵的。）$R;", "ドミナス訓練生");

            Say(pc, 11001622, 434, "那邊！在鬼鬼祟崇的做甚麼！！$R;" +
            "$R……我的訓練$R;" +
            "這麼沈悶累人？……ドミナス君？$R;", "グンバール教官");

            Say(pc, 111, "Sir、No、Sir！$R;", "ドミナス訓練生");
            //
            /*
            Say(pc, 131, "……。$R;" +
            "$P…………。$R;" +
            "$P………………。$R;" +
            "$P（……悪い！$R;" +
            "今、訓練中で話すことは出来ないんだ。）$R;" +
            "$R（サボっていると、教官……）$R;", "ドミナス訓練生");

            Say(pc, 11001622, 111, "（……ギロリ。）$R;", "グンバール教官");

            Say(pc, 131, "（ビクッ！）$R;" +
            "$R（……いや、軍曹に$R;" +
            "怒鳴られてしまう。）$R;", "ドミナス訓練生");

            Say(pc, 11001622, 434, "そこ！なにコソコソやっている！！$R;" +
            "$R……そんなにわたしの訓練が$R;" +
            "退屈かね？……ドミナス君？$R;", "グンバール教官");

            Say(pc, 111, "サー、ノー、サー！$R;", "ドミナス訓練生");
            */
        }

           }
                        
                }
            
            
        
     
    