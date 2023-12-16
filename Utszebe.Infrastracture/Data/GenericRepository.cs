using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseKeys
    {
        //private readonly StoreContext _ctx;

        //public GenericRepository(StoreContext ctx)
        //{
        //    _ctx = ctx;
        //}
        //public async Task<T> GetDataAsync()
        //{
        //    //TEMPORARY
        //    int id = 1;
        //    return await _ctx.Set<T>().FindAsync(id);
        //}
    }
}
