using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectAssetList", menuName = "GameObjectAssetList")]
public class GameObjectAssetList : ScriptableObject
{
    public List<GameObjectAssets> _assetsList = new List<GameObjectAssets>();

    [System.Serializable]
    public class GameObjectAssets
    {
        public string assetsName;
        public int count;
        public GameObject[] prefab;
    }
}
