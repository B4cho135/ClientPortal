using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApiResources
{
    public interface IRecordsApiClient
    {
        public IClientsApi Clients { get; set; }
        public IRecordingsApi Recordings { get; set; }
        public IAuthenticationResource Users { get; set; }
    }
}
