using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaLib;

namespace SagaProxy
{
    public class ValidationClientPacketFactory : Factory<ValidationClientPacketFactory, PacketInterpretation>
        {
        public ValidationClientPacketFactory()
            {
                this.FactoryType = FactoryType.CSV;
            }

            protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, PacketInterpretation item)
            {
                throw new NotImplementedException();
            }

            protected override uint GetKey(PacketInterpretation item)
            {
                return (uint)item.OpCode;
            }

            protected override void ParseCSV(PacketInterpretation item, string[] paras)
            {
                item.OpCode = (int)new System.ComponentModel.Int32Converter().ConvertFromString(paras[0]);
                item.Name = paras[1];
            }
        }
}
