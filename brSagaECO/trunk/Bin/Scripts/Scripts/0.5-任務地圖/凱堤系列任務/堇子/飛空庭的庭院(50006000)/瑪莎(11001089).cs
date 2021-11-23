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
    public class S11001089 : Event
    {
        public S11001089()
        {
            this.EventID = 11001089;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_03> Neko_03_amask = pc.AMask["Neko_03"];
            BitMask<Neko_03> Neko_03_cmask = pc.CMask["Neko_03"];


            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.與飛空艇的桃子對話) &&
                !Neko_03_cmask.Test(Neko_03.與鬼斬破多加對話))
            {
                Say(pc, 11001089, 131, "怎麼辦阿…$R理路……$R;");
                return;
            }
            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.與飛空艇的瑪莎對話) &&
                !Neko_03_cmask.Test(Neko_03.與飛空艇的桃子對話))
            {
                Say(pc, 11001089, 131, "怎麼辦阿…$R理路……$R;");
                return;
            }
            Neko_03_cmask.SetValue(Neko_03.與飛空艇的瑪莎對話, true);
            Say(pc, 11001089, 131, "！！…混城騎士團的裝甲步隊！？$R;" +
                "$R你們真是可惡！！$R怎可以隨便$R闖進別人的飛空庭呢？$R;");
            Say(pc, 11001095, 131, "我們是混城騎士團的$R空中偵察步隊$R;" +
                "$R我們是根據上級命令$R雖然知道這樣做不合情理$R但還是要搜查瑪莎的飛空庭！$R;" +
                "$P當然，除了我們要找的「對象」$R我們不會抓其他人的阿$R;" +
                "$R根據命令，調查結束以前$R這飛空庭暫時由我們管理$R;");
            Say(pc, 11001096, 131, "隊長！$R「對象」移送到貨物場完畢！$R;");
            Say(pc, 11001095, 131, "好！叫他們出發吧！$R;");
            Say(pc, 11001089, 131, "隊長?……理路！！$R;");
            PlaySound(pc, 2438, false, 100, 50);
            Wait(pc, 2000);
            Say(pc, 11001089, 131, "怎麼會……$R;");
            Say(pc, 11001096, 131, "找不到對象阿$R只有一隻寵物而已$R;" +
                "$R還有大量合成失敗物……$R;");
            Say(pc, 11001089, 131, "哎哎哎！！$R那…那個…是用來烹調的……$R;" +
                "$P還放了在床底呀…$R;" +
                "$R哎~真是！$R;");
        }
    }
}