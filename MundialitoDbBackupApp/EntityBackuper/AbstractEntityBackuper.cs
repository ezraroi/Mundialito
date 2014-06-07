using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MundialitoDbBackupApp
{
    public abstract class AbstractEntityBackuper<Entity>
    {
        private string entityName, directoryName;

        public AbstractEntityBackuper(string directoryName, string entityName)
        {
            this.entityName = entityName;
            this.directoryName = directoryName;
        }

        protected string EntityName { get { return entityName; } }

        protected abstract List<string> GetFieldsToBackup();

        protected abstract List<Entity> GetAllEntites();

        protected virtual object ProcessValue(object obj, string propName)
        {
            return obj;
        }

        protected String GetPropValue(object src, string propName)
        {
            var value = src.GetType().GetProperty(propName).GetValue(src, null);
            value = ProcessValue(value, propName);
            return value == null ? "NULL" : value.ToString();
        }

        protected List<string> GetObjectPublicProperties(Object item)
        {
            return item.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy).Select(pro => pro.Name).ToList();
        }

        public void Backup()
        {
            Program.WriteLine(string.Format("*** Backing up {0} ***", EntityName));
            var entities = GetAllEntites();
            Program.WriteLine(string.Format("Will backup {0} entities", entities.Count));
            var header = GetFieldsToBackup();
            var file = File.CreateText(Path.Combine(directoryName, EntityName + ".csv"));
            file.WriteLine(string.Join(",", header));

            foreach (var entity in entities)
            {
                var row = new List<string>();
                foreach (var item in header)
                {
                    row.Add(GetPropValue(entity, item));
                }
                file.WriteLine(string.Join(",", row));
            }
            file.Close();
            Program.WriteLine("*** Done ***");
        }

    }
}
