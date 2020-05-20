using System;

namespace Portal.Structure {

    public interface IDependencyLibarySetup : IDependencyLibrary {

        void Include<TInterface>(object item);

        void MarkForBuild<TInterface>(Type item);

        void Build();

    }

}
