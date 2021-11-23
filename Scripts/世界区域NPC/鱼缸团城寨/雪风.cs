
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaDB;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S60000021 : Event
    {
        public S60000021()
        {
            this.EventID = 60000021;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.Account.GMLevel > 200)
            {
                if (Select(pc, "咕咕任务调试控制台", "", "清除任务记录", "什么都不做") == 1)
                {
                    pc.CInt["天天1任务标记"] = 0;
pc.CInt["天天1任务技能点获得"] = 0;
                }
            }
            string gander = "大哥哥";
            if (pc.Gender == PC_GENDER.FEMALE)//如果为女性，则变成大姐姐
                gander = "大姐姐";
            if (pc.CInt["天天1任务标记"] < 1)//检查任务标记
            {
                Say(pc, 131, "那个，那边的"+ gander + "！！", "雪风");
                Say(pc, 131, "我有一事相求！！请问可以帮我一个忙吗？", "雪风");
                if (Select(pc, " ", "", "是什么忙呢？", "不好意思，我很忙") == 1)
                {
                    Say(pc, 131, "你可以帮我收集一根$CR胡萝卜$CD吗？", "雪风");
                    Select(pc, "", "", "好呀，不过你为什么要胡萝卜呢？");
                    Say(pc, 131, "是这样的，$R你知道「$CC传说中的肝神·天天$CD」吗？", "雪风");
                    Wait(pc, 200);
                    Say(pc, 131, "她是我们这里最最最最努力的人啦，$R我很仰慕她呢！", "雪风");
                    Wait(pc, 200);
                    Say(pc, 131, "昨天早上我出门拔萝卜的时候，$R遇到凶狠的兔子，抢走了我的胡萝卜！", "雪风");
                    Wait(pc, 200);
                    Say(pc, 131, "兔子还要袭击我，$R这时，天天出现了，她跟兔子打了起来，$R$R我，我不知道要怎么办，所以逃了回来。", "雪风");
                    Wait(pc, 1000);
                    Say(pc, 131, "回来之后冷静想了一下，$R天天是我的救命恩人啊，$R$R所以…嗯……，$R我想送她礼物，报答她的救命之恩。", "雪风");
                    Wait(pc, 200);
                    Say(pc, 131, "可是，$R经过昨天的事情，我已经不敢去拔萝卜了。", "雪风");
                    Wait(pc, 200);
                    Say(pc, 131, "我好怕会再次遇到那只凶狠的兔子，$R呜哇哇~~~~~", "雪风");
                    Wait(pc, 200);
                    Say(pc, 131, "所以" + gander + "，$R你能不能替我收集一根$CR胡萝卜$CD呢？", "雪风");
                    Wait(pc, 200);
                    Select(pc, " ", "", "好，我去帮你。");
                    Say(pc, 131, "太好了，" + gander + "你真是个好心人。", "雪风");
                    pc.CInt["天天1任务标记"] = 1;//任务标记
                    return;//结束脚本
                }
            }
            else if(pc.CInt["天天1任务标记"] == 1)//检查任务标记
            {
                if(CountItem(pc, 10004200) >= 1)//检查玩家有身上胡萝卜，并数量大于等于1
                {
                    Say(pc, 111, "哇！！你带来了，谢谢" + gander + "！！", "雪风");
                    Wait(pc, 1000);
                    Say(pc, 111, "可是只有一根胡萝卜总觉得有点单调呢？", "雪风");
                    Wait(pc, 200);
                    Say(pc, 111, "对了，我听说这个世界有一种$R可以让武器变得更强的$CC武器强化石$CD！$R$R天天姐姐似乎很想要这种石头，$R我想把它送给天天姐姐，那一定是最好的。$R可是要上哪弄这种石头呢……$R我不知道……", "雪风");
                    Wait(pc, 200);
                    Say(pc, 111, "所以" + gander + "，你能再帮我一个忙吗？$R真的是最后一个忙了！！$R帮我收集一个$CC武器强化石$CD吧，好不好？", "雪风");
                    Wait(pc, 200);
                    Select(pc, " ", "", "……既然已经答应，只能好人帮到底了");
                    pc.CInt["天天1任务标记"] = 2;//任务标记
                    return;//结束脚本
                }
                else//没有胡萝卜的话
                {
                    Say(pc, 131, "所以" + gander + "，你能不能代替我收集一根$CR胡萝卜$CD呢？", "雪风");
                    return;
                }
            }
            else if(pc.CInt["天天1任务标记"] == 2)//检查任务标记
            {
                if (CountItem(pc, 960000001) >= 1 && CountItem(pc, 10004200) >= 1)//检查玩家身上有武器强化石，并数量大于等于1
                {
                    TakeItem(pc, 10004200, 1);
                    TakeItem(pc, 960000001, 1);
                    pc.CInt["天天1任务标记"] = 3;
                    GiveItem(pc, 100042001, 1);//给玩家1个内藏惊喜的胡萝卜
                    Say(pc, 111, "哇，哇哇哇！！$R" + gander + "你真的是太好了！！$R$R事不宜迟，$R我们赶紧把胡萝卜和强化石送给天天姐姐吧", "雪风");
                    Say(pc, 111, "对了，这样……$R把强化石，塞到萝卜里面……$R完成了，「内藏惊喜的胡萝卜」！$R$R那么" + gander + "……$R你能不能把这个带有强化石的胡萝卜，$R帮忙转交给天天姐姐呢？", "雪风");
                    Select(pc, " ", "", "……");
                    Say(pc, 111, "真的真的真的！！$R这是最后的最后的最后一个忙了！！$R$R之后我保证不会再麻烦你了。$R好嘛？", "雪风");
                    Select(pc, " ", "", "好好好，好好好。");

                    return;//结束脚本
                }
                else//没有的话
                {
                    Say(pc, 111, "所以" + gander + "，你能再帮我一个忙吗？$R真的是最后一个忙了！！$R帮我收集一个$CC武器强化石$CD吧，好不好？$R$R啊，对了，$R还有刚才的胡萝卜也要拿来！", "雪风");
                    return;
                }
            }
            else if(pc.CInt["天天1任务标记"] == 3)
            {
                Say(pc, 111, "你能不能把这个带有强化石的胡萝卜，$R帮忙转交给天天姐姐呢？", "雪风");
                return;//结束脚本
            }
            
        }
    }
}