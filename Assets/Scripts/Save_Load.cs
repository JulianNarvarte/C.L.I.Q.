using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DG.Tweening.Core.Easing;
public static class Save_Load
{
    public static void Save(GameManager gamemanager)
    {
        Data data = new Data(gamemanager);
        string dataPath = Application.persistentDataPath + "/data.save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static Data Load()
    {
        
        string dataPath = Application.persistentDataPath + "/data.save";
        
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            Data data = (Data) formatter.Deserialize(fileStream);
            fileStream.Close();
            return data;
        }
        else
        {
            Debug.LogError("No se encontro el archivo de guardado");
            return null;
        }
    }
}

