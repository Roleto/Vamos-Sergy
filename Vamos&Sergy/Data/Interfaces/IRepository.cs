using Vamos_Sergy.Models;
using System.Data;

namespace Vamos_Sergy.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T hero);
        IEnumerable<T> Read();
        T? Read(string id);
        T? ReadFromName(string name);
        T? ReadFromOwner(string id);
        void Update(T item);
        void Delete(string id);
    }
}