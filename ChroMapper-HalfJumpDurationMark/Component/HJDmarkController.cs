using UnityEngine;
using ChroMapper_HalfJumpDurationMark.HarmonyPatches;

namespace ChroMapper_HalfJumpDurationMark.Component
{
    public class HJDmarkController : MonoBehaviour
    {
        public float _njs;
        public float _songStartBeatOffset;
        public float _bpm;
        public float _hjd;
        public float _increasedHJDFactor = 1f;
        public GameObject _markObject;

        public void Start()
        {
            _njs = BeatSaberSongContainer.Instance.MapDifficultyInfo.NoteJumpSpeed;
            _songStartBeatOffset = BeatSaberSongContainer.Instance.MapDifficultyInfo.NoteStartBeatOffset;
            _bpm = BeatSaberSongContainer.Instance.Info.BeatsPerMinute;
            _hjd = SpawnParameterHelper.CalculateHalfJumpDuration(_njs, _songStartBeatOffset, _bpm);

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
            NJSEventGridContainer_UpdateHJDLinePatch.OnUpdateHJDLine += UpdateHJDmark;
            NJSEventGridContainer_UpdateDisplayHJDLinePatch.OnUpdateDisplayHJDLine += UpdateDisplayHJDmark;
        }
        private void OnDestroy()
        {
            UIMode.UIModeSwitched -= ChangeUIMode;
            EditorScaleController.EditorScaleChangedEvent -= EditorScaleUpdated;
            NJSEventGridContainer_UpdateHJDLinePatch.OnUpdateHJDLine -= UpdateHJDmark;
            NJSEventGridContainer_UpdateDisplayHJDLinePatch.OnUpdateDisplayHJDLine -= UpdateDisplayHJDmark;
        }

        private void ChangeUIMode(UIModeType uiMode)
        {
            if (!Settings.Instance.DisplayHJDLine)
                return;
            if (uiMode == UIModeType.Normal)
                _markObject.SetActive(true);
            else
                _markObject.SetActive(false);
        }
        private void EditorScaleUpdated(float obj) => RefreshPositions();

        private void RefreshPositions()
        {
            _markObject.transform.localPosition = new Vector3(0, -_hjd * EditorScaleController.EditorScale * _increasedHJDFactor, 0.5f);
        }

        private void UpdateHJDmark(float currentNJS)
        {
            if (currentNJS > _njs)
                _increasedHJDFactor = 1f;
            else
                _increasedHJDFactor = _njs / currentNJS;
            RefreshPositions();
        }

        private void UpdateDisplayHJDmark(bool value)
        {
            _markObject.SetActive(value);
        }
    }
}
