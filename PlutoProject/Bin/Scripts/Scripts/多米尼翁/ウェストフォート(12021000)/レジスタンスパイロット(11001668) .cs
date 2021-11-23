using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001668 : Event
    {
        public S11001668()
        {
            this.EventID = 11001668;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "やぁ！よくきたね！$R;" +
            "$Rパイロットの私に何か質問かね？$R;" +
            "遠慮はいらんぞ！$R;" +
            "なんでも聞いてくれたまえ！$R;", "レジスタンスパイロット");
            Say(pc, 131, "……なになに？$R;" +
            "$Rこのマシン、……私のマシンに$R;" +
            "興味があるだって！？$R;" +
            "$P……フッフッ。$R;" +
            "よし、質問に答えてやるぞ！$R;", "レジスタンスパイロット");
            Say(pc, 163, "私の可愛い子ちゃんは$R;" +
            "このマシンだ！$R;", "レジスタンスパイロット");

            Say(pc, 131, "……どうだ？$R;" +
            "この色とツヤ、傷一つ無いボディ$R;" +
            "そして……、このフォルム！$R;" +
            "美しいとは思わないかね！？$R;", "レジスタンスパイロット");
           }

           }
                        
                }
            
            
        
     
    