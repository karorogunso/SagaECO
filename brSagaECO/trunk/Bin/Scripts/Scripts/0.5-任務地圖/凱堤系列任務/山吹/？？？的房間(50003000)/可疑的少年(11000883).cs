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
            Say(pc, 0, 131, "呀！就是這個孩子！！$R;", "\"凱堤（山吹）\"");
            Say(pc, 0, 131, "…好像是遠處傳來的聲音？$R;");
            Say(pc, 0, 131, "號編22111994要價爾極!…個是真它$R;" +
                "$R土的變出物…集吧來收！$R驀符前我$R;" +
                "$R算庭遊入正實耀$R乎吾牢知讓$R;" +
                "$R怏失無修寞絛橄療定也走$R;" +
                "$R一剩走快也大亂木乎…$R;", "\"謎語團的聲音\"");
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
            Say(pc, 0, 131, "什麼意思呀…？$R那些話好奇怪唷!$R;" +
                "$R再去跟他們說話吧$R;");
            //EVENTEND*/

        }
    }
}