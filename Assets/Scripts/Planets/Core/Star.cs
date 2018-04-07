using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets.Core{
	public class Star : AstroBody {

		public void Create()
		{
			astroType = AstroType.Star;
			minRadius = 50.0f;
			maxRadius = 150.0f;

			this.SetScale();
		}
	}
}
