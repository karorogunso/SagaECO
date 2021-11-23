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
    public class S11000847 : Event
    {
        public S11000847()
        {
            this.EventID = 11000847;

        }

        public override void OnEvent(ActorPC pc)
        {
            NavigateCancel(pc);

            switch (Select(pc, "打算去哪儿啊？", "", "摩戈国际机场", "商人机场", "摩戈政府大楼前", "商人行会总部", "地摊", "酒馆广场", "哪儿也不去"))
            {
                case 1:
                    Say(pc, 131, "摩戈国际机场很熟吧$R;" +
                        "跟着箭头走，就能找到的喔$R;" +
                        "$P现在去军舰岛的飞空庭正在运行$R;" +
                        "$R以前还有到艾恩萨乌斯的飞空庭$R;" +
                        "可是现在不运行了$R;" +
                        "$P是因为国家之间关系不好啊$R;" +
                        "这样真是不方便…$R;");
                    Navigate(pc, 218, 172);
                    break;

                case 2:
                    Say(pc, 131, "商人国际机场很熟吧$R;" +
                        "跟着箭头走，就能找到的喔$R;" +
                        "$P那是商人行会的机场，$R;" +
                        "不过一般人也可以使用啦$R;" +
                        "$R但是费用比较高…$R;" +
                        "$P到被喻为「光之塔」$R;" +
                        "机械文明时代建筑的$R;" +
                        "飞空庭定期运行。$R;" +
                        "$R听说古代的遗物和机械到处都是$R;");
                    Navigate(pc, 42, 129);
                    break;

                case 3:
                    Say(pc, 131, "摩戈政府大楼前当然很熟吧$R;" +
                        "跟着箭头走，就能找到的喔$R;" +
                        "$P摩戈政府大楼和摩戈佣兵军团本部…$R;" +
                        "$R或是战士系行会分会等$R;" +
                        "都是重要设施的集中地阿$R;" +
                        "$P会给冒险者介绍很多工作，$R;" +
                        "您不妨试试。$R;");
                    Navigate(pc, 140, 65);
                    break;

                case 4:
                    Say(pc, 131, "商人行会总部当然很熟吧$R;" +
                        "跟着箭头走，就能找到的喔$R;" +
                        "$R贩卖到光之塔地方的「机票」$R;" +
                        "$P先跟您说一下…$R;" +
                        "还是不去为好呀$R;" +
                        "$P以前是聚集了很多好的商人$R;" +
                        "和大商人的商业区，所以出名唷$R;" +
                        "$R但现在就不一样了，$R;" +
                        "现在由当地有势力的大富豪、$R大商家所支配$R;" +
                        "一些普通的背囊、皮鞋$R;" +
                        "价钱贵得惊人呀$R;" +
                        "就是爱钱的人才会在那里呢$R;" +
                        "$P办完该办的事情就赶紧出来吧，$R;" +
                        "小心上当受骗喔。$R;" +
                        "最好别去那里阿$R;" +
                        "$R跟商人打交道的话，$R;" +
                        "去找地摊的商人就足够了吧$R;" +
                        "$P哎哟~$R;" +
                        "$R古鲁杜先生在的时候$R;" +
                        "可不是这样子喔$R;");
                    Navigate(pc, 80, 152);
                    break;

                case 5:
                    Say(pc, 131, "地摊当然很熟$R;" +
                        "跟着箭头走，就能找到的喔$R;" +
                        "$R有各种地摊，$R;" +
                        "食品、药物等冒险所需品$R;" +
                        "一般都可以找到的$R;");
                    Navigate(pc, 137, 189);
                    break;

                case 6:
                    Say(pc, 131, "酒馆广场当然很熟吧$R;" +
                        "跟著箭头走，就能找到的喔$R;" +
                        "$R附近有酒吧和武器商店，$R;" +
                        "矿工他们也经常利用$R;" +
                        "$P这城市还有宠物聚集的咖啡馆。$R;" +
                        "$R有时间找找看吧。$R;");
                    Navigate(pc, 151, 163);
                    break;

                case 7:
                    break;
            }
        }
    }
}