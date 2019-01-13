using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Website.Data {

    public interface IObjectPost<Obj> {

        Obj GetPostedObject();

    }

}
