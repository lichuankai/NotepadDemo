using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotepadDemo.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class AddAuthTokenHeaderParameter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }
            operation.Parameters.Add(new NonBodyParameter()
            {
                Name = "token",
                In = "header",
                Type = "string",
                Description = "token认证信息",
                Required = true
            });
        }
    }
}
