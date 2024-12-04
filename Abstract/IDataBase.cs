using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstract
{
    public interface IDataBase
    {
        public Task SaveAsync(IEnumerable<INetData> data);
    }
}
