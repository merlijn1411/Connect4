using System.Collections.Generic;
using UnityEngine;

public class CoinPrefabIndex : MonoBehaviour
{
    public static CoinPrefabIndex Instance;
    
    [SerializeField] private List<GameObject> coinPrefabPrefabs;
    public  List<GameObject> coinPrefabIndex => coinPrefabPrefabs;

    private void Awake()
    {
        Instance = this;
    }
}
