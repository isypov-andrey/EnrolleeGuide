using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolleeGuide.Stores
{
    public interface IStore<TItem>
    {
        Task<ICollection<TItem>> GetAllAsync();

        Task<TItem> GetAsync(TItem itemModel);

        Task SaveAsync(TItem itemModel);

        Task DeleteAsync(TItem itemModel);
    }
}
