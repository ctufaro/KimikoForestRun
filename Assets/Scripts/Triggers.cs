using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


namespace UnityStandardAssets._2D
{
    public class Triggers: MonoBehaviour
    {
        private static int deathCount;
        private CharacterController2D _controller;
        private CharacterController2DDriver _controllerDriver;
        public Text playerTime;
        
        void Awake()
        {
            _controller = GetComponent<CharacterController2D>();
            _controllerDriver = GetComponent<CharacterController2DDriver>();

            if (_controller)
            {
                _controller.onTriggerEnterEvent += new Action<Collider2D>(_controller_onTriggerEnterEvent);
            }

            //ads stuff
            Advertisement.Initialize("38662", false);
        }

        void _controller_onTriggerEnterEvent(Collider2D obj)
        {
            switch (obj.gameObject.name)
            {
                case ("RoundOverZone"):
                    Stop();
                    break;
                case ("Killzone"):
                    Killed();
                    break;
            }
        }

        public void Killed()
        {
          
            MainScript._resume = true;
            PlayerPrefs.SetString("CurrentTime", playerTime.text);
            PlayerPrefs.SetString("BestTime", GetBestTime());
            deathCount++;

            if (deathCount % 3 == 0)
            {
                if (Advertisement.isReady())
                {
                    Advertisement.Show(null, new ShowOptions
                    {
                        pause = true,
                        resultCallback = result =>
                        {
                            Application.LoadLevel(0);
                        }
                    });
                }
                else
                {
                    Application.LoadLevel(0);
                }
            }
            else
            {
                Application.LoadLevel(0);
            }

        }

        public void Stop()
        {
            _controllerDriver.forceStop = true;
        }

        private string GetBestTime()
        {
            int currentTime = PlayerPrefs.HasKey("CurrentTime") ? Convert.ToInt32(PlayerPrefs.GetString("CurrentTime")) : 0;
            int bestTime = PlayerPrefs.HasKey("BestTime") ? Convert.ToInt32(PlayerPrefs.GetString("BestTime")) : 0;

            if (bestTime == 0)
            {
                bestTime = currentTime;
                return bestTime.ToString();
            }
            else if (currentTime < bestTime)
            {
                return bestTime.ToString();
            }
            else
            {
                bestTime = currentTime;
                return bestTime.ToString();
            }

        }
    }
}
