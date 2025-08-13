using ChroMapper_HalfJumpDurationMark.Component;
using HarmonyLib;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChroMapper_HalfJumpDurationMark
{
    [Plugin("Half Jump Duration Mark")]
    public class Plugin
    {
        public static HJDmarkController hjdMarkController;
        public static Harmony _harmony;
        public const string HARMONY_ID = "com.github.rynan4818.ChroMapper-HalfJumpDurationMark";
        [Init]
        private void Init()
        {
            _harmony = new Harmony(HARMONY_ID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            SceneManager.sceneLoaded += SceneLoaded;
            Debug.Log("Half Jump Duration Mark Plugin has loaded!");
        }

        [Exit]
        private void Exit()
        {
            _harmony.UnpatchSelf();
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
