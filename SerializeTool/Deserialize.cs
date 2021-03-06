﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;



namespace SerializeTool
{
    public static class Deserialize
    {
        public static T DeSerializeByByteArr<T>(byte[] arr) where T:class
        {

            try
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Binder = new UBinder();
                MemoryStream stream = new MemoryStream(arr);
                stream.Position = 0;
                object obj = formatter.Deserialize(stream);
                T t = (T)obj;
                return t;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }
    }

    public class UBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Assembly ass = Assembly.GetExecutingAssembly();//GetEntryAssembly();
            return ass.GetType(typeName);
        }
    }
}
