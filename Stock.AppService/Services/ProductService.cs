using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{
    public class ProductService : BaseService<Product>
    {
        public ProductService(IRepository<Product> repository) : base(repository)
        {    
              
        }

        public new Product Create(Product entity)
        {
            if (this.UniqueName(entity.Name))
            {
                return base.Create(entity);
            }

            throw new System.Exception("The name is already in use");
        }
        private bool UniqueName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }

        public IEnumerable<Product> Search(Expression<Func<Product,bool>> filter)
        {
            return this.Repository.List(filter);
        }

        public void IncreaseStock(Product entity, int value)
        {
            entity.IncreaseStock(value);
        }

        public void DecreaseStock(Product entity, int value)
        {
            entity.DecreaseStock(value);
        }
    }
}