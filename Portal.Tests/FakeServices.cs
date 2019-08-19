using Portal.Structure.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Tests {

    public static class FakeServices {

        public static DependencyContainer Container { get; }

        static FakeServices() {
            Container = new DependencyContainer();
            Container.BuildFromAssemblies(typeof(FakeServices).Assembly);
        }

    }

}
