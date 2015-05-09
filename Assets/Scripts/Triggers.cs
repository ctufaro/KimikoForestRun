using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Triggers: MonoBehaviour
    {
        private CharacterController2D _controller;
        private CharacterController2DDriver _controllerDriver;
        
        void Awake()
        {
            _controller = GetComponent<CharacterController2D>();
            _controllerDriver = GetComponent<CharacterController2DDriver>();

            // listen to some events for illustration purposes
            //_controller.onControllerCollidedEvent += onControllerCollider;
            if (_controller)
            {
                _controller.onTriggerEnterEvent += new Action<Collider2D>(_controller_onTriggerEnterEvent);
            }
            //_controller.onTriggerExitEvent += onTriggerExitEvent;
        }

        void _controller_onTriggerEnterEvent(Collider2D obj)
        {
            switch (obj.gameObject.name)
            {
                case ("RoundOverZone"):
                    Stop();
                    break;
                case ("Killzone"):
                    Restart();
                    break;
            }
        }

        public void Restart()
        {
            Application.LoadLevel(0);
        }

        public void Stop()
        {
            _controllerDriver.forceStop = true;
        }
    }
}
