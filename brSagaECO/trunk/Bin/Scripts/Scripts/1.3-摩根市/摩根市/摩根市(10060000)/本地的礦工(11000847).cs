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

            switch (Select(pc, "打算去哪兒啊？", "", "摩根國際機場", "商人機場", "摩根政府大樓前", "商人行會總部", "地攤", "咖啡館廣場", "哪兒也不去"))
            {
                case 1:
                    Say(pc, 131, "摩根國際機場很熟吧$R;" +
                        "跟著箭頭走，就能找到的喔$R;" +
                        "$P現在去軍艦島的飛空庭正在運行$R;" +
                        "$R以前還有到阿伊恩薩烏斯的飛空庭$R;" +
                        "可是現在不運行了$R;" +
                        "$P是因為國家之間關係不好阿$R;" +
                        "這樣真是不方便…$R;");
                    Navigate(pc, 218, 172);
                    break;

                case 2:
                    Say(pc, 131, "商人國際機場很熟吧$R;" +
                        "跟著箭頭走，就能找到的喔$R;" +
                        "$P那是商人行會的機場，$R;" +
                        "不過一般人也可以使用啦$R;" +
                        "$R但是費用比較高…$R;" +
                        "$P到被喻為「光之塔」$R;" +
                        "機械文明時代建築的$R;" +
                        "飛空庭定期運行。$R;" +
                        "$R聽說古代的遺物和機械到處都是$R;");
                    Navigate(pc, 42, 129);
                    break;

                case 3:
                    Say(pc, 131, "摩根政府大樓前當然很熟吧$R;" +
                        "跟著箭頭走，就能找到的喔$R;" +
                        "$P摩根政府大樓和摩根傭兵軍團本部…$R;" +
                        "$R或是戰士系行會分會等$R;" +
                        "都是重要設施的集中地阿$R;" +
                        "$P會給冒險者介紹很多工作唷，$R;" +
                        "您不妨試試。$R;");
                    Navigate(pc, 140, 65);
                    break;

                case 4:
                    Say(pc, 131, "商人行會總部當然很熟吧$R;" +
                        "跟著箭頭走，就能找到的喔$R;" +
                        "$R販賣到光之塔地方的「機票」$R;" +
                        "$P先跟您說一下…$R;" +
                        "還是不去為好呀$R;" +
                        "$P以前是聚集了很多好的商人$R;" +
                        "和大商人的商業區，所以出名唷$R;" +
                        "$R但現在就不一樣了，$R;" +
                        "現在由當地有勢力的大富豪、$R大商家所支配$R;" +
                        "一些普通的背囊、皮鞋$R;" +
                        "價錢貴得驚人呀$R;" +
                        "就是愛錢的人才會在那裡呢$R;" +
                        "$P辦完該辦的事情就趕緊出來吧，$R;" +
                        "小心上當受騙喔。$R;" +
                        "最好別去那裡阿$R;" +
                        "$R跟商人打交道的話，$R;" +
                        "去找地攤的商人就足夠了吧$R;" +
                        "$P哎喲~$R;" +
                        "$R古魯杜先生在的時候$R;" +
                        "可不是這樣子喔$R;");
                    Navigate(pc, 80, 152);
                    break;

                case 5:
                    Say(pc, 131, "地攤當然很熟$R;" +
                        "跟著箭頭走，就能找到的喔$R;" +
                        "$R有各種地攤，$R;" +
                        "食品、藥物等冒險所需品$R;" +
                        "一般都可以找到的唷$R;");
                    Navigate(pc, 137, 189);
                    break;

                case 6:
                    Say(pc, 131, "咖啡館廣場當然很熟吧$R;" +
                        "跟著箭頭走，就能找到的喔$R;" +
                        "$R附近有酒吧和武器商店，$R;" +
                        "礦工他們也經常利用唷$R;" +
                        "$P這城市還有寵物聚集的咖啡館。$R;" +
                        "$R有時間找找看吧。$R;");
                    Navigate(pc, 151, 163);
                    break;

                case 7:
                    break;
            }
        }
    }
}