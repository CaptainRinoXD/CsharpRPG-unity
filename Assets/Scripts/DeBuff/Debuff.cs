using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff : ScriptableObject
{
   public abstract void Apply(GameObject target); //khoi tao abs de apply len cac buff
}
