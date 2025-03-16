/*
using Firebase.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Managers;
using GameAnalyticsSDK;



public class AnalyticsTracking : MonoBehaviour
{
   
    private bool fiveLevelComplete;
    private bool tenLevelComplete;
    private bool fifteenLevelComplete;
    private bool twentyLevelComplete;
    
    private int _levelTime;
    private float _time2;
    private int _moveCount;
    private int _levelIndex;
    private int _levelNumber;
    private int _attemptCount;
    
   
    void Start()
    {
        //AudioManager.Instance.PlayMusic(true);
        RegisterEvents();
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
        _levelNumber = _levelIndex;
        switch (_levelNumber)
        {
            case 5:
                fiveLevelComplete = true;
                break;
            case 10:
                tenLevelComplete = true;
                break;
            case 15:
                fifteenLevelComplete = true;
                break;
            case 20:
                twentyLevelComplete = true;
                break;
        }

        PlayerPrefs.SetInt("LastLevel", _levelIndex);
        if (!PlayerPrefs.HasKey($"FirstPlay_00{_levelNumber}"))
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,$"Level00{_levelNumber}");
            FirebaseAnalytics.LogEvent($"Start_Level00{_levelNumber}");
            PlayerPrefs.SetInt($"FirstPlay_00{_levelNumber}",1);
            //FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart, LevelParameters);
            if (fiveLevelComplete)
            {
                GameAnalytics.NewDesignEvent("Five_Level_Completed");
                FirebaseAnalytics.LogEvent("Five_Level_Completed");
            }else if (tenLevelComplete)
            {
                GameAnalytics.NewDesignEvent("Ten_Level_Completed");
                FirebaseAnalytics.LogEvent("Ten_Level_Completed");
            } else if (fifteenLevelComplete)
            {
                GameAnalytics.NewDesignEvent("Fifteen_Level_Completed");
                FirebaseAnalytics.LogEvent("Fifteen_Level_Completed");
            } else if (twentyLevelComplete)
            {
                GameAnalytics.NewDesignEvent("Twenty_Level_Completed");
                FirebaseAnalytics.LogEvent("Twenty_Level_Completed");
            }
        }
        
        _attemptCount = PlayerPrefs.GetInt($"Attempt_00{_levelNumber}",0);
        _attemptCount += 1;
        //GameAnalytics.NewDesignEvent($"Attempt_00{_levelNumber}", _attemptCount);
        PlayerPrefs.SetInt($"Attempt_00{_levelNumber}", _attemptCount);
        
        
    }

    

    private void RegisterEvents()
    {
        EventManager.Instance.ONLevelEnd += ONLevelEnd;
    }
    


    private void OnRevive()
    {
        GameAnalytics.NewDesignEvent($"Revive_Level00{_levelNumber}");
        
        //FirebaseAnalytics.LogEvent($"Revive_Level00{_levelNumber}", LevelParameters);
    }

    

    private void ONLevelEnd(bool isSuccess)
    {
        if (!isSuccess)
        {
            OnLevelFail();
            //SupersonicWisdom.Api.NotifyLevelFailed(ESwLevelType.Regular,_levelNumber,null);
        }
        else
        {
            OnLevelComplete();
            //SupersonicWisdom.Api.NotifyLevelCompleted(ESwLevelType.Regular,_levelNumber,null);
        }
    }

    private void OnLevelReload()
    {
        GameAnalytics.NewDesignEvent($"Reload_Level00{_levelNumber}");
        //FirebaseAnalytics.LogEvent($"Reload_Level00{_levelNumber}", LevelParameters);
        GameManager.Instance.ReloadLevel();
    }

   
    private void OnLevelComplete()
    {
        if (!PlayerPrefs.HasKey($"FirstComplete_00{_levelNumber}"))
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,$"Level00{_levelNumber}");
            PlayerPrefs.SetInt($"FirstComplete_00{_levelNumber}",1);
            FirebaseAnalytics.LogEvent($"Complete_Level00{_levelNumber}");
            //FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd, LevelParameters);
        }
        //GameAnalytics.NewDesignEvent($"Complete_00{_levelNumber}");
        
    }
    
    private void OnLevelFail()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail,$"Level00{_levelNumber}");
        FirebaseAnalytics.LogEvent($"Fail_Level00{_levelNumber}");
        //FirebaseAnalytics.LogEvent($"Level00{_levelNumber}_Fail", LevelParameters);
    }

    


    
}
*/