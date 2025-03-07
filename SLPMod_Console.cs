﻿using Rewired;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;
using UnityEngine.SceneManagement;

namespace SuperliminalPracticeMod
{
	class SLPMod_Console : MonoBehaviour
	{
		public static SLPMod_Console instance;
		public bool active;
		string input;

		Vector3 teleport1;
		Vector3 teleport2;
		

		private void Awake()
		{
			instance = this;
			active = false;
			input = "";
		}
		
		private void OnGUI()
		{
			if (GameManager.GM.player != false && PracticeModManager.Instance.pauseMenu.isInMenu == false)
				GameManager.GM.PM.canControl = !active;

			if (!active)
				return;

			float y = 0f;

			if (Event.current.type == EventType.KeyDown && Event.current.character == '\n')
			{
				ParseCommand(input);
				input = "";
				active = false;
				return;
			}

			if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.F11 || Event.current.keyCode == KeyCode.Escape)
			{
				active = false;
				return;
			}

			GUI.Box(new Rect(0, y, Screen.width, 30), "");
			GUI.backgroundColor = new Color(0, 0, 0, 0);
			GUI.SetNextControlName("SLP_Console");
			input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
			GUI.FocusControl("SLP_Console");
		}

		private void ParseCommand(string command)
		{
			MelonLogger.Log("Trying to parse \"" + command + "\"");
			string[] commandArray = command.Split(' ');
			if (commandArray[0].ToLower() == "teleport" && commandArray.Length >= 4)
			{
				float x, y, z;
				if (!float.TryParse(commandArray[1], out x) || !float.TryParse(commandArray[2], out y) || !float.TryParse(commandArray[3], out z))
					return;
				MelonLogger.Log("Trying to teleport to " + x + ", " + y + ", " + z);
				PracticeModManager.Instance.Teleport(new Vector3(x, y, z));
			}
			else if (commandArray[0].ToLower() == "scale" && commandArray.Length >= 2)
			{
				float newScale;
				if (float.TryParse(commandArray[1], out newScale))
					PracticeModManager.Instance.Scale(Math.Abs(newScale));
			}
			else if (commandArray[0].ToLower() == "load" && commandArray.Length >= 2)
			{
				int sceneIndex;
				if (!int.TryParse(commandArray[1], out sceneIndex))
					return;
				if (sceneIndex >= 0)
				{
					SceneManager.LoadScene(sceneIndex % SceneManager.sceneCountInBuildSettings);
				}
			}
			else if (commandArray[0].ToLower() == "noclip")
			{
				PracticeModManager.Instance.noClip = !PracticeModManager.Instance.noClip;
			}
			else if (commandArray[0].ToLower() == "showtriggers")
			{
				PracticeModManager.Instance.ToggleTriggerVisibility();
			}
			else if (commandArray[0].ToLower() == "mousemin" && commandArray.Length >= 2)
			{
				float mouseMinY;
				MelonLogger.Log("test");
				if (float.TryParse(commandArray[1], out mouseMinY))
				{
					MelonLogger.Log(mouseMinY.ToString());
					PracticeModManager.Instance.SetMouseMinY(mouseMinY);
				}
			}
			else if (commandArray[0].ToLower() == "showmoreinfo")
			{
				PracticeModManager.Instance.showMoreInfo = !PracticeModManager.Instance.showMoreInfo;
			}
			else if (commandArray[0].ToLower() == "resethidingtime")
			{
				PracticeModManager.Instance.ResetHidingTime();
			}
			else if (commandArray[0].ToLower() == "sethidingtime")
			{
				if (commandArray.Length == 2)
				{
					float seconds = float.Parse(commandArray[1]);
					PracticeModManager.Instance.SetHidingTime(seconds);
				}
				else if (commandArray.Length == 3)
				{
					int minutes = int.Parse(commandArray[1]);
					float seconds = float.Parse(commandArray[2]);
					if (minutes < 0)
						seconds = -seconds;
					seconds += 60 * minutes;
					PracticeModManager.Instance.SetHidingTime(seconds);
				}
				else
				{
					MelonLogger.Log("Expected 1 or 2 args");
					MelonLogger.Log("Usage: " + commandArray[0] + " [<minutes>] <seconds>");
				}
			}
			// For ease of creating teleport locations
			else if (commandArray[0].ToLower() == "settl1")
			{
				teleport1 = PracticeModManager.Instance.playerMotor.transform.position;
			}
			else if (commandArray[0].ToLower() == "settl2")
			{
				teleport2 = PracticeModManager.Instance.playerMotor.transform.position;

				string template = "\nnew TeleportLocation(new Vector3({0:F3}f, {1:F3}f, {2:F3}f), new Vector3({3:F3}f, {4:F3}f, {5:F3}f)),";
				string output = "";
				output += string.Format(template,
						teleport1.x, teleport1.y, teleport1.z,
						teleport2.x, teleport2.y, teleport2.z);
				output += string.Format(template,
						teleport2.x, teleport2.y, teleport2.z,
						teleport1.x, teleport1.y, teleport1.z);
				MelonLogger.Log(output);
			}
		}

		public void Toggle()
		{
			active = !active;
		}

	}
}
