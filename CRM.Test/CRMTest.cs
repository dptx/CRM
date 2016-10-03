using CRM.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRM.Test
{
    [TestFixture]
    public class CRMTest
    {
        #region Customer

        [Test]
        public void TestCustomerListView()
        {
            var controller = new CustomerController();
            var result = controller.CustomerList() as PartialViewResult;
            Assert.AreEqual("_CustomerList", result.ViewName);
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public void TestCustomerNotesView()
        {
            var controller = new CustomerController();
            var result = controller.Notes(1) as ViewResult;
            Assert.IsNotNull(result.Model);
        }

        #endregion
    }
}
