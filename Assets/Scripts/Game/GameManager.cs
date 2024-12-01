using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // 장면관리를 사용하기 위해 SceneManagement 네임스페이스 추가

public class GameManager : MonoBehaviour
{
    // 스크립트들을 짤때 Player를 직접 public으로 받아도 되지만 깔끔한 코딩을 위해 GameManager에서 Player를 받아 static으로 선언하여 사용한다
    public static GameManager instance; // static = 정적으로 사용하겠다는 키워드. 바로 메모리에 얹어버림
    // static으로 선언된 변수는 인스켓터에 나타나지 않는다
    
    [Header("# Game Control")] // Header = 인스펙터의 속성들을 이쁘게 구분시켜주는 타이틀
    public float gameTime; // 게임시간
    public float maxGametime = 2 * 10f; // 최대게임시간
    
    [Header("# Player Info")]
    public bool isLive; // 시간 정지 여부를 알려주는 bool 변수 선언
    public float health; // 생명력 관련 변수는 float로 설정
    public float maxHealth;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp; // 각 레벨의 필요경험치를 보관한 배열변수 선언 및 초기화

    [Header("# GameObject")]
    public Player player;
    public PoolManager pool; // 다양한 곳에서 쉽게 접근할 수 있도록 게임매니저에 풀 매니저 추가
    public LevelUp UILevelUp;
    public Result UIResult; // 게임결과 UI오브젝트를 저장할 변수 
    public GameObject enemyCleaner; // 게임 승리할때 적을 정리하는 클리너 변수

    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        maxHealth = DataManager.instance.nowPlayer.maxHealth;
    }

    void Update()
    {
        if (!isLive) {
            return; // Update 계얼조직에 isLive가 false이면 동작하지 못하도록 조건 추가
        }

        gameTime += Time.deltaTime;

        if (gameTime > maxGametime) {
            gameTime = maxGametime;
            GameVictory(); // 게임 시간이 최대시간을 넘기는 때에 게임 승리 함수 호출
        }
    }

    public void GameStart() // 유니티 기능을 사용하여 Button Start의 On Click과 함수를 연결
    {
        health = maxHealth; // 시작할 때 현재 체력과 최대 체력이 같도록 로직 추가

        // 임시 스크립트 (첫번째 캐릭터 선택)
        UILevelUp.Select(0); // 게임이 처음 시작하면 기본 근접무기 하나를 지급
        isLive = true;
        Resume(); // 게임 시작 함수 내에 시간 재개 함수 호출 
    }

    public void GameOver() // 게임오버 담당 함수
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine() // 바로 Stop()이 실행되면 플레이어의 Dead애니메이션이 실행되기전 시간이 멈출수있기에 게임오버 코루틴 작성
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f); // 0.5초의 딜레이를 준다

        UIResult.gameObject.SetActive(true); // 게임결과 UI오브젝트 활성화
        UIResult.Lose();
        DataManager.instance.nowPlayer.coin += kill;
        Stop(); // 시간을 멈춤
    }

    public void GameVictory() // 게임승리 담당 함수
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine() // 바로 Stop()이 실행되면 몬스터의 Dead 애니메이션이 실행되기전 시간이 멈출수있기에 코루틴 작성
    {
        isLive = false;
        enemyCleaner.SetActive(true); // 게임 승리 코루틴의 전반부에 적 클리너를 활성화

        yield return new WaitForSeconds(0.5f); // 0.5초의 딜레이를 준다

        UIResult.gameObject.SetActive(true); // 게임결과 UI오브젝트 활성화
        UIResult.Win();
        DataManager.instance.nowPlayer.coin += kill;
        Stop(); // 시간을 멈춤
    }

    public void GameRetry() // 게임재시작 함수
    {
        SceneManager.LoadScene("gameScene"); 
        // LoadScene = 이름 혹은 인덱스로 장면을 새롭게 부르는 함수, ()안에 Scene의 이름이나 순서를 배치, 현재의 SampleScene은 0번으로 되어있다    
    }

    public void ToLobby()
    {
        SceneManager.LoadScene("lobbyScene");
    }

    public void GetExp()
    {
        if (!isLive) { // 게임종료시 몬스터가 죽으면서 경험치를 얻게되면 레벨업 창이 뜰수있다
            return;
        }
        

        exp++;

        // if 조건으로 필요 경험치에 도달하면 레벨 업 하도록 작성
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) { // 무한레벨 구현을 위해 Min 함수를 사용하여 최고 경험치를 그대로 사용하도록 변경
            level++;
            exp = 0;
            UILevelUp.Show(); // 게임매니저의 레벨업 로직에 창을 보여주는 함수 호출
        }
    }

    public void Stop() // 시간정지, 레벨업시 호출
    {
        isLive = false;
        Time.timeScale = 0; // timeScale = 유니티의 시간 속도 (배율), 초기값은 1
    }

    public void Resume() // 이후 아이템 선택시 다시 시간이 작동
    {
        isLive = true;
        Time.timeScale = 1; // 만약 Time.timeScale = 2라면 2배속
    }
}