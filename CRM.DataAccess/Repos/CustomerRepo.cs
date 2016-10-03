using CRM.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataAccess.Repos
{
    public class CustomerRepo
    {
        public static int Save(Customer customer)
        {
            try
            {
                using (var db = new Context())
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();

                    return customer.CustomerID;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static Customer Get(int id)
        {
            try
            {
                using (var db = new Context())
                {
                    return db.Customers.Single(c => c.CustomerID == id);
                }
            }
            catch (Exception ex)
            {
                return new Customer();
            }
        }

        public static List<Customer> GetCustomers()
        {
            try
            {
                using (var db = new Context())
                {
                    var customers = from c in db.Customers
                                select c;

                    return customers.ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Customer>();
            }
        }

        public static bool Update(Customer customer)
        {
            try
            {
                using (var db = new Context())
                {
                    db.Customers.Attach(customer);
                    db.Entry(customer).State = EntityState.Modified;
                    return db.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                using (var db = new Context())
                {
                    var customer = db.Customers.Single(c => c.CustomerID == id);
                    db.Customers.Remove(customer);
                    var result = db.SaveChanges();

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
