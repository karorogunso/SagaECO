using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31302000
{
    public class S11001910 : Event
    {
        public S11001910()
        {
            this.EventID = 11001910;
        }

        public override void OnEvent(ActorPC pc)
        {
Say(pc, 131, "らっしゃいっ！$R;" + 
"$Rお土産に「お風呂セット」は$R;" + 
"いかがでしょうか！$R;", "家具屋ジュグラー");
        	switch(Select(pc, "ゆっくりしていきなっ！", "", "もういい","大きな家具が見たい","小さな家具が見たい"))
        	{
        	case 2:
        		OpenShopBuy(pc, 408);	
        	break;	
        	case 3:
        		OpenShopBuy(pc, 409);	
        	break;
        	}


Say(pc, 131, "（……レンテちゃん、かわいいなぁ。）$R;" + 
"$R……あっ！$R;" + 
"またのお越し、お待ちしておりますっ！$R;", "家具屋ジュグラー");

}
}

        
    }


