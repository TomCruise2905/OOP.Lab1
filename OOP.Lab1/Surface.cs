
using OOP.Lab1.Particles;

namespace OOP.Lab1.ModelComponents
{
	public interface ISurface
	{
		Cell HandlePhonon(Phonon p);
	}

	public class BoundarySurface : ISurface
	{
		public SurfaceLocation Location { get; }
		protected Cell cell;

		public BoundarySurface(SurfaceLocation location, Cell cell)
		{
			Location = location;
			this.cell = cell;
		}
		public virtual Cell HandlePhonon(Phonon p)
		{
			// Only need to account for perfectly specular reflection
			// TODO: Alter the phonon direction vector as though it just collided with a mirror
			return cell;
		}

		public void RedirectPhonon()
        {

        }
	}
}
