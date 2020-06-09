using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace SnailGame
{
    public class mySceneManager : Singleton<mySceneManager>
    {
        public Button raceButton;
        public TextMeshProUGUI systemText;

        private void Start()
        {
            Time.timeScale = 1;
            CheckRaceButton();
        }

        public void CheckRaceButton()
        {

            if (CheckNumberSnails() >= 2)
            {
                systemText.enabled = false;
                raceButton.interactable = true;
            }
        }

        public int CheckNumberSnails()
        {
            string path = Application.dataPath + "/Snails/";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string[] files = Directory.GetFiles(path);
            List<string> snailsPath = new List<string>();
            foreach (string f in files)
            {
                if (f.EndsWith(".json"))
                {
                    snailsPath.Add(f);
                }
            }

            return snailsPath.Count;


        }

        public void Race()
        {
            ChangeScene("Race");
        }

        public void NewSnail()
        {
            ChangeScene("PersonalizationScene");
        }

        private void ChangeScene(string name)
        {
            SceneManager.LoadScene(name);
            MusicManager.Instance.LoadClip(name);
        }
        public void ManageSnail()
        {
            string path = Application.dataPath + "/Snails";
            string[] words = path.Split('/');
            string newPath = "";
            for (int i = 0; i < words.Length; i++)
            {
                newPath += words[i] + "\\";
            }
            System.Diagnostics.Process.Start("explorer.exe", "/root," + newPath);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
