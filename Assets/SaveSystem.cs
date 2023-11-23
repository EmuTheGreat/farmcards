using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void Save(InterfaceManager manager)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.sys";
        FileStream stream = new FileStream(path, FileMode.Create);

        InterfaceData data = new InterfaceData(manager);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static InterfaceData Load()
    {
        string path = Application.persistentDataPath + "/save.sys";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InterfaceData data = bf.Deserialize(stream) as InterfaceData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
