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
            Say(pc, 0, 0, "請求！$R;" +
            "也許我怎麼成為！$R;" +
            "只放過這個家伙！$R;", "ドミニオンの戦士");

            Say(pc, 0, 0, "ドミニオン…敵人ヲ捕捉…攻擊スルドミニオン的戰士$R;", "ＤＥＭ");

            NPCMotion(pc, 11002168, 331, true, 10);

            Say(pc, 0, 0, "…ッ！！$R;", "ドミニオンの戦士");
            NPCMove(pc, 11002171, 27, 28, 380, 7, 331, 1);
            Wait(pc, 495);
            NPCMotion(pc, 11002168, 341, false, 10);
            Wait(pc, 165);
            Wait(pc, 660);
            NPCMotion(pc, 11002171, 363, true, 10);
            PlaySound(pc, 2143, false, 100, 50);
            Wait(pc, 660);
            Wait(pc, 660);
            NPCMotion(pc, 11002168, 111, true, 10);
            Wait(pc, 990);

            Say(pc, 0, 0, "！！$R;", "ドミニオンの少女");
            Say(pc, 0, 0, "相似,並且逃掉…！$R;" +
            "只你…幸存…！$R;", "ドミニオンの戦士");
            Say(pc, 0, 0, "父親……？$R;", "ドミニオンの少女");
            Say(pc, 0, 0, "……。$R;", "ドミニオンの戦士");
            Say(pc, 0, 0, "ドミニオンの戦士は、$R;" +
            "被朋友的攻擊打倒了…。$R;", "");
            Wait(pc, 1980);
            Say(pc, 0, 0, "正好是イイ。$R;" +
            "新家伙,アノ女人ヲ殺セ。。$R;", "ＤＥＭ");
            if (Select(pc, "打倒面前攻擊殺セ朋友的敵人", "", "攻击同伴", "打倒眼前的敌人") == 1)
            {
                Wait(pc, 990);
                NPCMove(pc, 11002168, -9850, 9950, 270, MoveType.NONE);
                NPCMotion(pc, 11002169, 363, true, 10);
                NPCMove(pc, 11002169, -9750, 9850, 225, MoveType.NONE);
                NPCMove(pc, 11002170, -9850, 10050, 315, MoveType.NONE);
                PlaySound(pc, 2169, false, 100, 50);
                Wait(pc, 990);
                Wait(pc, 990);
                Say(pc, 0, 0, "攻击了在了面前的朋友…。$R;", "");
                Say(pc, 0, 0, "…朋友ノ背后切リヲ确认。$R;" +
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