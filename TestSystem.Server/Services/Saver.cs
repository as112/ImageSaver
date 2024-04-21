using Microsoft.AspNetCore.Server.HttpSys;
using System;
using System.Text.Json;
using TestSystem.Core.Models;

namespace TestSystem.Server.Services
{
    public class Saver<T> : ISaver<T> where T : BaseFile
    {
        private static List<T> _data = new();
        public IEnumerable<T> GetList(string directory)
        {
            var filePath = Path.Combine(directory, "list.json");
            if (!File.Exists(filePath)) 
            { 
                return Enumerable.Empty<T>();
            }
            return ReadFileAndDeserialize(filePath);
        }

        public void SaveInfo(T item, string directory)
        {
            var filePath =  Path.Combine(directory, "list.json");
            if (!File.Exists(filePath))
            {
                _data.Add(item);
            }
            else
            {
                _data = ReadFileAndDeserialize(filePath);
                _data.Add(item);
            }
            var json = JsonSerializer.Serialize(_data);
            File.WriteAllText(filePath, json);
        }
        public void DeleteInfo(T item, string directory)
        {
            var filePath = Path.Combine(directory, "list.json");
            
            _data = ReadFileAndDeserialize(filePath);
            _data.Remove(item);
            var json = JsonSerializer.Serialize(_data);
            File.WriteAllText(filePath, json);
        }
        private List<T> ReadFileAndDeserialize(string filePath)
        {
            List<T> result = new List<T>();
            using (StreamReader r = new StreamReader(filePath))
            {
                var list = r.ReadToEnd();
                result = JsonSerializer.Deserialize<List<T>>(list) ?? new();
            }
            return result;
        }
    }
}
