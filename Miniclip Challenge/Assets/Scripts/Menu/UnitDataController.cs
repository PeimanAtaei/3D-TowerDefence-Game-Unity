using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataController : MonoBehaviour
{
    public static UnitDataController instance;

    [System.Serializable]
    public class Unit
    {
        public Sprite icon;
        public GameObject objectPb;
        public GameObject gridButtonPb;
        public List<Label> labels;
        public List<Ability> abilities;

    }

    [System.Serializable]
    public class Ability
    {
        public string title;
        public int value;
    }

    [System.Serializable]
    public class Label
    {
        public string title;
        public string value;
    }

    public List<Unit> unitInfo;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
