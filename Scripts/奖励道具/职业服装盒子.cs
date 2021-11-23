
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
    public class S910000008 : Event
    {
        public S910000008()
        {
            this.EventID = 910000008;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(CountItem(pc, 910000008) >= 1)
            {
                TakeItem(pc, 910000008, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            List<uint> list = getlist();
            GiveItem(pc, list[SagaLib.Global.Random.Next(0, list.Count - 1)], 1);
        }
        List<uint> getlist()
        {
            List<uint> idsC = new List<uint>();
            idsC.Add(60128800);
            idsC.Add(60128801);
            idsC.Add(60128900);
            idsC.Add(60128901);
            idsC.Add(60146300);
            idsC.Add(60146400);
            idsC.Add(60146401);
            idsC.Add(60128000);
            idsC.Add(60128001);
            idsC.Add(60128100);
            idsC.Add(60128101);
            idsC.Add(60146500);
            idsC.Add(60146600);
            idsC.Add(60128200);
            idsC.Add(60128201);
            idsC.Add(60128300);
            idsC.Add(60128301);
            idsC.Add(60146700);
            idsC.Add(60146800);
            idsC.Add(60128400);
            idsC.Add(60128401);
            idsC.Add(60128500);
            idsC.Add(60128501);
            idsC.Add(60146900);
            idsC.Add(60147000);
            idsC.Add(61084400);
            idsC.Add(61084401);
            idsC.Add(60128600);
            idsC.Add(60128601);
            idsC.Add(60128700);
            idsC.Add(60128701);
            idsC.Add(60147100);
            idsC.Add(60147200);
            idsC.Add(60129000);
            idsC.Add(60129001);
            idsC.Add(60129100);
            idsC.Add(60129101);
            idsC.Add(60147300);
            idsC.Add(60147400);
            idsC.Add(60129200);
            idsC.Add(60129201);
            idsC.Add(60129300);
            idsC.Add(60129301);
            idsC.Add(60147500);
            idsC.Add(60147600);
            idsC.Add(60129400);
            idsC.Add(60129401);
            idsC.Add(60129500);
            idsC.Add(60129501);
            idsC.Add(60147700);
            idsC.Add(60147800);
            idsC.Add(60129600);
            idsC.Add(60129601);
            idsC.Add(60129700);
            idsC.Add(60129701);
            idsC.Add(60147900);
            idsC.Add(60147901);
            idsC.Add(60148000);
            idsC.Add(60148001);
            idsC.Add(60129800);
            idsC.Add(60129801);
            idsC.Add(60129900);
            idsC.Add(60129901);
            idsC.Add(60148100);
            idsC.Add(60148200);
            idsC.Add(60130000);
            idsC.Add(60130001);
            idsC.Add(60130100);
            idsC.Add(60130101);
            idsC.Add(60130200);
            idsC.Add(60130201);
            idsC.Add(60130300);
            idsC.Add(60130301);
            idsC.Add(60148500);
            idsC.Add(60148600);
            idsC.Add(60130400);
            idsC.Add(60130401);
            idsC.Add(60148700);
            idsC.Add(60148800);
            return idsC;
        }
    }
}

