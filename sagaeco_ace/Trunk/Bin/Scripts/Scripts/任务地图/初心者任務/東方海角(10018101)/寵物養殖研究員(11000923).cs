using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018101) NPC基本信息:寵物養殖研究員(11000923) X:203 Y:88
namespace SagaScript.M10018101
{
    public class S11000923 : Event
    {
        public S11000923()
        {
            this.EventID = 11000923;
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

            Say(pc, 11000923, 131, "您好，$R;" +
                                   "对这些小家伙感兴趣吗?$R;", "宠物养殖研究员");

            selection = Select(pc, "想听关于宠物的说明吗?", "", "听宠物的说明", "听关于饲养宠物的注意事项", "现在不听");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000923, 131, "在ECO裡，您还可以饲养宠物哦$R;" +
                                               "$P除了这里还有其他很多种的宠物$R;" +
                                               "$R和喜欢的宠物一起冒险如何?$R;" +
                                               "$P和宠物在一起$R;" +
                                               "可以提高特定能力$R;" +
                                               "$R或在战斗中一同作战唷$R;" +
                                               "$P还有可以骑的「骑乘宠物」喔$R;" +
                                               "$R旁边的「炽色步行龙」和$R;" +
                                               "「骑乘用爬爬虫」就是「骑乘宠物」$R;" +
                                               "最后还有叫「猫灵」的宠物$R;" +
                                               "$P跟埃米尔说话时见过吧?$R;" +
                                               "就是在埃米尔旁边飘著的小家伙$R;" +
                                               "它叫「猫灵」$R;" +
                                               "还有其他的宠物，自己寻找吧$R;", "宠物养殖研究员");
                        break;

                    case 2:
                        Say(pc, 11000923, 131, "饲养宠物跟职业也有关系$R;" +
                                               "$R可以让它攻击$R;" +
                                               "但不能使用宠物的「技能」$R;" +
                                               "$R…还有那种情况$R;" +
                                               "$P但是，想跟它在一起，$R;" +
                                               "培养感情是最重要的$R;" +
                                               "「自己喜欢的宠物」才是最好的呀$R;" +
                                               "$R用感情去饲养它吧$R;" +
                                               "$P用感情饲养的宠物$R;" +
                                               "在「战斗」中会更强哦$R;" +
                                               "$P还有那种情况$R;" +
                                               "$R饲养宠物也有适合的地方$R;" +
                                               "饲养时请参考说明吧$R;", "宠物养殖研究员");
                        break;
                }

                selection = Select(pc, "想听关于宠物的说明吗?", "", "听宠物的说明", "听关于饲养宠物的注意事项", "现在不听");
            }         
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11000923, 131, "什么? 听过了埃米尔的说明吗?$R;", "宠物养殖研究员");
        } 
    }
}
