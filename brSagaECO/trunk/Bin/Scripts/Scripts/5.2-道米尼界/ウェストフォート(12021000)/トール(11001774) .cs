using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001774 : Event
    {
        public S11001774()
        {
            this.EventID = 11001774;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "お前はどうしてこっちの$R;" +
            "世界に来たんだ？$R;", "トール");

            Say(pc, 11001773, 131, "そりゃ、お前。$R;" +
            "こっちの世界でなら一旗揚げられる$R;" +
            "と思ったからな。$R;", "ヨルン");

            Say(pc, 131, "不順な動機だな。$R;" +
            "そんなことしか思わないのか$R;" +
            "この世界の状況を見て？$R;", "トール");

            Say(pc, 11001773, 131, "まあな……そりゃ感じるよ。$R;" +
            "だから、俺たちだけが$R;" +
            "のほほんと暮らしてられないと$R;" +
            "思ったからよ、力かしてやろうって$R;" +
            "気にはなるだろ。$R;", "ヨルン");

            Say(pc, 132, "ふむ、少しタイタニアという種族の$R;" +
            "認識を改めないといけないようだ。$R;", "トール");

            Say(pc, 11001773, 134, "おい、タイタニアにどんな$R;" +
            "偏見持ってんだお前。$R;", "ヨルン");
           }

           }
                        
                }
            
            
        
     
    