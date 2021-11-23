using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Linq;
using System.Text;
using SagaDB.Item;
using Microsoft.CSharp;
using System.IO;

using SagaMap.Network.Client;
using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.SkillDefinations;
using System.Reflection;
using SagaMap.Skill;

namespace SagaMap.Titles
{
    public partial class TitleEventManager : Singleton<TitleEventManager>
    {
        public void OnAddItem(MapClient client, Item item)
        {
            ActorPC pc = client.Character;
            if (item.Name.Length > 0)
            {
                //优雅：开箱获得10件优雅前缀的装备
                if (item.Name.Substring(0, 3) == "优雅的")
                    s.TitleProccess(pc, 99, 1);

                //深渊：开箱获得10件深渊前缀的装备
                if (item.Name.Substring(0, 3) == "深渊的")
                    s.TitleProccess(pc, 100, 1);

                //迅捷：开箱获得20件迅捷前缀的装备
                if (item.Name.Substring(0, 3) == "迅捷的")
                    s.TitleProccess(pc, 101, 1);

                //丰饶：开箱获得20件丰饶前缀的装备
                if (item.Name.Substring(0, 3) == "丰饶的")
                    s.TitleProccess(pc, 102, 1);

                //海贼王：获得10把伟大海贼的剑
                if (item.Name == "伟大海贼的剑")
                    s.TitleProccess(pc, 103, 1);

                //海洋之怒：获得10个怒卷的海洋之心
                if (item.Name == "怒卷的海洋之心")
                    s.TitleProccess(pc, 104, 1);

                //冷冽寒流：获得10个冷冽的寒流月光
                if (item.Name == "冷冽的寒流月光")
                    s.TitleProccess(pc, 105, 1);

                //坚不可摧：获得10个坚固的海洋结晶
                if (item.Name == "坚固的海洋结晶")
                    s.TitleProccess(pc, 106, 1);
            }
        }
    }
}

