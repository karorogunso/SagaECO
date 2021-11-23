using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
using System.Globalization;
using System.Net;
using System.Linq;

namespace SagaScript.M19230001
{
    public class S19230001 : Event
    {
        public S19230001()
        {
            this.EventID = 19230001;
        }
        public override void OnEvent(ActorPC pc)
        {
           
			//Say(pc,11000776,0,"你生蓋滿了第5個印章了！","接待泰迪");
		
			
			
			Random Rand = new Random();
			int randVal = Rand.Next(1,10000);
			
			if(randVal <= 1000){
				GiveItem(pc,10001250,1);
			}else if(randVal <= 2000){
				GiveItem(pc,20050011,1);
			}else if(randVal <= 3000){
				GiveItem(pc,20050012,1);
			}else if(randVal <= 4000){
				GiveItem(pc,20050013,1);
			}else if(randVal <= 5000){
				GiveItem(pc,20050056,1);
			}else if(randVal <= 6000){
				GiveItem(pc,20050057,1);
			}else if(randVal <= 7000){
				GiveItem(pc,20050063,1);
			}else if(randVal <= 8000){
				GiveItem(pc,20050083,1);
			}else if(randVal <= 9000){
				GiveItem(pc,20050015,1);
			}else if(randVal <= 10000){
				GiveItem(pc,20050061,1);
			}
			
			
        }
    }
}