using UnityEngine;
using UnityEngine.UI;
using ServiceLocator.Main;
using ServiceLocator.Events;

namespace ServiceLocator.UI
{
    public class MapButton : MonoBehaviour
    {
        [SerializeField] private int MapId;

        private EventService event_service;

        //private void Start() => GetComponent<Button>().onClick.AddListener(OnMapButtonClicked);

        public void Init(EventService event_service)
        {
            this.event_service = event_service;

            GetComponent<Button>().onClick.AddListener(OnMapButtonClicked);
        }

        // To Learn more about Events and Observer Pattern, check out the course list here: https://outscal.com/courses
        private void OnMapButtonClicked() =>  event_service.OnMapSelected.InvokeEvent(MapId);
    }
}