using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    [RequireComponent(typeof(MeshFilter))]
    public class SpawnCrateController : MonoBehaviour
    {
        [SerializeField] private GameObject _cratePrefab;
        
        private float _spawnTime = 8f;
        private float _xDim;
        private float _zDim;
        private Mesh _mesh;
        private Vector3 _center;
        private GameObject _previousCrate;

        private void Start()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
            _xDim = _mesh.bounds.size.x;
            _zDim = _mesh.bounds.size.z;
            _center = transform.position;
        }

        private void Update()
        {
            if (_spawnTime <= 0)
            {
                _spawnTime = Random.Range(20f, 40f);
                if (_previousCrate != null) return;
                SpawnInside(_cratePrefab);
            }
            _spawnTime -= Time.deltaTime;
        }
        
        private void SpawnInside(GameObject spawnObject){
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(-_xDim, _xDim) + _center.x;
            pos.y = 0f;
            pos.z = Random.Range(-_zDim, _zDim) + _center.z;
            _previousCrate = Instantiate(spawnObject, pos, Quaternion.identity);
        }
    }
}
