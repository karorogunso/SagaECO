
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S100200000 : Event
    {
        public S100200000()
        {
            this.EventID = 100200000;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 100200000) > 0)
            {
                ChangeMessageBox(pc);
                Say(pc, 0, "巧克力慕斯是一道制作过程非常简单的法式甜品，你只需要准备三个材料$R一块巧克力、一点点糖、和一些蛋清。$R巧克力的品质决定一切！$R这可是美味的起点。");
                Say(pc, 0, "首先将巧克力切碎，然后用隔水炖锅，用文火慢慢将巧克力煨融化。");
                Say(pc, 0, "然后从鸡蛋中取蛋清，一定要确保没有一点点蛋黄掺入。$R因为蛋黄富含脂肪，而蛋白则反之。$R最好在蛋清里加一点点柠檬汁，柠檬汁会让蛋白凝聚在一起。$R如果蛋黄不小心掉进去了，小心的舀出来就可以啦。");
                Say(pc, 0, "然后就是搅拌慕斯的时间啦！$R搅拌蛋清直到搅出泡沫，泡沫越大越好哦，这样慕斯的口味才会更加清爽$R当蛋清起泡泡的时候，加入一点点细砂糖~");
                Say(pc, 0, "然后融化好的巧克力里，加入三分之一的搅拌好的蛋白！$R然后趁蛋清没变冷的时候快点搅拌！不然会变得很难搅哦。$R随后加入剩余的蛋白，之后就慢慢搅。");
                Say(pc, 0, "将搅拌好的慕斯，倒入杯子中，在冰箱里放2个小时就完成啦！$R是不是非常简单呢？$R$R$CR※如果料理等级没有达到2，会有非常大的几率做出黑暗料理！$CD");
            }
        }
    }
}

