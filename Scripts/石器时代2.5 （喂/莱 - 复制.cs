
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S20900003 : Event
    {
        public S20900003()
        {
            this.EventID = 20900003;
        }

        public override void OnEvent(ActorPC pc)
        {
            服務(pc);
            //HandleQuest(pc, 6);
            //SagaMap.Network.Client.MapClient.FromActorPC(pc).CreateAnotherPaper(1);
        }
        void 服務(ActorPC pc)
        {
            switch (Select(pc, "請選擇交談內容", "", "强化装备", "潜在强化", "離開"))
            {
                case 1:
                    ItemEnhance(pc);
                    break;
                case 2:
                    Say(pc, 159, "那麼，$R請務必小心。$R$R要回到飛艇上來，$R記得使用鑰匙哦。", "亞利亞");
                    //Warp(pc,)
                    break;
                case 3:
                    Say(pc, 159, "您請慢走~", "亞利亞");
                    break;
            }
        }
    }
}

