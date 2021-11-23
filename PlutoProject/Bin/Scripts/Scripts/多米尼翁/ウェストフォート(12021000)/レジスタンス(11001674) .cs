using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001674 : Event
    {
        public S11001674()
        {
            this.EventID = 11001674;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 112, "オレ見ちまったんだよ……。$R;", "レジスタンス");

            Say(pc, 11001673, 131, "何をですか？$R;", "レジスタンス");

            Say(pc, 65535, "軍艦島に居る妖精が人をさらって$R;" +
            "行く瞬間をよ……。$R;", "レジスタンス");

            Say(pc, 11001673, 131, "え？！そんな、冗談$R;" +
            "言わないでくださいよ。$R;", "レジスタンス");

            Say(pc, 65535, "う、嘘じゃないんだ！$R;" +
            "信じてくれよ！$R;", "レジスタンス");

            Say(pc, 11001673, 132, "た、確かに最近、行方不明者が$R;" +
            "出ているとの報告は受けていますが$R;" +
            "……何か関係が。$R;", "レジスタンス");

            Say(pc, 65535, "わからねぇ、わからねぇけど$R;" +
            "あいつは危険だと言うことは$R;" +
            "オレにはわかる……。$R;", "レジスタンス");

            Say(pc, 11001673, 132, "触らぬ神に祟り無し……ですね。$R;", "レジスタンス");
           }

           }
                        
                }
            
            
        
     
    