using System;

namespace Portal.Data.Sqlite {

    public interface ILogger {

        void Log(string context, string message);

        void Log(Exception e);

    }

}
