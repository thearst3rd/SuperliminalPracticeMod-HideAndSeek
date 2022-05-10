using UnityEngine;

namespace SuperliminalPracticeMod
{

	class TeleportLocations
	{
		private static object[] TELEPORT_LOCATIONS = new object[]
		{
			null, // index 0, no level
			// 1 - Induction
			new object[]{},
			// 2 - Optical
			new object[]{},
			// 3 - Cubism
			new object[]
			{
				new object[] { new Vector3(-320.030f, -3.768f, -137.590f), new Vector3(-141.622f, 35.647f, -132.487f) },
			},
			// 4 - Blackout
			new object[]
			{
				new object[] { new Vector3(113.419f, -0.178f, -140.764f), new Vector3(81.170f, -0.178f, -101.321f) },
				new object[] { new Vector3(81.170f, -0.178f, -101.321f), new Vector3(113.419f, -0.178f, -140.764f) },
			},
			// 5 - Clone
			new object[]{},
			// 6 - Dollhouse
			new object[]
			{
				new object[] { new Vector3(-1269.308f, -11.395f, 1442.407f), new Vector3(667.486f, 20.478f, 1071.159f) },
			},
			// 7 - Labyrinth
			new object[]
			{
				new object[] { new Vector3(-45.105f, -15.866f, 236.990f), new Vector3(243.259f, 95.925f, -193.374f) },
				new object[] { new Vector3(243.259f, 95.925f, -193.374f), new Vector3(-45.105f, -15.866f, 236.990f) },
				new object[] { new Vector3(296.502f, -2.440f, -68.978f), new Vector3(297.150f, -14.481f, 27.686f) },
				new object[] { new Vector3(297.150f, -14.481f, 27.686f), new Vector3(296.502f, -2.440f, -68.978f) },
				new object[] { new Vector3(333.318f, -42.402f, 118.021f), new Vector3(359.914f, -40.295f, 116.453f) },
				new object[] { new Vector3(359.914f, -40.295f, 116.453f), new Vector3(333.318f, -42.402f, 118.021f) },
				new object[] { new Vector3(384.667f, -37.559f, 108.211f), new Vector3(482.517f, -45.392f, 119.006f) },
				new object[] { new Vector3(482.517f, -45.392f, 119.006f), new Vector3(384.667f, -37.559f, 108.211f) },
				new object[] { new Vector3(498.353f, -49.395f, 114.405f), new Vector3(628.927f, -44.766f, -38.486f) },
				new object[] { new Vector3(628.927f, -44.766f, -38.486f), new Vector3(498.489f, -45.392f, 116.986f) },
				new object[] { new Vector3(645.085f, -44.768f, -38.239f), new Vector3(638.325f, -41.118f, 95.586f) },
				new object[] { new Vector3(638.325f, -41.118f, 95.586f), new Vector3(645.085f, -44.768f, -38.239f) },
				new object[] { new Vector3(664.254f, -41.118f, 95.485f), new Vector3(661.586f, 58.705f, 974.756f) },
				new object[] { new Vector3(661.586f, 58.705f, 974.756f), new Vector3(664.254f, -41.118f, 95.485f) },
			},
			// 8 - Whitespace
			new object[]
			{
				new object[] { new Vector3(-185.904f, -13.574f, 181.048f), new Vector3(6.510f, 102.959f, -211.390f) },
				new object[] { new Vector3(6.510f, 102.959f, -211.390f), new Vector3(-185.904f, -13.574f, 181.048f) },
				new object[] { new Vector3(-440.026f, 102.951f, -47.577f), new Vector3(297.842f, -24.208f, -136.666f) },
				new object[] { new Vector3(297.842f, -24.208f, -136.666f), new Vector3(-440.026f, 102.951f, -47.577f) },
				new object[] { new Vector3(-592.214f, 102.959f, -1069.499f), new Vector3(-84.589f, -94.574f, -293.704f) },
				new object[] { new Vector3(-84.589f, -94.574f, -293.704f), new Vector3(-592.214f, 102.959f, -1069.499f) },
				new object[] { new Vector3(-233.158f, -158.267f, -289.999f), new Vector3(-312.779f, -177.317f, -462.453f) },
				new object[] { new Vector3(-312.779f, -177.317f, -462.453f), new Vector3(-233.158f, -158.267f, -289.999f) },
			},
		};

		public static float RADIUS = 3.0f;

		public static object[] GetTeleportLocations(int levelIndex)
		{
			if (levelIndex < 1 || levelIndex > 8)
				return new object[]{};
			return (object[]) TELEPORT_LOCATIONS[levelIndex];
		}
	}
}
