
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S60000003: Event
    {
        public S60000003()
        {
            this.EventID = 60000003;
        }

        public override void OnEvent(ActorPC pc)
        {
            DateTime endDT = new DateTime(2017, 2, 12);//设置结束日期为2017年2月12日
            if (DateTime.Now < endDT)
            {
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 天天"] == 0)//此段代码为天天所用
                {
                    ChangeMessageBox(pc);
                    Say(pc, 0, "我这把剑可是涂满了毒药的毒剑（舔）", "天天");
                    Say(pc, 0, "骗你的", "天天");
                    Select(pc, " ", "", "你有收到奇怪的卡片吗？");
                    Say(pc, 0, "有的", "天天");
                    Say(pc, 0, "我是好人", "天天");
                    Say(pc, 0, "（感觉很耿直的样子……）", pc.Name);
                    Say(pc, 0, "（……去认识认识其他参赛者吧）", pc.Name);
                    pc.CInt["CC新春活动 天天"] = 1;//该part的最终标记
                    if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 兔麻麻"] == 2 && pc.CInt["CC新春活动 番茄茄"] == 3 && pc.CInt["CC新春活动 天宫希"] == 3 && pc.CInt["CC新春活动 沙月"] == 1 && pc.CInt["CC新春活动 夏影"] == 1 && pc.CInt["CC新春活动 天天"] == 1 && pc.CInt["CC新春活动 暗鸣"] == 6)
                    {
                        ChangeMessageBox(pc);
                        Say(pc, 0, "似乎把参赛者都认识完了呢……", pc.Name);
                        Say(pc, 0, "去找那个奇怪的新年c看看好了", pc.Name);
                        pc.CInt["CCHelloComplete"] = 1;
                        return;
                    }
                    return;
                }
            }



            if (pc.CInt["天天1任务标记"] == 3)//检查任务标记
            {
                Say(pc, 111, "要把这个胡萝卜给我？为什么？$R受到一个小女孩的托付？$R$R你说她叫雪风？$R……没有印象的名字呢。", "传说中的肝神·天天");
                Wait(pc, 1000);
                Say(pc, 111, "不过，既然是给我的，那我就收下吧。$R$R嗬……胡萝卜啊。", "传说中的肝神·天天");
                Wait(pc, 200);
                Say(pc, 111, "正好口有点渴了，那我就把它吃掉啰。", "传说中的肝神·天天");
                Wait(pc, 200);
                Select(pc, " ", "", "啊…那个是");
                Say(pc, 111, "（咔嚓——）", " ");
                PlaySound(pc, 3275, false, 100, 50);//播放音效，ID3275
                Wait(pc, 500);
                Say(pc, 111, "（传来什么东西碎掉的声音）", " ");
                Say(pc, 0, "！？！？", "传说中的肝神·天天");
                if (pc.CInt["天天1任务技能点获得"] != 1)//检查记录
                {
                    pc.SkillPoint3 += 1;//得到一个技能点
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();//发送玩家信息封包
                    ShowEffect(pc, 4131);//显示特效，ID4131
                    pc.CInt["天天1任务技能点获得"] = 1;//技能点获取标记
                    Say(pc, 131, "获得了1点技能点", " ");
                }
                pc.CInt["天天1任务标记"] = 4;//任务标记
                return;//结束脚本
            }
            else if (pc.CInt["天天1任务标记"] == 4)//检查任务标记
            {
                //Say(pc, 0, "持之以恒的努力才是最重要的，但是……$R$R（不知道为什么$R　总觉得天天看我的眼神发生了变化）", "天天");
                if (pc.Level >= 45 && pc.Job == PC_JOB.GLADIATOR && pc.CInt["雲切任务"] < 1)
                {
                    Say(pc, 0, "嗯？$R你想更进一步强化$R你的【雲切】技能？$R$R我是可以教你的啦，但是$R那本【进阶雲切秘籍】在某次$R我与【正体不明】决斗的时候弄丢了。", "传说中的肝神·天天");
                    Say(pc, 0, "如果你能帮我找回来的话，$R我兴许可以教教你。", "传说中的肝神·天天");
                    pc.CInt["雲切任务"] = 1;
                    return;
                }
                else if (CountItem(pc, 910000098) >= 1 && pc.CInt["雲切任务"] <= 2)
                {
                    string s = "";
                    if (pc.Gender == PC_GENDER.FEMALE) s = "$R不过还好你是女孩子，哈哈。";
                    Say(pc, 0, "不错，你找来了【进阶雲切秘籍】，$R$R那么我就兑现承诺，$R教你更强大的【雲切】技巧吧。", "传说中的肝神·天天");
                    Say(pc, 0, "￥#@！￥！@#*（）& $R$R（在天天教了你许多之后……）$R$R", "传说中的肝神·天天");
                    Say(pc, 0, "其实你并不需要自宫..= =" + s, "传说中的肝神·天天");
                    Say(pc, 0, "雲切是一种可以暴露敌人弱点的技能，$R$R如果你看得不够透彻，$R只能说明你修行得还不够。", "传说中的肝神·天天");
                    Say(pc, 0, "这本【进阶雲切秘籍】你留着吧，$R上面记录了如何让你$R详细修炼【雲切】的技巧。$R$R继续照着上面刻苦修炼，$R能不能领悟，就看你的悟性了。", "传说中的肝神·天天");
                    pc.CInt["雲切任务"] = 3;
                    return;
                }
                else
                    Say(pc, 0, "持之以恒的努力才是最重要的，一起加油吧2", "天天");
                return;
            }
            else
                Say(pc, 0, "持之以恒的努力才是最重要的，一起加油吧", "天天");
        }
    }
}