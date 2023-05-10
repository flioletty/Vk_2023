using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkDataBase.Repositories
{
    public class BaseRepo
    {
        protected readonly VkCaseDbContext _context;

        public BaseRepo(VkCaseDbContext context)
        {
            _context = context;
        }

    }
}
