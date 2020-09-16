using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem
{
    public interface IFileManager
    {
        void ShowStructure(Action<int, string> render);
    }
}
