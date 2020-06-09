using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SnailGame
{
    public class UI : MonoBehaviour
    {
        public Slider bodySlider;
        public Slider shellSlider;
        public Slider hatSlider;
        public InputField snailName;
        public Snail snail;
        public TextMeshProUGUI systemText;
        public Button saveButton;
        private float s = 75;
        private float v = 100;

        public string succesfulMessage;
        public string errorMessage;

        private void Start()
        {
            InitScrollbars();
        }
        private void InitScrollbars()
        {
            Color.RGBToHSV(snail.data.bodyColor, out float h, out float s, out float v);
            bodySlider.value = h * 360;
            Color.RGBToHSV(snail.data.shellColor, out h, out s, out v);
            shellSlider.value = h * 360;
            systemText.text = "";
        }

        public void ChangeSprite()
        {
            snail.data.bodyColor = Color.HSVToRGB(bodySlider.value / 360f, s / 100f, v / 100f);
            snail.data.shellColor = Color.HSVToRGB(shellSlider.value / 360f, s / 100f, v / 100f);
            snail.PersonalizeSnail();

        }

        public void ChangeName()
        {
            snail.nameText.text = snailName.text;
            snail.data.snailName = snailName.text;
        }

        public void ChangeHat()
        {
            snail.hat.sprite = snail.hatsList[(int)hatSlider.value];
            snail.data.hatNumber = (int)hatSlider.value;
        }
        public void Save()
        {
            snail.data.snailName = snailName.text;
            if (snail.SerializeJSON())
            {
                systemText.color = Color.yellow;
                systemText.text = succesfulMessage;
                saveButton.interactable = false;
                Invoke(nameof(Restart), 2f);
            }
            else
            {
                systemText.color = Color.red;
                systemText.text = errorMessage;

            }

        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void BackButton()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
