using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10058000
{
    public class S11001685 : Event
    {
        public S11001685()
        {
            this.EventID = 11001685;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Select(pc, "アクロポリスに行く？", "", "行かない", "行く") == 2)
           {
               Say(pc, 131, "あー悪いね。$R;" +
               "アタイの自慢の愛車が$R;" +
               "故障しちゃったみたいで$R;" +
               "アクロポリスまで送って$R;" +
               "やれそうにないンだよ。$R;" +
               "$Pすまないけど自力で$R;" +
               "帰ってもらえるかな？$R;" +
               "$R大丈夫！$R;" +
               "アンタなら問題ないはずだよ！$R;", "レジスタンス傭兵");
           }
                        
                }
            }
            
        }
     
    