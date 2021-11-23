
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
    public class S80000612: Event
    {
        public S80000612()
        {
            this.EventID = 80000612;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.CInt["魔导师转职任务"] == 7)
            {
                Say(pc, 0, "星麻麻是让你来找我回去吧？$R$R……啊，但我暂时还不能回去，$R冒险者，请你帮忙把这个交给星麻麻吧。", "小吱");
                Say(pc, 0, "把这个交给她，她看到就懂了！", "小吱");
                return;
            }
            Say(pc, 0, "彷佛听到有人说我帅~！$R不对，应该赶快完成任务才行……", "小吱");
        }
    }
}