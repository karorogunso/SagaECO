
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
    public class S60000020 : Event
    {
        public S60000020()
        {
            this.EventID = 60000020;
        }
        void 猜拳(ActorPC pc)
        {
            if (CountItem(pc, 952000002) > 10)
            {
                int set = Select(pc, "对方准备出拳了！！你出？？", "", "石头", "剪刀", "布");
                int random = Global.Random.Next(1, 3);
                switch (set)
                {
                    case 1:
                        switch (random)
                        {
                            case 1://石头
                                ShowEffect(pc, 4512);
                                ShowEffect(pc, 147, 138, 4512);
                                Wait(pc, 2000);
                                Say(pc, 2110, "对方出了『$CW石头$CD』，平局。");
                                Say(pc, 2110, "啊啊，平局呢", "鱼缸男");
                                if (Select(pc, "要继续玩猜拳游戏吗？", "", "继续玩", "不玩了") == 1)
                                    猜拳(pc);
                                break;
                            case 2://剪刀
                                ShowEffect(pc, 4512);
                                ShowEffect(pc, 147, 138, 4513);
                                Wait(pc, 2000);
                                Say(pc, 2110, "对方出了『$CW剪刀$CD』，你赢了！");
                                Say(pc, 2110, "纳尼！！！！", "鱼缸男");
                                SInt["元旦活动NPC获得"] -= 10;
                                if (CountItem(pc, 952000002) > 10)
                                    GiveItem(pc, 952000002, 10);
                                if (Select(pc, "要继续玩猜拳游戏吗？", "", "继续玩", "不玩了") == 1)
                                    猜拳(pc);
                                break;
                            case 3://布
                                ShowEffect(pc, 4512);
                                ShowEffect(pc, 147, 138, 4514);
                                Wait(pc, 2000);
                                Say(pc, 2110, "对方出了『$CW布$CD』，你输了！");
                                Say(pc, 2110, "哈哈，我赢了！", "鱼缸男");
                                SInt["元旦活动NPC获得"] += 10;
                                if (CountItem(pc, 952000002) > 10)
                                    TakeItem(pc, 952000002, 10);
                                if (Select(pc, "要继续玩猜拳游戏吗？", "", "继续玩", "不玩了") == 1)
                                    猜拳(pc);
                                break;
                        }
                        break;
                    case 2:
                        switch (random)
                        {
                            case 1://石头
                                ShowEffect(pc, 4513);
                                ShowEffect(pc, 147, 138, 4512);
                                Wait(pc, 2000);
                                Say(pc, 2110, "对方出了『$CW石头$CD』，你输了！！。");
                                Say(pc, 2110, "哈哈，我赢了！", "鱼缸男");
                                SInt["元旦活动NPC获得"] += 10;
                                if (CountItem(pc, 952000002) > 10)
                                    TakeItem(pc, 952000002, 10);
                                if (Select(pc, "要继续玩猜拳游戏吗？", "", "继续玩", "不玩了") == 1)
                                    猜拳(pc);
                                break;
                            case 2://剪刀
                                ShowEffect(pc, 4513);
                                ShowEffect(pc, 147, 138, 4513);
                                Wait(pc, 2000);
                                Say(pc, 2110, "对方出了『$CW剪刀$CD』，平局。");
                                Say(pc, 2110, "纳尼，居然是平局", "鱼缸男");
                                if (Select(pc, "要继续玩猜拳游戏吗？", "", "继续玩", "不玩了") == 1)
                                    猜拳(pc);
                                break;
                            case 3://布
                                ShowEffect(pc, 4513);
                                ShowEffect(pc, 147, 138, 4514);
                                Wait(pc, 2000);
                                Say(pc, 2110, "对方出了『$CW布$CD』，你赢了！");
                                Say(pc, 2110, "呃——啊！！！", "鱼缸男");
                                SInt["元旦活动NPC获得"] -= 10;
                                if (CountItem(pc, 952000002) > 10)
                                    GiveItem(pc, 952000002, 10);
                                if (Select(pc, "要继续玩猜拳游戏吗？", "", "继续玩", "不玩了") == 1)
                                    猜拳(pc);
                                break;
                        }
                        break;
                    case 3:
                        switch (random)
                        {
                            case 1://石头
                                ShowEffect(pc, 4514);
                                ShowEffect(pc, 147, 138, 4512);
                                Wait(pc, 2000);
                                Say(pc, 2110, "对方出了『$CW石头$CD』，你赢了！");
                                Say(pc, 2110, "呜哦哦哦哦哦——————！！", "鱼缸男");
                                SInt["元旦活动NPC获得"] -= 10;
                                if (CountItem(pc, 952000002) > 10)
                                    GiveItem(pc, 952000002, 10);
                                if (Select(pc, "要继续玩猜拳游戏吗？", "", "继续玩", "不玩了") == 1)
                                    猜拳(pc);
                                break;
                            case 2://剪刀
                                ShowEffect(pc, 4514);
                                ShowEffect(pc, 147, 138, 4513);
                                Say(pc, 2110, "对方出了『$CW剪刀$CD』，你输了！！");
                                Say(pc, 2110, "呜嗷嗷嗷嗷嗷！", "鱼缸男");
                                SInt["元旦活动NPC获得"] += 10;
                                if (CountItem(pc, 952000002) > 10)
                                    TakeItem(pc, 952000002, 10);
                                if (Select(pc, "要继续玩猜拳游戏吗？", "", "继续玩", "不玩了") == 1)
                                    猜拳(pc);
                                break;
                            case 3://布
                                ShowEffect(pc, 4514);
                                ShowEffect(pc, 147, 138, 4514);
                                Say(pc, 2110, "对方出了『$CW布$CD』，平局。");
                                Say(pc, 2110, "哼", "鱼缸男");
                                if (Select(pc, "要继续玩猜拳游戏吗？", "", "继续玩", "不玩了") == 1)
                                    猜拳(pc);
                                break;
                        }
                        break;
                }
            }
            else
            {
                Say(pc, 2110, "切！！$R你的『$CM熊熊糖罐$CD』根本不够！！", "鱼缸男");
                return;
            }
        }
        void 猜点数(ActorPC pc)
        {
            if (CountItem(pc, 952000002) > 10)
            {
                int set = Select(pc, "对方准备投掷骰子了，你猜？", "", "1", "2", "3", "4", "5", "6");
                int random = Global.Random.Next(1, 6);
                switch (random)
                {
                    case 1:
                        ShowEffect(pc, 146, 139, 4523);
                        Wait(pc, 2000);
                        if (set == 1)
                        {
                            Say(pc, 2110, "哦哦！！$R你猜中了呢！！");
                            GiveItem(pc, 952000002, 60);
                            SInt["元旦活动NPC获得"] -= 60;
                        }
                        else
                        {
                            Say(pc, 2110, "你猜错啦！！！");
                            TakeItem(pc, 952000002, 10);
                            SInt["元旦活动NPC获得"] += 10;
                        }
                        if (Select(pc, "要继续玩猜点数游戏吗？", "", "继续玩", "不玩了") == 1)
                            猜点数(pc);
                        break;
                    case 2:
                        ShowEffect(pc, 146, 139, 4524);
                        Wait(pc, 2000);
                        if (set == 2)
                        {
                            Say(pc, 2110, "哦哦！！$R你猜中了呢！！");
                            GiveItem(pc, 952000002, 60);
                            SInt["元旦活动NPC获得"] -= 60;
                        }
                        else
                        {
                            Say(pc, 2110, "你猜错啦！！！");
                            TakeItem(pc, 952000002, 10);
                            SInt["元旦活动NPC获得"] += 10;
                        }
                        if (Select(pc, "要继续玩猜点数游戏吗？", "", "继续玩", "不玩了") == 1)
                            猜点数(pc);
                        break;
                    case 3:
                        ShowEffect(pc, 146, 139, 4525);
                        Wait(pc, 2000);
                        if (set == 3)
                        {
                            Say(pc, 2110, "哦哦！！$R你猜中了呢！！");
                            GiveItem(pc, 952000002, 60);
                            SInt["元旦活动NPC获得"] -= 60;
                        }
                        else
                        {
                            Say(pc, 2110, "你猜错啦！！！");
                            TakeItem(pc, 952000002, 10);
                            SInt["元旦活动NPC获得"] += 10;
                        }
                        if (Select(pc, "要继续玩猜点数游戏吗？", "", "继续玩", "不玩了") == 1)
                            猜点数(pc);
                        break;
                    case 4:
                        ShowEffect(pc, 146, 139, 4526);
                        Wait(pc, 2000);
                        if (set == 4)
                        {
                            Say(pc, 2110, "哦哦！！$R你猜中了呢！！");
                            GiveItem(pc, 952000002, 60);
                            SInt["元旦活动NPC获得"] -= 60;
                        }
                        else
                        {
                            Say(pc, 2110, "你猜错啦！！！");
                            TakeItem(pc, 952000002, 10);
                            SInt["元旦活动NPC获得"] += 10;
                        }
                        if (Select(pc, "要继续玩猜点数游戏吗？", "", "继续玩", "不玩了") == 1)
                            猜点数(pc);
                        break;
                    case 5:
                        ShowEffect(pc, 146, 139, 4527);
                        Wait(pc, 2000);
                        if (set == 5)
                        {
                            Say(pc, 2110, "哦哦！！$R你猜中了呢！！");
                            GiveItem(pc, 952000002, 60);
                            SInt["元旦活动NPC获得"] -= 60;
                        }
                        else
                        {
                            Say(pc, 2110, "你猜错啦！！！");
                            TakeItem(pc, 952000002, 10);
                            SInt["元旦活动NPC获得"] += 10;
                        }
                        if (Select(pc, "要继续玩猜点数游戏吗？", "", "继续玩", "不玩了") == 1)
                            猜点数(pc);
                        break;
                    case 6:
                        ShowEffect(pc, 146, 139, 4528);
                        Wait(pc, 2000);
                        if (set == 6)
                        {
                            Say(pc, 2110, "哦哦！！$R你猜中了呢！！");
                            GiveItem(pc, 952000002, 60);
                            SInt["元旦活动NPC获得"] -= 10;
                        }
                        else
                        {
                            Say(pc, 2110, "你猜错啦！！！");
                            TakeItem(pc, 952000002, 10);
                            SInt["元旦活动NPC获得"] += 10;
                        }
                        if (Select(pc, "要继续玩猜点数游戏吗？", "", "继续玩", "不玩了") == 1)
                            猜点数(pc);
                        break;
                }
            }
        }
        public override void OnEvent(ActorPC pc)
        {
            string g = "小姑娘";
            if (pc.Gender == PC_GENDER.MALE)
                g = "小伙子";
            Say(pc, 2110, "嘿！那边的" + g + "$R$R你身上有『$CM熊熊糖罐$CD』吗！！", "鱼缸男");
            if (Select(pc, "你的回答是！！？？", "", "有的有的！", "没有，快滚") == 1)
            {
                Say(pc, 2110, "那么！$R你要不要赌上10个『$CM熊熊糖罐$CD』，$R和我玩『$CM猜拳$CD』或『$CM猜点数$CD』的游戏呢？？", "鱼缸男");
                switch (Select(pc, "那么，你的回答是！！？？", "", "来！猜拳！！", "来玩赌大小！！！", "不玩，快滚"))
                {
                    case 1:
                        猜拳(pc);
                        break;
                    case 2:
                        if (CountItem(pc, 952000002) > 10)
                        {
                            Say(pc, 2110, "那么，规则就是！$R$R我掷骰子，$R你猜对了就算你赢，$R你猜错了就算我赢。$R$R你赢了我给你60个『$CM熊熊糖罐$CD』,$R你输了就给我10个！！", "鱼缸男");
                            if (Select(pc, "玩猜大小吗？", "", "来！玩！！！！", "不玩，快滚") == 1)
                            {
                                猜点数(pc);
                            }
                        }
                        else
                        {
                            Say(pc, 2110, "切！！$R你的『$CM熊熊糖罐$CD』根本不够！！", "鱼缸男");
                            return;
                        }
                        break;
                    case 3:
                        break;
                }
            }
            else
            {

            }
        }
    }
}