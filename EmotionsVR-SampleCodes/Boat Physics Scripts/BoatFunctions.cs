using UnityEngine;

public class BoatFunctions : MonoBehaviour
{
    Rigidbody _rb;
    BuoyancyObject _buoyancyObject;

    #region Waterfall Trigger Variables
    [Header("-----------Waterfall Trigger Variables-----------")]
    [SerializeField] Transform _waterfallTargetTransform;
    [SerializeField] float _newWaterHeight = 12.6f;
    [SerializeField] private float _newWaterHeightRangeValue = .1f;
    [SerializeField] float _waterfallForceAmount = 10f;
    #endregion

    #region Hitting The Rock Trigger Variables
    [Header("-----------Hitting The Rock Trigger Variables-----------")]
    [SerializeField] Transform _hitTriggerTargetTransform;
    [SerializeField] float __hitTriggerForceAmount = 10f;
    #endregion

    #region Hitting The Rock Collision Variables
    [Header("-----------Hitting The Rock Collision Variables-----------")]
    [SerializeField] Transform _hitCollisionTargetTransform;
    [SerializeField] float __hitCollisionForceAmount = 10f;
    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _buoyancyObject = GetComponent<BuoyancyObject>();
    }

    public void OnWaterfallTrigger(GameObject triggerObj)
    {
        triggerObj.SetActive(false);

        Vector3 direction = (_waterfallTargetTransform.position - transform.position).normalized;
        _rb.AddForce(direction * _waterfallForceAmount, ForceMode.Impulse);
        _buoyancyObject.WaterHeight = _newWaterHeight;
        _buoyancyObject.WaterHeightRangeValue = _newWaterHeightRangeValue;
    }

    public void OnHittingTheRockTrigger(GameObject triggerObj)
    {
        triggerObj.SetActive(false);

        Vector3 direction = (_hitTriggerTargetTransform.position - transform.position).normalized;
        _rb.AddForce(direction * __hitTriggerForceAmount, ForceMode.Impulse);
    }

    public void OnHittingTheRockCollision()
    {
        Vector3 direction = (_hitCollisionTargetTransform.position - transform.position).normalized;
        _rb.AddForce(direction * __hitCollisionForceAmount, ForceMode.Impulse);
    }
}
