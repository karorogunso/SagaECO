using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000790 : Event
    {
        public S11000790()
        {
            this.EventID = 11000790;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NNCYFlags> mask = pc.CMask["NNCY"];
            if (!mask.Test(NNCYFlags.爬爬蟲拉丁第一次對話) && pc.Level > 30 && pc.Fame > 19)//_5A85
            {
                mask.SetValue(NNCYFlags.爬爬蟲拉丁第一次對話, true);
                //_5A85 = true;
                Say(pc, 131, "♪$R;");
                Say(pc, 131, "可爱吧?$R;" +
                    "$R它叫「骑乘用爬爬虫」$R;" +
                    "是可以载着主人到处走的骑乘宠物唷$R;");
                Say(pc, 131, "由「小型爬爬虫」训练成的$R骑乘宠物，就是这小家伙了$R;" +
                    "$R「小型爬爬虫」力量大，$R所以可以载人和很多行李$R;" +
                    "$P法伊斯特市的「皮尔」最近在分发呢$R我也去拿了一只$R我要把它训练成骑乘宠物喔！$R;");
                int a;
                a = Select(pc, "要问有关骑乘动物的吗??", "", "跟一般宠物有什么分别?", "怎样分配的?", "怎样才可以得到?", "没什么要问的");
                while (a != 4)
                {
                    switch (a)
                    {
                        case 1:
                            Say(pc, 131, "不论如何，可以骑乘，$R就是跟一般宠物最大的差别啰$R;" +
                                "$R骑乘方法很简单，$R只要双击宠物的图示就可以了$R;" +
                                "$P乘坐的时候$R只要操控「骑乘宠物」就可以了$R这样一来，移动和打斗$R都是由「骑乘宠物」完成$R;" +
                                "$P受攻击时，$R受伤的也是「骑乘宠物」$R;" +
                                "$R不过，「骑乘宠物」$R打败魔物的经验值$R以及掉落的道具$R您就不能得到了。要记住啊！$R;");
                            break;
                        case 2:
                            Say(pc, 131, "「骑乘宠物」在您骑乘的时候$R跟魔物打斗的话$R宠物自己也会成长哦$R;" +
                                "$R这一点跟一般宠物一样$R;" +
                                "$P当「骑乘宠物」无法走动时$R失去战斗能力，亲密度会减1喔$R;" +
                                "$R使用「四叶草糖果」$R亲密度恢复程度$R跟其他宠物一样喔$R;");
                            break;
                        case 3:
                            Say(pc, 131, "每一种宠物得到的方法都不同喔$R但要得到「骑乘用爬爬虫」$R;" +
                                "只要把您细心饲养的$R「小型爬爬虫」送到这来$R;" +
                                "$P我会把它养育成$R「骑乘用爬爬虫」哦$R;" +
                                "$P「骑乘用爬爬虫」$R虽然可以运载很多行李$R但腿比较短$R;" +
                                "$P只要您的宠物在「小型爬爬虫」时期$R好好训练$R当它长成「骑乘用爬爬虫」时$R也可以敏捷地行动呀$R;");
                            break;
                    }
                    a = Select(pc, "要问有关骑乘动物的吗??", "", "跟一般宠物有什么分别?", "怎样分配的?", "怎样才可以得到?", "没什么要问的");
                }
                return;
            }
            Say(pc, 131, "哞哞$R;");
        }
    }
}