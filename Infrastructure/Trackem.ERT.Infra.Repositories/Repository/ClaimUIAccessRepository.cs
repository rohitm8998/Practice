using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackem.ERT.Infra.Contracts;
using Trackem.ERT.Infra.EFCore;
using Trackem.ERT.Infra.EFCore.Entities;

namespace Trackem.ERT.Infra.Repositories.Repository
{
    public class ClaimUIAccessRepository : RepositoryBase<ClaimUIAccess>, IClaimUIAccessRepository
    {
        public ClaimUIAccessRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
