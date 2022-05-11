using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DualConnector;

namespace Tinkoff_POS_S920.Command
{
    public class Request
    {
        public ISAPacket requestPOS { get; set; }

        public Int64 nCom { get; set; }
        public Int64 BaudRate { get; set; }
        public Int64 ByteSize { get; set; }
        public Int64 Parity { get; set; }
        public Int64 StopBits { get; set; }
        public Int64 FlowCtrl { get; set; }

        public Request()
        {
            requestPOS= (ISAPacket) new SAPacket();
            nCom = 6;
            BaudRate = 115200;
            ByteSize = 8L;
            Parity = 0L;
            StopBits = 0L;
            FlowCtrl = 2L;
        }
    }
}
