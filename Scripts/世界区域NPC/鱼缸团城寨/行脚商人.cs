
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
    public class S80000000 : Event
    {
        public S80000000()
        {
            this.EventID = 80000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "都是一些常用的东西，$R随便挑随便看，呵呵呵~$R$R", "行脚商人");
            switch (Select(pc, "请选择购买种类", "", "基础装备", "印章收集", "离开"))
            {
                case 1:
                    OpenShopByList(pc, 10000, SagaDB.Npc.ShopType.None, 10001911, 10045400, 60030000, 50090100, 60050100, 60050800, 60060500, 60071100, 60080900);
                    break;
                case 2:
                    Say(pc, 131, "哦霍霍，$R你是怎么知道我在寻找$R『盖满印章』的『印章收集册』的？$R$R", "行脚商人");
                    Say(pc, 131, "嗯？$R你问我收集这东西干嘛？$R$R其实我也不知道，$R是一股带着番茄味的$R『神秘力量』驱使着我这么做的。", "行脚商人");
                    Say(pc,131, "每次接手『印章收集册』的时候，$R我都要把我的家当给出去一点，$R$R再这样下去我得要破产了啊...", "行脚商人");
                    switch(Select(pc,"怎么办呢？","","提交『草原收集册』","那你为什么不罢工呢？","离开"))
                    {
                        case 1:
                            if(CheckStampGenre(pc, StampGenre.Pururu))
                            {
                                ClearStampGenre(pc, StampGenre.Pururu);
                                Say(pc, 131, "哦？$R你盖满了『草原收集册』啊。$R$R我看看……", "行脚商人");
                                Say(pc, 131, "很不错，拿着，这是你的奖励。$R$R这份盖满的收集页我就收走了，$R我再给你份新的，$R你再盖满它的时候可以再来找我。", "行脚商人");

                                GiveItem(pc, 910000116, 5);
                                pc.Gold += 100000;
                                GiveItem(pc, 910000104, 1);
                                SInt["系统印章收集册"]++;
                                PlaySound(pc, 3424, false, 100, 50);
                                TitleProccess(pc, 14, 1);
                            }
                            else
                            {
                                Say(pc,131,"哎，你没盖满呢。", "行脚商人");
                            }
                            break;
                        case 2:
                            Say(pc,131, "其实我本人本来很喜欢『印章收集册』的。$R$R突然有一天晚上做梦，$R梦见一个绿头发的家伙叫我收集每个人手上盖满印章的『印章收集册』！" +
                                "$R$R虽然我很不情愿，$R但不知不觉已经收集了这么多了。$R（行脚商人指了指箱子里的收集册）$R（其实里面只有"+SInt["系统印章收集册"]+"本）", "行脚商人");
                            Say(pc,131,"其实那家伙长得挺可爱的……", "行脚商人");
                            Say(pc,131,"真希望能在现实中见到她一次啊……$R$R啊，话扯远了，$R其实我只希望那个绿头发的家伙，$R能够来把我的损失都挽救回来。$R$R哎——", "行脚商人");
                            Select(pc, " ", "", "你不知道梦都是假的吗？", "你永远都见不到她的！");
                            Say(pc,131, "虽然那是一场梦，$R但那股$CG番茄味的清香$CD让我永生难忘啊..$R$R喂！你难道有什么意见吗？！", "行脚商人");
                            Say(pc,131,"因为她在梦里说，$R当我收集到$CR10000本$CD的时候，$R她就会来见我的！！$R$R可是，还有好遥远啊...", "行脚商人");
                            Select(pc," ","","真可伶...","我看你是永远都收集不到了...", "行脚商人");
                            Say(pc,131,"不试试怎么知道呢！$R$R真的..真的好想再见一面...", "行脚商人");
                            break;
                    }
                    break;
            }
        }
    }
}