
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace WeeklyExploration
{
    public partial class Trial : Event
    {
        public Trial()
        {
            this.EventID = 50000002;
        }
        int count = 0;
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "你想去【光之塔】探险吗？", "特派员J");
            选项(pc);
        }
        void 选项(ActorPC pc)
        {
            switch (Select(pc, "请选择", "", "开始单人试炼", "重置通关数（每日限1次）", "离开"))
            {
                case 1:
                    单人试炼(pc);
                    break;
                case 2:
                    pc.CInt["当前通关数"] = 0;
                    选项(pc);
                    break;
                case 3:
                    return;
            }
        }
        void 单人试炼(ActorPC pc)
        {
            int misssin = pc.CInt["当前通关数"];
            if(misssin != 0)
                if (Select(pc, string.Format("将会从第{0}关开始进行，$R是否继续？", pc.CInt["当前通关数"]), "", string.Format("确定从第{0}关开始", pc.CInt["当前通关数"]), "离开") == 2) return;
            switch(misssin)
            {
                case 0:
                    开始第一关(pc);
                    break;
                default:
                    return;
            }
        }
        void Start(ActorPC pc)
        {
            pc.TInt["试炼临时地图"] = CreateMapInstance(10001001, 90001000, 35, 15, true, 999, true);
            uint mapid = (uint)pc.TInt["试炼临时地图"];
            Warp(pc, mapid, 194, 251);
            Timer 刷怪计时 = new Timer("试炼首次刷怪计时", 1000, 1000);
            刷怪计时.AttachedPC = pc;
            刷怪计时.OnTimerCall += 刷怪计时_OnTimerCall;
            刷怪计时.Activate();
            Announce(pc, "试炼敌人将在30秒后到达战场。");
        }

        void 刷怪计时_OnTimerCall(Timer timer, ActorPC pc)
        {
            count++;
            if (count >= 30)
            {
                timer.Deactivate();
                Announce(pc, "s");
            }
            if(count == 19)
            {
                Announce(pc, "还有10秒，请做好战斗准备");
            }
        }
    }
}

