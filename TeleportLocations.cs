using UnityEngine;

namespace SuperliminalPracticeMod
{

	class TeleportLocations
	{
		private static object[] TELEPORT_LOCATIONS = new object[]
		{
			null, // index 0, no level
			// 1 - Induction
			new object[]
			{
				new object[] { new Vector3(-13.233f, 0.712f, 50.535f), new Vector3(84.555f, 9.210f, -221.000f) },
				new object[] { new Vector3(84.555f, 9.210f, -221.000f), new Vector3(-13.233f, 0.712f, 50.535f) },
			},
			// 2 - Optical
			new object[]{
				new object[] { new Vector3(-158.971f, 2.232f, -60.462f), new Vector3(140.456f, 50.368f, -731.923f) },
				new object[] { new Vector3(140.723f, 50.347f, -767.954f), new Vector3(-158.971f, 2.232f, -60.462f) },
			},
			// 3 - Cubism
			new object[]
			{
				new object[] { new Vector3(-57.293f, 21.287f, -40.468f), new Vector3(-320.538f, -3.910f, -113.134f) },
				new object[] { new Vector3(-320.538f, -3.910f, -113.134f), new Vector3(-57.293f, 21.287f, -40.468f) },
				new object[] { new Vector3(-320.030f, -3.768f, -137.590f), new Vector3(-141.622f, 35.647f, -132.487f) },
			},
			// 4 - Blackout
			new object[]
			{
				new object[] { new Vector3(171.129f, 0.667f, -107.289f), new Vector3(123.137f, 15.417f, 205.199f) },
				new object[] { new Vector3(123.137f, 15.417f, 205.199f), new Vector3(171.129f, 0.667f, -107.289f) },
				new object[] { new Vector3(113.419f, -0.178f, -140.764f), new Vector3(81.170f, -0.178f, -101.321f) },
				new object[] { new Vector3(81.170f, -0.178f, -101.321f), new Vector3(113.419f, -0.178f, -140.764f) },
			},
			// 5 - Clone
			new object[]{
				new object[] { new Vector3(300.227f, -7.763f, 74.889f), new Vector3(174.380f, 9.160f, -67.218f) },
				new object[] { new Vector3(174.380f, 9.160f, -67.218f), new Vector3(300.227f, -7.763f, 74.889f) },
			},
			// 6 - Dollhouse
			new object[]
			{
				new object[] { new Vector3(367.622f, 15.487f, 196.611f), new Vector3(-1257.135f, -10.995f, 1442.206f) },
				new object[] { new Vector3(-1257.135f, -11.395f, 1442.206f), new Vector3(367.622f, 15.487f, 196.611f) },
				new object[] { new Vector3(319.584f, -39.659f, 216.282f), new Vector3(319.564f, 14.634f, 219.083f) },
				new object[] { new Vector3(-1269.308f, -11.395f, 1442.407f), new Vector3(667.486f, 20.478f, 1071.159f) },
			},
			// 7 - Labyrinth
			new object[]
			{
				new object[] { new Vector3(-43.208f, -15.867f, 234.613f), new Vector3(667.385f, 59.769f, 974.911f) },
				new object[] { new Vector3(667.385f, 59.769f, 974.911f), new Vector3(-43.208f, -15.867f, 234.613f) },
				new object[] { new Vector3(-46.861f, -15.866f, 236.019f), new Vector3(239.054f, 95.865f, -193.254f) },
				new object[] { new Vector3(239.054f, 95.865f, -193.254f), new Vector3(-46.861f, -15.866f, 236.019f) },
				new object[] { new Vector3(289.242f, 89.785f, -189.421f), new Vector3(245.698f, 95.865f, -193.308f) },
				//new object[] { new Vector3(245.698f, 95.865f, -193.308f), new Vector3(289.242f, 89.785f, -189.421f) },
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

				// TODO, minor: figure out where these belong in above list
				new object[] { new Vector3(284.779f, 93.590f, -124.041f), new Vector3(198.620f, 137.802f, -131.359f) },
				new object[] { new Vector3(296.565f, -2.440f, -76.796f), new Vector3(296.730f, 90.925f, -77.032f) },
				new object[] { new Vector3(324.787f, -42.402f, 108.443f), new Vector3(322.035f, -9.074f, 109.398f) },
			},
			// 8 - Whitespace
			new object[]
			{
				new object[] { new Vector3(-195.760f, -12.766f, 180.820f), new Vector3(-312.789f, -177.325f, -469.847f) },
				new object[] { new Vector3(-312.789f, -177.325f, -469.847f), new Vector3(-195.760f, -12.766f, 180.820f) },
				new object[] { new Vector3(-185.904f, -13.574f, 181.048f), new Vector3(6.510f, 102.959f, -211.390f) },
				new object[] { new Vector3(6.510f, 102.959f, -211.390f), new Vector3(-185.904f, -13.574f, 181.048f) },
				new object[] { new Vector3(-440.026f, 102.951f, -47.577f), new Vector3(297.842f, -24.208f, -136.666f) },
				new object[] { new Vector3(297.842f, -24.208f, -136.666f), new Vector3(-440.026f, 102.951f, -47.577f) },
				//new object[] { new Vector3(-592.214f, 102.959f, -1069.499f), new Vector3(-84.589f, -94.574f, -293.704f) },
				new object[] { new Vector3(-84.589f, -94.574f, -293.704f), new Vector3(-592.214f, 102.959f, -1069.499f) },
				new object[] { new Vector3(-312.779f, -177.317f, -462.453f), new Vector3(-233.158f, -158.267f, -289.999f) },

				// TODO, minor: figure out where these belong in above list
				new object[] { new Vector3(-412.348f, -9.486f, -2863.766f), new Vector3(-412.348f, 104.116f, -2849.272f) },
				new object[] { new Vector3(42.843f, -71.138f, -218.566f), new Vector3(42.835f, -25.064f, -214.950f) },
				new object[] { new Vector3(-121.440f, -138.583f, -301.736f), new Vector3(-117.879f, -90.562f, -302.015f) },
				new object[] { new Vector3(-237.109f, -158.275f, -237.138f), new Vector3(-194.474f, -102.280f, -245.970f) },
				//new object[] { new Vector3(-194.474f, -102.280f, -245.970f), new Vector3(-237.109f, -158.275f, -237.138f) },
			},
		};

		public static float RADIUS = 2.0f;

		public static object[] GetTeleportLocations(int levelIndex)
		{
			if (levelIndex < 1 || levelIndex > 8)
				return new object[]{};
			return (object[]) TELEPORT_LOCATIONS[levelIndex];
		}
	}
}
