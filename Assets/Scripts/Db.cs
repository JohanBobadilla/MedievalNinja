using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Db : MonoBehaviour
{
    public static Db instance;
    public List<Armor> armorSets;
    public List<Face> faceSets;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;    
    }
}
