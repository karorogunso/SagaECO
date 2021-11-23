using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30060009
{
    public class S11001088 : Event
    {
        public S11001088()
        {
            this.EventID = 11001088;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];

            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.與商人之家的瑪莎對話) &&
                !Neko_03_cmask.Test(Neko_03.與飛空艇的桃子對話))//_7A33)
            {
                Say(pc, 11001088, 131, "??$R為什麼要這樣?$R;" +
                    "$R難道…您改變了主意？$R不幫助我們嗎?$R;");
            }
            else if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.與初心者學校老師對話) &&
                !Neko_03_cmask.Test(Neko_03.與商人之家的瑪莎對話))
            {
                Neko_03_cmask.SetValue(Neko_03.與商人之家的瑪莎對話, true);
                Say(pc, 11001088, 131, "??$R;" +
                    "$R你也在追捕理路嗎？$R;" +
                    "$P……！！$R難道你己經是$R混城騎士團的成員了！？$R;");
                Say(pc, 11001086, 131, "瑪莎，請放心吧$R阿高普路斯市$R行會評議會$R剛剛跟我們聯系了$R;" +
                    "$R為了聽這位路藍的委託$R而到這裡來的$R;");
                Say(pc, 11001088, 131, "哎！是老奶奶拜託的…$R;" +
                    "$R幸好…$R;");
                Say(pc, 0, 131, "瑪莎??$R;" +
                    "$R老奶奶??$R;");
                Say(pc, 11001086, 131, "瑪莎是評議會會長路藍的孫女呀$R;");
                Say(pc, 0, 131, "！！！$R;");
                Say(pc, 11001088, 131, "那孩子現在潛入了我的飛空庭阿$R;" +
                    "$R是為了逃避混城騎士團的追捕吧$R;" +
                    "$P他名子叫「理路」$R;" +
                    "$P…他好像在貨物場$R看到了什麼$R;" +
                    "$R所以現在被追揖阿$R;");
                Say(pc, 11001086, 131, "雖然應該不會是要殺人滅口…$R;" +
                    "$R該不會是理路看到了$R他們想藏起來的東西吧…$R;" +
                    "$P…瑪莎$R夠了…$R;");
                Say(pc, 11001088, 131, "嗯！…好吧！$R;" +
                    "$P我想用飛空庭$R把那孩子送到城市去$R;" +
                    "$R搭乘飛空庭去$R混城騎士團應該不會發現吧$R;" +
                    "$P對了$R;" +
                    "$R您可以要幫忙嗎？$R;" +
                    "$P我一個人送他去有點不安呀$R但埃米爾又不知道去那兒了$R;" +
                    "$R偏偏就在這時間不見了…吱吱…$R;");
            }
            else
            {
                Say(pc, 11001088, 131, "…嚇死我了！$R;" +
                    "$R呀！歡迎光臨唷$R;");
                return;
            }
            switch (Select(pc, "怎麼辦呢？", "", "幫忙！", "不幫忙！"))
            {
                case 1:
                    Say(pc, 11001088, 131, "多謝您了♪$R;" +
                        "$R那麼就立刻出發吧！$R準備好了嗎？$R;");
                    switch (Select(pc, "準備好了嗎？", "", "準備好了", "還沒好"))
                    {
                        case 1:
                            if (pc.PossesionedActors.Count != 0)
                            {
                                Say(pc, 11001088, 131, "哎！有人在憑依對吧?$R;" +
                                    "$R不行！您自己一個人搭乘吧！$R;");
                                Say(pc, 11001088, 131, "那麼，都準備好了，才出發吧♪$R;");
                                return;
                            }
                            Say(pc, 11001088, 131, "好！$R那麼我們出發吧！！♪$R;");
                            pc.CInt["Neko_03_Map1"] = CreateMapInstance(50006000, 10018100, 220, 66);

                            Warp(pc, (uint)pc.CInt["Neko_03_Map1"], 7, 12);
                            //EVENTMAP_IN 6 1 7 12 4
                            /*
                            if (a)
                            {
                                Say(pc, 11001088, 131, "咦?…飛空庭好像被航空管制$R不能飛了$R;" +
                                    "$R不好意思唷$R稍等一下再過來吧$R;");
                                return;
                            }//*/
                            break;
                        case 2:
                            Say(pc, 11001088, 131, "那麼，都準備好了，才出發吧♪$R;");
                            break;
                    }
                    break;
                case 2:
                    Say(pc, 11001088, 131, "是嗎？沒辦法了！$R;" +
                        "$R唉！這事得保守秘密呀$R;" +
                        "$R拜託您了$R;");
                    break;
            }
        }
    }
}