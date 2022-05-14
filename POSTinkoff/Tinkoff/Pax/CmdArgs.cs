using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTinkoff.Tinkoff.Pax
{
    internal class CmdArgs
    {
        private const string ccParamPort = "-p";
        private const string ccParamBRate = "-b";
        private const string ccParamAmount = "-a";
        private const string ccParamOperID = "-o";
        private const string ccParamCurrency = "-c";
        private const string ccParamTerminalID = "-z";
        private const string ccParamRefNum = "-r";
        private const string ccParamTransID = "-n";
        private const string ccParamTrack = "-t";
        private const string ccParamAuthCode = "-u";
        private const string ccParamMode1 = "-l";
        private const string ccParamMode = "-m";
        private const string ccParamReceiptFlag = "-f";
        private const string ccParamSpan = "-s";

        public int Parse(string[] args)
        {
            try
            {
                foreach (string str1 in args)
                {
                    string str = str1;
                    string t = "";
                    CmdArgs.ARGS_TAB argsTab = CmdArgs.ARGS_TAB.tabArgum.Find((Predicate<CmdArgs.ARGS_TAB>)(x => x.txtName.Find((Predicate<string>)(y =>
                    {
                        t = y;
                        return str.StartsWith(y);
                    })) != null));
                    if (argsTab != null)
                        argsTab.value = str.Substring(t.Length);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Parse cmd line: {0}", (object)ex.Message);
                return 1;
            }
            return 0;
        }

        public string Get(CmdArgs.CMD_ARGUMENTS arg) => CmdArgs.ARGS_TAB.GetArg(arg);

        public enum CMD_ARGUMENTS
        {
            NO_ARG = -1, // 0xFFFFFFFF
            C_PORT = 0,
            C_BAUDERATE = 1,
            C_AMOUNT = 2,
            C_OPERID = 3,
            C_CURRENCY = 4,
            C_TERMINALID = 5,
            C_REFNUM = 6,
            C_TRANSID = 7,
            C_TRACK = 8,
            C_AUTHCODE = 9,
            C_MODE1 = 10, // 0x0000000A
            C_MODE = 11, // 0x0000000B
            C_RECEIPTFLAG = 12, // 0x0000000C
            C_SPAN = 13, // 0x0000000D
            C_ADDAMOUNT = 14, // 0x0000000E
            C_CREDITAMOUNT = 15, // 0x0000000F
            C_FILEDATA = 16, // 0x00000010
            C_FIELD06 = 17, // 0x00000011
            C_FIELD08 = 18, // 0x00000012
            C_FIELD09 = 19, // 0x00000013
            C_FIELD10 = 20, // 0x00000014
            C_FIELD11 = 21, // 0x00000015
            C_FIELD15 = 22, // 0x00000016
            C_FIELD16 = 23, // 0x00000017
            C_FIELD17 = 24, // 0x00000018
            C_FIELD18 = 25, // 0x00000019
            C_FIELD19 = 26, // 0x0000001A
            C_FIELD21 = 27, // 0x0000001B
            C_FIELD23 = 28, // 0x0000001C
            C_FIELD28 = 29, // 0x0000001D
            C_FIELD29 = 30, // 0x0000001E
            C_FIELD30 = 31, // 0x0000001F
            C_FIELD32 = 32, // 0x00000020
            C_FIELD34 = 33, // 0x00000021
            C_FIELD36 = 34, // 0x00000022
            C_FIELD39 = 35, // 0x00000023
            C_FIELD40 = 36, // 0x00000024
            C_FIELD41 = 37, // 0x00000025
            C_FIELD42 = 38, // 0x00000026
            C_FIELD43 = 39, // 0x00000027
            C_FIELD46 = 40, // 0x00000028
            C_FIELD49 = 41, // 0x00000029
            C_FIELD50 = 42, // 0x0000002A
            C_FIELD51 = 43, // 0x0000002B
            C_FIELD52 = 44, // 0x0000002C
            C_FIELD53 = 45, // 0x0000002D
            C_FIELD54 = 46, // 0x0000002E
            C_FIELD56 = 47, // 0x0000002F
            C_FIELD57 = 48, // 0x00000030
            C_FIELD67 = 49, // 0x00000031
            C_FIELD63 = 50, // 0x00000032
            C_FIELD76 = 51, // 0x00000033
            C_FIELD77 = 52, // 0x00000034
            C_FIELD79 = 53, // 0x00000035
            C_FIELD80 = 54, // 0x00000036
            C_FIELD81 = 55, // 0x00000037
            C_FIELD82 = 56, // 0x00000038
            C_FIELD83 = 57, // 0x00000039
            C_FIELD86 = 58, // 0x0000003A
            C_FIELD90 = 59, // 0x0000003B
            C_FIELD71 = 60, // 0x0000003C
            C_FIELD89 = 61, // 0x0000003D
            C_COUNT = 62, // 0x0000003E
        }

        private class ARGS_TAB
        {
            internal static List<CmdArgs.ARGS_TAB> tabArgum = new List<CmdArgs.ARGS_TAB>()
      {
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_PORT, new List<string>()
        {
          "-p"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_BAUDERATE, new List<string>()
        {
          "-b"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_SPAN, new List<string>()
        {
          "-s"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_RECEIPTFLAG, new List<string>()
        {
          "-f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_AMOUNT, new List<string>()
        {
          "-a",
          "-00f",
          "-0f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_ADDAMOUNT, new List<string>()
        {
          "-01f",
          "-1f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_CURRENCY, new List<string>()
        {
          "-c",
          "-04f",
          "-4f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_TRACK, new List<string>()
        {
          "-t",
          "-12f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_AUTHCODE, new List<string>()
        {
          "-u",
          "-13f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_REFNUM, new List<string>()
        {
          "-r",
          "-14f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_OPERID, new List<string>()
        {
          "-o",
          "-25f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_TRANSID, new List<string>()
        {
          "-n",
          "-26f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_TERMINALID, new List<string>()
        {
          "-z",
          "-27f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_CREDITAMOUNT, new List<string>()
        {
          "-31f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_MODE1, new List<string>()
        {
          "-l",
          "-64f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_MODE, new List<string>()
        {
          "-m",
          "-65f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FILEDATA, new List<string>()
        {
          "-70f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD06, new List<string>()
        {
          "-6f",
          "-06f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD08, new List<string>()
        {
          "-8f",
          "-08f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD09, new List<string>()
        {
          "-9f",
          "-09f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD10, new List<string>()
        {
          "-10f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD11, new List<string>()
        {
          "-11f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD15, new List<string>()
        {
          "-15f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD16, new List<string>()
        {
          "-16f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD17, new List<string>()
        {
          "-17f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD18, new List<string>()
        {
          "-18f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD19, new List<string>()
        {
          "-19f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD21, new List<string>()
        {
          "-21f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD23, new List<string>()
        {
          "-23f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD28, new List<string>()
        {
          "-28f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD29, new List<string>()
        {
          "-29f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD30, new List<string>()
        {
          "-30f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD32, new List<string>()
        {
          "-32f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD34, new List<string>()
        {
          "-34f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD36, new List<string>()
        {
          "-36f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD39, new List<string>()
        {
          "-39f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD40, new List<string>()
        {
          "-40f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD41, new List<string>()
        {
          "-41f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD42, new List<string>()
        {
          "-42f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD43, new List<string>()
        {
          "-43f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD46, new List<string>()
        {
          "-46f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD49, new List<string>()
        {
          "-49f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD50, new List<string>()
        {
          "-50f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD51, new List<string>()
        {
          "-51f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD52, new List<string>()
        {
          "-52f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD53, new List<string>()
        {
          "-53f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD54, new List<string>()
        {
          "-54f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD56, new List<string>()
        {
          "-56f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD57, new List<string>()
        {
          "-57f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD63, new List<string>()
        {
          "-63f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD67, new List<string>()
        {
          "-67f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD71, new List<string>()
        {
          "-71f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD76, new List<string>()
        {
          "-76f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD77, new List<string>()
        {
          "-77f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD79, new List<string>()
        {
          "-79f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD80, new List<string>()
        {
          "-80f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD81, new List<string>()
        {
          "-81f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD82, new List<string>()
        {
          "-82f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD83, new List<string>()
        {
          "-83f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD86, new List<string>()
        {
          "-86f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD89, new List<string>()
        {
          "-89f"
        }),
        new CmdArgs.ARGS_TAB(CmdArgs.CMD_ARGUMENTS.C_FIELD90, new List<string>()
        {
          "-90f"
        })
      };
            public readonly CmdArgs.CMD_ARGUMENTS arg;
            public readonly List<string> txtName;
            public string value;

            private ARGS_TAB()
            {
                this.arg = CmdArgs.CMD_ARGUMENTS.NO_ARG;
                this.txtName = (List<string>)null;
                this.value = (string)null;
            }

            private ARGS_TAB(CmdArgs.CMD_ARGUMENTS _arg, List<string> _txt)
              : this()
            {
                this.arg = _arg;
                this.txtName = _txt;
            }

            public static void UpdateArg(CmdArgs.CMD_ARGUMENTS arg, string val)
            {
                CmdArgs.ARGS_TAB argsTab = CmdArgs.ARGS_TAB.tabArgum.Find((Predicate<CmdArgs.ARGS_TAB>)(x => x.arg == arg));
                if (argsTab == null)
                    return;
                argsTab.value = val;
            }

            public static string GetArg(CmdArgs.CMD_ARGUMENTS arg) => CmdArgs.ARGS_TAB.tabArgum.Find((Predicate<CmdArgs.ARGS_TAB>)(x => x.arg == arg))?.value;
        }
    }
}
