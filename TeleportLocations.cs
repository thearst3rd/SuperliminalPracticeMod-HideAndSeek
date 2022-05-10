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
				new object[] { new Vector3(-320f, -3.76f, -137.6f), new Vector3(-143.2f, 35.65f, -132f) },
			},
			// 4 - Blackout
			new object[]
			{
				new object[] { new Vector3(81.2f, 0f, -100f), new Vector3(115f, 0f, -140.7f) },
				new object[] { new Vector3(97f, 0f, -140.7f), new Vector3(81.2f, 0f, -100f) },
			},
			// 5 - Clone
			new object[]{},
			// 6 - Dollhouse
			new object[]
			{
				new object[] { new Vector3(-1972.0f, -444.0f, -190.0f), new Vector3(-36.0f, -412.9f, -561.0f) },
			},
			// 7 - Labyrinth
			new object[]
			{
				new object[] { new Vector3(-1.0f, 0.0f, 0.6f), new Vector3(-288.5f, -112.1f, 431f) },
				new object[] { new Vector3(-288.5f, -112.1f, 431f), new Vector3(-1.0f, 0.0f, 0.6f) },
				new object[] { new Vector3(53.7f, -110.6f, 220.0f) , new Vector3(53.2f, -98.6f, 125.3f) },
				new object[] { new Vector3(53.2f, -98.6f, 125.3f), new Vector3(53.7f, -110.6f, 220.0f) },
				new object[] { new Vector3(87.7f, -138.7f, 308.5f), new Vector3(116.4f, -136.5f, 310.4f) },
				new object[] { new Vector3(116.4f, -136.5f, 310.4f), new Vector3(87.7f, -138.7f, 308.5f) },
				new object[] { new Vector3(239.1f, -141.7f, 313.0f) , new Vector3(141.0f, -133.7f, 302.5f) },
				new object[] { new Vector3(141.0f, -133.7f, 302.5f), new Vector3(239.1f, -141.7f, 313.0f) },
				new object[] { new Vector3(385.5f, -141.0f, 155.5f), new Vector3(254.9f, -141.6f, 310.9f) },
				new object[] { new Vector3(255.0f, -145.7f, 308.0f), new Vector3(385.5f, -141.0f, 155.5f) },
				new object[] { new Vector3(394.7f, -137.3f, 289.7f), new Vector3(401.5f, -141.0f, 155.7f) },
				new object[] { new Vector3(401.5f, -141.0f, 155.7f), new Vector3(394.7f, -137.3f, 289.7f) },
				new object[] { new Vector3(421.2f, -37.0f, 1168.0f), new Vector3(421.0f, -137.3f, 289.5f) },
				new object[] { new Vector3(421.0f, -137.3f, 289.5f), new Vector3(421.2f, -37.0f, 1168.0f) },
			},
			// 8 - Whitespace
			new object[]
			{
				new object[] { new Vector3(6.5f, 103.0f, -211.4f), new Vector3(-185.7f, -13.4f, 180.9f) },
				new object[] { new Vector3(-185.7f, -13.4f, 180.9f), new Vector3(6.5f, 103.0f, -211.4f) },
				new object[] { new Vector3(297.8f, -24.0f, -136.0f), new Vector3(-437.3f, 103.0f, -63.6f) },
				new object[] { new Vector3(-437.3f, 103.0f, -63.6f), new Vector3(297.8f, -24.0f, -136.0f) },
				new object[] { new Vector3(-73.0f, -74.5f, -243.1f), new Vector3(-53.8f, -74.5f, -243.2f) },
				new object[] { new Vector3(-84.6f, -94.5f, -293.7f), new Vector3(-592.0f, 103.0f, -1069.1f) },
				new object[] { new Vector3(-237.1f, -158.2f, -242.9f), new Vector3(-237.1f, -158.2f, -239.0f) },
				new object[] { new Vector3(-312.5f, -177.3f, -461.5f), new Vector3(-233.1f, -158.2f, -289.9f) },
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
