using UnityEngine;
using System.IO;

public class DataSavingSystem : MonoBehaviour
{
    public static DataSavingSystem instance;
    private int isFirstRun;

    private void Awake()
    {
        SetUpSingleton();
        CreateDataManager();
    }

    private void Start()
    {
        DetectFirstAppLaunch();
    }

    private void SetUpSingleton()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void CreateDataManager()
    {
        gameObject.AddComponent<DataManager>();
        Debug.Log("Creating data manager");
    }

    private void DetectFirstAppLaunch()
    {
        isFirstRun = PlayerPrefs.GetInt("isFirst");
        if (isFirstRun == 0)
        {
            Debug.Log("first run");
            PlayerPrefs.SetInt("isFirst", 1);
            PlayerPrefs.Save();
            SaveJsonData();
        }
        else
        {
            Debug.Log("welcome again!");
            LoadJsonData();
        }
    }

    private SaveData CreateJsonDataForSaving()
    {
        SaveData saveData = new SaveData { currency = DataManager.GetPlasmaCount(),
            currentLevel = DataManager.GetCurrentLevel(),
            shieldCapacity = DataManager.GetMaxShieldEnergy(),
            damage = DataManager.GetPlayerDamage(),
            playerHealth = DataManager.GetPlayerHealth() };
        return saveData;
    }

    public void SaveJsonData()
    {
        try
        {
            string json = JsonUtility.ToJson(CreateJsonDataForSaving());

            if (!string.IsNullOrEmpty(json))
                File.WriteAllText(Application.persistentDataPath + "/savedata.txt", json);
            else
                Debug.Log("Json data for saving is empty");

            Debug.Log(json);
        }
        catch(System.Exception ex)
        {
            Debug.Log("Exception happened " + ex);
        }
    }
   
    private string ReadJsonData()
    {
        return File.ReadAllText(Application.persistentDataPath + "/savedata.txt");
    }

    public void LoadJsonData()
    {
        try
        {
            string json = ReadJsonData();

            if (!string.IsNullOrEmpty(json))
            {
                SaveData saveData = JsonUtility.FromJson<SaveData>(json);

                DataManager.SetPlasma(saveData.currency);
                DataManager.SetCurrentLevel(saveData.currentLevel);
                DataManager.SetMaxShieldEnergy(saveData.shieldCapacity);
                DataManager.SetPlayerDamage(saveData.damage);
                DataManager.SetPlayerHealth(saveData.playerHealth);

                Debug.Log("Loaded. " + json);
            }
            else
            {
                Debug.Log("json data for loading is empty");
                Application.Quit();
            }
        }
        catch (System.IO.FileNotFoundException)
        {
            SaveJsonData();
            Debug.Log("Save file was not found. Creating one");
        }
        catch (System.Exception ex)
        {
            Debug.Log("Exception " + ex);
            Application.Quit();
        }
    }

    private class SaveData
    {
        [Header("Progress variables")]
        public int currency;
        public int currentLevel = 1;

        [Header("Player ship properties")]
        public int playerHealth = 100;
        public int shieldCapacity = 200;
        public float damage = 10f;
    }
}
