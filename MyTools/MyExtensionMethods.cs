using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyTools
{
    public static class MyExtensionMethods
    {
        /// <summary>
        /// Hàm chuyển chuối string sang mã MD5
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encrypt2MD5(this string data)
        {
            MD5CryptoServiceProvider myMD5 = new MD5CryptoServiceProvider();
            byte[] b = System.Text.Encoding.UTF8.GetBytes(data);
            b = myMD5.ComputeHash(b);
            StringBuilder s = new StringBuilder();
            foreach (byte p in b)
            {
                s.Append(p.ToString("x").ToLower());
            }
            return s.ToString();
        }
        /// <summary>
        /// Hàm chuyển tiếng việt có dấu sang không dấu dạng "abc-def"
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertToUnSign(this string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace(" ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        /// <summary>
        /// Hàm lấy ra danh sách giá trị của các phần tử con trong một chuỗi xml
        /// </summary>
        /// <param name="xmlString"> chuỗi xml truyền vào</param>
        /// <returns></returns>
        public static List<string> GetListValueFromXmlString(this string xmlString)
        {
            try
            {
                XElement xElement = XElement.Parse(xmlString);
                List<string> list = new List<string>();
                foreach (var item in xElement.Elements())
                {
                    list.Add(item.Value);
                }
                return list;
            }
            catch (Exception)
            {

                return null ;
            }

        }
        /// <summary>
        /// Hàm kiểm tra xem class của object có thuộc tính với tên như vậy không?
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool HasProperty(this object obj,string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }
        /// <summary>
        /// Hàm đổi giá trị boolean của thuộc tính của đối tượng từ true thành false hoặc ngược lại
        /// </summary>
        /// <param name="obj">Đối tượng có thuộc tính cần đổi giá trị</param>
        /// <param name="propertyName"> Tên thuộc tính cần đổi giá trị</param>
        /// <returns></returns>
        public static bool ChangeBoolValue(this object obj, string propertyName)
        {
            try
            {
                if (obj.HasProperty(propertyName))
                {
                    var propInfo = obj.GetType().GetProperty(propertyName);
                    if (object.ReferenceEquals(propInfo.PropertyType,typeof(bool)))
                    {
                        bool newValue = !(bool)propInfo.GetValue(obj);
                        propInfo.SetValue(obj, newValue);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception )
            {

                return false;
            }

        }
        public static string[] mangso = { "không", "một","hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        //Đọc số hàng chục
        public static string DocHangChuc(double so, bool daydu=true)
        {
            string chuoi = "";
            int chuc = (int)Math.Floor(so / 10);
            int donvi = (int)so % 10;
            if (chuc > 1)
            {
                chuoi = " " + mangso[chuc] + " mươi";
                if (donvi == 1)
                {
                    chuoi += " mốt";
                }
            }
            else if (chuc == 1)
            {
                chuoi = " mười";
                if (donvi == 1)
                {
                    chuoi += " một";
                }
            }
            else if (daydu && donvi > 0)
            {
                chuoi = " lẻ";
            }
            if (donvi == 5 && chuc >= 1)
            {
                chuoi += " lăm";
            }
            else if (donvi > 1 || (donvi == 1 && chuc == 0))
            {
                chuoi += " " + mangso[donvi];
            }
            return chuoi;
        }
        //Đọc block 3 số
        public static string DocBlock(double so, bool daydu)
        {
            string chuoi = "";
            int tram = (int)Math.Floor(so / 100);
            so = so % 100;
            if (daydu || tram > 0)
            {
                chuoi = " " + mangso[tram] + " trăm";
                chuoi += DocHangChuc(so, true);
            }
            else
            {
                chuoi = DocHangChuc(so, false);
            }
            return chuoi;
        }
        //Đọc số hàng triệu
        public static string DocHangTrieu(double so, bool daydu)
        {
            string chuoi = "";
            int trieu = (int)Math.Floor(so / 1000000);
            so = so % 1000000;
            if (trieu > 0)
            {
                chuoi = DocBlock(trieu, daydu) + " triệu, ";
                daydu = true;
            }
            double nghin = Math.Floor(so / 1000);
            so = so % 1000;
            if (nghin > 0)
            {
                chuoi += DocBlock(nghin, daydu) + " nghìn, ";
                daydu = true;
            }
            if (so > 0)
            {
                chuoi += DocBlock(so, daydu);
            }
            return chuoi;
        }

        //Đọc số
        public static string DocSo(this double so)
        {
            if (so == 0) return mangso[0];
            string chuoi = "", hauto = "";
            do
            {
                double ty = so % 1000000000;
                so = Math.Floor(so / 1000000000);
                if (so > 0)
                {
                    chuoi = DocHangTrieu(ty, true) + hauto + chuoi;
                }
                else
                {
                    chuoi = DocHangTrieu(ty, false) + hauto + chuoi;
                }
                hauto = " tỷ, ";
            } while (so > 0);
            try
            {
                if (chuoi.Trim().Substring(chuoi.Trim().Length - 1, 1) == ",")
                { chuoi = chuoi.Trim().Substring(0, chuoi.Trim().Length - 1); }
            }
            catch { }
            return chuoi.Trim() + " đồng";
        }
    }
}
