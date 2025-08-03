using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:木偶使行會(30049000) NPC基本信息:木偶使總管(11000024) X:3 Y:3
namespace SagaScript.M30049000
{
    public class S11000024 : Event
    {
        public S11000024()
        {
            this.EventID = 11000024;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Puppetry> Puppetry_amask = pc.AMask["Puppetry"];
            BitMask<Puppetry> Puppetry_cmask = pc.CMask["Puppetry"];

            int sel1 = 0, sel2 = 0;

            Say(pc, 131, "欢迎光临人偶师行会$R;");
            if (Puppetry_cmask.Test(Puppetry.第一次对话))
            {
                do
                {
                    sel2 = Select(pc, "做什么呢??", "", "买东西", "制作石像", "询问石像的使用方法", "询问关于石像出售的问题", "制造魔物模型", "关于魔物模型", "什么也不做");
                    switch (sel2)
                    {
                        case 1:
                            OpenShopBuy(pc, 66);
                            break;
                        case 2:
                            Synthese(pc, 2038, 1);
                            break;
                        case 3:
                            石像介绍(pc);
                            break;
                        case 4:
                            Say(pc, 131, "您知道通信出售是什么吗？$R;" +
                                "$R就是您看完目录以后$R在原地预定的话，$R;" +
                                "$R在2-3天内商品就会到达的$R;" +
                                "非常便利的东西。$R;" +
                                "$P石像通信出售是更加便利的$R;" +
                                "$R如果看目录以后选购的话$R就可以直接得到商品了。$R;" +
                                "是不是很便利呢？$R;");
                            do
                            {
                                sel1 = Select(pc, "怎么办呢?", "", "使用石像目录。", "看目录", "在目录上登记。", "回去");
                                switch (sel1)
                                {
                                    case 1:
                                        GiveItem(pc, 10029600, 1);
                                        Say(pc, 131, "在目录上登记的话需要500金币。$R;" +
                                            "$R如果有钱的话可以试一下。$R;" +
                                            "得到石像目录。$R;");
                                        break;
                                    case 2:
                                        Say(pc, 131, "看目录的方法更简单了。$R;" +
                                            "$R双击『石像目录』就可以了！$R;" +
                                            "$P但是有一点要注意。$R;" +
                                            "在这一章目录上显示的$R只是这目录的内容。$R;" +
                                            "$R并不会显示别的目录的内容。$R;");
                                        break;
                                    case 3:
                                        Say(pc, 131, "想登陆商品到目录上的话，$R就在店面设定时$R;" +
                                            "在登陆与否的选项，选定就可以了$R;" +
                                            "$R这样店面就会自动登录到目录上。$R;" +
                                            "$P但是每登陆一次$R就要花『500金币』。$R;" +
                                            "$R想要增加销售额，$R是不是应该试一试呢。$R;");
                                        break;
                                    case 4:
                                        break;
                                }
                            } while (sel1 != 4);
                            break;
                        case 5:
                            Synthese(pc, 2024, 1);
                            break;
                        case 6:
                            Say(pc, 131, "魔物模型是可以装饰在$R;" +
                                "飞空庭上的魔物娃娃。$R;" +
                                "$P如果有魔物样子的绘画$R;" +
                                "只要有画板（画作完成）就可以得到$R;" +
                                "和它一样的娃娃$R;" +
                                "$P您知道怎么绘画魔物吗？$R;");
                            switch (Select(pc, "知道吗?", "", "知道", "不知道"))
                            {
                                case 1:
                                    Say(pc, 131, "那就做一个漂亮的比基亚吧。$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "要绘画就需要『画板』和$R;" +
                                        "「魔物绘图」两种必要道具。$R;" +
                                        "$R要做『画板』的$R;" +
                                        "就要拜托给贤者帮忙了。$R;" +
                                        "$P知道画板的制作方法吗？$R;");
                                    switch (Select(pc, "知道吗", "", "知道", "不知道"))
                                    {
                                        case 1:
                                            Say(pc, 131, "那就做一个漂亮的比基亚吧。$R;");
                                            break;
                                        case 2:
                                            Say(pc, 131, "制作方法如下。$R;" +
                                                "$R需要的道具有『刷子』，$R技能要『精制道具』3级，$R;" +
                                                "$R材料有『木浆卷』一个、$R;" +
                                                "$R『杰利科』四个和『木材』一个$R;" +
                                                "$P绘图不是一定会成功的。$R;" +
                                                "$P所以最好是多准备一个$R;" +
                                                "画板比较好。$R;");
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 7:
                            break;
                    }
                } while (sel2 == 3 || sel2 == 4);
                return;
            }
            if (Puppetry_amask.Test(Puppetry.已經獲得木偶) || pc.Marionette != null)
            {
                Say(pc, 131, "您好像看过神秘的『活动木偶』了。$R;" +
                    "$P如果看过了，$R那就应该给您介绍『石像』吧！$R;");
                第一次对话(pc);
                return;
            }
            Say(pc, 131, "我是人偶师总管$R;" +
                "$P这个世界上有很多神秘的东西，$R;" +
                "$R活动木偶可算是当中最神秘的哦！$R;");
        }

        void 第一次对话(ActorPC pc)
        {
            BitMask<Puppetry> Puppetry_cmask = pc.CMask["Puppetry"];

            Say(pc, 131, "石像是活动木偶的变异形态，$R全身散发着神秘感$R;" +
                "$R他们是拥有独立思想的活动木偶$R;" +
                "$P只要给活动木偶能够思考的道具的话$R它们就会成为石像了$R;" +
                "$P石像通常在我们休息的时候活动的$R;" +
                "$P他们会帮我们看店或者击退魔物。$R;" +
                "$R风雨不改，为我们辛勤工作的$P就是石像啰$R;");
            if (!Puppetry_cmask.Test(Puppetry.第一次对话))
            {
                Say(pc, 131, "来，给您个好东西。$R;" +
                    "名字是『石像杯』$R;" +
                    "$P它是能够让活动木偶$R有思考能力的道具。$R;" +
                    "$R和活动木偶合成的话$R就会变成石像了。$R;" +
                    "$P拥有思考能力的石像$R是不能复原到活动木偶状态的。$R;" +
                    "$R所以在合成的时候，要好好想清楚喔！$R;");
                if (CheckInventory(pc, 10002100, 1))
                {
                    Puppetry_cmask.SetValue(Puppetry.第一次对话, true);
                    GiveItem(pc, 10002100, 1);
                    Say(pc, 131, "得到石像金寶利$R;");
                    石像介绍(pc);
                    return;
                }
                Say(pc, 131, "…$R;" +
                    "$R咦？不能给您哦$R;" +
                    "把行李整理好再来吧。$R;");
                return;
            }
            石像介绍(pc);
        }

        void 石像介绍(ActorPC pc)
        {

            switch (Select(pc, "问什么呢?", "", "石像是什么？", "让人看店的方法", "搜集道具的方法", "回去"))
            {
                case 1:
                    第一次对话(pc);
                    break;
                case 2:
                    Say(pc, 131, "让石像看店的，就叫『石像商店』。$R;" +
                        "$P把石像商店就当作是自己的店就好了。$R;" +
                        "$R里面出售的什么道具，价格高低$R都由您自己定！$R;" +
                        "$P设定石像商店的方法很简单。$R;" +
                        "$P用道具控制石像，就可以让它$R;" +
                        "做您想命令它做的事情了。$R;" +
                        "$P选择『看店』的话$R就可以设定石像商店了。$R;" +
                        "$P设定想要出售的道具、价格、数量等$R;" +
                        "$R三个项目后，点击OK键。$R;" +
                        "$P设定就完成了。$R;" +
                        "$P结束冒险后想休息时$R选择『交给石像后退出』就可以了。$R;" +
                        "这样的话$R石像就会自动为您出售道具了。$R;" +
                        "$P怎样?简单吧？$R;");
                    石像介绍(pc);
                    break;
                case 3:
                    Say(pc, 131, "在您休息的时候$R可以让石像搜集道具。$R;" +
                        "$R可以搜集的东西是一下八种。$R;" +
                        "『食物、矿物、植物、魔法物、$R;" +
                        "宝物箱、出土品、各种物件、$R奇怪的东西等』。$R;" +
                        "$P虽然可能会搜集到『杰利科』$R那样一般的东西，$R;" +
                        "但是偶尔也会搜集到稀有的道具的。$R;" +
                        "$R大致上都是已定的，但是实际上$R;" +
                        "您能够搜集到什么道具$R就要看您的『运气』了。$R;" +
                        "$P各种石像都有自己擅长搜集的道具类型$R;" +
                        "$R像鱼人那样喜欢吃的石像$R;" +
                        "一定会在搜集食物上有一套吧？$R;" +
                        "$P像让它看店的时候$R使用道具的话，可以设定各种性能$R;" +
                        "$P设定完毕后点击$R『交给石像后退出』就可以了。$R;" +
                        "$R呵呵...听起来很不错啊，呵呵。$R;" +
                        "$P石像拥有的道具$R是放在『石像仓库』里的。$R;" +
                        "$R在石像仓库里的东西，如果不移到$R道具栏的话就不能使用。$R;" +
                        "$P石像仓库是在石像设定目录里$R选择的话就可以看了。$R;" +
                        "$R道具是不会消失或者丢失的，$R所以放心吧。$R;");
                    石像介绍(pc);
                    break;
                case 4:
                    break;
            }
        }
    }
}
