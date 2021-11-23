using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:阿高路普斯上城(10023000) NPC基本信息:ディム博士(11002272) X:145 Y:65
namespace SagaScript.M10023000
{
    public class S11002272 : Event
    {
        public S11002272()
        {
            this.EventID = 11002272;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = pc.CMask["Acropolisut_01"];

            if (pc.Level < 80)
            {
                Say(pc, 131, "您好，我是個專門研究時空的學者。");
                return;
            }
            if (!Acropolisut_01_mask.Test(Acropolisut_01.次元安定石))
            {
                Acropolisut_01_mask.SetValue(Acropolisut_01.次元安定石, true);
                Say(pc, 131, "您好，我是個專門研究時空的學者。" +
                    "$R最近發現了一種可以扭曲時空的石頭，" +
                    "$R基於好奇心而跳進了那時空裂縫，" +
                    "$R結果就掉進了時空隊道來到了這個世界。" +
                    "$P但因為傳送途中大腦受到了衝擊" +
                    "$R所以不太記得之前的事情了" +
                    "$P簡單來說就是ＧＭ已經忘記了" +
                    "$R我這個混蛋之前在正服是說甚麼對白的啦！" +
                    "$P剛剛我好像說了甚麼奇怪的話？" +
                    "$R因為衝擊的關係我有些時候會變得語無倫次。$R;" +
                    "$R對不起，還是說回正事吧！" +
                    "$P這就是我之前提到的可以扭曲時空的石頭，" +
                    "$R只要你帶著這個石頭" +
                    "$R到世界各地的時空接合點" +
                    "$R就可以把空間扭曲進到時空裂縫中。" +
                    "$P但因為扭曲時空時會產生巨大的能量" +
                    "$R所以每次進入時空裂縫時都會令石頭碎裂" +
                    "$R這點請務必注意一下" +
                    "$P雖然我記得它是有個比較正式的名字，" +
                    "$R但就是想不起來啊！$R;" +
                    "$R暫時就先叫它＂次元安定石＂吧！" +
                    "$P製作次元安定石的方法其實很簡單，" +
                    "$R只要在堅硬鵝卵石上邊貼上紋章紙，" +
                    "$R再把神力水倒在上邊就可以了。" +
                    "$R今天就送一顆給你當作見面禮吧！");
                GiveItem(pc, 10014651, 1);
                return;
            }
            Say(pc, 131, "$R只要在堅硬鵝卵石上邊貼上紋章紙，" +
                    "$R再把神力水倒在上邊，" +
                    "$R就可以製成次元安定石了。");
        }
    }
}