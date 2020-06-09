using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace SnailGame
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("SnailSpawn")]
        public GameObject snail;
        public Vector2 instatiatePosition;
        private float incrementalPositionY = 2;
        public float endPositionY = 7.4f;
        [Header("Finish")]
        public float finishPointX;
        public Vector3 cameraTransform;
        private Snail winSnail;

        void Start()
        {
            InstatitateSnails(CheckNumberSnails());
            Time.timeScale = 0;
            StartCoroutine(StartRace());

        }

        public List<string> CheckNumberSnails()
        {
            string path = Application.dataPath + "/Snails/";
            string[] files = Directory.GetFiles(path);
            List<string> snailsPath = new List<string>();
            foreach (string f in files)
            {
                if (f.EndsWith(".json"))
                {
                    snailsPath.Add(f);
                }
            }
            incrementalPositionY = endPositionY / snailsPath.Count;
            return snailsPath;


        }

        private void InstatitateSnails(List<string> s)
        {
            for (int i = 0; i < s.Count; i++)
            {
                CreateSnail(s[i]);
            }
        }
        private void CreateSnail(string fileName)
        {
            GameObject s = Instantiate(snail);
            s.transform.position = instatiatePosition;
            instatiatePosition.y -= incrementalPositionY;
            s.GetComponent<Snail>().DeSerializeJSON(fileName);

        }

        private IEnumerator StartRace()
        {
            yield return new WaitForSecondsRealtime(3f);
            Time.timeScale = 1;
        }

        public void Finish(Snail snail)
        {
            Time.timeScale = 0;
            winSnail = snail;
            StartCoroutine(FinishGame());
        }

        private IEnumerator FinishGame()
        {
            yield return new WaitForSecondsRealtime(2f);
            Camera.main.transform.position = cameraTransform;
            winSnail.gameObject.transform.position = new Vector3(cameraTransform.x, 0, 0);
            winSnail.gameObject.transform.localScale = Vector3.one;
            yield return new WaitForSecondsRealtime(5f);
            Exit();
        }

        private void Exit()
        {
            MusicManager.Instance.LoadClip("MainMenu");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
