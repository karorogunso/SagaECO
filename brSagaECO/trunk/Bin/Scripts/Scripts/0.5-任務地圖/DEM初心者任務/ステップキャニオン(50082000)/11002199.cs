using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50082000
{
    public class S11002199 : Event
    {
        public S11002199()
        {
            this.EventID = 11002199;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "頼む！$R;" +
            "私はどうなっても良い！$R;" +
            "こいつだけは見逃してやってくれ！$R;", "ドミニオンの戦士");

            Say(pc, 0, 0, "ドミニオン…敵ヲ捕捉…攻撃スル…$R;", "ＤＥＭ");

            NPCMotion(pc, 11002168, 331, true, true);

            Say(pc, 0, 0, "…ッ！！$R;", "ドミニオンの戦士");
            NPCMove(pc, 11002171, 27, 28, 380, 7, 331, 1);
            Wait(pc, 495);
            NPCMotion(pc, 11002168, 341, false, true);
            Wait(pc, 165);
            Wait(pc, 660);
            NPCMotion(pc, 11002171, 363, true, true);
            PlaySound(pc, 2143, false, 100, 50);
            Wait(pc, 660);
            Wait(pc, 660);
            NPCMotion(pc, 11002168, 111, true, true);
            Wait(pc, 990);

            Say(pc, 0, 0, "！！$R;", "ドミニオンの少女");
            Say(pc, 0, 0, "に、逃げろ…！$R;" +
            "お前だけは…生き延びて…！$R;", "ドミニオンの戦士");
            Say(pc, 0, 0, "お父…さん…？$R;", "ドミニオンの少女");
            Say(pc, 0, 0, "……。$R;", "ドミニオンの戦士");
            Say(pc, 0, 0, "ドミニオンの戦士は、$R;" +
            "仲間の攻撃によって倒された…。$R;", "");
            Wait(pc, 1980);
            Say(pc, 0, 0, "…丁度イイ。$R;" +
            "新シイ奴、アノ 女ヲ殺セ。$R;", "ＤＥＭ");
            if (Select(pc, "殺セ", "", "仲間を攻撃する", "目の前の敵を倒す") == 1)
            {
                Wait(pc, 990);
                NPCMove(pc, 11002168, -9850, 9950, 270, MoveType.NONE);
                NPCMotion(pc, 11002169, 363, true, true);
                NPCMove(pc, 11002169, -9750, 9850, 225, MoveType.NONE);
                NPCMove(pc, 11002170, -9850, 10050, 315, MoveType.NONE);
                PlaySound(pc, 2169, false, 100, 50);
                Wait(pc, 990);
                Wait(pc, 990);
                Say(pc, 0, 0, "目の前にいた「仲間」を攻撃した…。$R;", "");
                Say(pc, 0, 0, "…仲間ノ裏切リヲ確認。$R;" +
                "攻撃目標ヲ変更…。$R;" +
                "「イレギュラー」トシテ排除ヲ行ウ。$R;", "ＤＥＭ");
                Say(pc, 0, 0, "！$R;", "ドミニオンの少女");
                NPCMove(pc, 11002172, 26, 0, 380, 7, 122, 10);
                Say(pc, 0, 0, "イレギュラー…。$R;" +
                "イレギュラー…。$R;" +
                "排除スル…。$R;", "ＤＥＭ");

                int oldMap = pc.CInt["Beginner_Map"];
                pc.CInt["Beginner_Map"] = CreateMapInstance(50083000, 10023100, 250, 132);
                LoadSpawnFile(pc.CInt["Beginner_Map"], "DB/Spawns/50083000.xml");
                Warp(pc, (uint)pc.CInt["Beginner_Map"], 30, 34);

                DeleteMapInstance(oldMap);
            }
            else
            {
                Wait(pc, 990);
                NPCMove(pc, 11002168, -9850, 9950, 315, MoveType.NONE);
                NPCMotion(pc, 11002169, 363, true, true);
                NPCMove(pc, 11002169, -9750, 9850, 270, MoveType.NONE);
                NPCMove(pc, 11002170, -9850, 10050, 315, MoveType.NONE);
                PlaySound(pc, 2143, false, 100, 50);
                Wait(pc, 990);
                Wait(pc, 990);

                Say(pc, 0, 0, "気が付いた時には、$R;" +
                "目の前にいた「仲間」を攻撃していた…$R;", "");

                Say(pc, 0, 0, "…仲間ノ裏切リヲ確認。$R;" +
                "攻撃目標ヲ変更…。$R;" +
                "「イレギュラー」トシテ排除ヲ行ウ。$R;", "ＤＥＭ");

                Say(pc, 0, 0, "！$R;", "ドミニオンの少女");
                NPCMove(pc, 11002172, 26, 0, 380, 7, 122, 10);

                Say(pc, 0, 0, "イレギュラー…。$R;" +
                "イレギュラー…。$R;" +
                "排除スル…。$R;", "ＤＥＭ");
                int oldMap = pc.CInt["Beginner_Map"];
                pc.CInt["Beginner_Map"] = CreateMapInstance(50083000, 10023100, 250, 132);
                LoadSpawnFile(pc.CInt["Beginner_Map"], "DB/Spawns/50083000.xml");
                Warp(pc, (uint)pc.CInt["Beginner_Map"], 30, 34);

                DeleteMapInstance(oldMap);
            }
        }
    }
}