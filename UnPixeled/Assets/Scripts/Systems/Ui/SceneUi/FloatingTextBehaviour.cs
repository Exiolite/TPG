using System;
using System.Globalization;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems.Ui.SceneUi
{
    public class FloatingTextBehaviour : MonoBehaviour
    {
        private Vector3 _spawnPosition;
        private Animation _animation;
        
        
        
        public void SpawnFloatingText(Transform parent, float value, int colorIndex)
        {
            var textColor = GameManager.instance.damageColor[colorIndex];
            var go = Instantiate(this, new Vector3(_spawnPosition.x + Random.Range(0, 5), _spawnPosition.y + 5, _spawnPosition.z + Random.Range(0, 5)), Quaternion.Euler(60, 45, 0));
            go.GetComponent<TextMesh>().text = value.ToString(CultureInfo.InvariantCulture);
            go.GetComponent<TextMesh>().color = textColor;
        }


        
        private void Awake()
        {
            _spawnPosition = transform.position;
            _animation = GetComponent<Animation>();
        }

        private void Update()
        {
            if (_animation.isPlaying == false) Destroy(gameObject);
        }
    }
}
