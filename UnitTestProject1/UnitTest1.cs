using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using поворотPictureBox;

using static поворотPictureBox.ImageList;

namespace UnitTestProject1
{
    [TestClass]
    public class поворотPictureBox
    {
        [TestMethod]
        public void TestMethod1()
        {
            var a = Assert.ThrowsException<System.Exception>(() => ImageList.sizeException(-1));
        }
    }
}
