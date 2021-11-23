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
                Say(pc, 11001088, 131, "??$R为什么要这样?$R;" +
                    "$R难道…您改变了主意？$R不帮助我们吗?$R;");
            }
            else if (Neko_03_amask.Test(Neko_03.堇子任務開始) &&
                !Neko_03_amask.Test(Neko_03.堇子任務完成) &&
                Neko_03_cmask.Test(Neko_03.與初心者學校老師對話) &&
                !Neko_03_cmask.Test(Neko_03.與商人之家的瑪莎對話))
            {
                Neko_03_cmask.SetValue(Neko_03.與商人之家的瑪莎對話, true);
                Say(pc, 11001088, 131, "??$R;" +
                    "$R你也在追捕理路吗？$R;" +
                    "$P……！！$R难道你己经是$R混成骑士团的成员了！？$R;");
                Say(pc, 11001086, 131, "玛莎，请放心吧$R阿克罗波利斯$R行会评议会$R刚刚跟我们联系了$R;" +
                    "$R是听了路蓝的委托$R而到这里来的$R;");
                Say(pc, 11001088, 131, "哎！是老奶奶拜托的…$R;" +
                    "$R幸好…$R;");
                Say(pc, 0, 131, "玛莎??$R;" +
                    "$R老奶奶??$R;");
                Say(pc, 11001086, 131, "玛莎是评议会会长路蓝的孙女呀$R;");
                Say(pc, 0, 131, "！！！$R;");
                Say(pc, 11001088, 131, "那孩子现在潜入了我的飞空庭啊$R;" +
                    "$R是为了逃避混成骑士团的追捕吧$R;" +
                    "$P他名子叫「理路」$R;" +
                    "$P…他好像在货物场$R看到了什么$R;" +
                    "$R所以现在被追揖啊$R;");
                Say(pc, 11001086, 131, "虽然应该不会是要杀人灭口…$R;" +
                    "$R该不会是理路看到了$R他们想藏起来的东西吧…$R;" +
                    "$P…玛莎$R够了…$R;");
                Say(pc, 11001088, 131, "嗯！…好吧！$R;" +
                    "$P我想用飞空庭$R把那孩子送到城市去$R;" +
                    "$R搭乘飞空庭去$R混成骑士团应该不会发现吧$R;" +
                    "$P对了$R;" +
                    "$R您可以要帮忙吗？$R;" +
                    "$P我一个人送他去有点不安呀$R但埃米尔又不知道去那儿了$R;" +
                    "$R偏偏就在这时间不见了…吱吱…$R;");
            }
            else
            {
                Say(pc, 11001088, 131, "…吓死我了！$R;" +
                    "$R呀！欢迎光临$R;");
                return;
            }
            switch (Select(pc, "怎么办呢？", "", "帮忙！", "不帮忙！"))
            {
                case 1:
                    Say(pc, 11001088, 131, "多谢您了♪$R;" +
                        "$R那么就立刻出发吧！$R准备好了吗？$R;");
                    switch (Select(pc, "准备好了吗？", "", "准备好了", "还没好"))
                    {
                        case 1:
                            if (pc.PossesionedActors.Count != 0)
                            {
                                Say(pc, 11001088, 131, "哎！有人在凭依对吧?$R;" +
                                    "$R不行！您自己一个人搭乘吧！$R;");
                                Say(pc, 11001088, 131, "那么，都准备好了，才出发吧♪$R;");
                                return;
                            }
                            Say(pc, 11001088, 131, "好！$R那么我们出发吧！！♪$R;");
                            pc.CInt["Neko_03_Map1"] = CreateMapInstance(50006000, 10018100, 220, 66);

                            Warp(pc, (uint)pc.CInt["Neko_03_Map1"], 7, 12);
                            //EVENTMAP_IN 6 1 7 12 4
                            /*
                            if (a)
                            {
                                Say(pc, 11001088, 131, "咦?…飞空庭好像被航空管制$R不能飞了$R;" +
                                    "$R不好意思唷$R稍等一下再过来吧$R;");
                                return;
                            }//*/
                            break;
                        case 2:
                            Say(pc, 11001088, 131, "那么，都准备好了，出发吧♪$R;");
                            break;
                    }
                    break;
                case 2:
                    Say(pc, 11001088, 131, "是吗？没办法了！$R;" +
                        "$R唉！这事得保守秘密呀$R;" +
                        "$R拜托您了$R;");
                    break;
            }
        }
    }
}