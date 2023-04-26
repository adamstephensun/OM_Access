using System;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class AssetManager : MonoBehaviour
{
    [Serializable]
    public struct WorkAssets{
        public string name;
        public AudioClip infoBoardRec;
        public AudioClip audioDescRec;
        public AudioClip QandARec;
        public AudioClip bio;
        public VideoPlayer videoPlayer;
    }

    public WorkAssets[] assetList;
}
