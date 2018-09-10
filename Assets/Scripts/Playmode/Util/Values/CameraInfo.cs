using UnityEngine;

namespace Playmode.Util.Values
{
    public static class CameraInfo
    {
        //BEN_CORRECTION : Warning non géré. Camera.main peut causer un "NullPtr".
        public static float Height => 2f * Camera.main.orthographicSize;
        public static float Width => Height * Camera.main.aspect;
    }
}


