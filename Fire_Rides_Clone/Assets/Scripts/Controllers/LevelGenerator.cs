using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject cubePrefab;
      //  [SerializeField] private Transform ;

      private GameObject _lastObj;
     
        private float _maxRange = 1f;
        private float _minRange = -1f;
      [SerializeField]  private int spawnCount = 20;
      [SerializeField] private float offSet = 12f;
      [SerializeField] private GameObject firstObj;
    
      private GameObject currentObj; 




      private void Awake()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                if (i % 2 == 0)
                {
                    if (i == 0)
                    {
                        var firstCreatedObj = Instantiate(cubePrefab,this.transform);
                        firstCreatedObj.transform.position = new Vector3(0,this.transform.position.y,firstObj.transform.position.z+firstObj.transform.localScale.z);
                        _lastObj = firstCreatedObj;
                    }
                    else
                    {
                        var obj = Instantiate(cubePrefab,this.transform);
                        obj.transform.position = new Vector3(0, Random.Range(_minRange, _maxRange)+_lastObj.transform.position.y,_lastObj.transform.localScale.z + _lastObj.transform.position.z);
                        _lastObj = obj;
                    }
                }
                else
                {
                    var obj = Instantiate(cubePrefab,this.transform);
                    obj.transform.position =
                        new Vector3(0,_lastObj.transform.position.y-offSet, _lastObj.transform.position.z);
                }
            }
        }
    }
}