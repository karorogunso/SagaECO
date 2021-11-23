using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000673 : Event
    {
        public S11000673()
        {
            this.EventID = 11000673;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            Say(pc, 131, "欢迎光临法伊斯特市!$R;");
            if (!mask.Test(PSTFlags.觀光局職員第一次對話))//_4a86)
            {
                mask.SetValue(PSTFlags.觀光局職員第一次對話, true);
                //_4a86 = true;
                Say(pc, 131, "简单介绍一下这个地方！$R;" +
                    "$P这里是法伊斯特岛正中央的地方$R;" +
                    "$R无论是去哪个地方都很方便$R;" +
                    "以这里作为基地，进行冒险的话$R;" +
                    "会更方便的$R;" +
                    "$P南边有住宅和学校的$R;" +
                    "是法伊斯特国民居住地$R;" +
                    "$R北边有绿盾军的本部和仓库$R;" +
                    "还有可以通往商业区的路$R;" +
                    "$P此外还有牧场以及果园等$R;" +
                    "所以是个散步的好地方啊！$R;");
                return;
            }
            switch (Select(pc, "要给您带路吗？", "", "不用了", "管理局", "农夫行会总部", "绿盾军本部", "商店区", "学校", "城市整体説明"))
            {
                case 2:
                    Say(pc, 131, "法伊斯特管理局是$R;" +
                        "办理各种文书手续的地方$R;" +
                        "$R并给年轻的冒险家$R;" +
                        "委托简单的事情$R;");
                    Navigate(pc, 174, 84);
                    break;
                case 3:
                    Say(pc, 131, "这里是农夫行会总部吧$R;" +
                        "不仅是农夫行会$R;" +
                        "$R也有生产系行会分会$R;" +
                        "好好利用吧！$R;");
                    Navigate(pc, 174, 100);
                    break;
                case 4:
                    Say(pc, 131, "绿盾军的本部就是那个建筑$R;" +
                        "$R绿盾军如其名$R;" +
                        "是负责这个国家防御和$R;" +
                        "城市安全的国家！$R;" +
                        "$P就连喜欢小狗的人聚在一起$R;" +
                        "他们也管的！$R;");
                    Navigate(pc, 28, 100);
                    break;
                case 5:
                    Say(pc, 131, "这里是酒馆和武器商店的集中地$R;" +
                        "非常繁华$R;" +
                        "带您到商店区的入口吧$R;" +
                        "请随着箭头方向走$R;");
                    Navigate(pc, 114, 103);
                    break;
                case 6:
                    Say(pc, 131, "挂着大钟的建筑物就是学校$R;" +
                        "谁都可以上学的$R;" +
                        "去看看吧！$R;");
                    Navigate(pc, 147, 119);
                    break;
                case 7:
                    Say(pc, 131, "简单介绍一下这个地方！$R;" +
                        "$P这里是法伊斯特岛正中央的地方$R;" +
                        "$R无论是去哪个地方都很方便$R;" +
                        "以这里作为基地，进行冒险的话$R;" +
                        "会更方便的$R;" +
                        "$P南边有住宅和学校的$R;" +
                        "是法伊斯特国民居住地$R;" +
                        "$R北边有绿盾军的本部和仓库$R;" +
                        "还有可以通往商业区的路$R;" +
                        "$P此外还有牧场以及果园等$R;" +
                        "所以是个散步的好地方啊！$R;");
                    break;
            }
        }
    }
}