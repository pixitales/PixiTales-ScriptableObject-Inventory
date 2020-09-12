using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu = null;

    public UnityEvent onPauseEvent;
    public UnityEvent onResumeEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClose(pauseMenu);
        }
    }

    public void OpenClose(CanvasGroup canvasGroup)
    {
        if (canvasGroup.alpha == 0)
        {
            //Time.timeScale = 0f;

            onPauseEvent.Invoke();
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
        else
        {
            //Time.timeScale = 1f;

            onResumeEvent.Invoke();
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}
