// Team //noComment
//
// Matt Kerr
// Mary Floyd
// Tim Unger
//
// CSCD350
// Spring 2015

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TriviaMaze;

namespace GameWindowUnitTest
{
    [TestClass]
    public class GameWindowTest
    {
        [TestMethod]
        public void TestIsAdjacentLockVert1()
        {
            GameWindow gw = new GameWindow(0);
            bool result = gw.IsAdjacentLockVert(3, 2);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsAdjacentLockVert2()
        {
            GameWindow gw = new GameWindow(0);
            bool result = gw.IsAdjacentLockVert(3, 3);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsAdjacentLockVert3()
        {
            GameWindow gw = new GameWindow(0);
            bool result = gw.IsAdjacentLockVert(3, 4);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsAdjacentLockVert4()
        {
            GameWindow gw = new GameWindow(0);
            bool result = gw.IsAdjacentLockVert(2, 3);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsAdjacentLockHoriz1()
        {
            GameWindow gw = new GameWindow(0);
            bool result = gw.IsAdjacentLockHoriz(2, 3);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsAdjacentLockHoriz2()
        {
            GameWindow gw = new GameWindow(0);
            bool result = gw.IsAdjacentLockHoriz(3, 3);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsAdjacentLockHoriz3()
        {
            GameWindow gw = new GameWindow(0);
            bool result = gw.IsAdjacentLockHoriz(4, 3);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsAdjacentLockHoriz4()
        {
            GameWindow gw = new GameWindow(0);
            bool result = gw.IsAdjacentLockHoriz(3, 2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestSetUserAnswer1()
        {
            GameWindow gw = new GameWindow(0);
            gw.SetUserAnswer("test");
            Assert.AreEqual(gw.user_answer, "test");
        }

        [TestMethod]
        public void TestSetUserAnswer2()
        {
            GameWindow gw = new GameWindow(0);
            gw.SetUserAnswer("test");
            Assert.AreNotEqual(gw.user_answer, "test2");
        }
    }
}
