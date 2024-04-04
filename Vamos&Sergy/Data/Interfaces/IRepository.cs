using PracticeModul.Models;
using System.Data;

namespace PracticeModul.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T hero);
        IEnumerable<T> Read();
        T? Read(string id);
        T? ReadFromName(string name);
        void Update(T item);
        void Delete(string id);
    }
}