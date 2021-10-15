using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOP.Lab1.ModelComponents;
using OOP.Lab1.Particles;
using System.Linq;
namespace OOP.Lab1.Tests
{
    [TestClass]
    public class PhononTests
    {
        [TestMethod]
        public void SetSignTest()
        {
            Phonon phonon = new Phonon(1);

            phonon.SetSign(-1);

            Assert.AreEqual(phonon.Sign, -1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Phonon phonon = new Phonon(1);

            phonon.Update(1, 2, Polarization.LA);

            Assert.AreEqual(phonon.Frequency, 1);
            Assert.AreEqual(phonon.Speed, 2);
            Assert.AreEqual(phonon.Polarization, Polarization.LA);
        }
    }
}
