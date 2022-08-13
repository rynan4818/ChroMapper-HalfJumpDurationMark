using UnityEngine;

namespace ChroMapper_HalfJumpDurationMark.Component
{
    public class HJDmarkController : MonoBehaviour
    {
        public float _hjd;
        public GameObject _markObject;
        public void Start()
        {
            var njs = BeatSaberSongContainer.Instance.DifficultyData.NoteJumpMovementSpeed;
            var offset = BeatSaberSongContainer.Instance.DifficultyData.NoteJumpStartBeatOffset;
            var bpm = BeatSaberSongContainer.Instance.Song.BeatsPerMinute;
            _hjd = 4f;
            var num = 60 / bpm;
            while (njs * _hjd * num > 17.999f)
                _hjd /= 2;
            _hjd += offset;
            if (_hjd < 0.25f) _hjd = 0.25f;

            var ifObject = GameObject.Find("Note Interface Scaling Offset/Interface");
            var noteIfOffset = GameObject.Find("Note Grid/Note Interface Scaling Offset");
            _markObject = Instantiate(ifObject);
            _markObject.transform.SetParent(noteIfOffset.transform);
            Destroy(_markObject.gameObject.GetComponent<VisualFeedback>());
            _markObject.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            _markObject.transform.localScale = new Vector3(1f, 1f, 0.1f);
            RefreshPositions();
            EditorScaleController.EditorScaleChangedEvent += EditorScaleUpdated;
            UIMode.UIModeSwitched += ChangeUIMode;
        }
        private void OnDestroy()
        {
            UIMode.UIModeSwitched -= ChangeUIMode;
            EditorScaleController.EditorScaleChangedEvent -= EditorScaleUpdated;
        }

        private void ChangeUIMode(UIModeType uiMode)
        {
            if (uiMode == UIModeType.Normal)
                _markObject.SetActive(true);
            else
                _markObject.SetActive(false);
        }
        private void EditorScaleUpdated(float obj) => RefreshPositions();

        private void RefreshPositions()
        {
            _markObject.transform.localPosition = new Vector3(0, -_hjd * EditorScaleController.EditorScale, 0.5f);
        }
    }
}
