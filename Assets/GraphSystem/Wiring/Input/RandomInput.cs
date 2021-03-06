using Assets.Klak.Wiring;
using UnityEngine;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Input/Random Input")]
    public class RandomInput : NodeBase
    {
        [SerializeField] private int _seed;
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        public MathParser.RoundMethodTypes roundMethod;

        [Inlet]
        public int Seed
        {
            set
            {
                if (!enabled)
                    return;
                _seed = value;
                InitState();
            }
        }
        [Inlet]
        public float Min
        {
            set
            {
                if (!enabled)
                    return;
                _min = value;
            }
        }
        [Inlet]
        public float Max
        {
            set
            {
                if (!enabled)
                    return;
                _max = value;
            }
        }

        [SerializeField, Outlet] private FloatEvent _floatValueEvent = new FloatEvent();
        [SerializeField, Outlet] private IntEvent _intValueEvent = new IntEvent();

        protected override void Start()
        {
            base.Start();
            InitState();
        }

        private void InitState()
        {
            Random.InitState(_seed);
        }

        protected override void InvokeEvents()
        {
            base.InvokeEvents();
            var value = Random.Range(_min, _max);
            //Debug.Log(value);
            _floatValueEvent.Invoke(value);
            _intValueEvent.Invoke(MathParser.Round(value, roundMethod));
        }
    }
}