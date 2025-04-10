using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tests_For_Campus_Jobs_Project.DatabaseWork;
using Moq;
using campusjobv2.Controllers;
using campusjobv2;
using Microsoft.EntityFrameworkCore;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using campusjobv2.Models.Entities;
using campusjobv2.Models;
using System.Diagnostics;

namespace Tests_For_Campus_Jobs_Project
{
    public class Testsv2
    {
        //INTEGRATION TESTS

        [Test]
        public void Test_Index_View()
        {
            //accessing databaseWork
            DatabaseWork dbWork = new DatabaseWork();

            //new dbContext
            ApplicationDbContext context = new ApplicationDbContext(dbWork.currentOptions);

            //Mocking the ILogger
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<campusjobv2.Controllers.AdminController>>();
            ILogger<AdminController> adminLogger = mockLogger.Object;
            AdminController adminController = new AdminController(context, adminLogger);

            //Mocking the HttpContext
            var mockHttpContext = new Mock<HttpContext>();
            HttpContext httpContext = mockHttpContext.Object;

            var result = adminController.Index("Macy");

            //reaches "return View(model)" end point
            Assert.That(result.GetType().Name.Contains("ViewResult"));
        }

        [Test]
        public void Test_Index_RedirectToAction()
        {
            //accessing databaseWork
            DatabaseWork dbWork = new DatabaseWork();

            //new dbContext
            ApplicationDbContext context = new ApplicationDbContext(dbWork.currentOptions);

            //Mocking the ILogger
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<campusjobv2.Controllers.AdminController>>();
            ILogger<AdminController> adminLogger = mockLogger.Object;
            AdminController adminController = new AdminController(context, adminLogger);

            //Mocking the HttpContext
            var mockHttpContext = new Mock<HttpContext>();
            HttpContext httpContext = mockHttpContext.Object;

            var result = adminController.Index("Macy");

            //reaches "return RedirectToAction("Index", "Login")" end point
            Assert.That(result.GetType().Name.Contains("RedirectToActionResult"));
        }

        [Test]
        public void Test_CreateStudentAccount_Success()
        {

            //accessing databaseWork
            DatabaseWork dbWork = new DatabaseWork();

            //new dbContext
            ApplicationDbContext context = new ApplicationDbContext(dbWork.currentOptions);

            /*Mock<DbContextOptions<ApplicationDbContext>> mockCurrentOptions = new Mock<DbContextOptions<ApplicationDbContext>>();
            DbContextOptions<ApplicationDbContext> currentOptions = mockCurrentOptions.Object;
            Mock<ApplicationDbContext> mockContext = new Mock<ApplicationDbContext>(currentOptions);
            ApplicationDbContext context = mockContext.Object;*/

            //Mocking the ILogger
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<campusjobv2.Controllers.AdminController>>();
            ILogger<AdminController> adminLogger = mockLogger.Object;
            AdminController adminController = new AdminController(context, adminLogger);

            //Mocking the HttpContext
            var mockHttpContext = new Mock<HttpContext>();
            HttpContext httpContext = mockHttpContext.Object;

            //add recruiter to database?????
            User testRecruiter = new User { First_Name = "RecruiterName", Last_Name = "Surname", Email = "RecruiterName@address.com", Password = "password", Role = 2 }; // recruiter
            Recruiter testDbRecruiter = new Recruiter { User = testRecruiter };

            string sqlQuery = $"INSERT INTO Users (First_Name, Last_Name, Email, Password, Role) VALUES ({testRecruiter.First_Name}, {testRecruiter.Last_Name}, {testRecruiter.Email}, {testRecruiter.Password}, {testRecruiter.Role});";
            dbWork.InsertIntoDb(sqlQuery);
            sqlQuery = $"SELECT User_ID FROM Users WHERE First_Name='{testRecruiter.First_Name}';";
            int result = dbWork.InsertIntoDb(sqlQuery);
            if (result >0)
            {
                Debug.WriteLine("It affected something");
            }
            else
            {
                Debug.WriteLine("It didn't");
            }

            //sqlQuery = $"INSERT INTO Recruiters (User_ID, User) VALUES ({ userID },{testRecruiter});";


            //get data from database

            //create new student
            User testStudent = new User { First_Name = "StudentName", Last_Name = "Surname", Email = "StudentName@address.com", Password = "password", Role = 3 };  //student
            //insert student into database
            StudentAccountModel studentAccountModel = new StudentAccountModel
            {
                FirstName = "StudentName",
                LastName = "Surname",
                Email = "StudentName@address.com",
                Department = "Software Engineering",
                //recruiterID
                //RecruiterId = recruiter.Recruitment_ID,
                IsVisaRestricted = false,
                VisaExpiryDate = null
            };

            //adminController.CreateStudentAccount(studentAccountModel);
        }
    }
}
