using MiRAPI.DataModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI
{
    public class AuthToken
    {
        public int UserId { get; set; }

        public string Token { get; set; }

        public DateTime ActiveTill { get; set; }

        public static AuthToken Create(int userId)
        {
            return new AuthToken { Token = Guid.NewGuid().ToString(), UserId = userId, ActiveTill = DateTime.Now.AddDays(10) };
        }
    }

    public static class AppState
    {
        public static IDictionary<string, AuthToken> Tokens { get; } = new ConcurrentDictionary<string, AuthToken>();

        internal static string AddToken(User user)
        {
            var token = AuthToken.Create(user.ID);

            Tokens.Add(token.Token, token);

            return token.Token;
        }

        internal static void RemoveAuth(string token)
        {
            Tokens.Remove(token);
        }
    }
}
