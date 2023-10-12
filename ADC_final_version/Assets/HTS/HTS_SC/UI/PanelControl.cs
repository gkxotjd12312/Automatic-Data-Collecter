using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public GameObject panel; // 조작할 패널
    public Button restartButton; // 다시 시작 버튼
    public Button mainmenuButton; // 메인메뉴 버튼

    private bool isTimePaused = false; // 시간 정지 여부 저장

    private void Start()
    {
        // 버튼에 클릭 이벤트 연결
        restartButton.onClick.AddListener(RestartGame);
        mainmenuButton.onClick.AddListener(GoMainMenu);

        // 초기에는 패널을 비활성화
        panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ESC 키로 패널의 활성/비활성 상태 전환
            TogglePanel();
        }

        // 패널이 열려있는 동안 시간 정지
        if (isTimePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f; // 정지하지 않은 경우 시간 정상 흐름
        }
    }

    private void TogglePanel()
    {
        // 패널의 활성/비활성 상태 전환 및 시간 정지 여부 설정
        panel.SetActive(!panel.activeSelf);
        isTimePaused = panel.activeSelf;
    }

    private void RestartGame()
    {
        // 다시 시작 버튼 클릭 시 게임 재시작 로직 추가
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoMainMenu()
    {
        // 버튼 클릭 시 메인메뉴 이동 로직 추가
        SceneManager.LoadScene("0.MainMenu");
    }
}

