using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城東邊吊橋(10023100) NPC基本信息:禮物搬運工人(13000025) X:227 Y:121
namespace SagaScript.M10023100
{
    public class S13000025 : Event
    {
        public S13000025()
        {
            this.EventID = 13000025;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "欢迎来到$CL樱の梦 - ECO$CD$R;", "TT");

            if (pc.AInt["FKT"] != 1 && DateTime.Now.ToString("yyyyMMdd") == "20090712")
            {
                if (pc.Level > 49 && pc.Fame > 29)
                {
                    BitMask<FGarden> fgarden = pc.AMask["FGarden"];
                    if (pc.FGarden == null)
                        pc.FGarden = new SagaDB.FGarden.FGarden(pc);
                    GiveItem(pc, 10022700, 1);
                    fgarden.SetValue(FGarden.得到飛空庭鑰匙, true);
                    Say(pc, 131, "得到了$CL飞空庭钥匙$CD$R;", "TT");
                    if (pc.Level > 69)
                    {
                        int b = Global.Random.Next(1, 10);
                        if (b <= 6)
                        {
                            GiveItem(pc, 30001200, 1);
                            Say(pc, 131, "得到了$CL小屋$CD$R;", "TT");

                            int c = Global.Random.Next(1, 31);
                            switch (c)
                            {
                                case 1:
                                    GiveItem(pc, 30050100, 1);
                                    Say(pc, 131, "得到了$CL古典风地板$CD$R;", "TT");
                                    break;
                                case 2:
                                    GiveItem(pc, 30050101, 1);
                                    Say(pc, 131, "得到了$CL乡村风地板$CD$R;", "TT");
                                    break;
                                case 3:
                                    GiveItem(pc, 30050102, 1);
                                    Say(pc, 131, "得到了$CL日耳曼风地板$CD$R;", "TT");
                                    break;
                                case 4:
                                    GiveItem(pc, 30050103, 1);
                                    Say(pc, 131, "得到了$CL金属风格地板$CD$R;", "TT");
                                    break;
                                case 5:
                                    GiveItem(pc, 30050104, 1);
                                    Say(pc, 131, "得到了$CL清凉风地板$CD$R;", "TT");
                                    break;
                                case 6:
                                    GiveItem(pc, 30050105, 1);
                                    Say(pc, 131, "得到了$CL流行的砖制地板$CD$R;", "TT");
                                    break;
                                case 7:
                                    GiveItem(pc, 30050106, 1);
                                    Say(pc, 131, "得到了$CL幻想风地板$CD$R;", "TT");
                                    break;
                                case 8:
                                    GiveItem(pc, 30050107, 1);
                                    Say(pc, 131, "得到了$CL大理石地板$CD$R;", "TT");
                                    break;
                                case 9:
                                    GiveItem(pc, 30050108, 1);
                                    Say(pc, 131, "得到了$CL木纹地板$CD$R;", "TT");
                                    break;
                                case 10:
                                    GiveItem(pc, 30050110, 1);
                                    Say(pc, 131, "得到了$CL高级和风地板$CD$R;", "TT");
                                    break;
                                case 11:
                                    GiveItem(pc, 30050111, 1);
                                    Say(pc, 131, "得到了$CL圣诞夜地板$CD$R;", "TT");
                                    break;
                                case 12:
                                    GiveItem(pc, 30050112, 1);
                                    Say(pc, 131, "得到了$CL飘雪地板$CD$R;", "TT");
                                    break;
                                case 13:
                                    GiveItem(pc, 30050113, 1);
                                    Say(pc, 131, "得到了$CL心形花纹地板$CD$R;", "TT");
                                    break;
                                case 14:
                                    GiveItem(pc, 30050114, 1);
                                    Say(pc, 131, "得到了$CL友情风地板$CD$R;", "TT");
                                    break;
                                case 15:
                                    GiveItem(pc, 30050115, 1);
                                    Say(pc, 131, "得到了$CL猫猫便利店地板$CD$R;", "TT");
                                    break;
                                case 16:
                                    GiveItem(pc, 30040000, 1);
                                    Say(pc, 131, "得到了$CL古典风墙纸$CD$R;", "TT");
                                    break;
                                case 17:
                                    GiveItem(pc, 30040001, 1);
                                    Say(pc, 131, "得到了$CL乡村风墙纸$CD$R;", "TT");
                                    break;
                                case 18:
                                    GiveItem(pc, 30040002, 1);
                                    Say(pc, 131, "得到了$CL日耳曼风墙纸$CD$R;", "TT");
                                    break;
                                case 19:
                                    GiveItem(pc, 30040003, 1);
                                    Say(pc, 131, "得到了$CL矿工风墙纸$CD$R;", "TT");
                                    break;
                                case 20:
                                    GiveItem(pc, 30040004, 1);
                                    Say(pc, 131, "得到了$CL清凉风墙纸$CD$R;", "TT");
                                    break;
                                case 21:
                                    GiveItem(pc, 30040005, 1);
                                    Say(pc, 131, "得到了$CL可爱风墙纸$CD$R;", "TT");
                                    break;
                                case 22:
                                    GiveItem(pc, 30040006, 1);
                                    Say(pc, 131, "得到了$CL幻想风墙纸$CD$R;", "TT");
                                    break;
                                case 23:
                                    GiveItem(pc, 30040007, 1);
                                    Say(pc, 131, "得到了$CL野性风墙纸$CD$R;", "TT");
                                    break;
                                case 24:
                                    GiveItem(pc, 30040008, 1);
                                    Say(pc, 131, "得到了$CL木纹墙纸$CD$R;", "TT");
                                    break;
                                case 25:
                                    GiveItem(pc, 30040010, 1);
                                    Say(pc, 131, "得到了$CL高级和风墙纸$CD$R;", "TT");
                                    break;
                                case 26:
                                    GiveItem(pc, 30040011, 1);
                                    Say(pc, 131, "得到了$CL飘雪墙纸$CD$R;", "TT");
                                    break;
                                case 27:
                                    GiveItem(pc, 30040012, 1);
                                    Say(pc, 131, "得到了$CL心形花纹墙纸$CD$R;", "TT");
                                    break;
                                case 28:
                                    GiveItem(pc, 30040013, 1);
                                    Say(pc, 131, "得到了$CL童真风墙纸$CD$R;", "TT");
                                    break;
                                case 29:
                                    GiveItem(pc, 30040014, 1);
                                    Say(pc, 131, "得到了$CL友情风墙纸$CD$R;", "TT");
                                    break;
                                case 30:
                                    GiveItem(pc, 30040015, 1);
                                    Say(pc, 131, "得到了$CL童真风墙纸（粉色）$CD$R;", "TT");
                                    break;
                                case 31:
                                    GiveItem(pc, 30040016, 1);
                                    Say(pc, 131, "得到了$CL猫猫便利店墙纸$CD$R;", "TT");
                                    break;
                            }
                        }
                        else if (b <= 9)
                        {
                            int c = Global.Random.Next(1, 14);
                            switch (c)
                            {
                                case 1:
                                    GiveItem(pc, 30000000, 1);
                                    Say(pc, 131, "得到了$CL阿克罗波利斯小屋$CD$R;", "TT");
                                    break;
                                case 2:
                                    GiveItem(pc, 30000100, 1);
                                    Say(pc, 131, "得到了$CL乡村小屋$CD$R;", "TT");
                                    break;
                                case 3:
                                    GiveItem(pc, 30000200, 1);
                                    Say(pc, 131, "得到了$CL摩戈小屋$CD$R;", "TT");
                                    break;
                                case 4:
                                    GiveItem(pc, 30000300, 1);
                                    Say(pc, 131, "得到了$CL艾恩萨乌斯小屋$CD$R;", "TT");
                                    break;
                                case 5:
                                    GiveItem(pc, 30000400, 1);
                                    Say(pc, 131, "得到了$CL诺森小屋$CD$R;", "TT");
                                    break;
                                case 6:
                                    GiveItem(pc, 30000500, 1);
                                    Say(pc, 131, "得到了$CL东方小屋$CD$R;", "TT");
                                    break;
                                case 7:
                                    GiveItem(pc, 30000600, 1);
                                    Say(pc, 131, "得到了$CL泰迪小屋$CD$R;", "TT");
                                    break;
                                case 8:
                                    GiveItem(pc, 30000700, 1);
                                    Say(pc, 131, "得到了$CL冰雪小屋$CD$R;", "TT");
                                    break;
                                case 9:
                                    GiveItem(pc, 30000800, 1);
                                    Say(pc, 131, "得到了$CL宝石小屋$CD$R;", "TT");
                                    break;
                                case 10:
                                    GiveItem(pc, 30000900, 1);
                                    Say(pc, 131, "得到了$CL城堡小屋$CD$R;", "TT");
                                    break;
                                case 11:
                                    GiveItem(pc, 30001000, 1);
                                    Say(pc, 131, "得到了$CL魔法小屋$CD$R;", "TT");
                                    break;
                                case 12:
                                    GiveItem(pc, 30001100, 1);
                                    Say(pc, 131, "得到了$CL背包小屋$CD$R;", "TT");
                                    break;
                                case 13:
                                    GiveItem(pc, 30001500, 1);
                                    Say(pc, 131, "得到了$CL阴森古堡风小屋$CD$R;", "TT");
                                    break;
                                case 14:
                                    GiveItem(pc, 30001600, 1);
                                    Say(pc, 131, "得到了$CL海洋小屋$CD$R;", "TT");
                                    break;
                            }
                        }
                        else
                        {
                            int c = Global.Random.Next(1, 8);
                            switch (c)
                            {
                                case 1:
                                    GiveItem(pc, 30001300, 1);
                                    Say(pc, 131, "得到了$CL瞌睡羊小屋$CD$R;", "TT");
                                    break;
                                case 2:
                                    GiveItem(pc, 30001400, 1);
                                    Say(pc, 131, "得到了$CL糕点小屋$CD$R;", "TT");
                                    break;
                                case 3:
                                    GiveItem(pc, 30001700, 1);
                                    Say(pc, 131, "得到了$CL森林音乐家的屋子$CD$R;", "TT");
                                    break;
                                case 4:
                                    GiveItem(pc, 30001800, 1);
                                    Say(pc, 131, "得到了$CL古老的神社$CD$R;", "TT");
                                    break;
                                case 5:
                                    GiveItem(pc, 30001900, 1);
                                    Say(pc, 131, "得到了$CL令人怀念的学校$CD$R;", "TT");
                                    break;
                                case 6:
                                    GiveItem(pc, 30002000, 1);
                                    Say(pc, 131, "得到了$CL茅草屋顶的房子$CD$R;", "TT");
                                    break;
                                case 7:
                                    GiveItem(pc, 30002100, 1);
                                    Say(pc, 131, "得到了$CL猫猫便利店$CD$R;", "TT");
                                    break;
                                case 8:
                                    GiveItem(pc, 30002200, 1);
                                    Say(pc, 131, "得到了$CL婚约的教堂$CD$R;", "TT");
                                    break;
                            }
                        }

                    }
                    pc.AInt["FKT"] = 1;
                }
                else if (pc.Level > 29 && pc.Level < 50)
                {
                    GiveItem(pc, 10048701, 10);
                    GiveItem(pc, 10048702, 10);
                    pc.Gold += 50000;
                    Say(pc, 131, "得到了$CL大人的ECO恢复罐头$CD$R;" +
                        "得到了$CL大人的ECO恢复罐头（苏打味）$CD$R;" +
                        "得到了$CL50000G$CD$R;", "TT");
                    pc.AInt["FKT"] = 1;
                }
                else if (pc.Level < 30)
                {
                    GiveItem(pc, 10048701, 10);
                    GiveItem(pc, 10048702, 10);
                    Say(pc, 131, "得到了$CL大人的ECO恢复罐头$CD$R;" +
                        "得到了$CL大人的ECO恢复罐头（苏打味）$CD$R;", "TT");
                    pc.AInt["FKT"] = 1;
                }
                else
                {
                    Say(pc, 131, "你的$CL声望$CD不够哦,$R去做任务弄一点回来吧.$R;", "TT");
                }

            }
            /*
            if (pc.CInt["CXZ"] != 1)
            {
                Say(pc, 131, "你是第一次来这里吧.$R;" +
                    "作为欢迎送你一点礼物好了$R");
                switch (Select(pc, "接受礼物么", "", "接受", "不接受"))
                {
                    case 1:
                        if (pc.Level <= 5)
                        {
                            Say(pc, 131, "呃......$R多玩一下然后回来找我吧.$R;");
                            return;
                        }
                        Say(pc, 131, "来,这是给你的礼物$R;");
                        if (DateTime.Now.ToShortDateString() == "2009-06-28" ||
                            DateTime.Now.ToShortDateString() == "2009-06-29" ||
                            DateTime.Now.ToShortDateString() == "2009-6-28" ||
                            DateTime.Now.ToShortDateString() == "2009-6-29")
                        {
                            if (pc.Gender == PC_GENDER.FEMALE)
                            {
                                GiveItem(pc, 60105200, 1);
                                Say(pc, 131, "获得一个 漂亮的和服连衣裙♀$R;");
                            }
                            else
                            {
                                GiveItem(pc, 50004800, 1);
                                GiveItem(pc, 50013800, 1);
                                Say(pc, 131, "获得一个 管家服上衣♂$R;");
                                Say(pc, 131, "获得一个 管家服下衣♂$R;");
                            }
                        }
                        GiveItem(pc, 10043300, 10);
                        GiveItem(pc, 10000501, 10);
                        GiveItem(pc, 10000508, 10);
                        Say(pc, 131, "获得十个 三明治$R;" +
                            "获得十个 高级魔法药水$R;" +
                            "获得十个 高级耐力药水$R;");
                        if (pc.Level < 21)
                        {
                            pc.CInt["CXZ"] = 1;
                            return;
                        }
                        if (pc.Level < 31)
                        {
                            Say(pc, 131, "得到10000G$R;");
                            pc.Gold += 10000;
                            
                            pc.CInt["CXZ"] = 1;
                            return;
                        }
                        if (pc.Level < 41)
                        {
                            Say(pc, 131, "得到20000G$R;");
                            pc.Gold += 20000;
                            pc.CInt["CXZ"] = 1;
                            return;
                        }
                        Say(pc, 131, "得到30000G$R;");
                        pc.Gold += 30000;
                        pc.CInt["CXZ"] = 1;
                        return;
                    case 2:
                        Say(pc, 131, "嗯......$R;" +
                            "不要么?$R;" +
                            "$P那下次给你好了!$R;");
                        break;
                }
                Say(pc, 131, "还有其他什么事么?$R;");
            }
            Say(pc, 131, "想去哪里么?$R;" +
                "一次要收2000G哦$R;");
            if (pc.Gold > 1999)
            {
                Say(pc, 131, "那么,$R想去哪里呢?$R;");
                switch (Select(pc, "想去哪里?", "", "东方海角", "西方海角", "北方海角", "南方海角", "算了"))
                {
                    case 1:
                        Warp(pc, 10018100, 194, 64);
                        pc.Gold -= 2000;
                        break;
                    case 2:
                        Warp(pc, 10035000, 54, 175);
                        pc.Gold -= 2000;
                        break;
                    case 3:
                        Warp(pc, 10001000, 96, 21);
                        pc.Gold -= 2000;
                        break;
                    case 4:
                        Warp(pc, 10046000, 150, 221);
                        pc.Gold -= 2000;
                        break;
                }
                
            }
            else
                Say(pc, 131, "钱好像不够呢!");

            Say(pc, 131, "努力吧,少年$R;" + 
                "$P有问题的话请去$ECOAce论坛$CD汇报哦", "TT");//*/
        }
    }
}
