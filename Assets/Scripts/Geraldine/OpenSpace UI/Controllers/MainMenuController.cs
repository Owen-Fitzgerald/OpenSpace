using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Geraldine.OpenSpaceUI.Behaviours;
using Geraldine.OpenSpaceGame;
using Geraldine._4XEngine.Loaders;

namespace Geraldine.OpenSpaceUI.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        public GameObject MainMenuStack, EditorToolStack;
        public Button ContinueButton;
        public SaveLoadMenu SaveLoadMenu;
        public NewGameMenu NewGameMenu;


        public void Start()
        {
            string[] paths = Directory.GetFiles(GameFileManager.GameFilePathBase, string.Format("*{0}", GameFileManager.GameFileExtension));
            if(paths.Length > 0)
            { 
                ContinueButton.interactable = true;
                m_LastGamePath = paths[0];
            }
            else { ContinueButton.interactable = false;}

            GalaxySizeLoader.Instance.ReloadData();
            GalaxyAgeLoader.Instance.ReloadData();
            PlanetClassLoader.Instance.ReloadData();
            SystemInitializerLoader.Instance.ReloadData();
        }

        public void ReturnToMenu()
        {
            SaveLoadMenu.Close();
            NewGameMenu.Close();
            EditorToolStack.SetActive(false);

            MainMenuStack.SetActive(true);
        }

        public void OpenNewGameUI()
        {
            SaveLoadMenu.Close();
            EditorToolStack.SetActive(false);
            MainMenuStack.SetActive(false);

            NewGameMenu.Open();
        }

        public void OpenLoadGameUI()
        {
            EditorToolStack.SetActive(false);
            MainMenuStack.SetActive(false);
            NewGameMenu.Close();

            SaveLoadMenu.Open(enLoadSaveMode.LoadGame);
        }

        public void OpenEditorToolsMenu()
        {
            SaveLoadMenu.Close();
            NewGameMenu.Close();
            MainMenuStack.SetActive(false);

            EditorToolStack.SetActive(true);
        }

        public void ContinueGame()
        {
            string[] paths = Directory.GetFiles(GameFileManager.GameFilePathBase, string.Format("*{0}", GameFileManager.GameFileExtension));
            GameFileManager.GameLoadInstruction = enGameLoadInstruction.Load;
            GameFileManager.GamePath = paths[0];
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }

        public void LoadGame(string gamePath)
        {
            GameFileManager.GameLoadInstruction = enGameLoadInstruction.Load;
            GameFileManager.GamePath = gamePath;
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }

        public void NewGame(string mapPath)
        {
            GameFileManager.GameLoadInstruction = enGameLoadInstruction.New;
            GameFileManager.MapLoadInstruction = String.IsNullOrEmpty(mapPath) ?
                                                    enMapLoadInstruction.Generate :
                                                    enMapLoadInstruction.Load;
            GameFileManager.MapToLoadPath = mapPath;
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }

        public void NavigateToNewScene(int ID)
        {
            SceneManager.LoadScene(ID);
        }

        private string m_LastGamePath;
    }
}
