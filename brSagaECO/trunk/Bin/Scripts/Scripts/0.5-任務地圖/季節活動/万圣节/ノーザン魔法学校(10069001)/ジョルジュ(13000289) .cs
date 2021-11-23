using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000289 : Event
    {
        public S13000289()
        {
            this.EventID = 13000289;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ＯＨ　ＹＥＡＨ！！$R;" +
            "久々に学校に来てみたら$R;" +
            "ソーウェン祭とはついてるぜ！$R;", "ジョルジュ");

            Say(pc, 131, "ヘイ、ユー！！$R;" +
            "なかなかイカシタほうき$R;" +
            "持ってるじゃないか。$R;" +
            "$Pイイね、イイね。$R;" +
            "オレのソウルに熱いパッションの$R;" +
            "脈動がジンジンくるぜ！$R;" +
            "$Pもし良けりゃ$R;" +
            "このオレが、もっとクールで$R;" +
            "ロックなほうきにカスタムして$R;" +
            "やるぜ？$R;" +
            "$P……ウップス！$R;" +
            "タダってわけにはいかねえ。$R;" +
            "$Pそうだな……$R;" +
            "『ノーザンの魔法チケット』５枚で$R;" +
            "どうだ？$R;", "ジョルジュ");
            if (Select(pc, "どうする？", "", "遠慮します", "お願いします") == 2)
            {
                if (CountItem(pc, 10048009) >= 5)
                {
                    Say(pc, 131, "ザッツ　オール　ライト！！$R;" +
                    "ナイスな決断だ。$R;", "ジョルジュ");

                    Say(pc, 131, "デザインを選びな！$R;" +
                    "$R$CR※交換を行うと$R;" +
                    "武具の強化値、及びカードスロット$R;" +
                    "装着済のイリスカードは消失します。$CD$R;", "ジョルジュ");
                    switch (Select(pc, "どうする？", "", "やっぱりやめる", "魔法のほうきＤＸ改", "魔法のほうきＤＸ改（紫）", "魔法のほうきＤＸ改（水色）"))
                    {
                        case 1:
                            return;
                        case 2:
                            if (CountItem(pc, 60072101) > 0)
                            {
                                Say(pc, 131, "ちょっと待ってな！$R;" +
                                "$P………。$R;" +
                                "$P……。$R;" +
                                "$P…。$R;" +
                                "$Pパーフェクト！！$R;" +
                                "$Rできたぜ、受け取りな。$R;", "ジョルジュ");
                                PlaySound(pc, 2040, false, 100, 50);
                                TakeItem(pc, 10048009, 5);
                                TakeItem(pc, 60072101, 1);
                                GiveItem(pc, 60072102, 1);
                                Say(pc, 0, 131, "『魔法のほうきＤＸ改』を$R;" +
                                "手に入れた！$R;", " ");
                            }
                            else
                            {
                                Say(pc, 0, 131, "没有魔法のほうきＤＸ$R;", " ");
                            }
                            return;
                        case 3:
                            if (CountItem(pc, 60072101) > 0)
                            {
                                Say(pc, 131, "ちょっと待ってな！$R;" +
                                "$P………。$R;" +
                                "$P……。$R;" +
                                "$P…。$R;" +
                                "$Pパーフェクト！！$R;" +
                                "$Rできたぜ、受け取りな。$R;", "ジョルジュ");
                                PlaySound(pc, 2040, false, 100, 50);
                                TakeItem(pc, 10048009, 5);
                                TakeItem(pc, 60072101, 1);
                                GiveItem(pc, 60072103, 1);
                                Say(pc, 0, 131, "『魔法のほうきＤＸ改（紫）』を$R;" +
                                "手に入れた！$R;", " ");
                            }
                            else
                            {
                                Say(pc, 0, 131, "没有魔法のほうきＤＸ$R;", " ");
                            }
                            return;
                        case 4:
                            if (CountItem(pc, 60072101) > 0)
                            {
                                Say(pc, 131, "ちょっと待ってな！$R;" +
                                "$P………。$R;" +
                                "$P……。$R;" +
                                "$P…。$R;" +
                                "$Pパーフェクト！！$R;" +
                                "$Rできたぜ、受け取りな。$R;", "ジョルジュ");
                                PlaySound(pc, 2040, false, 100, 50);
                                TakeItem(pc, 10048009, 5);
                                TakeItem(pc, 60072101, 1);
                                GiveItem(pc, 60072104, 1);
                                Say(pc, 0, 131, "『魔法のほうきＤＸ改（水色）』を$R;" +
                                "手に入れた！$R;", " ");
                            }
                            else
                            {
                                Say(pc, 0, 131, "没有魔法のほうきＤＸ$R;", " ");
                            }
                            return;
                    }
                    
                }
                ShowEffect(pc, 10501, 4539);
                Say(pc, 131, "ベイビー、チケットが足りないぜ？$R;", "ジョルジュ");
            }
        }
    }
}