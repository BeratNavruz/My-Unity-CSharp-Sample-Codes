using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyancyObject : MonoBehaviour
{
    public Transform[] Floaters;
    public float UnderWaterDrag = 3f;
    public float UnderWaterAngularDrag = 1f;
    public float AirDrag = 0f;
    public float AirAngularDrag = .05f;
    public float FloatingPower = 15f;
    public float WaterHeight = 0f;
    public float WaterHeightRangeValue = .1f;
    float _currentWaterHeight;

    private Rigidbody _rb;

    private int _floatersUnderWater;
    private bool _underWater;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        FloatingPower = _rb.mass * 30 / Floaters.Length;
        _currentWaterHeight = WaterHeight;
    }

    private void FixedUpdate()
    {
        _floatersUnderWater = 0;
        for (int i = 0; i < Floaters.Length; i++)
        {
            _currentWaterHeight = Random.Range(WaterHeight - WaterHeightRangeValue, WaterHeight + WaterHeightRangeValue);

            float difference = Floaters[i].position.y - _currentWaterHeight;

            if (difference < 0)
            {
                _rb.AddForceAtPosition(Vector3.up * FloatingPower * Mathf.Abs(difference), Floaters[i].position, ForceMode.Force);
                _floatersUnderWater++;
                if (!_underWater)
                {
                    _underWater = true;
                    SwitchState(true);
                }
            }
        }

        if (_underWater && _floatersUnderWater == 0)
        {
            _underWater = false;
            SwitchState(false);
        }
    }

    void SwitchState(bool isUnderWater)
    {
        if (isUnderWater)
        {
            _rb.drag = UnderWaterDrag;
            _rb.angularDrag = UnderWaterAngularDrag;
        }
        else
        {
            _rb.drag = AirDrag;
            _rb.angularDrag = AirAngularDrag;
        }
    }
}
