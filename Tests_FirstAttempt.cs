using campusjobv2;
using campusjobv2.Models;
using campusjobv2.Controllers;
using System;
using campusjobv2.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static Tests_For_Campus_Jobs_Project.DatabaseWork;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Moq;
using Microsoft.EntityFrameworkCore.Storage;

namespace Tests_For_Campus_Jobs_Project
{
    public class TestingDoc_1
    {
        /* What to do:
         *  create mock of database
         *  use mock to test the functions:
         *      - add user
         *      - delete user
         *      - query user info (is it correct)
         *      - is user getting access to the correct information
         *      - log out correctly
         *      
         *      - can shifts be made correctly
         *      - can shifts be accepted correctly
         *      - etc
         */

        public User testStudent = new User { First_Name = "StudentName", Last_Name = "Surname", Email = "StudentName@address.com", Password = "password", Role = 3};  //student
        public User testAdmin = new User { First_Name = "AdminName", Last_Name = "Surname", Email = "AdminName@address.com", Password = "password", Role = 1 }; // admin
        public User testRecruiter = new User { First_Name = "RecruiterName", Last_Name = "Surname", Email = "RecruiterName@address.com", Password = "password", Role = 2 }; // recruiter
        

        [Test]
        public void Test_InsertNewUser()
        {
            /*
             * submit following details:
             *  correct email + correct password
             *  correct email + wrong password
             *  wrong email
             */

            /*
             * create new profile
             * set username 
             * set password
             * test both
             */

            

            string sqlQuery = $"INSERT INTO Users (First_Name, Last_Name, Email, Password, Role) VALUES ({testStudent.First_Name}, {testStudent.Last_Name}, {testStudent.Email}, {testStudent.Password}, {testStudent.Role});" ;

            DatabaseWork dbWork = new DatabaseWork();
            int columnsChanged = dbWork.InsertIntoDb(sqlQuery);

            //look for user table
            //inject account info into table
            //retrieve account from database

            //check if something has changed
            if (columnsChanged > 0)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Test_InsertExistingUser()
        {
            string sqlQuery = $"INSERT INTO Users (First_Name, Last_Name, Email, Password, Role) VALUES ({testStudent.First_Name}, {testStudent.Last_Name}, {testStudent.Email}, {testStudent.Password}, {testStudent.Role});";

            DatabaseWork dbWork = new DatabaseWork();
            dbWork.InsertIntoDb(sqlQuery);
            int columnsChanged = new int();
            Exception info = null;
            try
            {
                columnsChanged = dbWork.InsertIntoDb(sqlQuery);
            }
            catch (Exception ex)
            {
                info = ex;
            }

            //if info is a specific exception type
            Assert.That(info != null);
            
            //ADD MORE
        }

        [Test]
        public void Test_InsertInvalidInformation()
        {
            //5 is too high
            testStudent.Role = 5;

            string sqlQuery = $"INSERT INTO Users (First_Name, Last_Name, Email, Password, Role) VALUES ({testStudent.First_Name}, {testStudent.Last_Name}, {testStudent.Email}, {testStudent.Password}, {testStudent.Role});";

            DatabaseWork dbWork = new DatabaseWork();
            dbWork.InsertIntoDb(sqlQuery);

            //is exception thrown
        }
    }
}
