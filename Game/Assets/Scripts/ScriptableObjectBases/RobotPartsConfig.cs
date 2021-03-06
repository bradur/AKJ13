using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "RobotPartsConfig", menuName = "New RobotPartsConfig")]
public class RobotPartsConfig : ScriptableObject
{
    [SerializeField]
    List<RobotPartConfig> partConfigs;
    [SerializeField]
    public float chanceToDrop;
    private System.Random random;

    public RobotPartsConfig() {
        random = new System.Random();
    }

    public RobotPartConfig getRandomPart() {
        float sum = 0f;
        List<WeightPair> set = new List<WeightPair>(partConfigs.Count);
        foreach (RobotPartConfig conf in partConfigs.Where(x => x.RobotPartType != RobotPartType.HealthPack)) {
            sum += conf.probability;
            WeightPair p = new WeightPair(sum, conf);
            set.Add(p);
        }
        double randomVal = random.NextDouble() * sum;
        return set.Where(x => x.weight >= randomVal).FirstOrDefault().config;
    }

    public RobotPartConfig getHealthPack() {
        return partConfigs.Where(x => x.RobotPartType == RobotPartType.HealthPack).FirstOrDefault();
    }

    private class WeightPair {
        public WeightPair(float w, RobotPartConfig conf) {
            weight = w;
            config = conf;
        }
        public float weight;
        public RobotPartConfig config;
    }
}

public enum RobotPartType {
    None,
    RobotPart,
    HealthPack
}

[Serializable]
public class RobotPartConfig {
    public float value = 1f;
    public Sprite sprite;
    public float probability;
    public RobotPartType RobotPartType = RobotPartType.RobotPart;
}