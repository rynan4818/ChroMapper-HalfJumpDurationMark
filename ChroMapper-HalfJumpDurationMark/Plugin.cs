using UnityEngine;
using UnityEngine.SceneManagement;
using ChroMapper_HalfJumpDurationMark.Component;

namespace ChroMapper_HalfJumpDurationMark
{
    [Plugin("Half Jump Duration Mark")]
    public class Plugin
    {
        public static HJDmarkController hjdMarkController;
        [Init]
        private void Init()
        {
            SceneManager.sceneLoaded += SceneLoaded;
            Debug.Log("Half Jump Duration Mark Plugin has loaded!");
        }

        [Exit]
        private void Exit()
        {
            Debug.Log("Half Jump Duration Mark:Application has closed!");
        }
        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex != 3) // Mapper scene
                return;
            if (hjdMarkController != null && hjdMarkController.isActiveAndEnabled)
                return;
            hjdMarkController = new GameObject("HalfJumpDurationMark").AddComponent<HJDmarkController>();
        }
    }
}
