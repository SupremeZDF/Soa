using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BTTaskDispatchService.AutoMapper
{
    public class AutoMapperTool<TIn, TOut>
    {
        public static Func<TIn, TOut> Func = null;
        static AutoMapperTool()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TIn), "x");
            List<MemberBinding> members = new List<MemberBinding>();
            //PropertyInfo[] propertyInfos = typeof(TOut).GetProperties().Where(x => x.GetCustomAttribute(typeof(ExpressGenericFuncAttrbuteTool),true) != null).ToArray();
            foreach (PropertyInfo into in typeof(TOut).GetProperties())
            {
                var item = typeof(TIn).GetProperty(into.Name);
                if (item == null)
                {
                    var propertyInfo = into.GetCustomAttributes(typeof(ExpressGenericFuncAttrbuteTool)).FirstOrDefault() as ExpressGenericFuncAttrbuteTool;
                    //var propertyInfo = propertyInfos.Where(x => (x.GetCustomAttribute(typeof(ExpressGenericFuncAttrbuteTool), true) as ExpressGenericFuncAttrbuteTool).PropertyConvertName == into.Name).FirstOrDefault();
                    if (propertyInfo != null)
                    {
                        item = typeof(TIn).GetProperty(propertyInfo.PropertyConvertName);
                        if (item != null)
                        {
                            if (item.PropertyType == into.PropertyType) 
                            {
                                var mem = Expression.Property(parameter, item);
                                var bin = Expression.Bind(into, mem);
                                members.Add(bin);
                            }
                        }
                    }
                    continue;
                }

                if (item.PropertyType == into.PropertyType)
                {
                    MemberExpression member = Expression.Property(parameter, item);
                    MemberBinding memberBinding = Expression.Bind(into, member);
                    members.Add(memberBinding);
                }
            }
            //FieldInfo [] fieldInfos = typeof(TOut).GetFields().Where(x => x.GetCustomAttribute(typeof(ExpressGenericFuncAttrbuteTool), true) != null).ToArray();
            foreach (FieldInfo fields in typeof(TOut).GetFields())
            {
                var filed = typeof(TIn).GetField(fields.Name);
                if (filed == null)
                {
                    var file = fields.GetCustomAttributes(typeof(ExpressGenericFuncAttrbuteTool)).FirstOrDefault() as ExpressGenericFuncAttrbuteTool;
                    //var file = fieldInfos.Where(x => (x.GetCustomAttribute(typeof(ExpressGenericFuncAttrbuteTool), true) as ExpressGenericFuncAttrbuteTool).PropertyConvertName == fields.Name).FirstOrDefault();
                    if (file != null)
                    {
                        filed = typeof(TIn).GetField(file.PropertyConvertName);
                        if (filed != null && filed.FieldType == fields.FieldType)
                        {
                            var mem = Expression.Field(parameter, filed);
                            var bin = Expression.Bind(fields, mem);
                            members.Add(bin);
                        };
                    }
                    continue;
                }
                if (fields.FieldType == filed.FieldType) 
                {
                    MemberExpression member = Expression.Field(parameter, filed);
                    MemberBinding memberBinding = Expression.Bind(fields, member);
                    members.Add(memberBinding);
                }
            }
            //成员变量初始化
            MemberInitExpression memberInit = Expression.MemberInit(Expression.New(typeof(TOut)), members);
            Expression<Func<TIn, TOut>> expression = Expression.Lambda<Func<TIn, TOut>>(memberInit, new ParameterExpression[] { parameter });
            //编译并执行
            Func = expression.Compile();
        }

        //public ExpressGenericFunc() { }


        /// <summary>
        /// model 转换
        /// </summary>
        public static TOut ToClass(TIn @in)
        {
            return Func.Invoke(@in);
        }
    }
}
