using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodDemo
{
    internal class UserLogin
    {
        // static data members, static fields
        private static string UserID;
        private static string UserName;
        private static string UserPassword;
       // private static string UserType;

        // static methods: setter methods
        public static void setUserID(string userId)
        {
            UserID = userId;
        }

        public static void setUserName(string username)
        {
            UserName = username;
        }

        public static void setUserPassword(string userpassword)
        {
            UserPassword = userpassword;
        }

        /*public static void setUserType(string usertype)
        {
            UserType = usertype;
        }*/

        // static methods: getter methods
        public static string getUserID()
        {
            return UserID;
        }

        public static string getUserName()
        {
            return UserName;
        }

        public static string getUserPassword()
        {
            return UserPassword;
        }

        /*public static string getUserType()
        {
            return UserType;
        }*/


        // clear user login info
        public static void ClearSessions()
        {
            UserID = string.Empty;
            UserName = string.Empty;
            UserPassword = string.Empty;
            //UserType = string.Empty;
        }
    }
}
