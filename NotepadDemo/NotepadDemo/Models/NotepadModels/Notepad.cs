using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotepadDemo.Models.NotepadModels
{
    /// <summary>
    /// 记事本实体类
    /// </summary>
    public class Notepad
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateTime { get; set; }
    }
}
