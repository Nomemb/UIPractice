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
    
    public Dictionary<int, TextData> Texts { get; private set; }
    public Dictionary<int, CollectionData> Collections { get; private set; }
    public List<CollectionData> StatCollections { get; private set; }
    public List<CollectionData> WealthCollections { get; private set; }
    public List<CollectionData> LevelCollections { get; private set; }

    public void Init()
    {
        Start = LoadSingleXml<StartData>("StartData");


        Texts = LoadXml<TextDataLoader, int, TextData>("TextData").MakeDic();
        
        // Collection
        var collectionLoader = LoadXml<CollectionDataLoader, int, CollectionData>("CollectionData");
        StatCollections = collectionLoader._collectionData.Where(c => c.type == CollectionType.Stat).ToList();
        WealthCollections = collectionLoader._collectionData.Where(c => c.type == CollectionType.Wealth).ToList();
        LevelCollections = collectionLoader._collectionData.Where(c => c.type == CollectionType.Level).ToList();

        Collections = collectionLoader.MakeDic();
    }

    /// <summary>
    /// XML 파일 로드 후 역직렬화함.
    /// </summary>
    /// <param name="name">데이터 파일 이름</param>
    /// <typeparam name="Item"></typeparam>
    /// <returns></returns>
    private Item LoadSingleXml<Item>(String name)
    {
        XmlSerializer xs = new XmlSerializer(typeof(Item));
        TextAsset textAsset = Resources.Load<TextAsset>("Data/" + name);
        // 자동으로 리소스 해제
        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
            return (Item)xs.Deserialize(stream);
    }

    /// <summary>
    /// XML 파일 로드 후 역직렬화
    /// </summary>
    /// <param name="name">데이터 파일 이름</param>
    /// <typeparam name="Loader"> ILoader 구현해야 함</typeparam>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Item"></typeparam>
    /// <returns></returns>
    private Loader LoadXml<Loader, Key, Item>(string name) where Loader : ILoader<Key, Item>, new()
    {
        XmlSerializer xs = new XmlSerializer(typeof(Loader));
        TextAsset textAsset = Resources.Load<TextAsset>("Data/" + name);
        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
            return (Loader)xs.Deserialize(stream);
    }
}