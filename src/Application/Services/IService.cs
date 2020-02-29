using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Services
{
    public interface IService<TBusinessEntity> 
        where TBusinessEntity :class 
    {
        TBusinessEntity Get(int id);

        IEnumerable<TBusinessEntity> GetAll();

        IEnumerable<TBusinessEntity> GetAllWhere(Expression<Func<TBusinessEntity, bool>> exp);

        TBusinessEntity Add(TBusinessEntity businessEntity);

        void Delete(int id);

        TBusinessEntity Edit(TBusinessEntity businessEntity);
    }
}
