using System;
using System.Linq;
using System.Collections.Generic;

namespace MVC5Course.Models
{
    public class ClientRepository : EFRepository<Client>, IClientRepository
    {
        public IQueryable<Client> All(bool isAll = false)
        {

            if (isAll)
            {
                return base.All();
            }

            return base.All().Where(p => p.CreditRating < 2 && p.Active == true);
        }

        public IQueryable<Client> SearchKeyword(string keyword, string CreditRating)
        {
            var data = this.All();

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(w => w.FirstName.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(CreditRating))
            {
                data = data.Where(w => w.CreditRating.ToString() == CreditRating);
            }

            return data;
        }

        internal Client Find(int id)
        {
            return this.All().FirstOrDefault(f => f.ClientId == id);
        }

        public override void Delete(Client entity)
        {
            entity.Active = false;
        }
    }

    public interface IClientRepository : IRepository<Client>
    {

    }
}