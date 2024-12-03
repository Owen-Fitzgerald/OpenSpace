using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using Geraldine.OpenSpaceGame;
using Geraldine.OpenSpaceUI.Interfaces;
using TMPro;
using Geraldine._4XEngine.GraphTree;
using Geraldine._4XEngine.GraphTree.Interfaces;

namespace Geraldine.OpenSpaceUI.Behaviours
{
	public enum enLoadSaveMode
	{
		LoadMap,
		SaveMap,
		LoadGame,
		SaveGame
	}

	/// <summary>
	/// Component that applies actions from the save-load menu UI to the hex map.
	/// Public methods are hooked up to the in-game UI.
	/// </summary>
	public class SaveLoadMenu : MonoBehaviour, ISelectItemFromList
    {

		[SerializeField]
		TMP_Text menuLabel, actionButtonLabel;

		[SerializeField]
		InputField nameInput;

		[SerializeField]
		RectTransform listContent;

		[SerializeField]
		SaveLoadItem itemPrefab;

		[SerializeField]
        INodeGraph<INode> nodeGraph;

		[SerializeField]
		GameControlBehaviour gameControl;

        [SerializeField]
        enLoadSaveMode loadSaveMode;


        public void Open(enLoadSaveMode loadSaveMode)
		{
			this.loadSaveMode = loadSaveMode;

			switch(loadSaveMode)
			{
				case enLoadSaveMode.LoadMap:
                    menuLabel.text = "Load Map";
                    actionButtonLabel.text = "Load";
                    break;
				case enLoadSaveMode.SaveMap:
                    menuLabel.text = "Save Map";
                    actionButtonLabel.text = "Save";
                    break;
				case enLoadSaveMode.LoadGame:
                    menuLabel.text = "Load Game";
                    actionButtonLabel.text = "Load";
                    break;
				case enLoadSaveMode.SaveGame:
                    menuLabel.text = "Save Game";
                    actionButtonLabel.text = "Save";
                    break;
			}

			FillList();
			gameObject.SetActive(true);
			HexMapCamera.Locked = true;
		}

		public void Close()
		{
			gameObject.SetActive(false);
			HexMapCamera.Locked = false;
		}

		public void Action()
		{
			string path = GetSelectedPath();
			if (path == null)
			{
				return;
			}
			if (loadSaveMode == enLoadSaveMode.SaveMap || loadSaveMode == enLoadSaveMode.SaveGame)
			{
				Save(path);
			}
			else
			{
				Load(path);
			}
			Close();
		}

		public void SelectItem(string name) => nameInput.text = name;

		public void Delete()
		{
			string path = GetSelectedPath();
			if (path == null)
			{
				return;
			}
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			nameInput.text = "";
			FillList();
		}

		void FillList()
		{
			for (int i = 0; i < listContent.childCount; i++)
			{
				Destroy(listContent.GetChild(i).gameObject);
			}

			string path;
			string searchPattern;
			if (loadSaveMode == enLoadSaveMode.LoadMap || loadSaveMode == enLoadSaveMode.SaveMap)
			{
                path = GameFileManager.MapFilePathBase;
				searchPattern = "*" + GameFileManager.MapFileExtension;
			}
            else
            {
                path = GameFileManager.GameFilePathBase;
                searchPattern = "*" + GameFileManager.GameFileExtension;
            }

            string[] paths = Directory.GetFiles(path, searchPattern);
			Array.Sort(paths);
			for (int i = 0; i < paths.Length; i++)
			{
				SaveLoadItem item = Instantiate(itemPrefab);
				item.Menu = this;
				item.MapName = Path.GetFileNameWithoutExtension(paths[i]);
				item.transform.SetParent(listContent, false);
			}
		}

		string GetSelectedPath()
		{
			string mapName = nameInput.text;
			if (mapName.Length == 0)
			{
				return null;
			}
			if(loadSaveMode == enLoadSaveMode.LoadMap || loadSaveMode == enLoadSaveMode.SaveMap)
				return Path.Combine(GameFileManager.MapFilePathBase, mapName + GameFileManager.MapFileExtension);
            else
                return Path.Combine(GameFileManager.GameFilePathBase, mapName + GameFileManager.GameFileExtension);
        }

		void Save(string path)
        {
            if (loadSaveMode == enLoadSaveMode.SaveMap)
			{
                GameFileManager.MapToLoadPath = path;
                GameFileManager.SaveMap(nodeGraph);
            }
            else if(loadSaveMode == enLoadSaveMode.SaveGame)
            {
                GameFileManager.GamePath = path;
                GameFileManager.SaveGame(gameControl.GameData);
            }
        }

		void Load(string path)
        {
            if (loadSaveMode == enLoadSaveMode.LoadMap)
            {
                GameFileManager.MapToLoadPath = path;
                GameFileManager.LoadMap(nodeGraph);
                HexMapCamera.ValidatePosition();
            }
            else if (loadSaveMode == enLoadSaveMode.LoadGame)
            {
                GameFileManager.GamePath = path;
                GameFileManager.GameLoadInstruction = enGameLoadInstruction.Load;
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            }
        }
	}
}