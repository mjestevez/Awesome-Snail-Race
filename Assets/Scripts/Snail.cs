using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace SnailGame
{
    public class Snail : MonoBehaviour
    {
        [Header("Snail Info")]
        public SpriteRenderer body;
        public SpriteRenderer shell;
        public SpriteRenderer ant;
        public SpriteRenderer hat;
        public List<Sprite> hatsList;
        public TextMeshProUGUI nameText;
        public SnailData data;

        [Header("Movement")]
        public float speed = 1f;
        public bool move = false;
        private float minSpeed = 0.1f;
        private float maxSpeed = 0.3f;
        

        private void Awake()
        {
            data = new SnailData();
            UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        }
        private void Start()
        {
            StartCoroutine(ChangeSpeed());
        }
        private void Update()
        {
            if (move)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                CheckWin();
            }
        }

        private void CheckWin()
        {
            if (transform.position.x >= GameManager.Instance.finishPointX)
            {
                GameManager.Instance.Finish(this);
            }
        }

        private IEnumerator ChangeSpeed()
        {
            while (true)
            {
                speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
                yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
            }


        }
        public void PersonalizeSnail()
        {
            body.color = data.bodyColor;
            shell.color = data.shellColor;
            ant.color = data.shellColor;
            nameText.text = data.snailName;
            hat.sprite = hatsList[data.hatNumber];
        }

        #region JSON
        public bool SerializeJSON()
        {
            if (data.snailName == "") return false;
            string path = Application.dataPath + "/Snails/";
            string fileName = path + data.snailName + ".json";
            StreamWriter writer = new StreamWriter(fileName);
            string dataJSON = JsonUtility.ToJson(data);
            writer.Write(dataJSON);
            writer.Close();
            return true;
        }

        public void DeSerializeJSON(string fileName)
        {
            StreamReader reader = new StreamReader(fileName);
            data = JsonUtility.FromJson<SnailData>(reader.ReadToEnd());
            reader.Close();
            PersonalizeSnail();
        }
        #endregion
    }
}
