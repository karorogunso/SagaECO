using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001673 : Event
    {
        public S11001673()
        {
            this.EventID = 11001673;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 132, "軍艦島に夜になると現れると言う$R;" +
            "妖精の話をご存知ですか？$R;", "レジスタンス");

            Say(pc, 11001674, 65535, "オレ見ちまったんだよ……。$R;" +
            "$Rその妖精が人をさらって$R;" +
            "行く瞬間をよ……。$R;", "レジスタンス");

            Say(pc, 131, "ほほぉ……。$R;", "レジスタンス");

            Say(pc, 11001674, 65535, "う、嘘じゃないんだ！$R;" +
            "信じてくれよ！$R;", "レジスタンス");

            Say(pc, 131, "僕が聞いた話によると$R;" +
            "闇の力がどうたらこうたらと$R;" +
            "語りかけてくるようですよ。$R;", "レジスタンス");

            Say(pc, 11001674, 65535, "や、やめてくれよ！$R;" +
            "そんな話は聞きたくない！$R;", "レジスタンス");

            Say(pc, 131, "そうですか……。$R;" +
            "僕としては非常に興味が$R;" +
            "あるのですが……。$R;", "レジスタンス");

            Say(pc, 11001674, 131, "お前……早まるなよ……？$R;", "レジスタンス");
           }

           }
                        
                }
            
            
        
     
    