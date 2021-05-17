using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Data.Wrapper.SubModules;

namespace Web.Data.Data.Wrapper
{
    public interface IAPIClient
    {
        public Delete Delete();

        public Fetch Fetch();

        public Patch Patch();

        public Post Post();
    }
}
