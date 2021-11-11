﻿/* Lab Question  (Test 2)
 * 
 * A bottom-up approach is typically used in OOP languages. This is the general design approach that has been used
 * for this software. Do you think it would have been easier or harder to approach this project using a top-down approach?
 * Why do you think a bottom up approach is generally more natural when using OOP languages?
 *
 */

using System;
using System.Collections.Generic;

using OOP.Lab1.ModelComponents;
using OOP.Lab1.Materials;

namespace OOP.Lab1
{
	// Model is comprised of a single material. Hardcoding the time step & number of phonons for now.
	class Model
	{
		private const double TIME_STEP = 5e-12;
		private const int NUM_PHONONS = 10000000;
		private Material material;
		private List<Cell> cells = new List<Cell>() { };
		private List<Sensor> sensors = new List<Sensor>() { };
		private readonly double highTemp;
		private readonly double lowTemp;
		private readonly double simTime;
		private readonly double tEq;

		public Model(Material material, double highTemp, double lowTemp, double simTime)
		{
			this.material = material;
			this.highTemp = highTemp;
			this.lowTemp = lowTemp;
			this.simTime = simTime;
			tEq = (highTemp + lowTemp) / 2;
		}
		//to add sensor
		public void AddSensor(int sensorID, double initTemp)
		{
			foreach (Sensor sensor in sensors)
			{
				if (sensor.ID == sensorID)
					throw new ArgumentException($"Sensor Id: {sensorID} is not unique.");

			}
			sensors.Add(new Sensor(sensorID, material, initTemp));
		}
		public void AddSensor(double initTemp)
		{

		}

		// to add cell
		public void AddCell(double length, double width, int sensorID)
		{

			if (cells.Count > 0)
			{
				if (cells[cells.Count - 1].Length != length || cells[cells.Count - 1].Width != width)
					throw new ArgumentException($"cell dimention does not match the following length: {cells[cells.Count - 1].Length}, width: {cells[cells.Count - 1].Width}");
			}
			foreach (var sensor in sensors)
			{
				if (sensor.ID == sensorID)
				{

					cells.Add(new Cell(length, width, sensor));
					sensor.AddToArea(cells[^1].Area);
					return;
				}

			}
			throw new ArgumentException($"Sensor Id: {sensorID} does not exist in the model.");
		}
		

		/// <summary>
		/// Automatically sets all the surfaces in the cells that constitute this model.
		/// Should be called after all the cells have been added
		/// </summary>
		/// <param name="tEq">The equilibrium temperature of the system</param>
		public void SetSurfaces(double tEq)
		{
			int numCells = cells.Count;
			if (numCells < 2)
			{
				throw new InvalidCellCount();
			}

			cells[0].SetEmitSurface(SurfaceLocation.left, highTemp);
			cells[0].SetTransitionSurface(SurfaceLocation.right, cells[1]);
			for (int i = 1; i < numCells - 1; ++i)
			{
				cells[i].SetTransitionSurface(SurfaceLocation.left, cells[i - 1]);
				cells[i].SetTransitionSurface(SurfaceLocation.right, cells[i + 1]);
			}
			cells[^1].SetEmitSurface(SurfaceLocation.right, lowTemp);
			cells[^1].SetTransitionSurface(SurfaceLocation.left, cells[numCells - 2]);

		}

		/// <summary>
		/// Calibrates the emitting surfaces in the model.
		/// </summary>
		/// <param name="tEq">System equilibrium temperature</param>
		/// <param name="effEnergy">Phonon packet effective energy</param>
		/// <param name="timeStep">Simulation time step</param>
		public void SetEmitPhonons(double tEq, double effEnergy, double timeStep)
		{
			foreach (var cell in cells)
			{
				cell.SetEmitPhonons(tEq, effEnergy, timeStep);
			}
		}

		/// <summary>
		/// Returns the total energy of the model (initial energy + emit energy)
		/// </summary>
		/// <returns>Total energy generated by the model over the course of the simulation</returns>
		private double GetTotalEnergy()
		{
			double emitEnergy = 0;
			foreach (var cell in cells)
			{
				emitEnergy += cell.EmitEnergy(tEq, simTime) + cell.InitEnergy(tEq);
			}
			return emitEnergy;


		}
		//foe invalid cell count
		class InvalidCellCount : Exception
		{
			public InvalidCellCount() { }
			public InvalidCellCount(string description = "") : base(String.Format("Invalid cell count {0}", description)) { }
		}
		public override string ToString()
		{
			string res = "";
			res += $"model total energy: {GetTotalEnergy()}\n";
			foreach (var cell in cells)
			{
				res += cell.ToString() + $"  {cell.TotalEmitPhonons()}" +'\n' ;
			}
			return res;
		}
	}
}
