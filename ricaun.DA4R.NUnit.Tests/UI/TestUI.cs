using NUnit.Framework;
using System;

namespace ricaun.DA4R.NUnit.Tests.UI
{
    public class TestUI
    {
        [Test]
        public void NewAppDB()
        {
            new AppDB();
        }

        [Test]
        public void NewAppDB_UI_ThrowsException()
        {
            Assert.Throws<System.IO.FileNotFoundException>(() =>
            {
                new AppDB().UI();
            });
        }
    }
}
