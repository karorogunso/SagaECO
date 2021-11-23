using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;
using SagaMap.Manager;
using SagaDB.Actor;
using SagaDB.Map;
using SagaDB.Item;
using SagaDB.Skill;
using SagaDB.Quests;

namespace SagaMap.Scripting
{
    public abstract partial class Event
    {
        /// <summary>
        /// 触发事件时执行的方法
        /// </summary>
        /// <param name="pc">触发该事件的玩家类</param>
        public abstract void OnEvent(ActorPC pc);

        public virtual void OnSpecialEvent(ActorPC pc)
        {

        }

        public virtual void OnTransportSource(ActorPC pc)
        {
            Say(pc, 131, this.questTransportSource, "");
            if (pc.Quest.Detail.ObjectID1 != 0)
                GiveItem(pc, pc.Quest.Detail.ObjectID1, (ushort)pc.Quest.Detail.Count1);
            if (pc.Quest.Detail.ObjectID2 != 0)
                GiveItem(pc, pc.Quest.Detail.ObjectID2, (ushort)pc.Quest.Detail.Count2);
            if (pc.Quest.Detail.ObjectID3 != 0)
                GiveItem(pc, pc.Quest.Detail.ObjectID3, (ushort)pc.Quest.Detail.Count3);
            Say(pc, 131, LocalManager.Instance.Strings.QUEST_TRANSPORT_GET, " ");
            PlaySound(pc, 2030, false, 100, 50);
        }

        public virtual void OnTransportDest(ActorPC pc)
        {
            if (pc.Quest.Detail.ObjectID1 != 0)
            {
                if (CountItem(pc, pc.Quest.Detail.ObjectID1) < pc.Quest.Detail.Count1)
                    pc.Quest.CurrentCount3 = 1;
            }
            if (pc.Quest.Detail.ObjectID2 != 0)
            {
                if (CountItem(pc, pc.Quest.Detail.ObjectID2) < pc.Quest.Detail.Count2)
                    pc.Quest.CurrentCount3 = 1;
            }
            if (pc.Quest.Detail.ObjectID3 != 0)
            {
                if (CountItem(pc, pc.Quest.Detail.ObjectID3) < pc.Quest.Detail.Count3)
                    pc.Quest.CurrentCount3 = 1;
            }
            if (pc.Quest.CurrentCount3 == 0)
            {
                Say(pc, 131, this.questTransportDest, "");
                if (pc.Quest.Detail.ObjectID1 != 0)
                    TakeItem(pc, pc.Quest.Detail.ObjectID1, (ushort)pc.Quest.Detail.Count1);
                if (pc.Quest.Detail.ObjectID2 != 0)
                    TakeItem(pc, pc.Quest.Detail.ObjectID2, (ushort)pc.Quest.Detail.Count2);
                if (pc.Quest.Detail.ObjectID3 != 0)
                    TakeItem(pc, pc.Quest.Detail.ObjectID3, (ushort)pc.Quest.Detail.Count3);
                Say(pc, 131, LocalManager.Instance.Strings.QUEST_TRANSPORT_GIVE, " ");
                PlaySound(pc, 2040, false, 100, 50);                
            }
        }

        public virtual void OnTransportCompleteSrc(ActorPC pc)
        {
            Say(pc, 131, this.questTransportCompleteSrc, "");            
        }

        public virtual void OnTransportCompleteDest(ActorPC pc)
        {
            Say(pc, 131, this.questTransportCompleteDest, "");            
        }
    }
}
