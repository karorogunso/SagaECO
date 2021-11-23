using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class 無限回廊傳送點 : Event
    {
        uint ItemID;
        uint mapID;
        byte x1, x2, y1, y2;

        public 無限回廊傳送點()
        {

        }

        protected void Init(uint eventID, uint ItemID, uint mapID, byte x1, byte y1, byte x2, byte y2)
        {
            this.EventID = eventID;
            this.ItemID = ItemID;
            this.mapID = mapID;
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;

        }

        public override void OnEvent(ActorPC pc)
        {
            int a = pc.PossesionedActors.Count + 1;
            int b = CountItem(pc, ItemID);
            if (a <= b)
            {
                int x = Global.Random.Next(x1, x2);
                int y = Global.Random.Next(y1, y2);
                TakeItem(pc, ItemID, (ushort)b);
                Warp(pc, mapID, (byte)x, (byte)y);
                return;
            } 
            Say(pc, 131, "传送点没有启动$R;" +
                 "$P可能是道具不足喔$R;");
        }
    }
}
