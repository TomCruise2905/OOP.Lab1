using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOP.Lab1.Geometry2D;
using OOP.Lab1.Particles;
using System.Linq;

namespace OOP.Lab1.Tests
{
    [TestClass]
    public class ParticleTests
    {
        [TestMethod]
        public void Drift()
        {
            Phonon phonon = new Phonon(1, 0, Polarization.LA,true, 0, new Point(10, 20), new Vector(-.5, .8), 1000);

            phonon.Drift(.1);

            double newX = 0, newY = 0;
            phonon.GetCoords(out newX, out newY);

            Assert.AreEqual(newX, -40);
            Assert.AreEqual(newY, 100);

        }
    }
}
