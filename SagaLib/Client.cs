using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;


namespace SagaLib
{
    public class Client
    {
        public NetIO netIO;
        public uint SessionID;

        public Client()
        {

        }

        public Client(Socket mSock, Dictionary<ushort, Packet> mCommandTable)
        {
            this.netIO = new NetIO(mSock, mCommandTable, this, null);
        }

        virtual public void OnConnect()
        {
            
        }

        public virtual void OnDisconnect() { }

    }
}