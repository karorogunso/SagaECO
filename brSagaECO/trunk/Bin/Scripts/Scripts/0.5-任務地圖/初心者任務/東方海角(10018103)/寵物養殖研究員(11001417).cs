using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018103) NPC基本信息:寵物養殖研究員(11001417) X:203 Y:88
namespace SagaScript.M10018103
{
    public class S11001417 : Event
    {
        public S11001417()
        {
            this.EventID = 11001417;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            int selection;

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11001417, 131, "您好，$R;" +
                                   "對這些小子感興趣嗎?$R;", "寵物養殖研究員");

            selection = Select(pc, "想聽關於寵物的說明嗎?", "", "聽寵物的說明", "聽關於飼養寵物的注意事項", "現在不聽");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11001417, 131, "在ECO裡，您還可以飼養寵物唷$R;" +
                                               "$P除了這裡還有其他很多種的寵物$R;" +
                                               "$R和喜歡的寵物一起冒險如何?$R;" +
                                               "$P和寵物在一起$R;" +
                                               "可以提高特定能力$R;" +
                                               "$R或在戰鬥中一同作戰唷$R;" +
                                               "$P還有可以騎的「騎乘寵物」喔$R;" +
                                               "$R旁邊的「火紅德拉古」和$R;" +
                                               "「爬爬蟲拉丁」就是「騎乘寵物」$R;" +
                                               "最後還有叫「凱堤」的寵物$R;" +
                                               "$P跟埃米爾說話時見過吧?$R;" +
                                               "就是在埃米爾旁邊飄著的小子$R;" +
                                               "它叫「凱堤」$R;" +
                                               "還有其他的寵物，自己尋找吧$R;", "寵物養殖研究員");
                        break;

                    case 2:
                        Say(pc, 11001417, 131, "飼養寵物跟職業也有關係$R;" +
                                               "$R可以讓它攻擊$R;" +
                                               "但不能使用寵物的「技能」$R;" +
                                               "$R…還有那種情況$R;" +
                                               "$P但是，想跟它在一起，$R;" +
                                               "培養感情是最重要的$R;" +
                                               "「自己喜歡的寵物」才是最好的呀$R;" +
                                               "$R用感情去飼養它吧$R;" +
                                               "$P用感情飼養的寵物$R;" +
                                               "在「戰鬥」中會更強唷$R;" +
                                               "$P還有那種情況$R;" +
                                               "$R飼養寵物也有適合的地方唷$R;" +
                                               "飼養時請參考說明吧$R;", "寵物養殖研究員");
                        break;
                }

                selection = Select(pc, "想聽關於寵物的說明嗎?", "", "聽寵物的說明", "聽關於飼養寵物的注意事項", "現在不聽");
            }         
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001417, 131, "什麼? 聽過了埃米爾的說明嗎?$R;", "寵物養殖研究員");
        } 
    }
}
