using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000844 : Event
    {
        public S11000844()
        {
            this.EventID = 11000844;

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<LightTower> LightTower_mask = pc.CMask["LightTower"];
            
            if (LightTower_mask.Test(LightTower.給予懷錶))
            {
                Say(pc, 131, "听着怀表的声音$R;" +
                    "就会觉得他在我的身边呢$R;" +
                    "$R太感谢您了…$R;");
                return;
            }

            if (CountItem(pc, 10024680) > 0)
            {
                LightTower_mask.SetValue(LightTower.給予懷錶, true);
                //_6a59 = true;
                TakeItem(pc, 10024680, 1);
                Say(pc, 131, "啊！这是？他的…$R;" +
                    "您在哪里得到这个的？$R;" +
                    "$R原来如此…您去过那座塔呀$R;" +
                    "$P谢谢...太感谢您了$R;" +
                    "$R没错，他肯定就是我死去的爱人。$R;" +
                    "原来很久以前就出事了…$R;" +
                    "$P这个怀表没有指针对吧?$R;" +
                    "那个指针在我这儿呢$R;" +
                    "$P是他去塔之前，在这棵树下$R;" +
                    "托我保管的…$R;" +
                    "$R每当伤心落泪时，我拿着指针，$R;" +
                    "时间就像是停留一般$R;" +
                    "“没关系…很快会回来的…”$R;" +
                    "说完这句话就走了…$R;" +
                    "$P现在这个表也可以动了$R;" +
                    "谢谢...太感谢您了$R;");
                return;
            }

            Say(pc, 131, "您好！小不点$R;" +
                "$R问我在干什么是吗？$R;" +
                "$P正在等人呢$R;" +
                "$R很久以前就约好了$R;" +
                "在这个树下等他$R;" +
                "$R您想听听有关我的故事吗?$R;");
            switch (Select(pc, "听不听奶奶的故事呢？", "", "听", "不听"))
            {
                case 1:
                    Say(pc, 131, "那想听一听吗?$R;" +
                        "$R从这裡往西走$R;" +
                        "有机械文明时代$R;" +
                        "未完成的建筑物唷$R;" +
                        "$P现在什么也没有，成了废墟！$R;" +
                        "$R以前摩戈和艾恩萨乌斯$R;" +
                        "还是联合的时候，曾打算重建呀$R;" +
                        "$P艾恩萨乌斯的人，$R;" +
                        "希望把它建成后，$R;" +
                        "想向世界展示自己的技术能力阿$R;" +
                        "$P那时，摩戈的所有居民，$R;" +
                        "为了重建这座塔而努力工作呀$R;" +
                        "$R我的爱人也一样$R;" +
                        "$P他离开我的时候跟我约好$R;" +
                        "$R在这个地方…$R;" +
                        "在这棵树下见面$R;" +
                        "$P但是他一直没有回来$R;" +
                        "只是听说塔上发生了很大的事故，$R;" +
                        "他也被卷进去了$R;" +
                        "$R我…我…伤心过度$R;" +
                        "怎么也不肯相信阿$R;" +
                        "$P也想过去找他$R;" +
                        "不过事故发生后$R;" +
                        "塔内出现了魔物去不了…$R;" +
                        "$R现在只能在这里等他回来$R;" +
                        "$P虽然现在我想他不会回来了$R;" +
                        "$R但是这个地方对我来说$R;" +
                        "非常重要的阿。$R;" +
                        "$P唉…谢谢您听完我的故事$R;" +
                        "$R我一直会在这里，$R;" +
                        "再来找我吧$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}