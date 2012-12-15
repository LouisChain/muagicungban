using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;

namespace muagicungban
{

    public class NL_Checkout
    {

        private String nganluong_url = "https://www.nganluong.vn/checkout.php";
        private String merchant_site_code = "26406";
        private String secure_pass = "@%@%!#@%";
        public String GetMD5Hash(String input)
        {

            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);

            bs = x.ComputeHash(bs);

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)
            {

                s.Append(b.ToString("x2").ToLower());

            }

            String md5String = s.ToString();

            return md5String;
        }

        public String buildCheckoutUrl(String return_url, String receiver, String transaction_info, String order_code, String price)
        {

            String secure_code = "";

            secure_code += this.merchant_site_code;

            secure_code += " " + HttpUtility.UrlEncode(return_url).ToLower();

            secure_code += " " + receiver;

            secure_code += " " + transaction_info;

            secure_code += " " + order_code;

            secure_code += " " + price;

            secure_code += " " + this.secure_pass;


            Hashtable ht = new Hashtable();

            ht.Add("merchant_site_code", this.merchant_site_code);

            ht.Add("return_url", HttpUtility.UrlEncode(return_url).ToLower());

            ht.Add("receiver", receiver);

            ht.Add("transaction_info", transaction_info);

            ht.Add("order_code", order_code);

            ht.Add("price", price);

            ht.Add("secure_code", this.GetMD5Hash(secure_code));


            String redirect_url = this.nganluong_url;

            if (redirect_url.IndexOf("?") == -1)
            {
                redirect_url += "?";
            }
            else if (redirect_url.Substring(redirect_url.Length - 1, 1) != "?" && redirect_url.IndexOf("&") == -1)
            {
                redirect_url += "&";
            }

            String url = "";


            IDictionaryEnumerator en = ht.GetEnumerator();

            while (en.MoveNext())
            {
                if (url == "")
                    url += en.Key.ToString() + "=" + en.Value.ToString();
                else
                    url += "&" + en.Key.ToString() + "=" + en.Value.ToString();
            }

            String rdu = redirect_url + url;

            return rdu;
        }

        public Boolean verifyPaymentUrl(String transaction_info, String order_code, String price, String payment_id, String payment_type, String error_text, String secure_code)
        {

            String str = "";

            str += " " + transaction_info;

            str += " " + order_code;

            str += " " + price;

            str += " " + payment_id;

            str += " " + payment_type;

            str += " " + error_text;

            str += " " + this.merchant_site_code;

            str += " " + this.secure_pass;


            String verify_secure_code = "";

            verify_secure_code = this.GetMD5Hash(str);


            if (verify_secure_code == secure_code) return true;

            return false;
        }

    }
}