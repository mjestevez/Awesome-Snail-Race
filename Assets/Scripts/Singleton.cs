﻿using UnityEngine;
namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T m_Instance;
        public static T Instance
        {
            get
            {
                // If no references found, search it.
                if (m_Instance == null)
                {
                    m_Instance = (T)FindObjectOfType(typeof(T));
                }

                return m_Instance;
            }
        }


    }
}

