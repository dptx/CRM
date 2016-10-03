using CRM.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataAccess
{
    public class Data
    {
        public Data()
        {

        }

        //public Data(IContext context)
        //{
        //    this.db = context;
        //}

        #region Account

        public static bool RegisterAccount(Account account)
        {
            try
            {
                using (var db = new Context())
                {
                    db.Accounts.Add(account);
                    var result = db.SaveChanges();

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Authenticate(Account account)
        {
            try
            {
                using (var db = new Context())
                {
                    return db.Accounts.Where(a => a.Email == account.Email && a.Password == account.Password).Count() == 1;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Customer

        public bool SaveCustomer(Customer customer)
        {
            try
            {
                using (var db = new Context())
                {
                    db.Customers.Add(customer);
                    var result = db.SaveChanges();

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Customer> GetCustomers()
        {
            try
            {
                using (var db = new Context())
                {
                    return db.Customers.Where(c => !string.IsNullOrEmpty(c.FirstName)).ToList();
                }
            }
            catch(Exception ex)
            {
                return new List<Customer>();
            }
        }

        #endregion

        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

    }
}
