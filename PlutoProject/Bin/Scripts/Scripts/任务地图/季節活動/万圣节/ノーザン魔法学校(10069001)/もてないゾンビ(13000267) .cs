using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000267 : Event
    {
        public S13000267()
        {
            this.EventID = 13000267;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "おいら〜、一度でいいから$R;" +
            "女の子と手をつなぎたいだ〜よ〜。$R;", "もてないゾンビ");
            if (Select(pc, "どうする？", "", "えっ……！？", "手をつなぐ") == 2)
            {
                Say(pc, 0, 131, "ぺちょっ！$R;" +
                "$P何か変な液体が$R;" +
                "手にべったりと付いた……。$R;", " ");

                Say(pc, 131, "おめぇ、優しいなぁ……。$R;" +
                "$Rありがとう、ありがとう。$R;" +
                "これで、やっと成仏できるだ〜よ〜。$R;", "もてないゾンビ");
                Wait(pc, 330);
                ShowEffect(pc, 13000267, 4011);
                Wait(pc, 1980);
                PlaySound(pc, 2040, false, 100, 50);
                //消失
                NPCHide(pc, 13000267);
                Say(pc, 0, 131, "満足して成仏したようだ……。$R;", " ");
            }
        }
    }
}