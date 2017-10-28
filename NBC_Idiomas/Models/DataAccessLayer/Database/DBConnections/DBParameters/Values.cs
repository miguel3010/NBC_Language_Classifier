namespace DataAccessLayer.Database.DBConnections.DBParameters
{
    using System.Collections.Generic;

    public class Value
    {
        private List<node> list = new List<node>();
        public void add(string key, string value)
        {
            node j = new node();
            j.key = key;
            j.value = value;
            list.Add(j);
        }

        public List<node> getList()
        {
            return list;
        }
    }
}
