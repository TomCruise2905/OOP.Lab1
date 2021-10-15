using System;
using System.Collections.Generic;
using System.Text;

namespace OOP.Lab1
{
	public class Sensor
	{
		private double heatCapacity;
		private List<double> temperatures = new List<double>() { };
		private List<double> xFluxes = new List<double>() { };
		private List<double> yFluxes = new List<double>() { };
		public int ID { get; }
		public double InitTemp { get; }
		public Material Material { get; }
		public Tuple<double, double>[] BaseTable { get; private set; }
		public Tuple<double, double>[] ScatterTable { get; private set; }
		public double HeatCapacity { get { return heatCapacity; } }
		public double Temperature { get; private set; }
		public double AreaCovered { get; private set; }

		public Sensor(int id, Material material, double initTemp)
		{
			ID = id;
			Material = material;
			InitTemp = initTemp;
			BaseTable = material.BaseData(initTemp, out heatCapacity);
			ScatterTable = material.ScatterTable(initTemp);
			Temperature = initTemp;
		}

		public override string ToString() => $"Sensor {ID}: {Math.Round(Temperature, 2)}";
	}

	public struct SensorMeasurements
	{
		public double InitTemp;
		public List<double> Temperatures;
		public List<double> XFluxes;
		public List<double> YFluxes;
		public void Deconstruct(out List<double> temps, out List<double> xfs, out List<double> yfs)
		{
			temps = Temperatures;
			xfs = XFluxes;
			yfs = YFluxes;
		}
	}
}
