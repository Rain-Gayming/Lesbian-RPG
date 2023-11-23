/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Linq;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.SceneTools
{
    [InitializeOnLoad]
    public static class ViewStateDrawer
    {
        private static Camera[] cameras;
        private static Texture2D emptyTexture;
        private static Texture2D eyeTexture;
        private static double lastUpdateTime = double.MinValue;
        private static ViewStateWrapper[] states;
        private static GUIContent viewContent;
        private static ViewState[] viewStates;
        private static GUIStyle viewStyle;

        static ViewStateDrawer()
        {
            SceneViewManager.AddListener(DrawViewStates, SceneViewOrder.normal, true);
        }

        private static void DrawViewStates(SceneView sceneView)
        {
            
        }

        internal class ViewStateWrapper
        {
            public ViewState state;
            public Camera camera;
            public Vector2 screenPoint;
            public float distance;
            public Vector3 position;

            public string title
            {
                get
                {
                    if (state != null) return state.title;
                    if (camera != null) return camera.gameObject.name;
                    return "";
                }
            }

            public void Dispose()
            {
                camera = null;
                state = null;
            }

            public void SetTo(SceneView view)
            {
                if (state != null)
                {
                    view.orthographic = state.is2D;
                    view.pivot = state.pivot;
                    view.size = state.size;
                    view.rotation = state.rotation;
                }
                else if (camera != null)
                {
                    SceneViewHelper.AlignViewToCamera(camera, view);
                }
            }
        }
    }
}