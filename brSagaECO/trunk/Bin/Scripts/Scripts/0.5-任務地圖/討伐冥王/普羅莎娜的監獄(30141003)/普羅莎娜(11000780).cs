using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30141003
{
    public class S11000780 : Event
    {
        public S11000780()
        {
            this.EventID = 11000780;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Crusade_Pluto> Crusade_Pluto_mask = pc.CMask["Crusade_Pluto"];

            if (Crusade_Pluto_mask.Test(Crusade_Pluto.拒絕討伐))
            {
                Say(pc, 11000780, 131, "$R呐！從那個傳送點$R回到您原來的地方吧！$R;");
                return;
            }
            if (Crusade_Pluto_mask.Test(Crusade_Pluto.幫助討伐))
            {
                Say(pc, 11000780, 131, "拜託，如果發現冥王的話$R幫我消滅它$R;");
                return;
            }
            Say(pc, 11000780, 131, "您到底…？！$R從哪裡來？$R;" +
                "$P啊是那樣…又開始了$R;" +
                "$P這裡危險!快回到原來的地方吧!$R;" +
                "$R來!從那個傳送點$R回到您原來的地方!$R;");

            switch (Select(pc, "怎麼辦?", "", "不知道説什麽，還是回去吧", "…這是什麽地方？"))
            {
                case 1:
                    Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, true);
                    break;
                case 2:
                    Say(pc, 11000780, 131, "…這裡是封印地獄怪獸「冥王」的$R地方…$R;" +
                        "$P我叫普羅莎娜$R是道米尼的神天使$R;" +
                        "$R負責監獄的監視工作$R;" +
                        "$P從您那裡能感受到$R「赤銅寶物珠」散發出來的黑暗波動$R;" +
                        "$R就是那個波動引來了魔物阿$R;");
                    switch (Select(pc, "怎麼辦?", "", "不知道説什麽，但是回去吧", "…冥王是什麽?"))
                    {
                        case 1:
                            Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, true);
                            break;
                        case 2:
                            Say(pc, 11000780, 131, "冥王是這個世界裡的地獄怪獸$R;" +
                                "$P宗族處罰我要監視著$R封印冥王的監獄$R;" +
                                "$P這個監獄建在次元之間$R和任何一個世界都不相通$R;" +
                                "$P就在次元的裂縫中，冥王逃跑了$R就像斷線的風筝一樣，不知去向$R;" +
                                "$R…可能去了埃米爾世界吧$R;" +
                                "$P您擁有只有我們家族的背德者$R才擁有的「赤銅寶物珠」力量$R;" +
                                "$P能不能代替我出去$R找找冥王的行蹤呢？$R;");
                            switch (Select(pc, "怎麼辦?", "", "幫他", "算了"))
                            {
                                case 1:
                                    Say(pc, 11000780, 131, "…謝謝您接受我的委託$R;" +
                                        "$R冥王喜歡又黑又濕的洞窟$R;");

                                    Say(pc, 11000780, 131, "拜託，如果發現冥王的話$R幫我消滅它$R;");
                                    Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, false);
                                    Crusade_Pluto_mask.SetValue(Crusade_Pluto.幫助討伐, true);
                                    break;
                                case 2:
                                    Say(pc, 11000780, 131, "…是嗎？$R很抱歉，向您提出這麽無理的要求$R;" +
                                        "$R來!從那個傳送點$R回到您原來的地方吧!$R;");
                                    Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, true);
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }
    }
}