using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RandomCurve : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve SizeCurve;


    // Update is called once per frame
    public float ReturnRandom(float time)
    {
        float value;

        float CurveWeightedRandom(AnimationCurve curve)
        {
                return curve.Evaluate(Random.value) * time;
        }
        value = CurveWeightedRandom(SizeCurve);
        Debug.Log(value);
        return value;


        
    }

}
