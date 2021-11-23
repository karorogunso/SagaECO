using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50006000
{
    public class S11001091 : Event
    {
        public S11001091()
        {
            this.EventID = 11001091;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];

            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.與飛空艇的瑪莎對話) &&
                !Neko_03_cmask.Test(Neko_03.與飛空艇的桃子對話))
            {
                Neko_03_cmask.SetValue(Neko_03.與飛空艇的桃子對話, true);
                Say(pc, 0, 131, "……??$R;" +
                    "$P…寵物飛天鼠$R好像要說什麼$R老看著這邊阿…$R;" +
                    "$R…發生什麼事呢…?$R;" +
                    "$P…飛天鼠的錢包裡$R好像藏了些什麼…?$R;" +
                    "$R好像是故意不給騎士團發現呀！！$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, 131, "發現了『舊匕首』！$R;");
                Say(pc, 11001089, 131, "…??$R;" +
                    "$R（為什麼？飛天鼠為什麼這樣？）$R;" +
                    "$P(咦！那匕首！$R是理路的?）$R;" +
                    "$R（原來飛天鼠救了他…！！$R謝天謝地阿）$R;");
                Say(pc, 11001091, 131, "……$R;");
                Say(pc, 11001089, 131, "（讓我看一看…$R…這裡刻了什麼？$R…是名字?…）$R;" +
                    "$P…$R;" +
                    "$P……$R;" +
                    "$P哇！！?$R;");
                Say(pc, 11001095, 131, "阿！嚇死我了！$R;" +
                    "$R幹嘛！?$R有什麼事阿！?$R;");
                Say(pc, 11001089, 131, "嗯！?不！$R嘻嘻…沒什麼$R;" +
                    "$P(……這裡！…$R…不就刻著「加多」嘛！）$R;");
                Say(pc, 0, 131, "(加多…$R難道！?會是那「抓鬼的加多」嗎??）$R;");
                Say(pc, 11001089, 131, "(可能是阿…$R那麼那孩子是加多的…）$R;" +
                    "$R(大事不妙阿！$R不快點告訴加多的話…！）$R;");
                Say(pc, 11001095, 131, "…??$R嗨…！?把那藏著的快拿出來！！$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, 131, "『舊匕首』被搶走了！$R;");
                Say(pc, 11001089, 131, "真是！！被發現了！！$R;" +
                    "$P您快逃吧！$R;" +
                    "$R得快點通知$R在「鬼魂安息處」的加多$R一起去救理路呀！！$R;");
                Say(pc, 0, 131, "瑪莎發現了地上的緊急按紐$R;");
                Say(pc, 11001095, 131, "…等等！！你在幹麼！？$R;");
                Wait(pc, 333);
                ShowEffect(pc, 0, 4023);
                Wait(pc, 1000);
                Warp(pc, 10025000, 113, 123);
                return;
            }
            Say(pc, 11001091, 131, "……$R;");
        }
    }
}