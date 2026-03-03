using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
    [CreateAssetMenu(menuName = "AAA/Menu")]
    public sealed class Menu : ScriptableObject
{   public List<string> names=new List<string>();
    public List<int> wuqis=new List<int>();
    public List<string> scenes=new List<string>();
    public string name1;
    public int wuqi;
    public string scene;
    
}
