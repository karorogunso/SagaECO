
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
    public class S80000611 : Event
    {
        public S80000611()
        {
            this.EventID = 80000611;
        }
        private void 魔导师转职任务5(ActorPC pc)
        {
                Say(pc, 0, "啾？啾啾。", "小吱？");
                Say(pc, 0, "恐怕这就是那位看上去$R很不靠谱的元素使的宠物了。", pc.Name);
                switch (Select(pc, "怎么办呢？", "", "把种子给沙鼠", "不如拿着种子去交咖啡馆任务！"))
                {
                    case 1:
                        TakeItem(pc, 10104406, 5);
                        TakeItem(pc, 10104404, 5);
                        pc.CInt["魔导师转职任务"] = 6;
                        Say(pc, 0, "啾，啾啾——", "小吱？");
                        Say(pc, 0, "（沙鼠把种子吃掉了）", " ");
                        Say(pc, 0, "啾啾，啾啾。", "小吱？");
                        Say(pc, 0, "（沙鼠做出讨要的动作，彷佛还想要种子）", " ");
                        Say(pc, 0, "？？？", pc.Name);
                        Say(pc, 0, "这就十分尴尬了。", pc.Name);
                        Say(pc, 0, "有一种被坑的感觉。", pc.Name);
                        Say(pc, 0, "为什么会变成这样呢？？", pc.Name);
                        Say(pc, 0, "不管怎么样，先想办法再弄一些种子来吧……", pc.Name);
                        Say(pc, 0, "在鱼缸团城寨外收集5个$CR谷物种子（条纹）$CD和5个$CR果实种子（尖刺）$CD吧。", " ");
                        return;
                    case 2:
                        Say(pc, 0, "…………", pc.Name);
                        Wait(pc, 3000);
                        Say(pc, 0, "……不，$R这毕竟是别人给的东西，这样不好。", pc.Name);
                        Say(pc, 0, "来，小吱，吃吧。", pc.Name);
                        TakeItem(pc, 10104406, 5);
                        TakeItem(pc, 10104404, 5);
                        pc.CInt["魔导师转职任务"] = 6;
                        Say(pc, 0, "啾，啾啾——", "小吱？");
                        Say(pc, 0, "（沙鼠把种子吃掉了）", " ");
                        Say(pc, 0, "啾啾，啾啾。", "小吱？");
                        Say(pc, 0, "（沙鼠做出讨要的动作，彷佛还想要种子）", " ");
                        Say(pc, 0, "？？？", pc.Name);
                        Say(pc, 0, "会不会我真的拿种子去$R交咖啡馆任务更好呢？$R$R顺便来锅鼠肉汤似乎也不错呢呵呵呵呵。", pc.Name);
                        Say(pc, 0, "哈啊……不行，$R还有事要请教她呢，$R$R先想办法再弄一些种子来吧……", pc.Name);
                        Say(pc, 0, "在鱼缸团城寨外$R收集5个$CR谷物种子（条纹）$CD和5个$CR果实种子（尖刺）$CD吧。", pc.Name);
                        return;
                }
        }

        private void 魔导师转职任务6(ActorPC pc)
        {
            if (CountItem(pc, 10104406) >= 5 && CountItem(pc, 10104404) >= 5)//检测道具
            {
                TakeItem(pc, 10104406, 5);
                TakeItem(pc, 10104404, 5);
                GiveItem(pc, 140000001, 1); //获得 沙鼠的护身符
                pc.CInt["魔导师转职任务"] = 7;
                NPCShow(pc, 80000612);//沙鼠·阿鲁玛现形
                NPCHide(pc, 80000611);//沙鼠隐身
                Say(pc, 0, "小吱？抢过你的食物后，$R飞快地啃光了，$R然后变成一个可爱的美少女。", " ");
                Say(pc, 0, "哇啊~~$R$R饿得我连变身的力气都没了，终于吃饱了。", "小吱？");
                Select(pc, " ", "", "你……你就是小吱？");
                Say(pc, 0, "对！在下小吱是也。$R你是……受星麻麻的委托来找我的吗？", "小吱");
                Select(pc, " ", "", "是的，你这是……？");
                Say(pc, 0, "咦，不用那么惊讶吧，$R这个世界上「阿鲁玛」，$R也就是所谓的「人形化」应该是$R很常见的才对啊。", "小吱");
                Say(pc, 0, "我也是饿得没力气了$R才不得已变回原型的……", "小吱");
                Say(pc, 0, "星麻麻是让你来找我回去吧？$R$R……啊，但我暂时还不能回去，$R冒险者，请你帮忙把这个交给星麻麻吧。", "小吱");
                Say(pc, 0, "把这个交给她，她看到就懂了！", "小吱");
                Select(pc, " ", "", "……");
                return;
            }
            else
            {
                Say(pc, 0, "啾啾，啾啾。", "小吱？");
                Say(pc, 0, "（沙鼠做出讨要的动作，彷佛还想要种子）", " ");
                Say(pc, 0, "没办法，再去收集$R5个$CR谷物种子（条纹）$CD$R和5个$CR果实种子（尖刺）$CD吧。", pc.Name);
            }
            return;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10104406) >= 5 && CountItem(pc, 10104404) >= 5) //检测种子持有数为5
            {
                if (pc.CInt["魔导师转职任务"] == 5)
                {
                    魔导师转职任务5(pc);
                    return;
                }
            }
            if (pc.CInt["魔导师转职任务"] == 6)
            {
                魔导师转职任务6(pc);
                return;
            }
            if (pc.CInt["魔导师转职任务"] == 7)
            {
                Say(pc, 0, "星麻麻是让你来找我回去吧？$R$R……啊，但我暂时还不能回去，$R冒险者，请你帮忙把这个交给星麻麻吧。", "小吱");
                Say(pc, 0, "把这个交给她，她看到就懂了！", "小吱");
                return;
            }
        }
    }
}