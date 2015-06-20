using System;
using System.Collections;
using System.Net;   
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


namespace UnityStandardAssets._2D
{
    public class Triggers: MonoBehaviour
    {
        private static int deathCount;
        private GameObject AdPanel;
        private GameObject AdPanel2;
        private CharacterController2D _controller;
        private CharacterController2DDriver _controllerDriver;
        public Text playerTime;
        public Text bigText;
        public Text companyLogo;
        public Text littleText;
        public string UnityGameId;
        
        void Awake()
        {
            _controller = GetComponent<CharacterController2D>();
            _controllerDriver = GetComponent<CharacterController2DDriver>();

            if (_controller)
            {
                _controller.onTriggerEnterEvent += new Action<Collider2D>(_controller_onTriggerEnterEvent);
            }

            #region Ads stuff
            if (Advertisement.isSupported)
            {
                Advertisement.allowPrecache = true;
                Advertisement.Initialize(UnityGameId, false);
            }

            AdPanel = GameObject.Find("AdPanel1");
            AdPanel.SetActive(false);

            AdPanel2 = GameObject.Find("AdPanel2");
            AdPanel2.SetActive(false);

            if (deathCount > 0)
            {
                GameObject.Find("PowerUp1").transform.position = new Vector3(-54f, 10.27f, 0);
            }

            #endregion
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
                case ("PowerUp1"):
                    PowerUp(obj.gameObject, true);
                    break;
                case ("PowerUp2"):
                    PowerUp(obj.gameObject, false);
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
                UnityInterstitialAd();
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

        private void RESTPanel()
        {
            using (var client = new WebClient())
            {
                client.Proxy = WebRequest.DefaultWebProxy;

                var jsonResponse = client.DownloadString("http://radsurge.azurewebsites.net/rest/coupons").Replace("}", "").Replace("\"", ""); ;
                var ss = jsonResponse.Split(new char[] { ',' });

                string big_message = ss[0].Split(new char[] { ':' })[1];
                string little_message = ss[1].Split(new char[] { ':' })[1];
                string company_logo = ss[2].Split(new char[] { ':' })[1];

                //Do something here 
                bigText.text = big_message;
                companyLogo.text = company_logo;
                littleText.text = little_message;
            }

        }

        public void PowerUp(GameObject go, bool fix)
        {
            //Fixed panel
            if (fix)
            {
                Destroy(go);
                StartCoroutine(PowerUp());
                AdPanel.SetActive(true);
            }
            //REST panel
            else
            {
                Destroy(go);
                StartCoroutine(PowerUp());
                AdPanel2.SetActive(true);
                RESTPanel();
            }
        }

        private IEnumerator PowerUp()
        {
            while (!_controller.isGrounded)
            {
                yield return null;
            }

            Stop();
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

        public void UnityInterstitialAd()
        {
            if (Advertisement.isReady())
            {
                //Advertisement.Show(null, new ShowOptions
                //{
                //    pause = true,
                //    resultCallback = result =>
                //    {
                //        Application.LoadLevel(0);
                //    }
                //});
                ShowOptions options = new ShowOptions { pause = true, resultCallback = HandleShowResult };
                Advertisement.Show(null, options);
            }
            else
            {
                Application.LoadLevel(0);
            }
        }

        private void HandleShowResult(ShowResult result)
        {
            print("Unity Ad Result: " + result); 
            Application.LoadLevel(0);
        }

    }
}
