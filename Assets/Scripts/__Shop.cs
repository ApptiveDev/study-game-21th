using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // 싱글톤 패턴 구현
    public static Shop Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

        [System.Serializable] public class ShopItem{
        public Sprite Image;
        public int Price;
        public bool IsPurchased = false;
    }

    public List<ShopItem> ShopItemsList;
    // 애니메이션 생략 [SerializeField] Animator NoCoinsAnim;
    [SerializeField] Text CoinsText;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    List<GameObject> ShopItemObjects = new List<GameObject>(); // Button buyBtn; 전역 선언 대신 추가

    void Start()
    {
        ItemTemplate = ShopScrollView.GetChild (0).gameObject;

        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate (ItemTemplate, ShopScrollView);
            g.transform.GetChild (0).GetComponent <Image>().sprite = ShopItemsList[i].Image;
            g.transform.GetChild (1).GetChild (0).GetComponent <Text>().text = ShopItemsList[i].Price.ToString();
            
            Button buyBtn = g.transform.GetChild (2).GetComponent <Button>();
            buyBtn.interactable = !ShopItemsList [i].IsPurchased;
        
            int index = i; // 로컬 변수에 인덱스 저장
            buyBtn.onClick.AddListener(() => OnShopItemBtnClicked(index));
        }
        
        Destroy(ItemTemplate); // 삭제할 오브젝트 지정
    }

    void OnShopItemBtnClicked(int itemIndex)
    {   
        if (Game.Instance.HasEnoughCoins(ShopItemsList[itemIndex].Price /*amount*/))
        {
            Game.Instance.UseCoins(ShopItemsList[itemIndex].Price /*amount*/);
        
            // 아이템 구매 처리
            ShopItemsList [itemIndex].IsPurchased = true;
            Button buyBtn = ShopScrollView.GetChild (itemIndex).GetChild (2).GetComponent <Button> ();
            buyBtn.interactable = false;
            buyBtn.transform.GetChild (0).GetComponent <Text> ().text = "PURCHASED";

            Game.Instance.UpdateAllCoinsUIText();
        }
        else
        {
            // 코인 없습니다.. 구현 NoCoinsAnim.SetTrigger ("NoCoins");
        }
    }



    public void OpenShop()
    {
        gameObject.SetActive (true);
    }

    public void CloseShop()
    {
        gameObject.SetActive (false);
    }

}
