using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class S11001338 : Event
    {
        public S11001338()
        {
            this.EventID = 11001338;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要怎么做呢？", "", "换男性发型", "打开宝箱15", "算了"))
            {
                case 1:
                    switch (Select(pc, "有什么可以帮助吗", "", "ラブモテ紹介状", "クラシックな紹介状", "ラテンの紹介状", "ワイルドな紹介状", "武家の紹介状", "空賊の紹介状", "貴族の紹介状", "委員長の紹介状", "アングリー紹介状", "プリンセスの紹介状", "真夏の紹介状", "ハロウイン紹介状", "歌姫の紹介状", "バレンタイン紹介状", "デビューへの紹介状", "アルバイトの紹介状", "伝説の紹介状", "北斗の紹介状", "夏休みの紹介状", "ルチルのヘアカタログ", "ふたご座の紹介状", "わがまま紹介状", "没有"))
              {
                case 1:
                    if (CountItem(pc, 10020768) > 0)
                    {
                        pc.HairStyle = 17;
                        pc.Wig = 255;
                        ShowEffect(pc, 4112);
                        PlaySound(pc, 2213, false, 100, 50);
                        TakeItem(pc, 10020768, 1);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要ラブモテ紹介状$R;");
                    }
                    return;
                case 2:
                    if (CountItem(pc, 10020770) > 0)
                    {
                    pc.HairStyle = 18;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020770, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要クラシックな紹介状$R;");
                    }
                    return;
                case 3:
                    if (CountItem(pc, 10020774) > 0)
                    {
                    pc.HairStyle = 34;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020774, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要ラテンの紹介状$R;");
                    }
                    return;
                case 4:
                    if (CountItem(pc, 10020776) > 0)
                    {
                    pc.HairStyle = 37;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020776, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "ワイルドな紹介状$R;");
                    }
                    return;
                case 5:
                    if (CountItem(pc, 10020777) > 0)
                    {
                    pc.HairStyle = 39;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020777, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要武家の紹介状$R;");
                    }
                    return;
                case 6:
                    if (CountItem(pc, 10020787) > 0)
                    {
                    pc.HairStyle = 55;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020787, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要空賊の紹介状$R;");
                    }
                    return;
                case 7:
                    if (CountItem(pc, 10020790) > 0)
                    {
                    pc.HairStyle = 59;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020790, 1);
                    }
                    else
                    {
                        Say(pc, 190, "需要貴族の紹介状$R;");
                    }
                    return;
                case 8:
                    if (CountItem(pc, 10020789) > 0)
                    {
                    pc.HairStyle = 58;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020789, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要委員長の紹介状$R;");
                    }
                    return;
                case 9:
                    if (CountItem(pc, 10075203) > 0)
                    {
                    pc.HairStyle = 84;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10075203, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要アングリー紹介状$R;");
                    }
                    return;
                case 10:
                    if (CountItem(pc, 10020769) > 0)
                    {
                    pc.HairStyle = 28;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020769, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要プリンセスの紹介状$R;");
                    }
                    return;
                case 11:
                    if (CountItem(pc, 10020788) > 0)
                    {
                        pc.HairStyle = 25;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020796, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要真夏の紹介状$R;");
                    }
                    return;
                case 12:
                    if (CountItem(pc, 10020792) > 0)
                    {
                        pc.HairStyle = 61;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020792, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要ハロウイン紹介状$R;");
                    }
                    return;
                case 13:
                    if (CountItem(pc, 10020784) > 0)
                    {
                        pc.HairStyle = 47;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020784, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要歌姫の紹介状$R;");
                    }
                    return;
                case 14:
                    if (CountItem(pc, 10020779) > 0)
                    {
                        pc.HairStyle = 40;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020779, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要バレンタイン紹介状$R;");
                    }
                    return;
                case 15:
                    if (CountItem(pc, 10020780) > 0)
                    {
                        pc.HairStyle = 41;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020780, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要デビューへの紹介状$R;");
                    }
                    return;
                case 16:
                    if (CountItem(pc, 10020795) > 0)
                    {
                        pc.HairStyle = 73;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020795, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要アルバイトの紹介状$R;");
                    }
                    return;
                case 17:
                    if (CountItem(pc, 10020797) > 0)
                    {
                        pc.HairStyle = 78;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020797, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要伝説の紹介状$R;");
                    }
                    return;
                case 18:
                    if (CountItem(pc, 10074500) > 0)
                    {
                        pc.HairStyle = 83;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10074500, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要北斗の紹介状$R;");
                    }
                    return;
                case 19:
                    if (CountItem(pc, 10074800) > 0)
                    {
                        pc.HairStyle = 85;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10074800, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "需要夏休みの紹介状$R;");
                    }
                    return;
                case 20:
                    if (CountItem(pc, 10020759) > 0)
                    {
                        pc.HairStyle = 19;
                        pc.Wig = 255;
                    ShowEffect(pc, 4112);
                    PlaySound(pc, 2213, false, 100, 50);
                    TakeItem(pc, 10020759, 1);
                    return;
                    }
                    else
                    {
                        Say(pc, 190, "ルチルのヘアカタログ$R;");
                    }
                    return;
                case 21:
                    if (CountItem(pc, 10020773) > 0)
                    {
                        pc.HairStyle = 19;
                        pc.Wig = 255;
                        ShowEffect(pc, 4112);
                        PlaySound(pc, 2213, false, 100, 50);
                        TakeItem(pc, 10020773, 1);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要ふたご座の紹介状$R;");
                    }
                    return;
                case 22:
                    if (CountItem(pc, 10075211) > 0)
                    {
                        pc.HairStyle = 106;
                        pc.Wig = 255;
                        ShowEffect(pc, 4112);
                        PlaySound(pc, 2213, false, 100, 50);
                        TakeItem(pc, 10075211, 1);
                        return;
                    }
                    else
                    {
                        Say(pc, 190, "需要わがまま紹介状$R;");
                    } 
                    return;
                 }
                 return;
                case 2:
                    if (CountItem(pc, 10022115) > 0)
                    {
                    GiveRandomTreasure(pc, "TREASURE_BOX15");
                    TakeItem(pc, 10022115, 1);
                    return;
                    }
                    else
                    {
                    Say(pc, 190, "你手上没有宝物箱15$R;");
                    }
                    return;
               }
                
           }
      }
}
