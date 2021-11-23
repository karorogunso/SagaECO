using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    /// <summary>
    /// 家具UNITActor
    /// </summary>
    public class ActorFurnitureUnit : ActorFurniture
    {
        public ActorFurnitureUnit()
        {
            this.type = ActorType.FURNITUREUNIT;
        }
    }
}
