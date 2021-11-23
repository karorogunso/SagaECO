using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30071000
{
    public class S11000282 : Event
    {
        public S11000282()
        {
            this.EventID = 11000282;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<LV85_Clothes> LV85_Clothes_mask = pc.CMask["LV85_Clothes"];
            int[] a = { 60002951, 60101351, 60101451, 60101551, 60101651, 60101751, 60101851, 60101951, 60102051, 60102151, 60102251, 60102351, 60102451, 60102551, 60102651, 60102751, 60102851, 60102951, 60103051, 60103151, 60103251, 60103351, 60103451, 60103551 };
            int[] b = { 60002950, 60101350, 60101450, 60101550, 60101650, 60101750, 60101850, 60101950, 60102050, 60102150, 60102250, 60102350, 60102450, 60102550, 60102650, 60102750, 60102850, 60102950, 60103050, 60103150, 60103250, 60103350, 60103450, 60103550 };
            if (LV85_Clothes_mask.Test(LV85_Clothes.已于阿魯斯对话) &&
                !LV85_Clothes_mask.Test(LV85_Clothes.已与阿魯斯的爱人对话) &&
                ((CountItem(pc, 60002951) > 0) ||
                (CountItem(pc, 60101351) > 0) ||
                (CountItem(pc, 60101451) > 0) ||
                (CountItem(pc, 60101551) > 0) ||
                (CountItem(pc, 60101651) > 0) ||
                (CountItem(pc, 60101751) > 0) ||
                (CountItem(pc, 60101851) > 0) ||
                (CountItem(pc, 60101951) > 0) ||
                (CountItem(pc, 60102051) > 0) ||
                (CountItem(pc, 60102151) > 0) ||
                (CountItem(pc, 60102251) > 0) ||
                (CountItem(pc, 60102351) > 0) ||
                (CountItem(pc, 60102451) > 0) ||
                (CountItem(pc, 60102551) > 0) ||
                (CountItem(pc, 60102651) > 0) ||
                (CountItem(pc, 60102751) > 0) ||
                (CountItem(pc, 60102851) > 0) ||
                (CountItem(pc, 60102951) > 0) ||
                (CountItem(pc, 60103051) > 0) ||
                (CountItem(pc, 60103151) > 0) ||
                (CountItem(pc, 60103251) > 0) ||
                (CountItem(pc, 60103351) > 0) ||
                (CountItem(pc, 60103451) > 0) ||
                (CountItem(pc, 60103551) > 0)))
            {
                LV85_Clothes_mask.SetValue(LV85_Clothes.已与阿魯斯的爱人对话, true);
                Say(pc, 131, "え、呪いをといて欲しい？$R;" +
                "$Rもちろんいいけど$R;" +
                "私のこと、誰にきいたの？$R;" +
                "$Pアルス！？$R;" +
                "$Rもうっ、アイツったら$R;" +
                "呪いをとく方法、知っているくせに$R;" +
                "まったく、もう、だらしがない！$R;" +
                "$Pわざわざ、アイアンサウスまで$R;" +
                "来てもらって悪かったわね。$R;" +
                "$Rいいわ、私が呪いをといてあげる。$R;" +
                "$P呪いの正体はアンデッドの体液よ。$R;" +
                "$Rアンデッドの体液がつくと$R;" +
                "著しく能力が下がってしまうの。$R;" +
                "$Pふき取ればいいんだけど$R;" +
                "そのため薬が、けっこう強くて$R;" +
                "普通にふき取ると$R;" +
                "防具も一緒に壊れてしまうのよ。$R;" +
                "$Pだから、まずは$R;" +
                "「武具強化」で防具自身の能力を$R;" +
                "高める必要があるわ。$R;" +
                "$Rそうね、５回、強化してあれば$R;" +
                "呪いをとくことが出来ると思うわ。$R;" +
                "$P武具強化は、この町にある$R;" +
                "大タタラ場でやってくれるから$R;" +
                "まずは、大タタラ場にいってみて。$R;", "アルスの彼女");
            }
            if (LV85_Clothes_mask.Test(LV85_Clothes.已与阿魯斯的爱人对话))
            {
                if (LV85_Clothes_mask.Test(LV85_Clothes.詛咒解除) &&
                !LV85_Clothes_mask.Test(LV85_Clothes.任务结束))
                {
                    LV85_Clothes_mask.SetValue(LV85_Clothes.任务结束, true);
                    Say(pc, 131, "呪われていたけど……。$R;" +
                    "その防具、とてもいい物ね。$R;" +
                    "$Rきっと名のあるブラックスミスが$R;" +
                    "心を込めて作ったんだわ。$R;" +
                    "$Pでも、アルスだって$R;" +
                    "負けないぐらいいいもの作るのよ。$R;" +
                    "$R今は、見習いだけど$R;" +
                    "いつかきっと$R;" +
                    "大きくなってアイアンサウスに$R;" +
                    "帰ってきてくれるって信じてる。$R;" +
                    "$Pウフフ。$R;" +
                    "$R今度、アルスにあったら$R;" +
                    "呪い解けたぞって伝えてくれるかな？$R;", "アルスの彼女");
                    return;
                }
                Say(pc, 131, "こんにちは。$R;" +
                "私でお役に立てること、あるかしら？$R;", "アルスの彼女");
                switch (Select(pc, "どうする？", "", "何もしない", "防具の呪いをとく"))
                {
                    case 2:
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].Refine == 5)
                            {
                                for (int i = 0; i < a.Length;i++ )
                                    if (pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].ItemID == a[i])
                                    {
                                        Say(pc, 131, "うん、いい具合に強化されているわ。$R;" +
                                        "これなら大丈夫。$R;" +
                                        "早速、アンデッドの体液を$R;" +
                                        "取り除く作業にとりかかりましょう。$R;", "アルスの彼女");

                                        Say(pc, 131, "じゃあ、準備してくるから$R;" +
                                        "ちょっと待っていてくれるかしら？$R;", "アルスの彼女");
                                        PlaySound(pc, 4010, false, 100, 50);
                                        pc.CInt["LV85_Clothes_Map"] = CreateMapInstance(50040000, 30071000, 5, 3);
                                        Wait(pc, 990);
                                        Warp(pc, (uint)pc.CInt["LV85_Clothes_Map"], 5, 3);
                                        return;
                                    }
                            }
                        }
                        Say(pc, 131, "じゃあ、呪いをときたい防具を$R;" +
                            "装備してちょうだい。$R;", "アルスの彼女");
                        break;
                }
                return;
            }
            Say(pc, 131, "我男朋友去了阿高普路斯，$R;" +
                "不知道會不會拈花惹草阿。$R;");
        }
    }
}