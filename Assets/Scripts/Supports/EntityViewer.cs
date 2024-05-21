using ScrollShooter.Supports;
using TMPro;
using UnityEngine;

namespace ScrollShooter.EntityScripts
{
    public class EntityViewer : MonoBehaviour
    {
        public Bar HP_bar;
        public TextMeshProUGUI _ammoCounter;

        private Entity _entity;

        private void Awake()
        {
            try
            {
                _entity = GetComponent<Entity>();
                _entity.OnSetHealthView += HP_bar.SetValueBar;
                _entity.OnSetAmmoView += SetAmmoCountInCounterView;
            }
            catch
            {
                Debug.LogError($"hp bar not found or null referens");
            }
        }

        private void SetAmmoCountInCounterView(float currentAmmo, float maxAmmo)
        {
            _ammoCounter.text = $"{currentAmmo} / {maxAmmo}";
        }
    }
}