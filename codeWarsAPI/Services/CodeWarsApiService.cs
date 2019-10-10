using codeWarsAPI.DataAccess;
using codeWarsAPI.Models;
using System;
using System.IO;
using System.Net;
using System.Linq;

namespace codeWarsAPI.Services
{
    public class CodeWarsApiService
    {
        private readonly string apiUrl = "https://www.codewars.com/api/v1/users/";
        private readonly string apiKata = "https://www.codewars.com/api/v1/code-challenges/";
        private readonly string apiKey = "?access_key=hyqJwybvFQNtkC3h2YFg/";
        private readonly CodewarsContext _context;

        public CodeWarsApiService(CodewarsContext context)
        {
            _context = context;
        }

        public bool PerformCheckAndAddAsync(CodeWarsAddModel cw)
        {
            Get(apiUrl + cw.Username + apiKey);
            Get(apiKata + cw.Kata.ToLower() + apiKey);
            return PutItemInDataBase(cw);
        }

        public bool Get(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw new Exception(uri.Contains("code-challenges") ? "The kata does not exist" : "The username is incorrect");
            }
        }

        public bool PutItemInDataBase(CodeWarsAddModel cw)
        {
            try
            {
                User u = _context.Users.Where(s => s.Username == cw.Username).First();
                Kata k = new Kata() { KataName = cw.Kata, UserId = u.UserId };
                _context.Katas.Add(k);
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Database error");
            }
        }
    }
}