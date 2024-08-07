using MvvmLight.Lx.DbAccess.Entitys;
using NewLife.Reflection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.DbAccess.Modules
{
    public class SqlQueryModule
    {
        public static List<T> FuzzyQueryable<T>()
        {
            var TableName = typeof(T).Name;             //获取表名

            var s1=typeof(T).GetProperty("Name");

            var list = new List<T>();
            PropertyInfo property=typeof(T).GetProperties().FirstOrDefault(x=>x.Name.EndsWith("Name"));
      

            //获取这个类的所有属性，但是模糊查询太费时间，我们需要过滤
            using (var context=new MvvmLightContext())
            {
                 list = context.Users.SqlQuery($"SELECT * FROM {TableName}").ToList() as List<T>;
            }
            return list;
        }

        public static List<T> Queryable<T>()
        {
            var TableName = typeof(T).Name;             //获取表名

            var list = new List<T>();
            string PropertyName = "";
            PropertyInfo property = typeof(T).GetProperties().FirstOrDefault(x => x.Name.EndsWith("Name"));
            /*  TypeFilter typeFilter = (type,filterName) => 
              {
                  if (filterName==null)
                  {
                      return false;
                  }

                   properties = typeof(T).GetProperties();
                  if (properties !=null)
                  {
                      PropertyName = properties.Where(x=>x.Name.EndsWith("Name")).FirstOrDefault().Name;

                      return PropertyName != null && PropertyName.Contains("Name");
                  }
                  return false;
              };*/


            /* if (typeFilter(typeof(T), "Name"))
             {
                 using (var context = new MvvmLightContext())
                 {
                     // 使用 PropertyName
                     list = context.Database.SqlQuery<T>($"SELECT {PropertyName} FROM {TableName}").ToList();
                 }
             }*/

            PropertyName = property.Name;
            using (var context = new MvvmLightContext())
            {
                var s1 = context.Database.SqlQuery<string>($"SELECT {PropertyName} FROM {TableName}").ToList();      //尽量不要写模糊查询
            }
            return list;
        }
    }
}
