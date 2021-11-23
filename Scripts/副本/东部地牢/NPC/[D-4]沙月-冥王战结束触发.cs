
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
    public partial class S100000102 : Event
    {
        public S100000102()
        {
            this.EventID = 100000102;
        }
        public override void OnEvent(ActorPC pc)
        {
            ChangeMessageBox(pc);
            if (pc.Party == null && pc.TInt["副本复活标记"] == 4)
                Warp(pc, (uint)pc.TInt["S20092000"], 128, 57);
            else
                Warp(pc, (uint)pc.Party.TInt["S20092000"], 128, 57);
            ShowEffect(pc, 130, 58, 4011);
            NPCMove(pc, 80001701, 130, 58, 500, 1, 7, 357, 20, 18);
            CreateNpcPict(pc, 81000002, 10136900, 126, 61, 5, 500, 0, 363, 10);
            NPCMotion(pc, 80001701, 357);
            NPCMotion(pc, 81000002, 363, true, 10);
            Say(pc, 0, "呼……呼……$R干得好，它已经暂时无法反抗了！", "沙月");
            ShowEffect(pc, 130, 58, 1022);//沙月脚下的法阵
            Say(pc, 0, "那么接下来只要把冥王的幻觉解开就可以了。", "沙月");
            Say(pc, 80001701, 571, "（低声咏唱）", "沙月");
            ShowEffect(pc, 127, 61, 4340);//定位阵
            Wait(pc, 2000);
            ShowEffect(pc, 126, 63, 4383);//驱散
            Wait(pc, 1000);
            ShowEffect(pc, 126, 63, 4254);//痊愈
            Wait(pc, 1000);
            ShowEffect(pc, 126, 63, 4412);//解开
            Wait(pc, 2000);
            Say(pc, 0, "这样就好…它已经摆脱了幻象的控制了", "沙月");
            Say(pc, 81000002, 361, "嗷嗷呜……！", "冥王");
            Say(pc, 0, "冥王注视着你低下头来，似乎在表示感谢");
            if (pc.CInt["狼牙护身符"] == 0)
            {
                GiveItem(pc, 110063600, 1);//狼牙护身符，待补完创建道具
                pc.CInt["狼牙护身符"] = 1;
            }
			NPCMove(pc, 80001701, 167, 34, 500, 210, 7, 0, 0, 16);
            Say(pc, 0, "辛苦大家了。虽然很艰难，但是幻象已经确实被解除了，$R事情也算终于告一段落，我们回去吧。", "沙月");
			Say(pc, 0, "（只是，究竟是谁，又是为了什么制造了这幻象呢…）");
			Say(pc, 0, "……总而言之，当你准备好了就告诉我吧，我来开启回到鱼缸岛的传送门。", "沙月");
			pc.TInt["东牢D-3"] = 4;
        }
    }
}