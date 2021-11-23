
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public partial class 暗鸣2: Event
    {
        public 暗鸣2()
        {
            this.EventID = 87000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Party == null)
            {
                if (pc.MapID == pc.TInt["S10054100"])
                    ShowDialog(pc, 20003);
                if (pc.MapID == pc.TInt["S20004000"])
                    ShowDialog(pc, 20008);
                if (pc.MapID == pc.TInt["S20003000"])
                    ShowDialog(pc, 20012);
                if (pc.MapID == pc.TInt["S20000000"])
                    ShowDialog(pc, 20013);
                if (pc.MapID == pc.TInt["S30131002"])
                    ShowDialog(pc, 20017);
                return;
            }
            if (pc.Party.Leader == null) return;
            if (pc.MapID == pc.Party.Leader.TInt["S10054100"])
                ShowDialog(pc, 20003);
            if (pc.MapID == pc.Party.Leader.TInt["S20004000"])
                ShowDialog(pc, 20008);
            if (pc.MapID == pc.Party.Leader.TInt["S20003000"])
                ShowDialog(pc, 20012);
            if (pc.MapID == pc.Party.Leader.TInt["S20000000"])
                ShowDialog(pc, 20013);
            if (pc.MapID == pc.Party.Leader.TInt["S30131002"])
                ShowDialog(pc, 20017);
        }
    }
}