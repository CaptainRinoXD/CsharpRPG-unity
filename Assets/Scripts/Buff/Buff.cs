using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{
   public abstract void Apply(GameObject target); //khoi tao abs de apply len cac buff
}
