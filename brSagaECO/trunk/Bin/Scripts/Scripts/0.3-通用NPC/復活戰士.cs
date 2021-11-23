using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public abstract class 復活戰士 : Event
    {
        uint mapID;
        byte x, y;

        public 復活戰士()
        {
        
        }

        protected void Init(uint eventID, uint mapID, byte x, byte y)
        {
            this.EventID = eventID;
            this.mapID = mapID;
            this.x = x;
            this.y = y;
        }

        public override void OnEvent(ActorPC pc)
        {
            try
            {
                string oldSave, newSave;

                oldSave = GetMapName(pc.SaveMap);
                newSave = GetMapName(mapID);

                if (oldSave == "")
                {
                    SetHomePoint(pc, mapID, x, y);

                    Say(pc, 131, "儲存點設定為$R;" +
                                 "『" + newSave + "』了!$R;", "復活戰士");
                    return;
                }
                Say(pc, 131, "現在的儲存點是$R;" +
                             "『" + oldSave + "』!$R;", "復活戰士");

                switch (Select(pc, "確定要存儲在這裏嗎?", "", "不改變", "我要存儲在這裏"))
                {
                    case 1:
                        break;

                    case 2:
                        SetHomePoint(pc, mapID, x, y);

                        Say(pc, 131, "儲存點設定為$R;" +
                                     "『" + newSave + "』了!$R;", "復活戰士");
                        break;
                }
            }
            catch
            {
                SetHomePoint(pc, mapID, x, y);

                Say(pc, 131, "儲存點設定為$R;" +
                             "『" + GetMapName(mapID) + "』了!$R;", "復活戰士");
            }
        }
    }
}
