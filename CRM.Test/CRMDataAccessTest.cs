using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CRM.DataAccess.Repos;
using CRM.DataAccess.Models;

namespace CRM.Test
{
	[TestFixture]
    public class CRMDataAccessTest
    {
        #region private test objects

        private Account account = new Account()
        {
            FirstName = "Test",
            LastName = "Testerson",
            Email = "test@gmail.com",
            Password = "Testword1"
        };

        private Customer customer = new Customer()
        {
            FirstName = "Test",
            LastName = "Testerson",
            Email = "test@gmail.com",
            Address = "123 Main St.",
            Phone = "817-555-1212",
            Company = "ABC Co."
        };

        private Note note = new Note()
        {
            AccountID = 1,
            Body = "Test Note",
            Created = DateTime.Now,
            CustomerID = 1
        };

        #endregion

        #region Account

        [Test]
        public void ShouldInsertAccount()
        {
            try
            {
                var accountID = AccountRepo.Register(account);
                Assert.Greater(accountID, 0);

                Account newAccount = AccountRepo.Get(accountID);
                Assert.AreEqual(account.FirstName, newAccount.FirstName);
                Assert.AreEqual(account.LastName, newAccount.LastName);
                Assert.AreEqual(account.Email, newAccount.Email);
                Assert.AreEqual(account.Password, newAccount.Password);
            }
            catch(AssertionException ae)
            {
                Assert.Fail(ae.Message);
            }
            finally
            {
                AccountRepo.Delete(account.AccountID);
            }
        }

        [Test]
        public void ShouldDeleteAccount()
        {
            var accountID = AccountRepo.Register(account);
            Assert.Greater(accountID, 0);

            var result = AccountRepo.Delete(accountID);
            Assert.IsTrue(result);

            var newAccount = AccountRepo.Get(accountID);
            Assert.IsNull(newAccount.FirstName);
        }

        [Test]
        public void ShouldAuthenticateAccount()
        {
            try
            {
                var accountID = AccountRepo.Register(account);
                var authAccountID = AccountRepo.Authenticate(account);

                Assert.AreEqual(accountID, authAccountID);
            }
            catch (AssertionException ae)
            {
                Assert.Fail(ae.Message);
            }
            finally
            {
                AccountRepo.Delete(account.AccountID);
            }
        }

        #endregion

        #region Customer

        [Test]
        public void ShouldInsertCustomer()
        {
            try
            {
                var customerID = CustomerRepo.Save(customer);
                Assert.Greater(customerID, 0);

                Customer newCustomer = CustomerRepo.Get(customerID);
                Assert.AreEqual(customer.FirstName, newCustomer.FirstName);
                Assert.AreEqual(customer.LastName, newCustomer.LastName);
                Assert.AreEqual(customer.Email, newCustomer.Email);
                Assert.AreEqual(customer.Company, newCustomer.Company);

            }
            catch (AssertionException ae)
            {
                Assert.Fail(ae.Message);
            }
            finally
            {
                CustomerRepo.Delete(customer.CustomerID);
            }
        }

        [Test]
        public void ShouldDeleteCustomer()
        {
            var id = CustomerRepo.Save(customer);
            Assert.Greater(id, 0);

            var result = CustomerRepo.Delete(id);
            Assert.IsTrue(result);

            var newCustomer = CustomerRepo.Get(id);
            Assert.IsNull(newCustomer.FirstName);
        }

        [Test]
        public void ShouldReadCustomer()
        {
            try
            {
                var id = CustomerRepo.Save(customer);
                Assert.Greater(id, 0);

                var newCustomer = CustomerRepo.Get(id);
                Assert.IsNotNull(customer.Email);
            }
            catch (AssertionException ae)
            {
                Assert.Fail(ae.Message);
            }
            finally
            {
                CustomerRepo.Delete(customer.CustomerID);
            }
        }

        [Test]
        public void ShouldUpdateCustomer()
        {
            try
            {
                CustomerRepo.Save(customer);
                Assert.Greater(customer.CustomerID, 0);

                customer.FirstName += "X";
                customer.LastName += "X";
                customer.Email += "X";

                var updated = CustomerRepo.Update(customer);
                Assert.IsTrue(updated);

                var updatedCustomer = CustomerRepo.Get(customer.CustomerID);
                Assert.AreEqual(customer.FirstName, updatedCustomer.FirstName);
                Assert.AreEqual(customer.LastName, updatedCustomer.LastName);
                Assert.AreEqual(customer.Email, updatedCustomer.Email);
            }
            catch (AssertionException ae)
            {
                Assert.Fail(ae.Message);
            }
            finally
            {
                CustomerRepo.Delete(customer.CustomerID);
            }
        }

        #endregion

        #region Note

        [Test]
        public void ShouldInsertNote()
        {
            try
            {
                note.Account = null;
                var newNote = NoteRepo.Save(note);
                Assert.IsNotNull(newNote.Body);

                Assert.AreEqual(note.Body, newNote.Body);
                Assert.AreEqual(note.AccountID, newNote.AccountID);
                Assert.AreEqual(note.CustomerID, newNote.CustomerID);
            }
            catch(AssertionException ae)
            {
                Assert.Fail(ae.Message);
            }
            finally
            {
                NoteRepo.Delete(note.NoteID);
            }
        }

        [Test]
        public void ShouldDeleteNote()
        {
            note.Account = null;
            NoteRepo.Save(note);
            Assert.Greater(note.NoteID, 0);

            var result = NoteRepo.Delete(note.NoteID);
            Assert.IsTrue(result);

            var deletedNote = NoteRepo.Get(note.NoteID);
            Assert.IsNull(deletedNote.Body);
        }

        [Test]
        public void ShouldReadNote()
        {
            try
            {
                note.Account = null;
                NoteRepo.Save(note);
                Assert.Greater(note.NoteID, 0);

                var newNote = NoteRepo.Get(note.NoteID);
                Assert.IsNotNull(newNote.Body);
            }
            catch (AssertionException ae)
            {
                Assert.Fail(ae.Message);
            }
            finally
            {
                NoteRepo.Delete(note.NoteID);
            }
        }

        #endregion
    }
}
