using System;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HeartManager : MonoBehaviour
    {
        private int _heartCount;
        private TextMeshProUGUI _heartCountLabel;
        private TextMeshProUGUI _heartTimerLabel;
        
        //Timer
        private DateTime _unlockTime;
        private TimeSpan _remainingTime;
        private readonly TimeSpan _zeroTime = TimeSpan.FromSeconds(0.0);
        private bool _canCountDown;
        private float _nextCheck;
        private bool _onCoolDown;

        private void Start()
        {
            CacheObjects();
            // EventManager.Instance.ONHeartDecrease += ONHeartDecrease;
        }
        
        private void CacheObjects()
        {
            var parent = transform.GetChild(0);
            _heartCountLabel = parent.GetChild(0).GetComponent<TextMeshProUGUI>();
            _heartTimerLabel = parent.GetChild(1).GetComponent<TextMeshProUGUI>();
            SetValues();
        }

        private void ONHeartsRefilled()
        {
            _heartCount = 5;
            _heartCountLabel.text = _heartCount.ToString();
            ES3.Save("HeartCount", _heartCount);
            _heartTimerLabel.text = "FULL";
        }

        private void ONHeartDecrease()
        {
            _heartCount -= 1;
            _heartCountLabel.text = _heartCount.ToString();
            ES3.Save("HeartCount", _heartCount);
            if (_heartCount <= 5)
            {
                //NotificationManager.Instance.RefillNotification();
            }
        }

        private void ONRanOutOfHeart()
        {
            _canCountDown = false;
            //EventManager.Instance.OnONRanOutOfHeart();
        }

        private void ONHeartTimerCompleted()
        {
            _heartCount += 1;
            _heartCountLabel.text = _heartCount.ToString();
            ES3.Save("HeartCount", _heartCount);
            if (_heartCount == 5)
            {
                _heartTimerLabel.text = "FULL";
            }
            
        }

        

        private void SetValues()
        {
            _heartCount = ES3.Load("HeartCount", 5);
            _heartCountLabel.text = _heartCount.ToString();
            if (_heartCount == 5)
            {
                _heartTimerLabel.text = "FULL";
            }
            else if(_heartCount is > 0 and < 5)
            {
                EvaluateTimer();
                
            }else if (_heartCount == 0)
            {
                ONRanOutOfHeart();
            }
        }
        
        private void EvaluateTimer()
        {
            if (!ES3.KeyExists("HeartTimer"))
            {
                ES3.Save("HeartTimer", DateTime.Now.AddMinutes(30));
            }
            _unlockTime = ES3.Load("HeartTimer", DateTime.Now.AddMinutes(30));
            _remainingTime = DateTime.Now.Subtract(_unlockTime);
            _heartTimerLabel.text = _remainingTime.ToString(@"mm\:ss");
            _canCountDown = true;
        }
        
        private void Update()
        {
            if (!_canCountDown) return;
            if (Time.time < _nextCheck) return;
            _nextCheck = Time.time + .5f;
            TimerCountDown();
            CheckUnlockStatus();
        }

        private void CheckUnlockStatus()
        {
            ES3.Save("LastGameTime",DateTime.Now);
            var index = _remainingTime.CompareTo(_zeroTime);
            if (index < 0) return;
            _canCountDown = false;
            _heartCount += 1;
            ES3.Save("HeartCount", _heartCount);
            ES3.DeleteKey("HeartTimer");
            SetValues();
        }

        private void TimerCountDown()
        {
            _remainingTime = DateTime.Now.Subtract(_unlockTime);
            _heartTimerLabel.text = _remainingTime.ToString(@"mm\:ss");
        }
    }
}
