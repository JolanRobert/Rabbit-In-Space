using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Data")]
public class DataSerializer : MonoBehaviour
{
    public static DataSerializer instance;
    public string path;
    private Encoding encoding = Encoding.UTF8;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void SetPath()
    {
        path = Application.persistentDataPath;
    }

    public void SaveData<T>(string fileName, T data)
    {
        SetPath();
        StreamWriter streamWriter = new StreamWriter(Path.Combine(path, fileName + ".xml"), false, encoding);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        
        xmlSerializer.Serialize(streamWriter, data);
        streamWriter.Close();
        
        Debug.Log("Save " + fileName);
    }

    public T LoadData<T>(string fileName)
    {
        SetPath();
        string completePath = Path.Combine(path, fileName + ".xml");
        if (File.Exists(completePath))
        {
            FileStream fileStream = new FileStream(completePath, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            T data = (T) xmlSerializer.Deserialize(fileStream);
            fileStream.Close();
            Debug.Log(fileName + " Data Loaded");

            return data;
        }
        Debug.LogWarning("Path don't exist");
        return default;
    }
}