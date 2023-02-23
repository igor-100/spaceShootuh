using SpaceShootuh.Configurations;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShootuh.Battle.Environment
{
    public class Level : MonoBehaviour, ILevel
    {
        [SerializeField] private MeshRenderer backgroundRenderer;
        [SerializeField] private float backgroundScrollSpeed = 0.1f;

        [SerializeField] private List<WaveConfig> waveConfigs;

        private Material backgroundMaterial;
        private Vector2 offset;

        public List<WaveConfig> WaveConfigs => waveConfigs;

        private void Awake()
        {
            backgroundMaterial = backgroundRenderer.material;
            offset = new Vector2(0f, backgroundScrollSpeed);
        }

        private void Update()
        {
            backgroundMaterial.mainTextureOffset += offset * Time.deltaTime;
        }
    }
}