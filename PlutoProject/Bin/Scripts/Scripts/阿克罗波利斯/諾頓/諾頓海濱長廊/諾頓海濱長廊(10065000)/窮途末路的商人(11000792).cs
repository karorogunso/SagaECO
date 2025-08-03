using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000792 : Event
    {
        public S11000792()
        {
            this.EventID = 11000792;
        }

        public override void OnEvent(ActorPC pc)
        {

            int selection;
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            Say(pc, 131, "太可怕了，我不知道要等到什么时候$R;");
            if (!mask.Test(NDFlags.窮途末路的商人结束))
            {
                switch (Select(pc, "搭话吗？", "", "怎么了？", "不搭话"))
                {
                    case 1:
                        Say(pc, 131, "您是冒险者吧，什么时候到这来的$R见过大商人古鲁杜了吗？$R;" +
                            "$P古鲁杜是我们商人行会里，$R交易额居冠的大商人唷$R;" +
                            "$P…$R;" +
                            "$P是吗…好像不清楚…$R;" +
                            "$R哎呀，也不知道到底等到什么时候$R;");
                        selection = Select(pc, "搭话吗？", "", "为什么坐立不安呢", "这是什么动物？", "不搭话");
                        while (selection != 3)
                        {
                            switch (selection)
                            {
                                case 1:
                                    mask.SetValue(NDFlags.窮途末路的商人对话一, true);
                                    Say(pc, 131, "那是因为，我有点害怕照顾这动物呀$R;" +
                                        "$P古鲁杜把动物委托我照顾后，$R自己去旅行了，让我等他回来$R;" +
                                        "$P这傢伙平时很温顺，$R但有时会露出可怕的牙齿$R或吐出火来$R;" +
                                        "$R我担心有天会被它咬，或变成碳阿$R;");
                                    break;
                                case 2:
                                    mask.SetValue(NDFlags.窮途末路的商人对话二, true);
                                    Say(pc, 131, "这是『炽色步行龙』是吗$R;" +
                                        "$P是古鲁杜根据机械时代的文献记载，$R用传说中的生物名字命名的$R;");
                                    break;
                                case 3:
                                    return;
                            }
                            if (mask.Test(NDFlags.窮途末路的商人对话一) && mask.Test(NDFlags.窮途末路的商人对话二))
                            {
                                Say(pc, 131, "跟您说话现在好多了$R;" +
                                    "$R说来话长，想不想听呢？$R;");
                                switch (Select(pc, "怎么办呀？", "", "听", "不听"))
                                {
                                    case 1:
                                        mask.SetValue(NDFlags.窮途末路的商人结束, true);
                                        Say(pc, 131, "谢谢$R;" +
                                            "$R那件事情是在我们行会商人在$R诺森北部遇难时发生的。$R;");
                                        Say(pc, 131, "那些商人为了找新的商团，$R正要到诺森北部$R;" +
                                            "$R在『永远的北方边界』$R遭到了魔物的袭击阿$R;" +
                                            "$P古鲁杜、我、复活战士救助队$R接到消息后，就向遇难地点前进了$R;" +
                                            "$R幸好成功把所有人救出来了，$R但行李全部被魔物抢走了$R;");
                                        Say(pc, 131, "剩下的行李裡头，$R有没见过的几个很大的蛋$R;" +
                                            "$R商人说，那些蛋是从冻僵的大地上，$R而且比机械时代更久远的$R地层发现的。$R;" +
                                            "$P古鲁杜拿着蛋，拿出桃子一样的东西，$R说是发掘品什么的？$R用超声波注射剂$R;" +
                                            "$R注入那颗蛋里$R;");
                                        Say(pc, 131, "几天后从那颗蛋里出来的$R就是『炽色步行龙』$R;" +
                                            "$R瞬间变的很大，$R到现在变成这么大了。$R;" +
                                            "$P古鲁杜说去拿给别的蛋里$R注射的桃子，就不见了$R;" +
                                            "$R遗传基因、派生物…？$R这些是机械时代发掘药品的主成分。$R我也不知道是什么意思…$R;" +
                                            "$P让我们在这里照顾德拉古…$R然后等他回来…$R太过份了吧$R;");
                                        Say(pc, 131, "说了这么长时间。对不起$R;" +
                                            "$R所以我只能在这裡等著古鲁杜回来$R;");
                                        break;
                                    case 2:
                                        Say(pc, 131, "是吗？$R;" +
                                            "$R没关系，是我硬要求的，不要介意$R;");
                                        break;
                                }
                            }
                            selection = Select(pc, "搭话吗？", "", "为什么坐立不安呢", "这是什么动物？", "不搭话");
                        }
                        break;
                    case 2:
                        break;
                }
                return;
            }
        }
    }
}
