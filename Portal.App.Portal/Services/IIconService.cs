using Portal.App.Portal.Models;
using Portal.Data.Web.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.App.Portal.Services {

    public interface IIconService {

        void ValidateIconPost(Icon icon, IPostedFile file);

    }

}
