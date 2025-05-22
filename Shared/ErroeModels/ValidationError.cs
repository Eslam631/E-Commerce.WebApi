using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErroeModels
{
    public class ValidationError
    {

        public string Falied { get; set; } = default!;

        public IEnumerable<string> Errors { get; set; } = [];
    }
}
