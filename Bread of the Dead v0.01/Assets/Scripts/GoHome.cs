using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LevelUp.GrabInteractions
{
    public class GoHome : MonoBehaviour
    {
        XRGrabInteractable m_GrabInteractable;

        [Tooltip("The Transform that the object will return to")]
        [SerializeField] float resetDelayTime;
        protected bool shouldReturnHome { get; set; }
        bool isController;
        public GameObject holster;

        // Start is called before the first frame update
        void Awake()
        {
            m_GrabInteractable = GetComponent<XRGrabInteractable>();
            shouldReturnHome = true;
        }

        private void OnEnable()
        {
            m_GrabInteractable.selectExited.AddListener(OnSelectExit); 
            m_GrabInteractable.selectEntered.AddListener(OnSelect);
        }

        private void OnDisable()
        {
            m_GrabInteractable.selectExited.RemoveListener(OnSelectExit);
            m_GrabInteractable.selectEntered.RemoveListener(OnSelect);
        }

        private void OnSelect(SelectEnterEventArgs arg0) => CancelInvoke("ReturnHome");
        private void OnSelectExit(SelectExitEventArgs arg0) => Invoke(nameof(ReturnHome), resetDelayTime);

        protected virtual void ReturnHome()
        {
            
            if (shouldReturnHome)
                transform.position = holster.transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {

            if (ControllerCheck(other.gameObject))
                return;

            var socketInteractor = other.gameObject.GetComponent<XRSocketInteractor>();

            if (socketInteractor == null)
                shouldReturnHome = true;

            else 
                shouldReturnHome = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if (ControllerCheck(other.gameObject))
                return;

            shouldReturnHome = true;
        }

        bool ControllerCheck(GameObject collidedObject)
        {
            //first check that this is not the collider of a controller
            isController = collidedObject.gameObject.GetComponent<XRBaseController>() != null ? true : false;
            return isController;
        }
    }
}
