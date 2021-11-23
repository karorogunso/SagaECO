using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript.M50037000
{
    public class S11001343 : Event
    {
        public S11001343()
        {
            this.EventID = 11001343;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];

            if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.與提多對話) &&
                !Neko_06_cmask.Test(Neko_06.尋找阿伊斯))
            {
                Say(pc, 111, "ちょうどいい。$Rいまノーザンへ向かっている$R飛空庭がいるな…。$R;" +
                "$Rそこの庭に君を転送しよう。$R便乗させてもらえるだろう。$R;" +
                "$P君の準備ができたら転送するから$Rもう一度僕に話しかけてくれ。$R;", "タイタス");
                if (Select(pc, "どうする？", "", "転送を頼む", "まだ頼まない") == 1)
                {
                    ShowEffect(pc, 4023);
                    Wait(pc, 1980);
                    pc.CInt["Neko_06_Map_04"] = CreateMapInstance(50038000, 10023000, 135, 64);
                    Warp(pc, (uint)pc.CInt["Neko_06_Map_04"], 7, 10);
                }
            }

            else if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.詢問塔尼亞代表) &&
                !Neko_06_cmask.Test(Neko_06.與提多對話))
            {
                Say(pc, 131, "何だ？$R;" +
                "$Rネコ魂…ネコマタか…？$R…いや、違うな。$R;" +
                "$P……！！$R;" +
                "$Rこれは…。$Rネコ魂と人間の魂が対融合している…！$R;" +
                "$R魅惑的だ、初めて見たな。$R;" +
                "$P人間の魂は誰だ…$R;" +
                pc.Name + "か？$R;" +
                "$Rなんだ、君か。$R;", "タイタス");

                Say(pc, 0, 111, "タイタスに$Rこれまでのいきさつを話した。$R;", " ");

                Say(pc, 131, "…なるほど、もとに戻る方法か。$R;" +
                "$R…無いわけではないな。$R;", "タイタス");
                PlaySound(pc, 2040, false, 100, 50);

                Say(pc, 0, 111, "『タイタスの水晶』を手渡された！$R;", " ");

                GiveItem(pc, 10011001, 1);

                Say(pc, 111, "これを持ってノーザンにある$Rアイシー島へ行きたまえ。$R;" +
                "$Rそしてアイシー族に$R特殊なプリズム加工をしてもらうんだ。$R;" +
                "$Pアイシー族は水晶のプリズム効果で$R自分の生命の半分を水晶体に投射して$R新しい仲間の生命を生み出すそうだ。$R;" +
                "$R同じ効果を使えば$R君とネコ魂を分離することも可能だろう。$R;", "タイタス");

                Say(pc, 111, "ちょうどいい。$Rいまノーザンへ向かっている$R飛空庭がいるな…。$R;" +
                "$Rそこの庭に君を転送しよう。$R便乗させてもらえるだろう。$R;" +
                "$P君の準備ができたら転送するから$Rもう一度僕に話しかけてくれ。$R;", "タイタス");
                Neko_06_cmask.SetValue(Neko_06.與提多對話, true);
                if (Select(pc, "どうする？", "", "転送を頼む", "まだ頼まない") == 1)
                {
                    ShowEffect(pc, 4023);
                    Wait(pc, 1980);
                    pc.CInt["Neko_06_Map_04"] = CreateMapInstance(50038000, 10023000, 135, 64);
                    Warp(pc, (uint)pc.CInt["Neko_06_Map_04"], 7, 10);
                }
            }
        }
    }
}