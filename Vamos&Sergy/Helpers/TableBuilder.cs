using System.Reflection;

namespace PracticeModul.Helpers
{
    public class ShowTableAttribute : Attribute
    {
    }
    public class TableBuilder
    {
        public string BuildTable<T>(IEnumerable<T> items)
        {
            string output = "";
            output += "<table><tr>";

            var props = typeof(T).GetProperties().Where(t => t.GetCustomAttribute<ShowTableAttribute>() != null);
            foreach (var propname in props.Select(t => t.Name))
            {
                output += "<th>" + propname + "</th>";
            }
            output += "</tr>";

            foreach (var item in items)
            {
                output += "<tr>";
                foreach (var prop in props)
                {
                    output += "<td>" + prop.GetValue(item) + "</td>";
                }
                output += "</tr>";
            }
            return output;
        }
    }
}
