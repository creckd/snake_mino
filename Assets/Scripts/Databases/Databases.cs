using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Databases : MonoBehaviour
{
    private static Databases instance = null;
    public static Databases Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<Databases>();
            return instance;
        }
    }

    [SerializeField]
    private ConfigDatabaseScriptableObject _configDatabase;

    [SerializeField]
    private ResourceDatabaseScriptableObject _resourceDatabase;

    public ConfigDatabaseScriptableObject GetConfigDatabase()
    {
        return _configDatabase;
    }

    public ResourceDatabaseScriptableObject GetResourceDatabase()
    {
        return _resourceDatabase;
    }
}
