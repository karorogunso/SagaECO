
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
    public partial class S100000101 : Event
    {
        public S100000101()
        {
            this.EventID = 100000101;
        }
        public override void OnEvent(ActorPC pc)
        {
            ChangeMessageBox(pc);
            CreateNpcPict(pc, 81000002, 10136900, 145, 44, 5, 300, 1, 111, 10);
            Say(pc, 0, "魔狼一族…想不到，居然真的存在。", "沙月");
            Say(pc, 0, "沙月，你来了啊……咦，莫库草呢？", pc.Name);
            NPCMove(pc, 80001701, 167, 34, 500, 6, 0xb, 366, 10, 0);
            Say(pc, 0, "莫库草？$R……啊，啊啊，我，我已经…放她离开了。$R你们不要在意了。", "沙月");
            Say(pc, 80001701, 131, "感觉到你们成功驱散了幻象，我就立刻赶过来了。做得好。", "沙月");
            NPCMove(pc, 80001701, 167, 34, 500, 1, 0xb, 366, 10, 0);
            Say(pc, 80001701, 322, "看起来，那头漆黑的巨兽就是冥王了。$R果然，隔着这么远都能感觉到它身上散发出的狂气", "沙月");
            Say(pc, 0, "虽然已经驱散了3个幻象源头，但是这里的幻象魔法却比外面更加扭曲。$R确实，任何生物在这种程度的幻象下恐怕都无法保持理智吧。", "沙月");
            Say(pc, 0, "很难想像，究竟是看到什么让它发了疯…想必是相当可怕的东西。", "沙月");
            Say(pc, 0, "要想打破施加在它身上的幻象，我们需要先想办法制服它，$R不然一切都是空谈。", "沙月");
            Say(pc, 0, "我的魔法虽然不足以驱散整个幻象，$R但是足以在这幻象的外壳上打开一条缝隙，让你们进入幻象的内部直面冥王", "沙月");
            Say(pc, 0, "要小心，在幻象的内部，一切都有可能发生。准备好了就告诉我吧。", "沙月");
            Select(pc, " ", "", "好的");
            
        }
    }
}