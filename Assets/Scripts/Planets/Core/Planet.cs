using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets.Core{

	public enum PlanetType{
		Solid,
		Gas
	}

	public class Planet : AstroBody {

		//Public
		public PlanetType planetType = PlanetType.Solid;

		public void Create()
		{
			astroType = AstroType.Planet;
			minRadius = 0.2f;
			maxRadius = 20.0f;

			this.SetScale();
		}
	}
}
