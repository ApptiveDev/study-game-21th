using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour
{
    [SerializeField] Slider expBar;
    [SerializeField] GameObject cardPanel;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject BlackHole;
    [SerializeField] GameObject Gas1;
    [SerializeField] GameObject Gas2;

    GameObject player;
    float timeBlackHole = 5f;
    float timeGas = 6f;
    public bool isSelecting = false;
    public bool isbigBlackHole = false;
    bool isStopgas = false;

    private List<string> cardPool = new List<string> {"Bullet Speed", "BlackHole", "Gas"};
    private List<GameObject> displayedCards = new List<GameObject>();

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (expBar.value >= expBar.maxValue && !isSelecting)
        {
            SelectCards();
        }
    }

    public void SelectCards()
    {
        isSelecting = true;
        Time.timeScale = 0f;

        cardPanel.SetActive(true);
        GenerateRandomCards();
    }

    void GenerateRandomCards()
    {
        foreach (GameObject card in displayedCards) Destroy(card);
        displayedCards.Clear();

        List<string> selectedCards = new List<string>();
        while (selectedCards.Count < 3 && cardPool.Count > 0)
        {
            int randomIndex = Random.Range(0, cardPool.Count);
            selectedCards.Add(cardPool[randomIndex]);
            //cardPool.RemoveAt(randomIndex);
        }

        for (int i = 0; i < selectedCards.Count; i++)
        {
            GameObject card = Instantiate(cardPrefab, cardPanel.transform);

            card.GetComponentInChildren<TMP_Text>().text = selectedCards[i];

            RectTransform rectTransform = card.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(-250 + 250 * i, 0);

            string cardName = selectedCards[i];
            card.GetComponent<Button>().onClick.AddListener(() =>
            {
                ApplyCardSelection(cardName);
            });

            displayedCards.Add(card);
        }
    }

    public void ApplyCardSelection(string cardName)
    {
        SpawnWeapon spawnWeapon = player.GetComponent<SpawnWeapon>();

        if (cardName == "Bullet Speed")
        {
            if (spawnWeapon.BulletDelay == 0.5f)
            {
                spawnWeapon.BulletDelay = 0.35f;
                cardPool.Insert(0, "Bullet Speed");
            }
            else if (spawnWeapon.BulletDelay == 0.35f) spawnWeapon.BulletDelay = 0.2f;
        }
        else if (cardName == "BlackHole")
        {
                StartCoroutine(SpawnBlackHole());
                cardPool.Insert(0, "BlackHole Scale");
                cardPool.Insert(0, "BlackHole Speed");
        }

        else if (cardName == "BlackHole Scale")
        {
            isbigBlackHole = true;
        }

        else if (cardName == "BlackHole Speed")
        {
            timeBlackHole = 3f;
        }

        else if (cardName == "Gas")
        {
            StartCoroutine(SpawnGas());
            cardPool.Insert(0, "Gas Speed");
        }

        else if (cardName == "Gas Speed")
        {
            timeGas = 4f;
        }

        Time.timeScale = 1f;
        cardPanel.SetActive(false);
        isSelecting = false;
    }

    public IEnumerator SpawnBlackHole()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBlackHole);
            GameObject blackholeInstance = Instantiate(BlackHole);
            blackholeInstance.transform.position = player.transform.position;

            BlackHoleSystem blackholeSystem = blackholeInstance.GetComponent<BlackHoleSystem>();
            blackholeSystem.BlackHoleDirection(player.transform.rotation);
        }
    }


    public IEnumerator SpawnGas()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeGas);
            GameObject gas1Instance = Instantiate(Gas1);
            GameObject gas2Instance = Instantiate(Gas2);
            gas1Instance.transform.position = player.transform.position;
            gas2Instance.transform.position = player.transform.position;
        }
    }
}