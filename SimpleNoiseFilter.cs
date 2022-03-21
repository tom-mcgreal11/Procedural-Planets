using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter
{
    Noise noise = new Noise();
    NoiseSettings settings;

    public SimpleNoiseFilter(NoiseSettings settings){
        this.settings = settings;
    }
    
    public float Evaluate(Vector3 point){
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amp = 1;
        
        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.centre);
            noiseValue += (v + 1) * .5f * amp;
            frequency *= settings.rough;
            amp *= settings.persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }


}
