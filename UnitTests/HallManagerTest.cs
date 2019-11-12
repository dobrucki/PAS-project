using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PAS_project.Models;

namespace UnitTests
{   [TestFixture]
    public class HallManagerTest
    {
        private static Hall GenerateTestHall()
        {
            var testSeatList = new List<Seat>();
            var testHall = new Hall(testSeatList);
            
            for (var c = 'A'; c <= 'H'; c++)
            {
                for(var i = 1 ; i<25 ; i++)
                {
                    testSeatList.Add(new Seat(c, i, testHall));
                }
            }

            return testHall;
        }

        [Test]
        public void AddHall_ValidHall_Adds()
        {
           
            
            var hallManager = new HallManager(new HallRepository());

            hallManager.AddHall(GenerateTestHall());

            Assert.AreEqual(hallManager.GetAllHalls().Count() , 1);
            
        }
        
        [Test]
        public void AddHall_InValidHall_Throws()
        {
           
            
            var hallManager = new HallManager(new HallRepository());
            

            var testHall = GenerateTestHall();
            hallManager.AddHall(testHall);
            

            Assert.Throws<Exception>(() => hallManager.AddHall(testHall));

        }
        
        [Test]
        public void GetHall_ValidId_Returns()
        {
           
            
            var hallManager = new HallManager(new HallRepository());
            

            var testHall = GenerateTestHall();
            hallManager.AddHall(testHall);
            

            Assert.AreEqual(testHall, hallManager.GetHall(1));

        }
        
        [Test]
        public void GetHall_InvalidId_Throws()
        {
           
            
            var hallManager = new HallManager(new HallRepository());
            

            var testHall = GenerateTestHall();
            hallManager.AddHall(testHall);


            Assert.Throws<Exception>(() =>hallManager.GetHall(3));

        }
        
        [Test]
        public void UpdateHall_ExistingHall_Updates()
        {
           
            
            var hallManager = new HallManager(new HallRepository());
            
            var testHall = GenerateTestHall();
            hallManager.AddHall(testHall);
            testHall.GetAllSeats().ToList()[0].Row = 'Y';
            hallManager.UpdateHall(testHall);
            

            Assert.AreEqual('Y', 
                hallManager.GetAllHalls().ToList()[0].GetAllSeats().ToList()[0].Row);

        }
        
        [Test]
        public void UpdateHall_NonexistentHall_Throws()
        {
           
            
            var hallManager = new HallManager(new HallRepository());
            
            var testHall1 = GenerateTestHall();
            var testHall2 = GenerateTestHall();
            hallManager.AddHall(testHall1);
            
            

            Assert.Throws<Exception>(() => hallManager.UpdateHall(testHall2));
        }
        
        [Test]
        public void DeleteHall_ValidId_Deletes()
        {
           
            
            var hallManager = new HallManager(new HallRepository());
            

            var testHall = GenerateTestHall();
            hallManager.AddHall(testHall);
            hallManager.DeleteHall(1);

            Assert.AreEqual(0, hallManager.GetAllHalls().Count());

        }
        
        
    }
}