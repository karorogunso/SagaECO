
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
    public partial class S100000104 : Event
    {
        public S100000104()
        {
            this.EventID = 100000104;
        }
        public override void OnEvent(ActorPC pc)
        {
            ChangeMessageBox(pc);
            if (pc.Party == null && pc.TInt["副本复活标记"] == 4)
                Warp(pc, (uint)pc.TInt["S60903000"], 35, 58);
            else
                Warp(pc, (uint)pc.Party.TInt["S60903000"], 35, 58);
            ShowEffect(pc, 36, 66, 5440);
            CreateNpcPict(pc, 81000004, 16010000, 36, 66, 4, 500, 1, 111, 10);
            CreateNpcPict(pc, 81000001, 60000006, 33, 59, 7, 500, 1, 111, 10);
            Say(pc, 81000004, 361, "怎么可能…我竟然会在幻象中…被击败……", "夺魂者");
            Say(pc, 81000004, 362, "已经…无法再帮助您实现愿望了……请……原谅……", "夺魂者");
            ShowPictCancel(pc, 81000004);
            //待补完道具塔罗牌 死神 暂未确定是一次性还是重复多次给
            if (CountItem(pc, 140000000) >= 1)
                pc.CInt["禁断之书的记忆-夺魂者"] = 1;
			if (pc.CInt["灾祸的见证者任务"] == 0)
                pc.CInt["灾祸的见证者任务"] = 1;
            Wait(pc, 2500);
            ShowEffect(pc, 36, 66, 5458);//幻象破碎
            Say(pc, 0, "……", "沙月");
            Say(pc, 0, "你怎么了，沙月？", pc.Name);
            Say(pc, 0, "沙月摇了摇头");
            NPCMove(pc, 81000001, 33, 59, 500, 5, 0xb, 111, 10, 0);
            Say(pc, 81000001, 2078, "不，只是它临死时候说的话很让我在意…", "沙月");
            Say(pc, 0, "主人…愿望…暮色教团……$R总觉得是非常熟悉的东西，但是我却从未听说过。", "沙月");
            Say(pc, 0, "而且，这个幻象魔法和我所使用的幻象魔法非常接近，$R不，$R不如说几乎是完全一样…$R所以刚才我才能那么轻易地从幻象上撕开缺口。", "沙月");
            Say(pc, 0, "这究竟是怎么回事……头好痛。总觉得这件事似乎和我有什么关联。", "沙月");
            Say(pc, 0, "你在说什么啊，刚刚不是你救了我吗？", pc.Name);
            Say(pc, 0, "啊……", "沙月");
            Say(pc, 0, "我相信沙月一定和这起事件没关系的，打起精神来吧", pc.Name);
            Say(pc, 0, "谢谢…你这么说我心里好过了不少", "沙月");
            Say(pc, 0, "那么，这次是真的要回去了。走吧！", "沙月");
            NPCMove(pc, 81000001, 36, 66, 500, 7, 0xb, 366, 10, 0);
            Wait(pc, 1800);
            ShowEffect(pc, 36, 66, 4083);
            ShowPictCancel(pc, 81000001);
/* 			if (Select(pc, " ", "", "脱离幻境", "我自己会/home！") == 1)
			{
						ShowEffect(pc, 4083);
						Wait(pc, 1500);
						ShowEffect(pc, 40, 62, 4299);
						CreateNpcPict(pc, 81000005, 60000128, 40, 62, 3, 500, 1, 111, 10);
						//Wait(pc, 2000);
						Say(pc, 0, "……", "？？？");
						Wait(pc, 2500);
						if (pc.Party == null && pc.TInt["副本复活标记"] == 4)
							Warp(pc, 10057002, 246, 116);
						else
							Warp(pc, 10057002, 246, 116);
			} */
        }
    }
}