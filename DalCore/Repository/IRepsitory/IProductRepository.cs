using Microsoft.AspNetCore.Mvc.Rendering;
using ModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCore.Repository.IRepsitory
{
  public  interface IProductRepository: IRepository<Product>
    {
        void Update(Product obj);
        IEnumerable<SelectListItem> GetAllDropDownList(string obj);
    }
}
