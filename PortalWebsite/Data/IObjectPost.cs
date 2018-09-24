using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalWebsite.Data {

    public interface IObjectPost<Obj> {

        Obj GetPostedObject();

    }

}
