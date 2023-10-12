using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public GameObject panel; // ������ �г�
    public Button restartButton; // �ٽ� ���� ��ư
    public Button mainmenuButton; // ���θ޴� ��ư

    private bool isTimePaused = false; // �ð� ���� ���� ����

    private void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ ����
        restartButton.onClick.AddListener(RestartGame);
        mainmenuButton.onClick.AddListener(GoMainMenu);

        // �ʱ⿡�� �г��� ��Ȱ��ȭ
        panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ESC Ű�� �г��� Ȱ��/��Ȱ�� ���� ��ȯ
            TogglePanel();
        }

        // �г��� �����ִ� ���� �ð� ����
        if (isTimePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f; // �������� ���� ��� �ð� ���� �帧
        }
    }

    private void TogglePanel()
    {
        // �г��� Ȱ��/��Ȱ�� ���� ��ȯ �� �ð� ���� ���� ����
        panel.SetActive(!panel.activeSelf);
        isTimePaused = panel.activeSelf;
    }

    private void RestartGame()
    {
        // �ٽ� ���� ��ư Ŭ�� �� ���� ����� ���� �߰�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoMainMenu()
    {
        // ��ư Ŭ�� �� ���θ޴� �̵� ���� �߰�
        SceneManager.LoadScene("0.MainMenu");
    }
}

