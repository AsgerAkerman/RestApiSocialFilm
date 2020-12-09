using DataBaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestApiSocialFilm.UserAuthentication
{
    public class AuthRepository
    {
        internal bool ValidateUser(string userName, string password)
        {
           yndlingsfilmDBEntities db = new yndlingsfilmDBEntities();
            var tempUser = db.Users.FirstOrDefault(u => u.Username == userName && u.Password == password);
            if (tempUser == null)
            {
                return false;
            }
            return true;
        }
    } 
    
}