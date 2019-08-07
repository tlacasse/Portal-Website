using Portal.Data.ActiveRecord.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Portal.Data.ActiveRecord.Loader {

    public sealed class ActiveRecordLoader {

        public IEnumerable<Assembly> Assemblies { get; }

        public IEnumerable<TableConfig> Tables { get; }

        public ActiveRecordLoader(params Assembly[] Assemblies) {
            this.Assemblies = Assemblies;
            this.Tables = GetTables();
        }

        private IEnumerable<TableConfig> GetTables() {
            List<TableConfig> tables = new List<TableConfig>();
            foreach (Assembly assembly in Assemblies) {
                foreach (Type type in assembly.ExportedTypes) {
                    if (IsActiveRecordType(type)) {
                        TableAttribute attribute = GetTableAttribute(type);
                        yield return new TableConfig() {
                            Name = attribute.Name,
                            Access = attribute.Access,
                            SourceType = type
                        };
                    }
                }
            }
        }

        private bool IsActiveRecordType(Type type) {
            return type.GetInterfaces().Any(t => t is IActiveRecord);
        }

        private TableAttribute GetTableAttribute(Type type) {
            TableAttribute attribute = type.GetCustomAttributes<TableAttribute>().SingleOrDefault();
            if (attribute == null) {
                throw new ActiveRecordLoadingException("TableAttribute not found on " + type.Name);
            }
            return attribute;
        }

    }

}
