using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M11001728
{
    public class S11001728 : Event
    {
        public S11001728()
        {
            this.EventID = 11001728;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<saga8_01> saga8_01_mask = new BitMask<saga8_01>(pc.CMask["saga8_01"]);
            //int selection;
            if (pc.WRPRanking <= 10)
            {

            if (saga8_01_mask.Test(saga8_01.入手勝者之証))
            {
                return;
            }
                Say(pc, 111, "そなたはフィールドチャンプのようだが、$R;" +
                "「勝者の証」を持っていないようだな。$R;" +
                "$P「勝者の証」を装備すれば、$R;" +
                "チャンプスキル「英雄の加護」を$R;" +
                "使用可能になるぞ。$R;" +
                "$Pこれがその「勝者の証」だ。$R;" +
                "$Rフィールドチャンプであるそなたに、$R;" +
                "これを授けよう。$R;" +
                "$P「勝者の証」は、$R;" +
                "フィールドチャンプであれば$R;" +
                "無条件で渡されるアイテムだ。$R;" +
                "$R手元に無くなったのなら、$R;" +
                "また我の元に来るが良い。$R;", "戦の調律者");
                GiveItem(pc, 50106100, 1);
                saga8_01_mask.SetValue(saga8_01.入手勝者之証, true);

            }
            Say(pc, 111, "我はフィールドチャンプの管理者。$R;" +
            "$Rこの世界における戦士同士の$R;" +
            "戦いの全てを司っている。$R;" +
            "$Rそなたの用件は何だ？$R;", "戦の調律者");
            switch (Select(pc, "用件は何だ？", "", "チャンプバトルに参加する", "フィールドチャンプについて聞く", "攻防戦について聞く", "何もしない"))
            {
                case 1:
                    Say(pc, 111, "戦いに参加するのか？$R;" +
                    "$Rここで参加を宣言すると、$R;" +
                    "フィールドで他の戦士たちと$R;" +
                    "戦い、競い合うことになるぞ。$R;" +
                    "$R覚悟はいいか？$R;", "戦の調律者");
                    if (Select(pc, "戦いに参加する？", "", "参加する", "参加しない") == 1)
                    {
                        Say(pc, 111, "そなたをチャンプバトルの参加者として$R;" +
                        "登録した。$R;" +
                        "$R互いに切磋琢磨し、$R;" +
                        "栄光をその手でつかみ取ってくれ。$R;", "戦の調律者");

                    }
                    break;
                case 3:
                    Say(pc, 111, "攻防戦とは、$R;" +
                    "ウェストフォートの所有権をかけた、$R;" +
                    "ドミニオン種族とDEMの戦いだ。$R;", "戦の調律者");           
                    break;
            }
        }

    }

}
            
            
        
     
    