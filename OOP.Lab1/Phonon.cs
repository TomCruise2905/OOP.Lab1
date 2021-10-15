using OOP.Lab1.Geometry2D;
using System;

namespace OOP.Lab1.Particles
{
	public enum Polarization
	{
		LA,
		TA
	}

	public class Phonon : Particle
	{
		/// <summary>
		/// The sign of the phonon will determine whether it has a contributory or a 
		/// detracting effect on the system temperature/flux.
		/// </summary>
		public int Sign { get; private set; }
		public double Frequency { get; private set; }
		public Polarization Polarization { get; private set; }
		public bool Active { get; set; }
		public double DriftTime { get; set; }

		public Phonon(int sign)
		{
			SetSign(sign);
			Active = true;
		}

		public Phonon(int sign,
			double frequency,
			Polarization polarization,
			bool active,
			double driftTime,
			Point position,
			Vector direction,
			double speed) : base(position, direction, speed)
		{
			Sign = sign;
			Frequency = frequency;
			Polarization = polarization;
			Active = active;
			DriftTime = driftTime;
		}

		/// <summary>
		/// Set the sign of the phonon
		/// </summary>
		/// <param name="sign">The sign of the phonon. Must be 1 or -1</param>
		/// <exception cref="ArgumentOutOfRangeException">Throws if the sign is not 1 or -1</exception>
		public void SetSign(int sign)
		{
			if (sign == -1 || sign == 1)
				Sign = sign;
			else
				throw new ArgumentOutOfRangeException("Phonon Sign must be either -1 or 1.");
		}

		public override void Update(double frequency, double speed, Polarization pol)
		{
			Frequency = frequency;
			Speed = speed;
			Polarization = pol;
		}
		public override string ToString()
		{
			return $"Frequency: {Frequency}\n" +
				   $"Polarization: {Polarization}\n" +
				   base.ToString();
		}
	}
}
