using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002239 : Event
    {
        public S11002239()
        {
            this.EventID = 11002239;
        }

        public override void OnEvent(ActorPC pc)
        {
            NPCMove(pc, 11002239, 4350, -1350, 135, MoveType.NONE);
            ShowEffect(pc, 11002239, 4501);

            Say(pc, 65535, "――！？$R;", "マイク・タルパ");
            Wait(pc, 495);
            NPCMotion(pc, 11002239, 331, true, true);
            Wait(pc, 990);
            switch (Select(pc, "要做甚麼？", "", "擊退怪物！", "對話", "沒有敵人"))
            // switch (Select(pc, "どうする？", "", "モンスターなので退治！", "話掛ける", "相手にしない"))
            {
                case 1:
                    Wait(pc, 990);

                    Say(pc, 65535, "――ちょ、……ちょちょッ！$R;" +
                    "$Rオイラ悪いモンスターちがう！！$R;" +
                    "オイラ、ただの｢タルパ｣よ！$R;", "マイク・タルパ");
                    Wait(pc, 330);

                    Say(pc, 65535, "……。$R;" +
                    "$P……分かってくれた？$R;", "マイク・タルパ");
                    //
                    /*
                    Say(pc, 65535, "――ちょ、……ちょちょッ！$R;" +
                    "$Rオイラ悪いモンスターちがう！！$R;" +
                    "オイラ、ただの｢タルパ｣よ！$R;", "マイク・タルパ");
                    Wait(pc, 330);

                    Say(pc, 65535, "……。$R;" +
                    "$P……分かってくれた？$R;", "マイク・タルパ");
                    */
                    if (Select(pc, "……分かってくれたか？", "", "分からないので退治！", "分かった！") == 1)
                    //if (Select(pc, "……分かってくれたか？", "", "分からないので退治！", "分かった！") == 1)
                      {
                        Say(pc, 65535, "あなた、わからない人！$R;" +
                        "$Pこうなれば仕方ないよ……。$R;", "マイク・タルパ");

                        Say(pc, 65535, "マイク、あんたと戦う！$R;", "マイク・タルパ");
                        Wait(pc, 330);
                        NPCMotion(pc, 11002239, 364, true, true);
                        Wait(pc, 2640);
                        NPCMotion(pc, 11002239, 331, true, true);
                        Wait(pc, 330);
                        Say(pc, 65535, "――覺悟吧！$R;", "マイク・タルパ");
                        //Say(pc, 65535, "――覚悟するよ！$R;", "マイク・タルパ");
                        Wait(pc, 330);

                        Say(pc, 65535, "とや～～！！$R;", "マイク・タルパ");
                        //Say(pc, 65535, "とや～～！！$R;", "マイク・タルパ");
                        Wait(pc, 330);
                        NPCMotion(pc, 11002239, 342, true, true);
                        Wait(pc, 2970);

                        Say(pc, 65535, "還有還有～！！$R;", "マイク・タルパ");
                        //Say(pc, 65535, "まだまだ～！！$R;", "マイク・タルパ");
                        NPCMove(pc, 11002239, 107, 77, 410, 7, 364, 12);
                        NPCMotion(pc, 11002239, 342, true, true);
                        Wait(pc, 2970);
                        NPCMove(pc, 11002239, 107, 77, 410, 7, 364, 14);
                        NPCMotion(pc, 11002239, 342, true, true);
                        Wait(pc, 2970);

                        Say(pc, 65535, "ま～だま～だいくよ！！$R;", "マイク・タルパ");
                        //Say(pc, 65535, "ま～だま～だいくよ！！$R;", "マイク・タルパ");
                        Wait(pc, 330);
                        NPCMove(pc, 11002239, 107, 77, 410, 7, 364, 16);
                        NPCMotion(pc, 11002239, 342, true, true);
                        Wait(pc, 1980);
                        NPCMove(pc, 11002239, 107, 77, 410, 7, 364, 18);
                        NPCMotion(pc, 11002239, 342, true, true);
                        Wait(pc, 3960);

                        Say(pc, 65535, "……。$R;", "マイク・タルパ");
                        NPCMove(pc, 11002239, 107, 77, 410, 7, 364, 14);
                        NPCMotion(pc, 11002239, 342, true, true);
                        Wait(pc, 1980);
                        NPCMove(pc, 11002239, 107, 77, 410, 7, 364, 10);
                        NPCMotion(pc, 11002239, 342, true, true);
                        Wait(pc, 1980);
                        NPCMove(pc, 11002239, 107, 77, 410, 7, 364, 6);
                        NPCMotion(pc, 11002239, 342, true, true);
                        Wait(pc, 2970);

                        Say(pc, 65535, "……う、うぐぅ。$R;", "マイク・タルパ");
                        //Say(pc, 65535, "……う、うぐぅ。$R;", "マイク・タルパ");
                        Wait(pc, 330);
                        NPCMotion(pc, 11002239, 362, true, true);
                        Wait(pc, 2640);
                        NPCMotion(pc, 11002239, 363, true, true);
                        Wait(pc, 330);

                        Say(pc, 65535, "……你、挺能幹的……。$R;" +
                        "$P……うぷっ。$R;" +
                        "$R……嗚嗚、糟糟的感覺……。$R;", "マイク・タルパ");
                        /*
                        Say(pc, 65535, "……あなた、なかなかやる……。$R;" +
                        "$P……うぷっ。$R;" +
                        "$R……うう、気持ち悪い……。$R;", "マイク・タルパ");
                        */
                        Wait(pc, 1980);
                        Wait(pc, 990);
                        NPCMove(pc, 11002239, 107, 77, 410, 7, 111, 10);
                        Wait(pc, 990);
                    }
                    return;
                case 2:
                    Say(pc, 65535, "這兒、大家タルパ、掘到了喔！$R;" +
                    "$Rタルパ、レジスタンスよ！$R;", "マイク・タルパ");
                    /*
                     Say(pc, 65535, "ここ、みんなタルパ、掘ったよ！$R;" +
                    "$Rタルパ、レジスタンスよ！$R;", "マイク・タルパ");
                    */
                    return;
            }
        }
    }
}


        
   


