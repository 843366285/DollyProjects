using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        #region Extensions
        /*
         * 参考地址：http://www.nopchina.net/post/webapi-httpclient.html
         * Api接口参数的标准性

          Get方式，可以有多个重载，有多个参数

          POST方式，只能有一个参数，并且用[FromBody]约束，如果有多个参数，需要以对象的方式进行传递

          Put方式，只能有两个参数，其中一个是通过Request.QueryString方式进行传递的，作为要更新对象的主键，别一个是[FromBody]字段，也是一个字段，如果多个字段需要把它封装成对象
         * **/

        /// <summary>
        /// 实现Get方式获取接口数据
        /// </summary>
        static async void dooGet()
        {
            string url = "http://localhost:52824/api/register?id=1&leval=5";
            //创建HttpClient（注意传入HttpClientHandler）
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            using (var http = new HttpClient(handler))
            {
                //await异步等待回应
                var reponse = await http.GetAsync(url);
                //确保HTTP成功状态值
                reponse.EnsureSuccessStatusCode();

                //awiat异步读取最后的Json(注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression=DecompressionMethods.Gzip)
                Console.WriteLine(await reponse.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// HttpClient实现Post请求
        /// </summary>
        static async void dooPost()
        {
            string url = "http://localhost:52824/api/register";
            var userId = "1";
            //设置HttpClientHandler的AutomaticDesompression
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            //创建HttpClient（注意传入HttpClientHandler）
            using (var http = new HttpClient(handler))
            {
                //使用FormUrlEncodedContent做HttpContent
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "",userId}//键名必须为空
                });

                //await异步等待回应
                var response = await http.PostAsync(url, content);

                //确保HTTP成功状态值
                response.EnsureSuccessStatusCode();
                //await异步读取最后的JSON(注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression=DecompressionMethods.Gzip)
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// HttpClient实现Put请求
        /// </summary>
        static async void dooPut(int userId)
        {
            string url = "http://localhost:52824/api/register?userid=" + userId;
            // var userId = "1";
            //设置HttpClientHandler的AutomaticDecompression
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            //创建HttpClient（注意传入HttpClientHandler）
            using (var http = new HttpClient(handler))
            {
                //使用FormUrlEncodedContent做HttpContent
                var content = new FormUrlEncodedContent(new Dictionary<string, string>() {
                    { "","数据"}//键名必须为空
                });

                //await异步等待回应

                var response = await http.PutAsync(url, content);
                //确保HTTP成功状态值
                response.EnsureSuccessStatusCode();
                //await异步读取最后的JSON(注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression=DecompressionMethods.GZip)
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
        #endregion
    }
}
