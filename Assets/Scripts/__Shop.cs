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

    [System.Serializable] public class ShopItem
    {
    public Sprite Image;
    public int Price;
    public bool IsPurchased = false;
    }

    public List<ShopItem> ShopItemsList;
    // 애니메이션 생략 [SerializeField] Animator NoCoinsAnim;

    [SerializeField] GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] GameObject ShopPanel;
	Button buyBtn;
 	void Start ()
	{
		int len = ShopItemsList.Count;
		for (int i = 0; i < len; i++) {
			g = Instantiate (ItemTemplate, ShopScrollView);
			g.transform.GetChild (0).GetComponent <Image> ().sprite = ShopItemsList [i].Image;
			g.transform.GetChild (1).GetChild (0).GetComponent <Text> ().text = ShopItemsList [i].Price.ToString ();
			buyBtn = g.transform.GetChild (2).GetComponent <Button> ();
			if (ShopItemsList [i].IsPurchased) {
				DisableBuyButton ();
			}
			buyBtn.AddEventListener (i, OnShopItemBtnClicked);
		}
	}

	void OnShopItemBtnClicked (int itemIndex)
	{
		if (Game.Instance.HasEnoughCoins (ShopItemsList [itemIndex].Price))
		{	Game.Instance.UseCoins (ShopItemsList [itemIndex].Price);

			ShopItemsList [itemIndex].IsPurchased = true;

			buyBtn = ShopScrollView.GetChild (itemIndex).GetChild (2).GetComponent <Button> ();
			DisableBuyButton ();

			Game.Instance.UpdateAllCoinsUIText ();


			Collection.Instance.AddAvatar (ShopItemsList [itemIndex].Image);
		}
        else
        {
			Debug.Log ("You don't have enough coins!!");
		}
	}

	void DisableBuyButton ()
	{
		buyBtn.interactable = false;
		buyBtn.transform.GetChild (0).GetComponent <Text> ().text = "PURCHASED";
	}

	public void OpenShop ()
	{
		ShopPanel.SetActive (true);
	}

	public void CloseShop ()
	{
		ShopPanel.SetActive (false);
	}
}
