using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Core.Models
{
    public class BaseFile
    {
        public required Guid Id { get; set; }
        public required string FilePath { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BaseFile file &&
                   Id.Equals(file.Id) &&
                   FilePath == file.FilePath;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FilePath);
        }
    }

}
