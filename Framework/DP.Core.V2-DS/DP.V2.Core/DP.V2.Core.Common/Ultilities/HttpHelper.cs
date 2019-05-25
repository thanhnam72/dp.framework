using DP.V2.Core.Common.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DP.V2.Core.Common.Ultilities
{
    public static class HttpHelper
    {
        public static BaseResponse<T> Request<T>(string url, object param, IDictionary<string, string> headers = null)
        {
            BaseResponse<T> result = new BaseResponse<T>();

            try
            {
                var hc = new HttpClient();
                HttpResponseMessage res;

                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));               

                if (headers != null)
                {
                    foreach(var item in headers)
                    {
                        hc.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                
                if (param != null)
                {
                    var dataAsString = JsonConvert.SerializeObject(param);
                    var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
                    res = hc.PostAsync(url, content).Result;
                }
                else
                {
                    res = hc.GetAsync(url).Result;
                }


                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var response = JsonConvert.DeserializeObject<BaseServiceResult<T>>(res.Content.ReadAsStringAsync().Result);
                    result = response.Result;
                }
                else
                {
                    result.ErrorCode = -1;
                    result.Errors = string.Format("Code return : {0}. Message: {1}", res.StatusCode.ToString(), res.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                result.ErrorCode = -1;
                result.Errors = ex.Message;
            }

            return result;
        }

        public static BaseResponse<T> RequestWithToken<T>(string url, object requestData, string token)
        {
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", token);
            var param = new
            {
                RequestData = requestData
            };
            return Request<T>(url, param, headers);
        }

        public static BaseResponse<T> RequestWithIdentity<T>(string url, object requestData, string us, string ps)
        {
            string hashPassword = PasswordSecurityHelper.GetHashedPassword(ps);
            var headers = new Dictionary<string, string>();
            headers.Add("Username", us);
            headers.Add("Password", hashPassword);
            headers.Add("Provider", "1");
            var param = new
            {
                RequestData = requestData
            };
            return Request<T>(url, param, headers);
        }
    }
}
