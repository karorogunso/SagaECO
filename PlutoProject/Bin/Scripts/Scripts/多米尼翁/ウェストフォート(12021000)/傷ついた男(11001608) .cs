using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001608 : Event
    {
        public S11001608()
        {
            this.EventID = 11001608;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 136, "ゴホッゴホッ……。$R;" +
            "俺になにか用かい？$R;" +
            "$R見ての通りＤＥＭの攻撃で$R;" +
            "俺の体はもうボロボロさ……。$R;" +
            "$Pレジスタンスに入って$R;" +
            "あの機械ヤローを$R;" +
            "一発ぶん殴ってやりたいが……。$R;" +
            "$Rクソッ、体が言うこと聞きやしねぇ。$R;" +
            "$P……これじゃ妹の仇も$R;" +
            "取れやしない……。$R;" +
            "$R情けねぇなぁ、俺……。$R;", "傷ついた男");
           }

           }
                        
                }
            
            
        
     
    