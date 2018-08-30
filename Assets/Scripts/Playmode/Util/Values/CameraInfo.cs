using UnityEngine;

namespace Playmode.Util.Values
{
    public static class CameraInfo
    {
        public static float Height => 2f * Camera.main.orthographicSize;
        public static float Width => Height * Camera.main.aspect;
    }
}


