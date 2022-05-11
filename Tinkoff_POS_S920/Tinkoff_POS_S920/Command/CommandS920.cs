using DualConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkoff_POS_S920.Command
{
    public enum OperationCode : Int32
    {
        Sale = 1,
        Return = 29,
        SendHost = 26,
        Cancel = 4,
        TotalCommand = 59,
        Report = 63
    }
    public class CommandS920
    {
        public Int32 com { get; set; }
        public String terminalID { get; set; }
        public Response response { get; set; }

        DCLink dCLink;

        public CommandS920()
        {
            response = new Response();
            dCLink = new DCLink();
        }

        public void CommandPOS(Request _request = null)
        {
            switch (com)
            {
                case 0:
                    SendTerminal(new Request());
                    break;
                case 1:
                    response = SaleTerminal(_request);
                    break;
                case 2:
                    response = ReturnTerminal(_request);
                    break;
                case 3:
                    Terminal(_request);
                    break;
                default:
                    break;
            }
        }

        public Response SendTerminal(Request _request)
        {
            dCLink = new DCLink();
            response = new Response();
            ISAPacket request = _request.requestPOS;
            ISAPacket responsePOS = (ISAPacket)new SAPacket();
            int res = dCLink.InitResources(); //dCLink.SetChannelTerminalParam(_request.nCom, _request.BaudRate, _request.ByteSize, _request.Parity, _request.StopBits, _request.FlowCtrl);
            if (res >= 0)
            {
                request.OperationCode = (Int32)OperationCode.SendHost;
                Int32 temp = dCLink.Exchange(ref request, ref responsePOS, 180000);
                if (temp != 0)
                {
                    response.ErrorCode = dCLink.ErrorCode;
                    response.MessageErrorCode = dCLink.ErrorDescription;
                }
                dCLink.Dispose();
                terminalID = responsePOS.TerminalID;
                return response;
            }
            else
            {
                dCLink.Dispose();
                return null;
            }        
        }

        public Response SaleTerminal(Request _request)
        {
            dCLink = new DCLink();
            ISAPacket request = _request.requestPOS;
            ISAPacket responsePOS = (ISAPacket)new SAPacket();

            int res = dCLink.InitResources(); //dCLink.SetChannelTerminalParam(_request.nCom, _request.BaudRate, _request.ByteSize, _request.Parity, _request.StopBits, _request.FlowCtrl);
            if (res == 0)
            {
                response.ErrorCode = dCLink.Exchange(ref request, ref responsePOS, 180000);
                if (response.ErrorCode != 0)
                {
                    response.ErrorCode = dCLink.ErrorCode;
                    response.MessageErrorCode = dCLink.ErrorDescription;
                }
            }
            else
            {
                response.ErrorCode = dCLink.ErrorCode;
                response.MessageErrorCode = dCLink.ErrorDescription;
            }
            response.responsePOS = responsePOS;
            dCLink.Dispose();
            return response;
        }

        public Response ReturnTerminal(Request _request)
        {
            dCLink = new DCLink();
            ISAPacket request = _request.requestPOS;
            ISAPacket responsePOS = (ISAPacket)new SAPacket();

            int res = dCLink.InitResources(); //dCLink.SetChannelTerminalParam(_request.nCom, _request.BaudRate, _request.ByteSize, _request.Parity, _request.StopBits, _request.FlowCtrl);
            if (res == 0)
            {
                response.ErrorCode = dCLink.Exchange(ref request, ref responsePOS, 180000);
                if (response.ErrorCode != 0)
                {
                    response.ErrorCode = dCLink.ErrorCode;
                    response.MessageErrorCode = dCLink.ErrorDescription;
                }
            }
            else
            {
                response.ErrorCode = dCLink.ErrorCode;
                response.MessageErrorCode = dCLink.ErrorDescription;
            }
            response.responsePOS = responsePOS;
            dCLink.Dispose();
            return response;
        }

        public Response Terminal(Request _request)
        {
            dCLink = new DCLink();
            ISAPacket request = _request.requestPOS;
            ISAPacket responsePOS = (ISAPacket)new SAPacket();

            int res = dCLink.InitResources(); //dCLink.SetChannelTerminalParam(_request.nCom, _request.BaudRate, _request.ByteSize, _request.Parity, _request.StopBits, _request.FlowCtrl);
            if (res == 0)
            {
                response.ErrorCode = dCLink.Exchange(ref request, ref responsePOS, 180000);
                if (response.ErrorCode != 0)
                {
                    response.ErrorCode = dCLink.ErrorCode;
                    response.MessageErrorCode = dCLink.ErrorDescription;
                }
            }
            else
            {
                response.ErrorCode = dCLink.ErrorCode;
                response.MessageErrorCode = dCLink.ErrorDescription;
            }
            response.responsePOS = responsePOS;
            dCLink.Dispose();
            return response;
        }

    }
}
