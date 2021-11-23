
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace 羽川柠剧情
{
    public partial class 羽川柠剧情 : Event
    {
        public 羽川柠剧情()
        {
            this.EventID = 60000026;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.CInt["羽川柠技能点任务标记"] == 1 && pc.CInt["黑暗料理1任务技能点获得"] == 1)
                TitleProccess(pc, 7, 1);//称号补领
            ChangeMessageBox(pc);
               
            BitMask<羽川柠> mark = pc.CMask["羽川柠对话"];
            if (!mark.Test(羽川柠.初次对话) || !mark.Test(羽川柠.和兔纸对话))
                初次对话(pc, mark);
            else if (mark.Test(羽川柠.和兔纸对话) ||!mark.Test(羽川柠.给羽川柠樱桃汁) ||!mark.Test(羽川柠.拿了黑暗料理给羽川柠))
                拿了黑暗料理给羽川柠(pc, mark);
        }
        void 初次对话(ActorPC pc, BitMask<羽川柠> mark)
        {
            if (!mark.Test(羽川柠.初次对话))
            {
                mark.SetValue(羽川柠.初次对话, true);
                Say(pc, 158, "啊啊啊啊啊啊啊啊！", "羽川柠");
                Say(pc, 158, "啊啊啊啊啊啊啊啊！$R啊啊啊啊啊啊啊啊啊啊啊！", "羽川柠");
                Say(pc, 158, "啊啊啊啊啊啊啊啊！$R啊啊啊啊啊啊啊啊啊啊啊！$R啊啊啊啊啊啊啊啊啊啊啊啊啊啊！", "羽川柠");
                Say(pc, 158, "啊啊啊啊啊啊啊啊！$R啊啊啊啊啊啊啊啊啊啊啊！$R啊啊啊啊啊啊啊啊啊啊啊啊啊啊！$R啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊！", "羽川柠");
                switch (Select(pc, " ", "", "那个...你怎么了呢？", "鬼叫些什么啊", "你疯了？", "神经病啊你"))
                {
                    case 1:
                        SInt["羽川柠选1次数"]++;
                        break;
                    case 2:
                        SInt["羽川柠选2次数"]++;
                        break;
                    case 3:
                        SInt["羽川柠选3次数"]++;
                        break;
                    case 4:
                        SInt["羽川柠选4次数"]++;
                        break;
                }
                Say(pc, 111, "……", "羽川柠");
                Say(pc, 158, "啊啊啊啊啊啊啊啊！$R啊啊啊啊啊啊啊啊啊啊啊！$R啊啊啊啊啊啊啊啊啊啊啊啊啊啊！$R啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊！", "羽川柠");
                Select(pc, " ", "", "这人没救了...");
                Select(pc, " ", "", "去找那边做料理的祭司帮忙想想办法吧");
            }
            else if (mark.Test(羽川柠.初次对话) && !mark.Test(羽川柠.和兔纸对话))
            {
                Say(pc, 0, "(去找那边做料理的祭司帮忙想想办法吧)");
            }
        }
        void 拿了黑暗料理给羽川柠(ActorPC pc, BitMask<羽川柠> mark)
        {
            if (mark.Test(羽川柠.和兔纸对话) && !mark.Test(羽川柠.拿了黑暗料理给羽川柠) && CountItem(pc, 110123200) >= 1)
            {
                Say(pc, 158, "啊啊啊啊啊啊啊啊！$R啊啊啊啊啊啊啊啊啊啊啊！$R啊啊啊啊啊啊啊啊啊啊啊啊啊啊！$R啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊！", "羽川柠");
                switch (Select(pc, "拿她怎么办呢？", "", "喂她吃『土豆烧牛肉』", "把『土豆烧牛肉』塞进她的嘴里"))
                {
                    case 1:
                        Say(pc, 0, "你舀了一勺黑色的液体$r喂给了她吃...");
                        break;
                    case 2:
                        Say(pc, 0, "你直接从碗里拿起一块不明柱状物$R塞进了她的嘴里！！！");
                        break;
                }
                TakeItem(pc, 110123200, 1);
                mark.SetValue(羽川柠.拿了黑暗料理给羽川柠, true);
                Say(pc, 158, "呜呜呜……", "羽川柠");
                Say(pc, 158, "呜……", "羽川柠");
                Say(pc, 158, "……", "羽川柠");
                Say(pc, 158, "感觉滑滑的...硬硬的....$R味道甜甜的...辣辣的...还带有点酸味......", "羽川柠");
                Say(pc, 158, "（咕噜..）", "羽川柠");
                Say(pc, 158, "！！！？？？？？？？", "羽川柠");
                Say(pc, 158, "呜呕——————————！！！！！", "羽川柠");
                Say(pc, 158, "呜呜呜呜呜呜呜！！！！！$R$R(她已经说不出话来了）", "羽川柠");
                Say(pc, 158, "(去帮她买一杯『100％天然樱桃汁』吧）", "羽川柠");
                return;
            }
            if(mark.Test(羽川柠.拿了黑暗料理给羽川柠) && !mark.Test(羽川柠.给羽川柠樱桃汁) && CountItem(pc, 10001911) >= 1)
            {
                mark.SetValue(羽川柠.给羽川柠樱桃汁, true);
                TakeItem(pc, 10001911, 1);
                Say(pc, 2003, "（她抢过你手上的樱桃汁，并猛灌了下去）");
                Say(pc, 2003, "咕噜——！咕噜——！咕噜——！$R大口大口的喝——！", "羽川柠");
                Say(pc, 2003, "卧槽！！！$R你干什么！！！$R你要杀了我吗！！！！！！", "羽川柠");
                Select(pc, " ", "", "你似乎恢复了？");
                Say(pc, 2003, "什...什么！$R我刚刚怎么了！", "羽川柠");
                Select(pc, " ", "", "你一直在发神经");
                Say(pc, 2003, "那是我在和万恶的『巴格』作斗争！$R它们几乎要把我弄疯了！！$R我只是在发泄情绪而已！！", "羽川柠");
                Say(pc, 2003, "话说回来，$R你为什么要把那种邪恶的东西$R塞进我嘴里！？$R你想杀了我吗！！！", "羽川柠");
                Select(pc, " ", "", "那边的元素使说能治疗你");
                Say(pc, 2003, "纳..纳尼！？$R我这病怎么可能光靠这种东西就治疗的好！", "羽川柠");
                Say(pc, 2003, "呜呜呜……", "羽川柠");
                if (pc.CInt["羽川柠技能点任务标记"] != 1)
{
                Say(pc, 2003, "你得到了一个技能点。");
                pc.SkillPoint3 += 1;//得到一个技能点
}
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();//发送玩家信息封包
                ShowEffect(pc, 4131);//显示特效，ID4131
                pc.CInt["羽川柠技能点任务标记"] = 1;//技能点获取标记
                if (pc.CInt["黑暗料理1任务技能点获得"] == 1)
                    TitleProccess(pc, 7, 1);
                Wait(pc, 3000);
                if (Select(pc, " ", "", "算了，还是不理她了吧...") == 2)
                {
                    mark.SetValue(羽川柠.提出了帮助羽川柠, true);
                    Say(pc, 2003, "真..真的吗！？$R冒险家，你真是太好了呜呜呜！", "羽川柠");
                    Say(pc, 2003, "『巴格』是一种非常！非常狡猾的怪物！$R我已经和它们斗争了无数年了！$R它们能帮人弄疯！！", "羽川柠");
                    Say(pc, 2003, "如..如果可以的话，$R能不能麻烦你帮我去消灭一部分『巴格』呢？$R我把它称为『迪巴格』！$R那样的话，我会非常地感谢你的！", "羽川柠");
                    Say(pc, 2003, "等你做好了准备，再和我对话吧！$R我会带你去『巴格』的地点。$R只能你一个人去哦！", "羽川柠");
                }
                return;
            }
            if (mark.Test(羽川柠.拿了黑暗料理给羽川柠) && !mark.Test(羽川柠.给羽川柠樱桃汁) && CountItem(pc, 10001911) < 1)
            {
                Say(pc, 158, "(去帮她买一杯『100％天然樱桃汁』吧）", "羽川柠");
                return;
            }
            if (mark.Test(羽川柠.拿了黑暗料理给羽川柠) && !mark.Test(羽川柠.提出了帮助羽川柠))
            {
                Say(pc, 2003, "呜呜呜……", "羽川柠");
                if (Select(pc, " ", "", "算了，还是不理她了吧...") == 2)
                {
                    mark.SetValue(羽川柠.提出了帮助羽川柠, true);
                    Say(pc, 2003, "真..真的吗！？$R冒险家，你真是太好了呜呜呜！", "羽川柠");
                    Say(pc, 2003, "『巴格』是一种非常！非常狡猾的怪物！$R我已经和它们斗争了无数年了！$R它们能帮人弄疯！！", "羽川柠");
                    Say(pc, 2003, "如..如果可以的话，$R能不能麻烦你帮我去消灭一部分『巴格』呢？$R那样的话，我会非常地感谢你的！", "羽川柠");
                    Say(pc, 2003, "等你做好了准备，再和我对话吧！$R我会带你去『巴格』的据点。$R只能你一个人去哦！", "羽川柠");
                }
                return;
            }
        }
    }
}

