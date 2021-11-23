using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Linq;
using System.Text;
using SagaDB.Item;
using Microsoft.CSharp;
using System.IO;

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
        public void OnSkillCastComplete(Actor sActor, Actor dActor, SkillArg arg)
        {
            if (sActor.MapID == 10054000) return;
            //鬼门关：使用黄泉之门2000次
            if (arg.skill.ID == 13105)
                s.TitleProccess(sActor, 107, 1);

            //扎心：使用3级刺心
            if (arg.skill.ID == 12109 && arg.skill.Level == 3)
                s.TitleProccess(sActor, 33, 1);

            //暴风雪：使用3级暴风雪
            if (arg.skill.ID == 14020 && arg.skill.Level == 3)
                s.TitleProccess(sActor, 31, 1);
        }
    }
}

