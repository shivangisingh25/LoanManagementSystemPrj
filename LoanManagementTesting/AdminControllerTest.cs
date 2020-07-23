using LoanManagementSystemPrj.Controllers;
using LoanManagementSystemPrj.Models;
using LoanManagementSystemPrj.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace LoanManagementTesting
{
    public class AdminControllerTest
    {
        LoanManagementContext db;
        [SetUp]
        public void Setup()
        {
            var loan = new List<Loan>
            {
                new Loan{Id = 1, AccNo = 123456789, Name="Dummy1", Age=30,Gender="Male",Address="Dummy Address 1",PhnNo="1122334455",AccType="Savings",AccBal=(decimal)52879.11,LoanAmt=(decimal)7895.11,LoanPremium=(decimal)785.33},
                new Loan{Id = 2, AccNo = 123456780, Name="Dummy2", Age=25,Gender="Female",Address="Dummy Address 2",PhnNo="1122334466",AccType="Salary",AccBal=(decimal)5710.22,LoanAmt=(decimal)8975.00,LoanPremium=(decimal)2578.22},
                new Loan{Id = 3, AccNo = 123456781, Name="Dummy3", Age=72,Gender="Male",Address="Dummy Address 3",PhnNo="1122334477",AccType="Current",AccBal=(decimal)87523.78,LoanAmt=(decimal)2245798.00,LoanPremium=(decimal)12874.32},
                new Loan{Id = 4, AccNo = 123456783, Name="Dummy4", Age=34,Gender="Female",Address="Dummy Address 4",PhnNo="1122334488",AccType="Savings",AccBal=(decimal)278546.51,LoanAmt=(decimal)21478.00,LoanPremium=(decimal)8795.27}
            };
            var loandata = loan.AsQueryable();
            var mockSet = new Mock<DbSet<Loan>>();
            mockSet.As<IQueryable<Loan>>().Setup(m => m.Provider).Returns(loandata.Provider);
            mockSet.As<IQueryable<Loan>>().Setup(m => m.Expression).Returns(loandata.Expression);
            mockSet.As<IQueryable<Loan>>().Setup(m => m.ElementType).Returns(loandata.ElementType);
            mockSet.As<IQueryable<Loan>>().Setup(m => m.GetEnumerator()).Returns(loandata.GetEnumerator());
            var mockContext = new Mock<LoanManagementContext>();
            mockContext.Setup(c => c.Loan).Returns(mockSet.Object);
            db = mockContext.Object;
        }


     
        [Test]
        public void get_Valid_Details()
        {
            admin loandata = new admin(db);
            adminController obj = new adminController(loandata);
            var data = obj.GetDetails();
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }


        [Test]
        public void get_Valid_Detail()
        {
            admin loandata = new admin(db);
            adminController obj = new adminController(loandata);
            var data = obj.GetDetail(2);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }


       




    }
}