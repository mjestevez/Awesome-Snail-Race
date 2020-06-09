using System;
using UnityEngine;

namespace SnailGame
{
    [Serializable]
    public class SnailData : ISerializationCallbackReceiver
    {
        public string snailName;
        public Color bodyColor;
        public Color shellColor;
        public int hatNumber;

        public SnailData()
        {
            snailName = "";
            bodyColor = Color.HSVToRGB(33f / 360f, 0.75f, 1f);
            shellColor = Color.HSVToRGB(198f / 360f, 0.75f, 1f);
            hatNumber = 0;
        }
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
        }

    }
}
