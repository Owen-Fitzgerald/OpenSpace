using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using Geraldine.OpenSpaceGame;
using Geraldine.OpenSpaceUI.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Geraldine._4XEngine.Localization;

namespace Geraldine.OpenSpaceUI.Behaviours
{
	/// <summary>
	/// Component that applies actions from the save-load menu UI to the hex map.
	/// Public methods are hooked up to the in-game UI.
	/// </summary>
	public class NewGameMenu : MonoBehaviour, ISelectItemFromList
    {
		[SerializeField]
		RectTransform listContent, SelectedEmpireContent, NewGameContent, GenerateMapOptions, SelectMapOptions;

		//[SerializeField]
		//EmpireSelectItem itemPrefab;

		[SerializeField]
		TextMeshProUGUI SelectedEmpireTMP;

		[SerializeField]
		Image SelectedEmpireLogo;

		[SerializeField]
		Toggle GenerateMapToggle, SelectMapToggle;

		[SerializeField]
		TMP_Dropdown GenerationScriptDropdown, GenerationSizeDropdown, SelectPremadeMapDropdown;

		[SerializeField]
		Button DeleteSelectedEmpireButton;

        public void Open()
		{
			//DatabaseManager.LoadDatabases();
			FillList();
			SelectedEmpireContent.gameObject.SetActive(false);
            NewGameContent.gameObject.SetActive(false);
			gameObject.SetActive(true);
			//HexMapCamera.Locked = true;
		}

		public void Close()
		{
			gameObject.SetActive(false);
			//HexMapCamera.Locked = false;
		}

		public void TransitionToNewGameScene()
		{
            if (SelectMapToggle.isOn)
            {
                string path = GetSelectedPath();
                GameFileManager.MapToLoadPath = path;
                GameFileManager.MapLoadInstruction = enMapLoadInstruction.Load;
            }
            else
            {
				//GameFileManager.MapToGenerateSize = (enMapSize)GenerationSizeDropdown.value;
                GameFileManager.MapLoadInstruction = enMapLoadInstruction.Generate;
            }
            GameFileManager.GameLoadInstruction = enGameLoadInstruction.New;

            SceneManager.LoadScene(2);
		}

		public void MapCreationToggleChanged()
		{
			if(GenerateMapToggle.isOn) { GenerateMapOptions.gameObject.SetActive(true); }
			else { GenerateMapOptions.gameObject.SetActive(false);}

			if(SelectMapToggle.isOn) { SelectMapOptions.gameObject.SetActive(true); }
			else { SelectMapOptions.gameObject.SetActive(false);}
		}

		public void SelectItem(string name)
		{
   //         EmpireInfo empireInfo = EmpireInfoDatabase.Instance.LoadedDatabase.FirstOrDefault(_ => _.UniqueID == name);

			//SelectedEmpireTMP.text = Locale.GetFormattedLocalizedTextBody(empireInfo.Name);

   //         SelectedEmpireContent.gameObject.SetActive(true);
   //         NewGameContent.gameObject.SetActive(true);
			//DeleteSelectedEmpireButton.interactable = empireInfo.UserCreated;
        }

		//private EmpireInfo m_SelectedEmpireInfo;

        void FillList()
        {
            for (int i = 0; i < listContent.childCount; i++)
			{
				Destroy(listContent.GetChild(i).gameObject);
            }

   //         IList<EmpireInfo> empires = EmpireInfoDatabase.Instance.LoadedDatabase;

			//for (int i = 0; i < empires.Count(); i++)
			//{
			//	EmpireSelectItem item = Instantiate(itemPrefab);
			//	item.Menu = this;
			//	item.Setup(empires[i]);
			//	item.transform.SetParent(listContent, false);
			//}

            SelectPremadeMapDropdown.options.Clear();
            GenerationSizeDropdown.options.Clear();
            GenerationScriptDropdown.options.Clear();

			string path;
			string searchPattern;
			path = GameFileManager.MapFilePathBase;
			searchPattern = "*" + GameFileManager.MapFileExtension;

			string[] paths = Directory.GetFiles(path, searchPattern);
			Array.Sort(paths);
			for (int i = 0; i < paths.Length; i++)
			{
				SelectPremadeMapDropdown.AddOptions(new List<TMP_Dropdown.OptionData>() { new TMP_Dropdown.OptionData(Path.GetFileNameWithoutExtension(paths[i])) });
			}

            //foreach (string name in Enum.GetNames(typeof(enMapSize)))
            //{
            //    GenerationSizeDropdown.AddOptions(new List<TMP_Dropdown.OptionData>() { new TMP_Dropdown.OptionData(name) });
            //}

            //foreach (string name in Enum.GetNames(typeof(enMapScript)))
            //{
            //    GenerationScriptDropdown.AddOptions(new List<TMP_Dropdown.OptionData>() { new TMP_Dropdown.OptionData(name) });
            //}
        }

        string GetSelectedPath()
		{
			string mapName = SelectPremadeMapDropdown.options[SelectPremadeMapDropdown.value].text;
			if (mapName.Length == 0)
			{
				return null;
			}
			return Path.Combine(GameFileManager.MapFilePathBase, mapName + GameFileManager.MapFileExtension);
        }
	}
}