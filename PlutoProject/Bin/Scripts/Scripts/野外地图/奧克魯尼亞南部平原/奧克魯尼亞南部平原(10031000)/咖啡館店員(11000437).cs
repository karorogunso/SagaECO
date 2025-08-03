using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Quests;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:咖啡館店員(11000437) X:119 Y:118
namespace SagaScript.M10031000
{
    public class S11000437 : Event
    {
        public S11000437()
        {
            this.EventID = 11000437;

            this.notEnoughQuestPoint = "哎呀，剩余的任务点数是0啊$R;" +
                "下次再来吧$R;";
            this.leastQuestPoint = 1;
            this.questFailed = "失败了?$R;" +
                "$R真是遗憾阿$R;" +
                "下次一定要成功喔$R;";
            this.alreadyHasQuest = "任务顺利吗?$R;";
            this.gotNormalQuest = "那拜托了$R;" +
                "$R等任务结束后，再来找我吧;";
            this.gotTransportQuest = "是啊，道具太重了吧$R;" +
                "所以不能一次传送的话$R;" +
                "分几次给就可以！;";
            this.questCompleted = "真是辛苦了$R;" +
                "$R任务成功了$R来！收报酬吧！;";
            this.transport = "哦哦…全部收来了吗?;";
            this.questCanceled = "嗯…如果是你，我相信你能做到的$R;" +
                "很期待呢……;";
            this.questTooEasy = "唔…但是对你来说$R;" +
                "说不定是太简单的事情$R;" +
                "$R那样也没关系嘛?$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Emil_Letter> Emil_Letter_mask = new BitMask<Emil_Letter>(pc.CMask["Emil_Letter"]);

            if (!Emil_Letter_mask.Test(Emil_Letter.埃米爾介紹書任務完成) &&
                CountItem(pc, 10043081) > 0)
            {
                埃米爾介紹書(pc);
                return;
            }

            switch (Select(pc, "欢迎光临酒馆!", "", "任务服务台", "什么也不做"))
            {
                case 1:
                    HandleQuest(pc, 6);
                    break;

                case 2:
                    break;
            }
        }

        void 埃米爾介紹書(ActorPC pc)
        {
            BitMask<Emil_Letter> Emil_Letter_mask = new BitMask<Emil_Letter>(pc.CMask["Emil_Letter"]);

            int selection;

            TakeItem(pc, 10043081, 1);

            Say(pc, 11000437, 131, "哎呀!!$R;" +
                                   "$R这不是『埃米尔介绍信』吗?$R;" +
                                   "$P看您拿着这封信，您是初心者吧?$R;" +
                                   "$R这里是「阿克罗波利斯下城」的$R;" +
                                   "「酒馆」的分店喔。$R;" +
                                   "$P刚开始冒险时，会有点辛苦的。$R;" +
                                   "$R这是我做的果酱哦，$R;" +
                                   "不嫌弃的话，在冒险的时候用吧。$R;", "酒馆店员");

            PlaySound(pc, 2042, false, 100, 50);
            GiveItem(pc, 10033900, 1);
            Say(pc, 0, 0, "得到『果酱』!$R;", " ");

            Say(pc, 11000437, 131, "那么，要挑战任务吗?$R;", "酒馆店员");

            selection = Select(pc, "想要挑战任务吗?", "", "有什么样的任务?", "挑战任务", "放弃");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        任務種類詳細解說(pc);
                        break;

                    case 2:
                        擊退皮露露(pc);
                        break;

                    case 3:
                        break;
                }

                selection = Select(pc, "想要挑战任务吗?", "", "有什么样的任务?", "挑战任务", "放弃");
            }
        }

        void 任務種類詳細解說(ActorPC pc)
        {
            int selection;

            Say(pc, 11000437, 131, "任务的要求几乎都很简单喔!$R;" +
                                   "$R「酒馆」除了卖粮食外，$R;" +
                                   "也会介绍一些工作给冒险者哦!$R;" +
                                   "$P久而久之，口碑越来越好了，$R;" +
                                   "所以在「阿克罗波利斯」周围，$R;" +
                                   "开了许多分店。$R;" +
                                   "$R最近魔物比较多，有点害怕呀…$R;" +
                                   "$P工作内容有「击退魔物」、$R;" +
                                   "「收集/搬运道具」等。$R;" +
                                   "$R当然，我们会根据任务内容，$R;" +
                                   "给予不同的报酬哦!$R;" +
                                   "$P工作内容不同，$R;" +
                                   "任务执行方式也不一样。$R;" +
                                   "$R想听详细的说明吗?$R;", "酒馆店员");

            selection = Select(pc, "想听什么说明呢?", "", "任务的注意事项", "关于「击退任务」", "关于「收集任务」", "关于「搬运任务」", "什么也不听");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000437, 131, "成功完成任务的话，$R;" +
                                               "可以得到相对应的经验值和报酬。$R;" +
                                               "$R但托付的任务并不是很多，$R;" +
                                               "有时候会供过于求，不太平衡啊!$R;" +
                                               "$P所以工作是有次数限制的。$R;" +
                                               "$R真是非常抱歉，因为还有别的冒险者，$R;" +
                                               "所以没办法只好这样，请您谅解呀!$R;" +
                                               "$P除此之外，$R;" +
                                               "为了避免有人承接任务却没有回报，$R;" +
                                               "所以任务都定了时限呀!$R;" +
                                               "$R规定时间内没有完成任务，$R;" +
                                               "就会当作任务失败哦!$R;" +
                                               "这个任务，就会给别的冒险者了。$R;" +
                                               "$P剩余的任务点数和任务所剩时间，$R;" +
                                               "可以在「任务视窗」确认喔。$R;" +
                                               "$R尽量不要失败，$R;" +
                                               "请您努力吧!$R;", "酒馆店员");
                        break;

                    case 2:
                        Say(pc, 11000437, 131, "「击退任务」就是要在指定的区域，$R;" +
                                               "抓到指定数量的魔物。$R;" +
                                               "$P例如：$R;" +
                                               "击退「阿克罗尼亚东方平原」的$R;" +
                                               "5只「皮露露」。$R;" +
                                               "$R接受这样的任务时，$R;" +
                                               "只要抓住指定区域的5只「皮露露」，$R;" +
                                               "任务就算成功了呢!$R;" +
                                               "$P其他地方的「皮露露」，$R;" +
                                               "并不会列入计算的。请多留意呀!$R;" +
                                               "$P委托内容和完成进度等，$R;" +
                                               "可以在「任务视窗」随时确认哦!$R;" +
                                               "$R执行任务时，只要打开这个视窗，$R;" +
                                               "就可以随时确认，很方便吧?$R;" +
                                               "$P任务成功后，要记得回报，$R;" +
                                               "这样才可以拿到报酬喔。$R;" +
                                               "$R关于「报酬」，$R;" +
                                               "可以在任何附近的「任务服务台」拿到，$R;" +
                                               "所以只要到附近的「服务台」就可以了。$R;", "酒馆店员");
                        break;

                    case 3:
                        Say(pc, 11000437, 131, "「收集任务」就是收集指定道具的任务唷!$R;" +
                                               "$P如果接到收集3个『杰利科』的任务。$R;" +
                                               "$R只要想尽办法收集3个『杰利科』，$R;" +
                                               "就算任务完成了。$R;" +
                                               "$P收集完以后，$R;" +
                                               "把道具拿到「任务服务台」就可以了。$R;" +
                                               "$R接受「收集任务」时，$R;" +
                                               "选择「任务服务台」$R;" +
                                               "就会显示交易视窗喔!$R;" +
                                               "$P把收集的道具，$R;" +
                                               "从道具视窗移到交易视窗的左边，$R;" +
                                               "$R点击『确认』再点击『交易』，$R;" +
                                               "道具就交易到「服务台」了。$R;" +
                                               "$P交易指定数量后，$R;" +
                                               "任务就算成功了。$R;" +
                                               "$R如果道具太重，$R;" +
                                               "一次交易不了，可以分批送出喔。$R;" +
                                               "$P我会清点交易的道具的。$R;" +
                                               "$R我不会算错啦，尽管放心!!$R;", "酒馆店员");
                        break;

                    case 4:
                        Say(pc, 11000437, 131, "「搬运任务」是从委托人那里取得道具，$R;" +
                                               "然后转交给收件人的任务哦!$R;" +
                                               "$P例如：$R;" +
                                               "在「阿克罗尼亚东方平原」的$R;" +
                                               "「酒馆店员」那力$R;" +
                                               "取得4个『杰利科』。$R;" +
                                               "$R然后拿给「阿克罗尼亚东方平原」的$R;" +
                                               "「鉴定师」。$R;" +
                                               "$P接到这样的任务的话，$R;" +
                                               "只要从我这里取得4个『杰利科』，$R;" +
                                               "送到「阿克罗尼亚东方平原」的$R;" +
                                               "「鉴定师」那里，就算成功了。$R;" +
                                               "$P要给予运送道具，$R;" +
                                               "只要跟相关的人交谈就可以了。$R;" +
                                               "$R任务成功以后，$R;" +
                                               "就跟「击退任务」一样，$R;" +
                                               "到「任务服务台」，拿取报酬就可以了。$R;", "酒馆店员");
                        break;
                }

                selection = Select(pc, "想听什么说明呢?", "", "任务的注意事项", "关于「击退任务」", "关于「收集任务」", "关于「搬运任务」", "什么也不听");
            }
        }

        void 擊退皮露露(ActorPC pc)
        {
            BitMask<Emil_Letter> Emil_Letter_mask = new BitMask<Emil_Letter>(pc.CMask["Emil_Letter"]);

            Say(pc, 11000437, 131, "最近「阿克罗波利斯」周围，$R;" +
                                   "出现了很多「皮露露」，$R;" +
                                   "能不能击退呢?$R;" +
                                   "$R「皮露露」是像布丁的天蓝色魔物。$R;" +
                                   "$P在任务清单选择任务后，$R;" +
                                   "点击『确认』，就可以接受任务了。$R;" +
                                   "$R那么，您想挑战吗?$R;", "酒馆店员");

            switch (Select(pc, "想怎么做呢?", "", "挑战任务", "再听一次说明", "放弃"))
            {
                case 1:
                    Emil_Letter_mask.SetValue(Emil_Letter.埃米爾介紹書任務完成, true);

                    HandleQuest(pc, 1);
                    break;

                case 2:
                    擊退皮露露(pc);
                    break;

                case 3:
                    break;
            }
        }
    }
}