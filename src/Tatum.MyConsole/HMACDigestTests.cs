using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TatumPlatform.MyConsole
{
    public class HMACDigestTests
    {
        string key = "1D899DF6-282C-4579-B871-1F918BFFC35F";
        string message = "{\"accountId\":\"615d9a199a8dee492f075a5f\",\"amount\":\"5\",\"reference\":\"94af6252-9eb8-4465-bf18-7221f612f341\",\"currency\":\"CELO\",\"txId\":\"0x01fac068c6f9a7a5ca446b884732f81fd163a38055f5b3ebc3ff86e35f376f91\",\"blockHeight\":7633965,\"blockHash\":\"0x76ba5093f52cbb748e4481c4f4e05705bccf7cb4ae5141b074db384cc96bbfb2\",\"from\":\"0x22579ca45ee22e2e16ddf72d955d6cf4c767b0ef\",\"to\":\"0x307eaba8b2c0f756d64d7ee704b9e88954fca8a9\",\"date\":1633525175800}";
        string sign = "gsAJqUelrnA0mHiTIL92j/kxjrHa5eX3IfnIqNAtnl8aHelT4DS8Mjc8Q7J1NJGg8X97oSs+s1fHcCVOJ9D4QA==";
        //x-payload-hash:gsAJqUelrnA0mHiTIL92j/kxjrHa5eX3IfnIqNAtnl8aHelT4DS8Mjc8Q7J1NJGg8X97oSs+s1fHcCVOJ9D4QA==,body:{"accountId":"615d9a199a8dee492f075a5f","amount":"5","reference":"94af6252-9eb8-4465-bf18-7221f612f341","currency":"CELO","txId":"0x01fac068c6f9a7a5ca446b884732f81fd163a38055f5b3ebc3ff86e35f376f91","blockHeight":7633965,"blockHash":"0x76ba5093f52cbb748e4481c4f4e05705bccf7cb4ae5141b074db384cc96bbfb2","from":"0x22579ca45ee22e2e16ddf72d955d6cf4c767b0ef","to":"0x307eaba8b2c0f756d64d7ee704b9e88954fca8a9","date":1633525175800}
        public void Test()
        {
            var sig = HashHMAC(key, message);
            if(sign == sig)
            {

            }
        }

        private static string HashHMAC(string key, string message)
        {
            var secretBye = Encoding.UTF8.GetBytes(key);
            var messageBye = Encoding.UTF8.GetBytes(message);
            var hash = new HMACSHA512(secretBye);
            var result = hash.ComputeHash(messageBye);
            var r = Convert.ToBase64String(result);
            return r;
        }
    }
}
