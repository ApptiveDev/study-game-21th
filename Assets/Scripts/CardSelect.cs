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
    [SerializeField] GameObject StopGas;

    GameObject player;
    public bool isSelecting = false;

    private List<string> cardPool = new List<string> { "Bullet", "BlackHole", "StopGas" };
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
            rectTransform.anchoredPosition = new Vector2(-300 + 300 * i, 0);

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

        if (cardName == "Bullet")
        {
            if (spawnWeapon.BulletDelay == 0.5f) spawnWeapon.BulletDelay = 0.25f;
            else if (spawnWeapon.BulletDelay == 0.25f) spawnWeapon.BulletDelay = 0.1f;
        }
        else if (cardName == "BlackHole")
        {
            StartCoroutine(SpawnBlackHole());
        }
        else if (cardName == "StopGas")
        {
            StartCoroutine(SpawnStopGas());
        }

        Time.timeScale = 1f;
        cardPanel.SetActive(false);
        isSelecting = false;
    }

    public IEnumerator SpawnBlackHole()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            GameObject blackholeInstance = Instantiate(BlackHole);
            blackholeInstance.transform.position = player.transform.position;

            BlackHoleSystem blackholeSystem = blackholeInstance.GetComponent<BlackHoleSystem>();
            blackholeSystem.BlackHoleDirection(player.transform.rotation);
        }
    }

    public IEnumerator SpawnStopGas()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            GameObject stopgasInstance = Instantiate(StopGas);
            stopgasInstance.transform.position = player.transform.position;
        }
    }
}