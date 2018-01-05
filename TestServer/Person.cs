using System;
using System.Collections.Generic;

[Serializable]
public class Person
{
    public string name;
    public int age;
    public Dictionary<string, string> dic = new Dictionary<string, string>();

    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
        dic.Add(name, "666");
    }
}
