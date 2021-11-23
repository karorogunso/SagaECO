using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum NDFlags
    {
        諾頓的妙藥 = 0x1,
        巴列麗拿的哥哥第一次对话 = 0x2,
        中央的巴列麗拿 = 0x4,
        //龙蛋相关
        窮途末路的商人对话一 = 0x100,
        窮途末路的商人对话二 = 0x200,
        窮途末路的商人结束 = 0x400,
        从巴列麗拿的哥哥获得物品 = 0x1000,
        //遗迹门口
        遗迹 = 0x2000, //0c62
        协助 = 0x4000, //0c63
        //大导师第一次对话的技能点
        大导师第一次对话 = 0x8000,
        //65职业装任务
        貝爾德咖魯德第一次对话 = 0x10000,
        
        职业装任务 = 0x20000,
        第一次职业装 = 0x40000,
        和维尔迪加尔德真身第一次对话 = 0x80000,
    }
}