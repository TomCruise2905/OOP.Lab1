using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOP.Lab1.Particles;
using System;
using System.Linq;

namespace OOP.Lab1.ModelComponents.Tests
{
    [TestClass()]
    public class CellTests
    {

        [TestMethod()]
        public void AddPhononTest()
        {
            Cell cell = new Cell(0, 0, GetSilicon(), 0);

            Phonon phonon = new Phonon(1);

            cell.AddPhonon(phonon);

            Assert.AreEqual(cell.Phonons.First(), phonon);
        }

        [TestMethod()]
        public void AddIncPhononTest()
        {
            Cell cell = new Cell(0, 0, GetSilicon(), 0);

            Phonon phonon = new Phonon(1);

            cell.AddIncPhonon(phonon);

            Assert.AreEqual(cell.IncomingPhonons.First(), phonon);

        }
        
        [TestMethod()]
        public void MergeIncPhononsTest()
        {
            Cell cell = new Cell(0, 0, GetSilicon(), 0);

            Phonon phonon = new Phonon(1);
            Phonon incPhonon1 = new Phonon(1);
            Phonon incPhonon2 = new Phonon(1);

            cell.AddPhonon(phonon);
            cell.AddIncPhonon(incPhonon1);
            cell.AddIncPhonon(incPhonon2);
            cell.MergeIncPhonons();

            Assert.AreEqual(cell.Phonons.Count, 3);
            Assert.AreEqual(cell.Phonons.Where((x) => {
                return !x.Equals(phonon) && !x.Equals(incPhonon1) && !x.Equals(incPhonon2);
            }).Count(), 0);
            Assert.AreEqual(cell.IncomingPhonons.Count, 0);
        }

        private Material GetSilicon()
        {
            DispersionData dData;
            dData.LaData = new double[] { -2.22e-7, 9260.0, 0.0 };
            dData.TaData = new double[] { -2.28e-7, 5240.0, 0.0 };
            dData.WMaxLa = 7.63916048e13;
            dData.WMaxTa = 3.0100793072e13;

            RelaxationData rData;
            rData.Bl = 1.3e-24;
            rData.Btn = 9e-13;
            rData.Btu = 1.9e-18;
            rData.BI = 1.2e-45;
            rData.W = 2.42e13;

            return new Material(in dData, in rData);
        }

    }
}