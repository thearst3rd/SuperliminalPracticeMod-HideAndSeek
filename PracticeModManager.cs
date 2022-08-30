using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperliminalPracticeMod
{
	class PracticeModManager : MonoBehaviour
	{
		public static PracticeModManager Instance;
		public bool noClip;
		public float noClipSpeed;
		public float defaultFarClipPlane;
		public GameObject player;
		public GameObject flashLight;
		public CharacterMotor playerMotor;
		public Camera playerCamera;
		public ResizeScript resizeScript;
		public Text playerText;
		public Text grabbedObjectText;
		public PauseMenu pauseMenu;
		public MouseLook mouseLook;

		public bool showMoreInfo;

		Vector3 storedPosition;
		Quaternion storedCapsuleRotation;
		float storedCameraRotation;
		float storedScale;
		int storedMap;
		float storeTime;
		float teleportTime;
		bool unlimitedRenderDistance;
		bool debugFunctions;
		bool triggersVisible;
		List<GameObject> triggerGameObjects;

		bool isHider;
		float hidingTime;
		bool namesVisible;
		bool localPlayerGrabbed;
		bool localPlayerCloned;

		object[] teleportLocations;
		bool canTeleport;
		List<GameObject> teleportGameObjects;

		float f7HoldTime;


		void Awake()
		{
			PracticeModManager.Instance = this;
			noClip = false;
			unlimitedRenderDistance = false;
			noClipSpeed = 10.0f;
			defaultFarClipPlane = 999f;

			showMoreInfo = false;

			storedPosition = Vector3.zero;
			storedCapsuleRotation = Quaternion.identity;
			storedCameraRotation = 0;
			storedScale = 1.0f;
			storedMap = -1;
			GameManager.GM.enableDebugFunctions = true;
			debugFunctions = false;
			GameManager.GM.GetComponent<LevelInformation>().LevelInfo.RandomLoadingScreens = new SceneReference[1] { GameManager.GM.GetComponent<LevelInformation>().LevelInfo.NormalLoadingScreen };
			base.gameObject.AddComponent<SLPMod_Console>();

			isHider = false;
			hidingTime = 0.0f;
			namesVisible = true;
			localPlayerGrabbed = false;
			localPlayerCloned = false;

			f7HoldTime = 0.0f;
		}

		private void OnLevelWasLoaded(int level)
		{
			triggerGameObjects = new List<GameObject>();
			triggersVisible = false;

			LevelInfo levelInfo = GameManager.GM.GetComponent<LevelInformation>().LevelInfo;

			int currentLevelIndex = levelInfo.GetLevelIndex(levelInfo.GetCurrentSceneSaveName());
			teleportLocations = TeleportLocations.GetTeleportLocations(currentLevelIndex);
			canTeleport = false;

			teleportGameObjects = new List<GameObject>();
			foreach (object[] teleportLocation in teleportLocations)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				gameObject.transform.position = (Vector3)teleportLocation[0];
				gameObject.transform.localScale = (2 * TeleportLocations.RADIUS) * Vector3.one;
				gameObject.GetComponent<SphereCollider>().enabled = false;
				MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
				component.material.shader = Shader.Find("Transparent/Diffuse");
				component.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.3f);

				teleportGameObjects.Add(gameObject);
			}
		}

		public void AddTriggerGO(GameObject go)
		{
			triggerGameObjects.Add(go);
		}

		public void ToggleTriggerVisibility()
		{
			if (GameManager.GM.player == null)
				return;

			triggersVisible = !triggersVisible;

			foreach (GameObject gameObject in triggerGameObjects)
			{
				gameObject.SetActive(triggersVisible);
			}

		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.F11))
				SLPMod_Console.instance.Toggle();

			if (Input.GetKeyDown(KeyCode.F12))
			{
				Transform performanceGraph = GameManager.GM.transform.Find("[Graphy]");
				if (performanceGraph != null)
					performanceGraph.gameObject.SetActive(!performanceGraph.gameObject.activeSelf);
			}

			GameManager.GM.enableDebugFunctions = debugFunctions;

			if (GameManager.GM.player == null)
				return;

			if (player != GameManager.GM.player)
			{
				player = GameManager.GM.player;
				playerMotor = player.GetComponent<CharacterMotor>();
				playerCamera = player.GetComponentInChildren<Camera>();
				mouseLook = playerCamera.GetComponent<MouseLook>();
				resizeScript = playerCamera.GetComponent<ResizeScript>();
				pauseMenu = GameObject.Find("UI_PAUSE_MENU").GetComponentInChildren<PauseMenu>(true);
				defaultFarClipPlane = playerCamera.farClipPlane;
				if(player.transform.Find("Flashlight") == null)
				{
					flashLight = new GameObject("Flashlight");
					flashLight.SetActive(false);
					this.flashLight.transform.parent = player.transform;
					this.flashLight.transform.localPosition = new Vector3(0f, playerCamera.transform.localPosition.y, 0f);
					Light light = this.flashLight.AddComponent<Light>();
					light.range = 10000f;
					light.intensity = 0.5f;
				}
				else
				{
					flashLight = player.transform.Find("Flashlight").gameObject;
				}

				if (GameObject.Find("PlayerText") == null && GameObject.Find("UI_PAUSE_MENU") != null)
				{
					playerText = NewPlayerText();
				}

				if (GameObject.Find("GrabbedObjectText") == null && GameObject.Find("UI_PAUSE_MENU") != null)
				{
					grabbedObjectText = NewGrabbedObjectText();
				}

				SLPMod_Console.instance.active = false;
			}

			if (Input.GetKeyDown(KeyCode.K))
			{
				noClip = !noClip;
				noClipSpeed = 10.0f;
			}

			playerMotor.enabled = !noClip;

			if (Input.GetKeyDown(KeyCode.F))
			{
				flashLight.gameObject.SetActive(!flashLight.gameObject.activeSelf);
			}

			if (noClip)
			{
				this.noClipSpeed += Input.mouseScrollDelta.y;
				this.noClipSpeed = Mathf.Max(0f, this.noClipSpeed);
				Vector3 directionVector = new Vector3(GameManager.GM.playerInput.GetAxisRaw("Move Horizontal"), 0f, GameManager.GM.playerInput.GetAxisRaw("Move Vertical"));
				if (Input.GetKey(KeyCode.Space))
				{
					directionVector.y += 1f;
				}
				if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl))
				{
					directionVector.y -= 1f;
				}
				playerMotor.transform.Translate(directionVector.normalized * Time.deltaTime * this.noClipSpeed);
				playerCamera.cullingMatrix = new Matrix4x4(Vector4.positiveInfinity, Vector4.positiveInfinity, Vector4.positiveInfinity, Vector4.positiveInfinity);
			}
			
			if(noClip || unlimitedRenderDistance)
			{
				playerCamera.cullingMatrix = new Matrix4x4(Vector4.positiveInfinity, Vector4.positiveInfinity, Vector4.positiveInfinity, Vector4.positiveInfinity);
				playerCamera.GetComponent<CameraSettingsLayer>().enabled = false;
				playerCamera.farClipPlane = 10000f;
			}
			else
			{

				playerCamera.GetComponent<CameraSettingsLayer>().enabled = true;
				playerCamera.ResetCullingMatrix();
			}

			if(playerText != null)
			{
				playerText.text = GetPlayerTextString();
			}

			if(grabbedObjectText != null)
			{
				grabbedObjectText.text = GetGrabbedObjectTextString();
			}

			if (Input.GetKey(KeyCode.F1))
			{
				float newScale = playerMotor.transform.localScale.x + Time.deltaTime * playerMotor.transform.localScale.x;
				Scale(newScale);
			}
			
			if (Input.GetKey(KeyCode.F2))
			{
				float newScale = playerMotor.transform.localScale.x - Time.deltaTime * playerMotor.transform.localScale.x;
				Scale(newScale);
			}

			if (Input.GetKeyDown(KeyCode.F3))
			{
				Scale(1);
			}

			if (Input.GetKeyDown(KeyCode.F4) && !noClip)
				unlimitedRenderDistance = !unlimitedRenderDistance;

			if (Input.GetKeyDown(KeyCode.F5))
				StorePosition();

			if (Input.GetKeyDown(KeyCode.F6))
				TeleportPosition();

			if (Input.GetKeyDown(KeyCode.F7))
				isHider = !isHider;

			if (Input.GetKey(KeyCode.F7))
			{
				f7HoldTime += Time.deltaTime;
				if (f7HoldTime > 3.0f)
					ResetHidingTime();
			}
			else
			{
				f7HoldTime = 0.0f;
			}

			if(Input.GetKeyDown(KeyCode.F9))
			{
				debugFunctions = !debugFunctions;
				GameManager.GM.enableDebugFunctions = debugFunctions;
			}

			if(resizeScript.isGrabbing && Input.GetKey(KeyCode.LeftShift))
			{
				resizeScript.ScaleObject(1f + (Input.mouseScrollDelta.y * 0.05f));
			}

			if (Input.GetKeyDown(KeyCode.F10))
				namesVisible = !namesVisible;

			MultiplayerMode multiplayerMode = GameManager.GM.GetComponent<MultiplayerMode>();
			List<GameObject> playerObjects = multiplayerMode.GetPlayerObjects();

			localPlayerGrabbed = false;
			localPlayerCloned = false;
			foreach (GameObject playerObject in playerObjects)
			{
				MultiplayerPlayer multiplayerPlayer = playerObject.GetComponent<MultiplayerPlayer>();
				TMPro.TextMeshPro textMesh = multiplayerPlayer.TextMesh;
				textMesh.enabled = namesVisible;

				if (multiplayerPlayer.IsMainLocalPlayer)
				{
					Photon.Realtime.Player owner = multiplayerPlayer.photonView.Owner;
					if (multiplayerMode.GetPlayerObjectForPlayer(owner) != multiplayerPlayer.gameObject)
						localPlayerGrabbed = true;
				}
				else
				{
					if (multiplayerPlayer.IsLocalPlayerOrCloneOf())
						localPlayerCloned = true;
				}
			}

			if (localPlayerGrabbed || localPlayerCloned)
				isHider = false;

			if (isHider)
				hidingTime += Time.deltaTime;

			canTeleport = false;
			Vector3 playerPosition = playerMotor.transform.localPosition;
			Vector3 teleportDestination = Vector3.zero;
			foreach (object[] location in teleportLocations)
			{
				float dist = (playerPosition - (Vector3)location[0]).magnitude;
				if (dist < TeleportLocations.RADIUS)
				{
					canTeleport = true;
					teleportDestination = (Vector3)location[1];
					break;
				}
			}

			if (canTeleport && Input.GetKeyDown(KeyCode.F8))
				playerMotor.transform.localPosition = teleportDestination;
		}

		public void SetMouseMinY(float mouseMinY)
		{
			if (GameManager.GM.player != null && GameManager.GM.playerCamera.GetComponent<MouseLook>() != null)
				GameManager.GM.playerCamera.GetComponent<MouseLook>().minimumY = (mouseMinY);
		}

		Text NewPlayerText()
		{
			Text newText;
			GameObject gameObject = new GameObject("PlayerText");
			gameObject.transform.parent = GameObject.Find("UI_PAUSE_MENU").transform.Find("Canvas");
			CanvasGroup cg = gameObject.AddComponent<CanvasGroup>();
			cg.interactable = false;
			cg.blocksRaycasts = false;
			newText = gameObject.AddComponent<Text>();
			RectTransform component = newText.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2((float)(Screen.currentResolution.width / 3), (float)(Screen.currentResolution.height / 3));
			component.pivot = new Vector2(0f, 1f);
			component.anchorMin = new Vector2(0f, 1f);
			component.anchorMax = new Vector2(0f, 1f);
			component.anchoredPosition = new Vector2(25f, -25f);
			foreach (Font font in Resources.FindObjectsOfTypeAll<Font>())
			{
				if (font.name == "BebasNeue Bold")
				{
					newText.font = font;
				}
			}
			newText.text = "hello world";
			newText.fontSize = 30;

			return newText;
		}

		Text NewGrabbedObjectText()
		{
			Text newText;
			GameObject gameObject = new GameObject("GrabbedObjectText");
			gameObject.transform.parent = GameObject.Find("UI_PAUSE_MENU").transform.Find("Canvas");
			CanvasGroup cg = gameObject.AddComponent<CanvasGroup>();
			cg.interactable = false;
			cg.blocksRaycasts = false;
			newText = gameObject.AddComponent<Text>();
			RectTransform component = newText.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2((float)(Screen.currentResolution.width / 3), (float)(Screen.currentResolution.height / 3));
			component.pivot = new Vector2(0f, .5f);
			component.anchorMin = new Vector2(0f, .5f);
			component.anchorMax = new Vector2(0f, .5f);
			component.anchoredPosition = new Vector2(25f, -25f);
			foreach (Font font in Resources.FindObjectsOfTypeAll<Font>())
			{
				if (font.name == "BebasNeue Bold")
				{
					newText.font = font;
				}
			}
			newText.text = "hello world";
			newText.fontSize = 30;

			return newText;
		}

		string GetPlayerTextString()
		{
			Vector3 position = playerMotor.transform.position;
			Vector3 velocity = playerMotor.GetComponent<CharacterController>().velocity;
			Vector3 rotation = playerMotor.transform.rotation.eulerAngles;
			float scale = playerMotor.transform.localScale.x;

			string hiderInfo = "";

			if (isHider)
				hiderInfo += "\nYou are a HIDER";
			else
				hiderInfo += "\nYou are a SEEKER";

			hiderInfo += "\nTime spent hiding: ";
			if (hidingTime < 0)
			{
				float seconds = Math.Abs((float)Math.Ceiling(10 * (hidingTime % 60.0f)) / 10.0f);
				int minutes = Math.Abs((int)Math.Ceiling(hidingTime / 60));
				hiderInfo += "-" + minutes.ToString("0") + ":";
				hiderInfo += seconds.ToString("00.0");
			}
			else
			{
				float seconds = (float)Math.Floor(10 * (hidingTime % 60.0f)) / 10.0f;
				int minutes = (int)Math.Floor(hidingTime / 60);
				hiderInfo += minutes.ToString("0") + ":";
				hiderInfo += seconds.ToString("00.0");
			}

			if (localPlayerGrabbed || localPlayerCloned)
			{
				hiderInfo += "\n";
				if (localPlayerGrabbed)
					hiderInfo += "\nYou're grabbed!";
				if (localPlayerCloned)
					hiderInfo += "\nYou're cloned!";
			}

			string dynamicInfo = "";

			if (debugFunctions)
				dynamicInfo += "\nDebug Functions";

			if (triggersVisible)
				dynamicInfo += "\nTriggers Visible";

			if (noClip)
				dynamicInfo += "\nNoClip";

			if (!noClip && unlimitedRenderDistance)
				dynamicInfo += "\nUnlimited Render Distance";

			if (flashLight.activeSelf)
				dynamicInfo += "\nFlashlight";

			if (Time.time - this.storeTime <= 1f)
				dynamicInfo += "\nPosition Stored";

			if (Time.time - this.teleportTime <= 1f)
				dynamicInfo += "\nTeleport";

			if (!namesVisible)
				dynamicInfo += "\nNames Hidden";

			if (canTeleport)
				dynamicInfo += "\nPress F8 to Teleport";

			String output = "Superliminal Hide and Seek\n\n";

			if (showMoreInfo)
			{
				output = string.Concat(new object[]
				{
					output,
					"Position: ",
					position.x.ToString("0.000"),
					", ",
					position.y.ToString("0.000"),
					", ",
					position.z.ToString("0.000"),
					"\n",
					"Rotation: ",
					playerCamera.transform.rotation.eulerAngles.x.ToString("0.000"),
					", ",
					rotation.y.ToString("0.000"),
					"\n",
				});
			}

			output = string.Concat(new object[]
			{
				output,
				"Scale: ",
				scale.ToString("0.0000")+"x",
				"\n",
			});

			if (showMoreInfo)
			{
				output = string.Concat(new object[]
				{
					output,
					"Horizontal Velocity: ",
					Mathf.Sqrt(velocity.x * velocity.x + velocity.z * velocity.z).ToString("0.000")+" m/s",
					"\n",
					"Vertical Velocity: ",
					velocity.y.ToString("0.000")+" m/s",
					"\n"
				});
			}

			return string.Concat(new object[]
			{
				output,
				hiderInfo,
				"\n",
				dynamicInfo
			});
		}

		string GetGrabbedObjectTextString()
		{
			if(showMoreInfo && resizeScript.isGrabbing && resizeScript.GetGrabbedObject() != null)
			{
				GameObject grabbedObject = resizeScript.GetGrabbedObject();
				string output = string.Concat(new object[]{
					grabbedObject.name+"\n",
					"Position: "+grabbedObject.transform.position.x.ToString("0.000")+", "+grabbedObject.transform.position.y.ToString("0.000")+", "+grabbedObject.transform.position.z.ToString("0.000")+"\n",
					"Scale: "+grabbedObject.transform.localScale.x.ToString("0.0000")+"x" 
				});
				if(grabbedObject.GetComponent<Collider>() != null)
				{
					Collider playerCollider = player.GetComponent<Collider>();
					Collider objectCollider = grabbedObject.GetComponent<Collider>();
					if(
						Physics.ComputePenetration(playerCollider, playerCollider.transform.position, playerCollider.transform.rotation, 
							objectCollider, objectCollider.transform.position, objectCollider.transform.rotation, 
							out Vector3 direction, out float distance))
					{
						Vector3 warpPrediction = player.transform.position + direction * distance;
						if (distance > 5)
						{
							output += "\nWarp Prediction: " + warpPrediction.x.ToString("0.000") + ", " + warpPrediction.y.ToString("0.000") + ", " + warpPrediction.z.ToString("0.000");
							output += "\nWarp Distance: " + distance.ToString("0.000");
						}
					}
					
				}

				return output;
			}
			else
			{
				return "";
			}
		}

		void StorePosition()
		{
			storedPosition = playerMotor.transform.position;
			storedCapsuleRotation = playerMotor.transform.rotation;
			storedCameraRotation = mouseLook.rotationY;
			storedScale = playerMotor.transform.localScale.x;
			storedMap = SceneManager.GetActiveScene().buildIndex;
			storeTime = Time.time;
		}

		void TeleportPosition()
		{
			if(storedMap == SceneManager.GetActiveScene().buildIndex)
			{
				playerMotor.transform.position = storedPosition;
				playerMotor.transform.rotation = storedCapsuleRotation;
				mouseLook.SetRotationY(storedCameraRotation);
				Scale(storedScale);
				teleportTime = Time.time;
			}
		}

		void ReloadCheckpoint()
		{
			GameManager.GM.TriggerScenePreUnload();
			GameManager.GM.GetComponent<SaveAndCheckpointManager>().ResetToLastCheckpoint();
		}

		void RestartMap()
		{
			GameManager.GM.TriggerScenePreUnload();
			GameManager.GM.GetComponent<SaveAndCheckpointManager>().RestartLevel();
		}

		public void Teleport(Vector3 position)
		{
			if (GameManager.GM.player == null)
				return;

			playerMotor.transform.position = position;
		}

		public void Scale(float newScale)
		{
			if (GameManager.GM.player != null && newScale > 0.0001f)
			{
				playerMotor.transform.localScale = new Vector3(newScale, newScale, newScale);
				PlayerResizer playerResizer = GameManager.GM.player.GetComponent<PlayerResizer>();
				playerResizer.MultiplayerReturnToSize = newScale;
				playerResizer.Poke();
			}
		}

		public void ResetHidingTime()
		{
			isHider = false;
			hidingTime = 0.0f;
		}

		public void SetHidingTime(float newHidingTime)
		{
			hidingTime = newHidingTime;
		}
	}
}
