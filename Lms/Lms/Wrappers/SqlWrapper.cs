using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Wrappers
{
    public class SqlWrapper : ISqlWrapper
    {

        public static string ConnectionString;

        public SqlWrapper()
        {
            ConnectionString = "LMSConnectionString";
        }

        public Task<List<T>> Query<T>(string sql)
        {
            throw new System.NotImplementedException();
        }

        


    }
}
