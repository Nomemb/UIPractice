using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public interface ILoader<Key, Item>
{
    Dictionary<Key, Item> MakeDic();
    bool Validate();
}

public class DataManager
{
    public StartData Start { get; private set; }
    
    public Dictionary<int, CollectionData> Collections { get; private set; }
    public List<CollectionData> StatCollections { get; private set; }
    public List<CollectionData> WealthCollections { get; private set; }
    public List<CollectionData> LevelCollections { get; private set; }
}