using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:裝模作樣的人(11000073) X:197 Y:145
namespace SagaScript.M10023001
{
    public class S20010090 : Event
    {
        public S20010090()
        {
            this.EventID = 20010090;
        }

        public override void OnEvent(ActorPC pc)
        {
            //BitMask<JOB3Z_01> Job3Z_01_mask = pc.CMask["Job3Z_01"];
            if ((pc.Job2X == PC_JOB.BLADEMASTER || pc.Job2X == PC_JOB.BOUNTYHUNTER) && (pc.JobLevel2T == 50 || pc.JobLevel2X == 50 )&& pc.Level >= 100)//剑士三转
            {

                Say(pc, 20010090, 131, "嘿?原来是" + pc.Name + "啊?$R;" +
                                       "好久不见了,虽然我认识的不是这个世界的你$R;", "凯特.西");
                Say(pc, 20010090, 131, "...你好像比我知道的那个你弱好多啊..$R;" +
                                        "没关系吗?$R;"+
                                        "我可以让你变得和那边差不多强哦?$R;", "凯特.西");
                switch (Select(pc, "相信这只猫吗?", "", "进行上位转生", "还是算了", "测试"))
                {
                    case 1:
                        //Job3Z_01_mask.SetValue(JOB3Z_01.转职成功, true);
                        //_3A37 = true;
                        Say(pc, 20010090, 131, "...变强后,弱小的阶段就回不来了$R;" +
                                        "你不会后悔吧?$R;", "凯特.西");
                        switch (Select(pc, "怎么办?", "", "当然没什么后悔的","还是算了"))
                        {
                            case 1:
                                ChangePlayerJob(pc, PC_JOB.GLADIATOR);
                                pc.Level = 1;
                                pc.JobLevel3 = 1;
                                pc.CEXP = 0;
                                pc.JEXP = 0;
                                ResetStatusPoint(pc);
                                pc.StatsPoint = 2;
                                //PARAM ME.JOB = 3
                                //PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4161);
                                Wait(pc, 4000);
                                Say(pc, 0, 0, "什么书和什么证都不要了啦$R;"+
                                              "这个世界的我,出现的因子还不够$R;"+
                                              "而我只是来旅行的啦$R;", "凯特.西");
                                Say(pc, 0, 0, "所以很多方面并不能完全发挥预期的作用$R;"+
                                              "见谅啦$R;", " 凯特.西");
                                Wait(pc, 4000);
                                Say(pc, 0, 0, "实力甩锅$R;", "谜之音");
                                break;
                            case 2:
                                break;
                        }
                        
                        break;
                    case 2:
                        Say(pc, 0, 0, "那么,我继续在这里休息$R;", " ");
                        break;
                    //case 3:
                    //    PlaySound(pc, 3087, false, 100, 50);
                    //    break;
                }
            }
            else if ((pc.Job2X == PC_JOB.FENCER || pc.Job2X == PC_JOB.DARKSTALKER) && (pc.JobLevel2T == 50 || pc.JobLevel2X == 50) && pc.Level >= 100)//骑士三转
            {

                Say(pc, 20010090, 131, "嘿?原来是" + pc.Name + "啊?$R;" +
                                       "好久不见了,虽然我认识的不是这个世界的你$R;", "凯特.西");
                Say(pc, 20010090, 131, "...你好像比我知道的那个你弱好多啊..$R;" +
                                        "没关系吗?$R;" +
                                        "我可以让你变得和那边差不多强哦?$R;", "凯特.西");
                switch (Select(pc, "相信这只猫吗?", "", "进行上位转生", "还是算了", "测试"))
                {
                    case 1:
                        //Job3Z_01_mask.SetValue(JOB3Z_01.转职成功, true);
                        //_3A37 = true;
                        Say(pc, 20010090, 131, "...变强后,弱小的阶段就回不来了$R;" +
                                        "你不会后悔吧?$R;", "凯特.西");
                        switch (Select(pc, "怎么办?", "", "当然没什么后悔的", "还是算了"))
                        {
                            case 1:
                                ChangePlayerJob(pc, PC_JOB.GUARDIAN);
                                pc.Level = 1;
                                pc.JobLevel3 = 1;
                                pc.CEXP = 0;
                                pc.JEXP = 0;
                                ResetStatusPoint(pc);
                                pc.StatsPoint = 2;
                                //PARAM ME.JOB = 3
                                //PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4161);
                                Wait(pc, 4000);
                                Say(pc, 0, 0, "什么书和什么证都不要了啦$R;" +
                                              "这个世界的我,出现的因子还不够$R;" +
                                              "而我只是来旅行的啦$R;", "凯特.西");
                                Say(pc, 0, 0, "所以很多方面并不能完全发挥预期的作用$R;" +
                                              "见谅啦$R;", " 凯特.西");
                                Wait(pc, 4000);
                                Say(pc, 0, 0, "实力甩锅$R;", "谜之音");
                                break;
                            case 2:
                                break;
                        }

                        break;
                    case 2:
                        Say(pc, 0, 0, "那么,我继续在这里休息$R;", " ");
                        break;
                    //case 3:
                    //    PlaySound(pc, 3087, false, 100, 50);
                    //    break;
                }
            }
            else if ((pc.Job2X == PC_JOB.ASSASSIN || pc.Job2X == PC_JOB.COMMAND) && (pc.JobLevel2T == 50 || pc.JobLevel2X == 50) && pc.Level >= 100)//盗贼三转
            {

                Say(pc, 20010090, 131, "嘿?原来是" + pc.Name + "啊?$R;" +
                                       "好久不见了,虽然我认识的不是这个世界的你$R;", "凯特.西");
                Say(pc, 20010090, 131, "...你好像比我知道的那个你弱好多啊..$R;" +
                                        "没关系吗?$R;" +
                                        "我可以让你变得和那边差不多强哦?$R;", "凯特.西");
                switch (Select(pc, "相信这只猫吗?", "", "进行上位转生", "还是算了", "测试"))
                {
                    case 1:
                        //Job3Z_01_mask.SetValue(JOB3Z_01.转职成功, true);
                        //_3A37 = true;
                        Say(pc, 20010090, 131, "...变强后,弱小的阶段就回不来了$R;" +
                                        "你不会后悔吧?$R;", "凯特.西");
                        switch (Select(pc, "怎么办?", "", "当然没什么后悔的", "还是算了"))
                        {
                            case 1:
                                ChangePlayerJob(pc, PC_JOB.ERASER);
                                pc.Level = 1;
                                pc.JobLevel3 = 1;
                                pc.CEXP = 0;
                                pc.JEXP = 0;
                                ResetStatusPoint(pc);
                                pc.StatsPoint = 2;
                                //PARAM ME.JOB = 3
                                //PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4161);
                                Wait(pc, 4000);
                                Say(pc, 0, 0, "什么书和什么证都不要了啦$R;" +
                                              "这个世界的我,出现的因子还不够$R;" +
                                              "而我只是来旅行的啦$R;", "凯特.西");
                                Say(pc, 0, 0, "所以很多方面并不能完全发挥预期的作用$R;" +
                                              "见谅啦$R;", " 凯特.西");
                                Wait(pc, 4000);
                                Say(pc, 0, 0, "实力甩锅$R;", "谜之音");
                                break;
                            case 2:
                                break;
                        }

                        break;
                    case 2:
                        Say(pc, 0, 0, "那么,我继续在这里休息$R;", " ");
                        break;
                    //case 3:
                    //    PlaySound(pc, 3087, false, 100, 50);
                    //    break;
                }
            }
            else if ((pc.Job2X == PC_JOB.DRUID || pc.Job2X == PC_JOB.BARD) && (pc.JobLevel2T == 50 || pc.JobLevel2X == 50) && pc.Level >= 100)//EC三转
            {

                Say(pc, 20010090, 131, "嘿?原来是" + pc.Name + "啊?$R;" +
                                       "好久不见了,虽然我认识的不是这个世界的你$R;", "凯特.西");
                Say(pc, 20010090, 131, "...你好像比我知道的那个你弱好多啊..$R;" +
                                        "没关系吗?$R;" +
                                        "我可以让你变得和那边差不多强哦?$R;", "凯特.西");
                switch (Select(pc, "相信这只猫吗?", "", "进行上位转生", "还是算了", "测试"))
                {
                    case 1:
                        //Job3Z_01_mask.SetValue(JOB3Z_01.转职成功, true);
                        //_3A37 = true;
                        Say(pc, 20010090, 131, "...变强后,弱小的阶段就回不来了$R;" +
                                        "你不会后悔吧?$R;", "凯特.西");
                        switch (Select(pc, "怎么办?", "", "当然没什么后悔的", "还是算了"))
                        {
                            case 1:
                                ChangePlayerJob(pc, PC_JOB.CARDINAL);
                                pc.Level = 1;
                                pc.JobLevel3 = 1;
                                pc.CEXP = 0;
                                pc.JEXP = 0;
                                ResetStatusPoint(pc);
                                pc.StatsPoint = 2;
                                //PARAM ME.JOB = 3
                                //PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4161);
                                Wait(pc, 4000);
                                Say(pc, 0, 0, "什么书和什么证都不要了啦$R;" +
                                              "这个世界的我,出现的因子还不够$R;" +
                                              "而我只是来旅行的啦$R;", "凯特.西");
                                Say(pc, 0, 0, "所以很多方面并不能完全发挥预期的作用$R;" +
                                              "见谅啦$R;", " 凯特.西");
                                Wait(pc, 4000);
                                Say(pc, 0, 0, "实力甩锅$R;", "谜之音");
                                break;
                            case 2:
                                break;
                        }

                        break;
                    case 2:
                        Say(pc, 0, 0, "那么,我继续在这里休息$R;", " ");
                        break;
                    //case 3:
                    //    PlaySound(pc, 3087, false, 100, 50);
                    //    break;
                }
            }
            else if ((pc.Job2X == PC_JOB.SORCERER || pc.Job2X == PC_JOB.SAGE) && (pc.JobLevel2T == 50 || pc.JobLevel2X == 50) && pc.Level >= 100)//WIZ三转
            {

                Say(pc, 20010090, 131, "嘿?原来是" + pc.Name + "啊?$R;" +
                                       "好久不见了,虽然我认识的不是这个世界的你$R;", "凯特.西");
                Say(pc, 20010090, 131, "...你好像比我知道的那个你弱好多啊..$R;" +
                                        "没关系吗?$R;" +
                                        "我可以让你变得和那边差不多强哦?$R;", "凯特.西");
                switch (Select(pc, "相信这只猫吗?", "", "进行上位转生", "还是算了", "测试"))
                {
                    case 1:
                        //Job3Z_01_mask.SetValue(JOB3Z_01.转职成功, true);
                        //_3A37 = true;
                        Say(pc, 20010090, 131, "...变强后,弱小的阶段就回不来了$R;" +
                                        "你不会后悔吧?$R;", "凯特.西");
                        switch (Select(pc, "怎么办?", "", "当然没什么后悔的", "还是算了"))
                        {
                            case 1:
                                ChangePlayerJob(pc, PC_JOB.FORCEMASTER);
                                pc.Level = 1;
                                pc.JobLevel3 = 1;
                                pc.CEXP = 0;
                                pc.JEXP = 0;
                                ResetStatusPoint(pc);
                                pc.StatsPoint = 2;
                                //PARAM ME.JOB = 3
                                //PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4161);
                                Wait(pc, 4000);
                                Say(pc, 0, 0, "什么书和什么证都不要了啦$R;" +
                                              "这个世界的我,出现的因子还不够$R;" +
                                              "而我只是来旅行的啦$R;", "凯特.西");
                                Say(pc, 0, 0, "所以很多方面并不能完全发挥预期的作用$R;" +
                                              "见谅啦$R;", " 凯特.西");
                                Wait(pc, 4000);
                                Say(pc, 0, 0, "实力甩锅$R;", "谜之音");
                                break;
                            case 2:
                                break;
                        }

                        break;
                    case 2:
                        Say(pc, 0, 0, "那么,我继续在这里休息$R;", " ");
                        break;
                    //case 3:
                    //    PlaySound(pc, 3087, false, 100, 50);
                    //    break;
                }
            }
            else
            {
                //PlaySound(pc, 1190, false, 100, 50);
                //ChangeBGM(pc, 1190, false, 100, 50);
                Say(pc, 20010090, 131, "嘿?原来是" + pc.Name + "啊?$R;" +
                                       "好久不见了,虽然我认识的不是这个世界的你$R;"+
                                       "最近过得好吗?", "凯特.西");
            }
        }
    }
}
