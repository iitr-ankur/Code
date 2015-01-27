using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextMobileFunctions
{
    public interface IDocumentManager
    {
        Task<bool> OpenDocument(string filePath);
    }
}
