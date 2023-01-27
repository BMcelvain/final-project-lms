using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Wrappers
{
    public interface ISqlWrapper
    {
        Task<List<T>> Query<T>(string sql);
    }
}
