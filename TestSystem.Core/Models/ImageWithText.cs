using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Core.Models
{
    public class ImageWithText : BaseFile
    {
        public Guid Id { get; set; }
        public string? AssociatedText { get; set; }
    }
}
