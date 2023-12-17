using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Linq;

public class SaveSystem : MonoBehaviour
{
    //�������� �� ���� (������ ����) P.S �����, ��� �� ����

    public void Load()
    {
        foreach (var persist in FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveState>())
        {
            persist.Load();
        }
    }

    public void Save()
    {
        foreach(var persist in FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveState>())
        {
            persist.Save();
        }
    }
}
