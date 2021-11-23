using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:沉默寡言的泰迪(13000126) X:229 Y:169
namespace SagaScript.M10071000
{
    public class S13000126 : Event
    {
        public S13000126()
        {
            this.EventID = 13000126;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "……見る？$R;", "無口なタイニー");
            switch (Select(pc, "どうする？", "", "見ない", "男物を見る", "女物を見る"))
            {
                case 2:
                    OpenShopBuy(pc, 248);
                    break;
                case 3:
                    OpenShopBuy(pc, 249);
                    break;
            }
            if (CountItem(pc, 10050300) > 0)
            {
                Say(pc, 65535, "……タイニーメダルくれたら$R;" +
                "もっとステキな水着を見せてあげる。$R;", "無口なタイニー");
                if (Select(pc, "どうする？", "", "あげない", "タイニーメダルをあげる") == 2)
                {
                    Say(pc, 65535, "……本当に、いいの？$R;", "無口なタイニー");
                    if (Select(pc, "どうする？", "", "やっぱり、あげない", "タイニーメダルをあげる") == 2)
                    {
                        Say(pc, 65535, "……嘘じゃないからね。$R;", "無口なタイニー");
                        if (Select(pc, "どうする？", "", "やっぱり、あげない", "くどいっ！") == 2)
                        {
                            Say(pc, 65535, "去年と一緒の水着だけどいいかな？$R;", "無口なタイニー");
                            if (Select(pc, "どうする？", "", "やっぱり、あげない", "いい！") == 2)
                            {
                                PlaySound(pc, 2041, false, 100, 50);

                                Say(pc, 0, 131, "『タイニーメダル』を失った。$R;", " ");
                                TakeItem(pc, 10050300, 1);

                                Say(pc, 65535, "……ありがと。$R;", "無口なタイニー");
                                OpenShopBuy(pc, 250);
                            }
                        }
                    }
                }
            }

        }
    }
}