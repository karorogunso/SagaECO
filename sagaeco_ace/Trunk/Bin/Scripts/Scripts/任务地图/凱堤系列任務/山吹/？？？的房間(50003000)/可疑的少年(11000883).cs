using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50003000
{
    public class S11000883 : Event
    {
        public S11000883()
        {
            this.EventID = 11000883;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];

            Neko_04_cmask.SetValue(Neko_04.與小孩對話, true);
            Say(pc, 0, 131, "呀！就是这个孩子！！$R;", "猫灵（山吹）");
            Say(pc, 0, 131, "…好像是远处传来的声音？$R;");
            Say(pc, 0, 131, "号编22111994要价尔极!…个是真它$R;" +
                "$R土的变出物…集吧来收！$R驀符前我$R;" +
                "$R算庭游入正实耀$R乎吾牢知让$R;" +
                "$R怏失无修寞絛橄疗定也走$R;" +
                "$R一剩走快也大乱木乎…$R;", "谜之声音");
            Say(pc, 0, 131, "什么意思呀…？");
            Wait(pc, 666);
            ShowEffect(pc, 11000883, 5049);
            Wait(pc, 666);
            pc.CInt["Neko04_03_Map"] = CreateMapInstance(50004000, 30114000, 13, 1);
            LoadSpawnFile(pc.CInt["Neko04_03_Map"], "DB/Spawns/50004000.xml");
            Warp(pc, (uint)pc.CInt["Neko04_03_Map"], 3, 5);
            //EVENTMAP_IN 4 1 3 5 4
            //SWITCH START
            //ME.WORK0 = -1 EVT1100088300
            //SWITCH END
            //EVENTEND
            //EVT1100088300
            /*
            Neko_04_cmask.SetValue(Neko_04.與小孩對話, false);
            Say(pc, 0, 131, "什么意思啊…？$R那些话好奇怪哦!$R;" +
                "$R再去跟他们说话吧$R;");
            //EVENTEND*/

        }
    }
}