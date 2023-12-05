using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Data;

namespace TeamProject_test_v1
{
    internal class PublicAPI
    {
        private static PublicAPI api = new PublicAPI();
        static HttpClient client = new HttpClient();

        private PublicAPI()
        {
        }

        public static PublicAPI getInstance() { return api; }

        private string DateAPI()
        {
            string url = "http://apis.data.go.kr/B090041/openapi/service/SpcdeInfoService/getRestDeInfo@sk"; // URL
            url = url.Replace("@sk", "?ServiceKey=Jd4QND/Ih4DWAlVpLckQmXqAAHYku6S1gsTtqKEAHFKdq31kJ9t3hQKayMFmOxciCvRGxF16EyavGF9AO6Z6mw==@pn"); // Service Key
            url = url.Replace("@pn", "&pageNo=1@nr");
            url = url.Replace("@nr", "&numOfRows=24@year");
            url = url.Replace("@year", "&solYear=2023@month");
            url = url.Replace("@month", "&solMonth=12");

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            string results = string.Empty;
            HttpWebResponse response;
            using (response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                //MessageBox.Show(reader.ReadToEnd());
                results = reader.ReadToEnd();
            }

            return results;
        }

        public Boolean GetAPI(string date)
        {
            string result = DateAPI();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);

            // "item" 노드를 선택
            XmlNodeList items = xml.SelectNodes("//item");

            // 각 날짜에 대한 루프
            foreach (XmlNode item in items)
            {
                // "locdate" 요소의 값을 가져오고 비교
                string apiDate = item["locdate"]?.InnerText;

                // API 날짜가 내가 보낸 날짜와 같다면 참을 반환
                if (apiDate == date)
                {
                    return true;
                }
            }

            // 모든 날짜에 대해 API 날짜와 일치하는 것을 찾지 못한 경우 거짓을 반환
            return false;
        }

    }
}
