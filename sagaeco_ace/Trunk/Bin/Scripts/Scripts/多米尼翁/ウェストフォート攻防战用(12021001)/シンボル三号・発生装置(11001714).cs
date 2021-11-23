using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021001
{
    public class S11001714 : Event
    {
        public S11001714()
        {
            this.EventID = 11001714;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (ODWarReviveSymbol(12021001, 3))
            {
                case SymbolReviveResult.Success:
                    PlaySound(pc, 2559, false, 100, 50);

                    Say(pc, 0, 131, "（……ピッ）$R;" +
                    "$R西部要塞シンボル$R;" +
                    "……展開準備中。$R;", "システム");          
                    break;
                case SymbolReviveResult.StillTrash:
                    PlaySound(pc, 2559, false, 100, 50);

                    Say(pc, 0, 131, "（……哔）$R;" +
                    "$R西部要塞象征$R;" +
                    "……无法展开，有障碍物干扰。$R;", "系统");
                    break;
                case SymbolReviveResult.NotDown :
                    PlaySound(pc, 2559, false, 100, 50);

                    Say(pc, 0, 131, "（……ピッ）$R;" +
                    "$R西部要塞象征$R;" +
                    "已展开。$R;", "システム");
                    break;
            }
        }

    }

}
            
            
        
     
    