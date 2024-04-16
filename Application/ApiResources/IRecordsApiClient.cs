using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApiResources
{
    public interface IRecordsApiClient
    {
        public IClientsResource Clients { get; set; }
        public IRecordingsResource Recordings { get; set; }
        public IAuthenticationResource Users { get; set; }
        public IPhotosResource Photos { get; set; }
        public IConfigurationResource Configuration { get; set; }
    }
}
