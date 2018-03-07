using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
   public class HexUtil
    {
        public const string START_DIRECTIVE = "E1400E010344D101405504";
        public const string END_DIRECTIVE = "0000FE000000390000FE0000003739000000";
        /// <summary>
        /// <函数：Encode>
        /// 作用：将字符串内容转化为16进制数据编码，其逆过程是Decode
        /// 参数说明：
        /// strEncode 需要转化的原始字符串
        /// 转换的过程是直接把字符转换成Unicode字符,比如数字"3"-->0033,汉字"我"-->U+6211
        /// 函数decode的过程是encode的逆过程.
        /// </summary>
        /// <param name="strEncode"></param>
        /// <returns></returns>
        public static string Encode(string strEncode)
        {
            string strReturn = "";//  存储转换后的编码
            //foreach (short shortx in strEncode.ToCharArray())
            foreach (char latter in strEncode)
            {
                int value = Convert.ToInt32(latter);
                strReturn += string.Format("{0:X}",value);
            }
            return strReturn;
        }
        public static List<string> ToStringArray(string hexString) {

            var loop =(int)Math.Ceiling((double)hexString.Length/ 8);
            List<string> writeDataList = new List<string>();
            int startCharIndex = 0;
            int endCharIndex = 8;
            for (int i = 1; i <= loop; i++)
            {
                string data = string.Empty;
                if (i<loop)
                {
                    endCharIndex = i == loop ? hexString.Length : i * 8-1;
                    data = hexString.Substring(startCharIndex, 8);
                    startCharIndex = endCharIndex + 1;
                }
                if (i == loop)
                {
                    int padLen = 8 - (hexString.Length - startCharIndex);
                    data = hexString.Substring(startCharIndex, (hexString.Length - startCharIndex));
                    data = data.PadRight(8, '0');
                }
                writeDataList.Add(data);
            }
            return writeDataList;
        }

        /// <summary>
        /// HexString to String
        /// </summary>
        /// <param name="hexString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HexStringToString(string hexString, Encoding encoding = null)
        {
            try
            {
                List<string> listBlock = new List<string>();
                GenarateHexList(listBlock, hexString);
                if (string.IsNullOrEmpty(hexString))
                    return string.Empty;

                if (encoding == null)
                    encoding = Encoding.ASCII;

                string[] byteitem = listBlock.ToArray();
                List<byte> lstByte = new List<byte>();
                foreach (string item in byteitem)
                    lstByte.Add(Convert.ToByte(item, 16));

                return encoding.GetString(lstByte.ToArray());
            }
            catch { }
            return null;
        }

        public static void GenarateHexList(List<string> listBlock, string hexString, int startIndex = 0)
        {
            try
            {
                if (startIndex > hexString.Length) return;
                string subTex = hexString.Substring(startIndex, 2);
                listBlock.Add(subTex);
                startIndex += 2;
                GenarateHexList(listBlock, hexString, startIndex);
            }
            catch { }
        }

        /// <summary>
        /// <函数：Decode>
        /// 作用：将16进制数据编码转化为字符串，是Encode的逆过程
        /// </summary>
        /// <param name="strDecode"></param>
        /// <returns></returns>
        public static string Decode(string strDecode)
        {
            string sResult = "";
            for (int i = 0; i < strDecode.Length / 4; i++)
            {
                sResult += (char)short.Parse(strDecode.Substring(i * 4, 4), global::System.Globalization.NumberStyles.HexNumber);
            }
            return sResult;
        }
    }
}
