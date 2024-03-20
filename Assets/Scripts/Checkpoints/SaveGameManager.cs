using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{

    //load/save game code was created from https://www.youtube.com/watch?v=aUi9aijvpgs&t=561s

    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    private GameData gameData;

    private List<ISaveData> saveDataObjects;

    private FileDataHandler dataHandler; 

    public static SaveGameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Save Game Manager in the scene. Newest one was destroyed.");
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        instance = this;

        //Debug.Log(Application.dataPath);
        this.dataHandler = new FileDataHandler(Application.dataPath + "/Save", fileName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded");
        this.saveDataObjects = FindAllISaveDataObjects();

        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        GameData temp = this.gameData;
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No save found. A new game must be started before data can be loaded.");
            this.gameData = temp;
            return;
        }

        DeserializeCheckpoints();

        foreach (ISaveData saveDataObject in saveDataObjects) 
        {
            saveDataObject.LoadData(gameData);
        } 
    }

    public void SaveGame()
    {
        if(this.gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        foreach (ISaveData saveDataObject in saveDataObjects)
        {
            saveDataObject.SaveData(gameData);
        }

        SerializeCheckpoints();

        dataHandler.Save(gameData);
    }

    public void DeleteSave()
    {
        dataHandler.Delete();
    }

    private List<ISaveData> FindAllISaveDataObjects()
    {
        //needs to implement MonoBehaviour and ISaveData to be found like this
        IEnumerable<ISaveData> saveDataObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveData>();

        return new List<ISaveData>(saveDataObjects);

    }

    private void SerializeCheckpoints()
    {
        gameData.checkpointKeys.Clear();
        gameData.checkpointValues.Clear();

        foreach(var pair in gameData.checkpoints)
        {
            gameData.checkpointKeys.Add(pair.Key);
            gameData.checkpointValues.Add(pair.Value);
        }
    }

    private void DeserializeCheckpoints()
    {
        gameData.checkpoints.Clear();

        if(gameData.checkpointKeys.Count != gameData.checkpointValues.Count)
        {
            throw new System.Exception("Number of keys and values do not match!");
        }

        for(int i = 0; i < gameData.checkpointKeys.Count; i++)
        {
            gameData.checkpoints.Add(gameData.checkpointKeys[i], gameData.checkpointValues[i]);
        }
    }

    public bool HasGameData()
    {
        return gameData != null;
    }
}
