using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

using SagaLib;
//所在地圖:下城(10024000) NPC基本信息:實驗室嚮導(18000146) X:156 Y:157
namespace SagaScript.M10024000
{
    public class S18000146 : Event
    {
        public S18000146()
        {
            this.EventID = 18000146;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);
            Say(pc, 131, "ギー　ギー$R;" +
            "ワレワレ　ハ$R;" +
            "エレキテル・ラボラトリー$R;" +
            "デス。$R;" +
            "$Rタダイマ　ボウケンシャノ$R;" +
            "ミナサマヲ　ラボラトリーヘ$R;" +
            "ゴアンナイ　シテオリマス$R;", "ラボの案内人");

            Say(pc, 131, "エレキテル・ラボラトリー　ヘ$R;" +
            "ムカワレマスカ　？$R;", "ラボの案内人");

            switch (Select(pc, "エレキテル・ラボへ……", "", "「エレキテルラボ」に向かう", "「ＤＥＭカスタマイズ部屋」に向かう", "今はやめとく"))
            {
                case 2:
                    Say(pc, 131, "イチメイサマ　ゴアンナーイ$R;", "ラボの案内人");
                    Say(pc, 0, 65535, "（エレキテルの足元の$R;" +
                    "　マンホールのふたが$R;" +
                    "　突然すごい勢いで開いた）$R;", " ");
                    pc.CInt["DEM_Customize_Map"] = CreateMapInstance(50072000, 10023100, 250, 132);
                    newbie.SetValue(DEMNewbie.第一次找实验室向导, true);
                    Warp(pc, (uint)pc.CInt["DEM_Customize_Map"], 13, 11);
                    break;
            }
        }
    }
}
