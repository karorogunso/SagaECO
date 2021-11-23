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
                Say(pc, 11001089, 131, "怎么办啊…$R理路……$R;");
                return;
            }
            if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.與飛空艇的瑪莎對話) &&
                !Neko_03_cmask.Test(Neko_03.與飛空艇的桃子對話))
            {
                Say(pc, 11001089, 131, "怎么办啊…$R理路……$R;");
                return;
            }
            Neko_03_cmask.SetValue(Neko_03.與飛空艇的瑪莎對話, true);
            Say(pc, 11001089, 131, "！！…混成骑士团的装甲步队！？$R;" +
                "$R你们真是可恶！！$R怎可以随便$R闯进别人的飞空庭呢？$R;");
            Say(pc, 11001095, 131, "我们是混城骑士团的$R空中侦察步队$R;" +
                "$R我们是根据上级命令$R虽然知道这样做不合情理$R但还是要搜查玛莎的飞空庭！$R;" +
                "$P当然，除了我们要找的「对象」$R我们不会抓其他人的$R;" +
                "$R根据命令，调查结束以前$R这飞空庭暂时由我们管理$R;");
            Say(pc, 11001096, 131, "队长！$R「对象」移送到货物场完毕！$R;");
            Say(pc, 11001095, 131, "好！叫他们出发吧！$R;");
            Say(pc, 11001089, 131, "队长?……理路！！$R;");
            PlaySound(pc, 2438, false, 100, 50);
            Wait(pc, 2000);
            Say(pc, 11001089, 131, "怎么会……$R;");
            Say(pc, 11001096, 131, "找不到「对象」啊$R只有一只宠物而已$R;" +
                "$R还有大量合成失败物……$R;");
            Say(pc, 11001089, 131, "哎哎哎！！$R那…那个…是用来烹调的……$R;" +
                "$P还放了在床底呀…$R;" +
                "$R哎~真是！$R;");
        }
    }
}