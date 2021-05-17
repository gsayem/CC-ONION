using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;

namespace CallCentreWeb.Extensions
{
    public enum SessionKey
    {
        DbConnectionStatus,
        DbConnectionStatusText,
        CurrentUserName
    }
    public static class SessionExtensions
    {
        public static void Set(this ISession session, SessionKey key, string value)
        {
            session.SetString(key.ToString(), value);
        }

        public static string Get(this ISession session, SessionKey key)
        {            
            return session.GetString(key.ToString());            
        }

        public static void Remove(this ISession session, SessionKey key)
        {
            session.Remove(key.ToString());            
        }        
    }
}
