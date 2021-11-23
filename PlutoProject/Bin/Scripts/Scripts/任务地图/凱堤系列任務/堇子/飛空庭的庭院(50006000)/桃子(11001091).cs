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
                    "$P…宠物飞天鼠$R好像要说什么$R老看着这边阿…$R;" +
                    "$R…发生什么事呢…?$R;" +
                    "$P…飞天鼠的钱包里$R好像藏了些什么…?$R;" +
                    "$R好像是故意不给骑士团发现呀！！$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, 131, "发现了『旧匕首』！$R;");
                Say(pc, 11001089, 131, "…??$R;" +
                    "$R（为什么？飞天鼠为什么这样？）$R;" +
                    "$P（咦！那匕首！$R是理路的?）$R;" +
                    "$R（原来飞天鼠救了他…！！$R谢天谢地啊）$R;");
                Say(pc, 11001091, 131, "……$R;");
                Say(pc, 11001089, 131, "（让我看一看…$R…这里刻了什么？$R…是名字?…）$R;" +
                    "$P…$R;" +
                    "$P……$R;" +
                    "$P哇！！?$R;");
                Say(pc, 11001095, 131, "啊！吓死我了！$R;" +
                    "$R干嘛！?$R有什么事阿！？$R;");
                Say(pc, 11001089, 131, "嗯！？不！$R嘻嘻…没什么$R;" +
                    "$P（……这里！…$R…不就刻着「加多」嘛！）$R;");
                Say(pc, 0, 131, "（加多…$R难道！？会是那「斩鬼者加多」吗??）$R;");
                Say(pc, 11001089, 131, "（可能是啊…$R那么那孩子是加多的…）$R;" +
                    "$R（大事不妙阿！$R不快点告诉加多的话…！）$R;");
                Say(pc, 11001095, 131, "…??$R嗨…！？把那藏着的快拿出来！！$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, 131, "『旧匕首』被抢走了！$R;");
                Say(pc, 11001089, 131, "真是！！被发现了！！$R;" +
                    "$P您快逃吧！$R;" +
                    "$R得快点通知$R在「鬼眠之岩」的加多$R一起去救理路呀！！$R;");
                Say(pc, 0, 131, "玛莎使用了地上的紧急按纽$R;");
                Say(pc, 11001095, 131, "…等等！！你在干嘛！？$R;");
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