using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIR;
    private string SAVE_DATA_FILE = "/SaveFile.txt"; // JSON 형식으로 저장될 것임.
    // 나같은 경우 coinManager에서 다 가져와야 함.


    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIR = Application.dataPath + "/studyAsset/script/Save/SaveFile";

        if(!Directory.Exists(SAVE_DATA_DIR))
        {
            Directory.CreateDirectory(SAVE_DATA_DIR);
        }
        // 씬 전환 될 때마다 세이브 
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 시작될 때, LoadData()
        LoadData();
    }
    public void SaveData()
    {
        // saveData 변수에 CoinManager에서 업글한 값 전부 가져오기.
        saveData.SavedShopPlusAttackSpeed = CoinManager.Instance.ShopPlusAttackSpeed;
        saveData.SavedShopPlusHp = CoinManager.Instance.ShopPlusHp;
        saveData.SavedShopPlusSpeed = CoinManager.Instance.ShopPlusSpeed;

        string json = JsonUtility.ToJson(saveData); // Json 화 시키기
        
        File.WriteAllText(SAVE_DATA_DIR+SAVE_DATA_FILE, json);

        Debug.Log(SAVE_DATA_DIR + SAVE_DATA_FILE);
        Debug.Log("저장 완");
        Debug.Log(json);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SaveData();
    }

    public void LoadData()
    {
        if(File.Exists(SAVE_DATA_DIR + SAVE_DATA_FILE))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIR + SAVE_DATA_FILE);
            SaveData saveData = JsonUtility.FromJson<SaveData>(loadJson); // 이게 뭐노

            CoinManager.Instance.SetShopHp(saveData.SavedShopPlusHp);
            CoinManager.Instance.SetShopSpeed(saveData.SavedShopPlusSpeed);
            CoinManager.Instance.SetShopAttackSpeed(saveData.SavedShopPlusAttackSpeed);

            Debug.Log("로드 완");
            Debug.Log(saveData.SavedShopPlusHp);
        }
        else
        {
            Debug.Log("No Such File");
        }
    }

}
