using CRM.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataAccess.Repos
{
    public class AccountRepo
    {
        public static int Register(Account account)
        {
            try
            {
                using (var db = new Context())
                {
                    db.Accounts.Add(account);
                    db.SaveChanges();

                    return account.AccountID;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static Account Get(int accountID)
        {
            try
            {
                using (var db = new Context())
                {
                    return db.Accounts.Single(a => a.AccountID == accountID);
                }
            }
            catch (Exception ex)
            {
                return new Account();
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                using (var db = new Context())
                {
                    var account = db.Accounts.Single(a => a.AccountID == id);
                    db.Accounts.Remove(account);
                    var result = db.SaveChanges();

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static int Authenticate(Account account)
        {
            try
            {
                using (var db = new Context())
                {
                    account = db.Accounts.Single(a => a.Email == account.Email && a.Password == account.Password);
                    return account.AccountID;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static bool IsEmailAvailable(string email)
        {
            try
            {
                using (var db = new Context())
                {
                    return db.Accounts.Where(a => a.Email.ToLower() == email.Trim().ToLower()).Count() == 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
