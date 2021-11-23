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

                    Say(pc, 131, "储存点设定为$R;" +
                                 "『" + newSave + "』了!$R;", "复活战士");
                    return;
                }
                Say(pc, 131, "现在的储存点是$R;" +
                             "『" + oldSave + "』!$R;", "复活战士");

                switch (Select(pc, "确定要存储在这里吗?", "", "不改变", "我要存储在这里"))
                {
                    case 1:
                        break;

                    case 2:
                        SetHomePoint(pc, mapID, x, y);

                        Say(pc, 131, "储存点设定为$R;" +
                                     "『" + newSave + "』了!$R;", "复活战士");
                        break;
                }
            }
            catch
            {
                SetHomePoint(pc, mapID, x, y);

                Say(pc, 131, "储存点设定为$R;" +
                             "『" + GetMapName(mapID) + "』了!$R;", "复活战士");
            }
        }
    }
}
