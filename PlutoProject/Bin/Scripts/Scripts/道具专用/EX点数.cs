using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Manager;

namespace SagaScript.M80060050
{
    public class S90000413 : Event
    {
        public S90000413()
        {
            this.EventID = 90000413;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.EXStatPoint < 210)
                TakeItem(pc, 16015200, 1);
            pc.EXStatPoint = (ushort)Math.Min(210, (int)(pc.EXStatPoint + 10));
            var client = MapClientManager.Instance.FindClient(pc);
            client.SendPlayerInfo();
        }
    }

    public class S90000414 : Event
    {
        public S90000414()
        {
            this.EventID = 90000414;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.EXStatPoint < 210)
                TakeItem(pc, 16015201, 1);
            pc.EXStatPoint = (ushort)Math.Min(210, (int)(pc.EXStatPoint + 20));

            var client = MapClientManager.Instance.FindClient(pc);
            client.SendPlayerInfo();
        }
    }

    public class S90000415 : Event
    {
        public S90000415()
        {
            this.EventID = 90000415;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.EXStatPoint < 210)
                TakeItem(pc, 16015202, 1);
            pc.EXStatPoint = (ushort)Math.Min(210, (int)(pc.EXStatPoint + 25));
            var client = MapClientManager.Instance.FindClient(pc);
            client.SendPlayerInfo();
        }
    }

    public class S90000417 : Event
    {
        public S90000417()
        {
            this.EventID = 90000417;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.EXSkillPoint < 3)
            {
                //pc.C
                TakeItem(pc, 16015400, 1);
                pc.SkillPoint += 3;
                pc.SkillPoint2T += 3;
                pc.SkillPoint2X += 3;
                pc.SkillPoint3 += 3;
                pc.EXSkillPoint += 1;
            }
            var client = MapClientManager.Instance.FindClient(pc);
            client.SendPlayerInfo();
        }
    }
}