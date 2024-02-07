using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            Debug.LogError("Found more than one Save Game Manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        //Debug.Log(Application.dataPath);

        //might need to change to Application.persistentDataPath for a real game as it saves in data. For now, saving to Unity Assets in a new Folder called SaveGame
        this.dataHandler = new FileDataHandler(Application.dataPath + "/SaveGame", fileName);
        this.saveDataObjects = FindAllISaveDataObjects();
        
        //remove this when added to the main menu
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No save found.");
            NewGame();
        }

        foreach (ISaveData saveDataObject in saveDataObjects) 
        {
            saveDataObject.LoadData(gameData);
        }

    }

    public void SaveGame()
    {
        foreach (ISaveData saveDataObject in saveDataObjects)
        {
            saveDataObject.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private List<ISaveData> FindAllISaveDataObjects()
    {
        //needs to implement MonoBehaviour and ISaveData to be found like this
        IEnumerable<ISaveData> saveDataObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveData>();

        return new List<ISaveData>(saveDataObjects);

    }
}
