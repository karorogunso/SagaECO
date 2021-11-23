using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001607 : Event
    {
        public S11001607()
        {
            this.EventID = 11001607;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 135, "甚麼？……老子笑了？$R;" +
                "$R那傢伙在ＤＥＭ的攻撃中$R;" +
                "老子在一刻失去了$R;" +
                "家園、金錢、然後家族……。$R;" +
                "$P真是幽默。$R;" +
                "至今積聚又堆積而來的東西$R;" +
                "在一瞬間成了灰燼？$R;" +
                "$R哈哈哈！$R;", "失去家園的男子");
            //
            /*
            Say(pc, 135, "なんだ？……俺を笑いに来たか？$R;" +
            "$RあいつらＤＥＭの攻撃で$R;" +
            "俺は家を、金を、そして家族を$R;" +
            "一度に失ってしまったんだ……。$R;" +
            "$Pおかしいよな。$R;" +
            "今まで積み重ねてきたものが$R;" +
            "一瞬で灰だぜ？$R;" +
            "$Rハッハッハ！$R;", "家を失った男");
            */
        }

           }
                        
                }
            
            
        
     
    