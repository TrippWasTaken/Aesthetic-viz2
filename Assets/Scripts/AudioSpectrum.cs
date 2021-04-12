using System;
using System.Collections;
using UnityEngine;
[RequireComponent (typeof (AudioSource))]

public class AudioSpectrum : MonoBehaviour{

    AudioSource audioSource;
    public static float[] samples = new float[512];
    public static float[] freqBand = new float[8];
    public static float[] bandBuff = new float[8];
    float[] bufferDec = new float[8];
    float[] bandHeight = new float[8];
    


    // Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update(){
        GetSpectrumAudioSource();
        makeFreqBands();
        bandBuffer();
    }

    void GetSpectrumAudioSource(){
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void makeFreqBands(){
        int count = 0;
        float average = 0;

        for (int i = 0; i < 8; i++){
            int sampleCount = (int)Mathf.Pow(2,i) * 2;

            if(i == 7){
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++){
                average += samples [count] * (count + 1);
                count++;
            }

            average /= count;
            freqBand[i] = average * 10;
        }
    }

    void bandBuffer(){
        for(int i = 0; i < 8; i++){
            if(freqBand[i] > bandBuff [i]){
                bandBuff[i] = freqBand[i];
                bufferDec [i] = 0.005f;
            }
            if(freqBand[i] < bandBuff[i]){
                bandBuff[i] -= bufferDec[i];
                bufferDec [i] *= 1.2f;
            }
        }
    }
}

