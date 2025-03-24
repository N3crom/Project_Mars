using UnityEngine;

public class S_TimerManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float timer;

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_LoadMainMenu rSE_LoadMainMenu;
    [SerializeField] RSE_UpdateTimer rSE_UpdateTimer;

    //[Header("RSO")]

    //[Header("SSO")]
    private void Update()
    {
        LevelTimer();
    }
    private void LevelTimer()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
            rSE_UpdateTimer.RaiseEvent(timer);
        }
        if(timer <= 0)
        {
            rSE_LoadMainMenu.RaiseEvent();
        }
    }
}