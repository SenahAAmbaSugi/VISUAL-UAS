using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.session
{
    static class Session
    {
        private static string id;
        private static string username;

        public static string Id
        {
            get { return id; }
            set { id = value; }
        }

        public static string Username
        {
            get { return username; }
            set { username = value; }
        }
    }
}

