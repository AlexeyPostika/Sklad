using DualConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTinkoff.Tinkoff.Pax
{
    public class Request
    {
        CmdArgs src;
        ISAPacket request;
        ISAPacket response;
        DCLink dcLink;
        public SAPacket sAPacket { get; set; }

        public Request(SAPacket _sAPacket )
        {
            src = new CmdArgs();
            request = (ISAPacket)new DualConnector.SAPacket();
            response = (ISAPacket)new DualConnector.SAPacket();
            dcLink = new DCLink();

            this.sAPacket = _sAPacket;

            //Инициализация параметров
            sAPacket.errorStatus.ErrorCode = dcLink.InitResources();
            if (sAPacket.errorStatus.ErrorCode == 0)
            {
                string s1 = src.Get(CmdArgs.CMD_ARGUMENTS.C_PORT);
                string s2 = src.Get(CmdArgs.CMD_ARGUMENTS.C_BAUDERATE);

                if (!string.IsNullOrEmpty(s1) && !string.IsNullOrEmpty(s2))
                {
                    long result1 = 1;
                    long result2 = 115200;
                    long.TryParse(s1, out result1);
                    long.TryParse(s2, out result2);
                    if (dcLink.SetChannelTerminalParam(result1, result2, 8L, 0L, 0L, 2L) != 0)
                    {
                        sAPacket.errorStatus.ErrorCode = 102;
                        goto label_return;
                    }

                    sAPacket.errorStatus.ErrorCode = dcLink.Exchange(ref request, ref response, 180000);
                }
            }
            label_return:
            Environment.Exit(sAPacket.errorStatus.ErrorCode);
        }
    }
}
