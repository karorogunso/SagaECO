using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001098 : Event
    {
        public S11001098()
        {
            this.EventID = 11001098;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];

            if (!fgarden.Test(FGarden.铁板收集完毕))
            {
                Say(pc, 131, "嗯!$R;" +
                    "$R我们是飞空艇的庭院专家$R;" +
                    "「庭院大师」$R;");
                return;
            }

            Say(pc, 131, "我就是木偶制作大师蒂阿$R;" +
                "现在开始简单讲解$R『阿克罗尼亚航空法』$R;" +
                "$R可以开始了吗？$R;");
            switch (Select(pc, "准备……", "", "还没好", "OK"))
            {
                case 1:
                    break;
                case 2:
                    int a = 0;
                    do
                    {
                        Say(pc, 131, "先谈关于燃料的话题$R;" +
                            "飞空庭使用的燃料是『摩戈炭』哦，$R;" +
                            "$R是从叫摩戈的地方采集的炭。$R;" +
                            "$R可以在摩戈市的冶炼所或$R『家具匠人』那里购买。$R;" +
                            "$P如果职业是矿工的话，可以直接在$R;" +
                            "摩根市的采集场开采哦。$R;" +
                            "$R可以说是最经济的方法。$R;" +
                            "下面是关于乘搭须知。$R;" +
                            "$R飞空庭飞行时，乘坐在飞空庭的人$R都会一起移动。$R;" +
                            "飞行前，最基本的就是$R通知目的地$R;" +
                            "$P因为如果突然移动，$R会引起乘客恐慌呢$R;" +
                            "$P最后谈关于目的地。$R;" +
                            "$R只要有港口的地方都可以去。$R;" +
                            "$P不过不可以随意进入别国的领土，$R不然会违法的喔。$R;" +
                            "$P想去自己所属国家以外的领土时，$R;" +
                            "先从飞空庭下来，$R办理正式手续后入境，就可以了。$R;" +
                            "$P讲座到这里结束。$R;" +
                            "有什么要问的吗？$R;");
                        a = Select(pc, "都清楚了吗？", "", "还不太理解。", "OK");
                        switch (a)
                        {
                            case 1:
                                Say(pc, 131, "OK$R;" +
                                    "那么再说明一次吧。$R;");
                                break;
                            case 2:
                                Say(pc, 131, "OK$R;" +
                                    "$R这个规则很重要$R;" +
                                    "$R随时都可以为您说明，$R想问的时候，再过来吧。$R;");
                                if (fgarden.Test(FGarden.听完飞空庭飞行规则))
                                    return;

                                fgarden.SetValue(FGarden.听完飞空庭飞行规则, true);
                                Say(pc, 131, "加固工程已经结束了没有啊？$R;" +
                                    "$R木偶制作大师雪列娜$R;" +
                                    "工程结束了吗？$R;");
                                Say(pc, 11001026, 131, "已经结束了。$R;" +
                                    "给您讲解下一步说明，$R跟我说话吧。$R;");
                                break;
                        }
                    } while (a == 1);
                    break;
            }
        }
    }
}