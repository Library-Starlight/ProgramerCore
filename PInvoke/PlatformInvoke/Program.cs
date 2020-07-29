using PlatformInvoke.Structure;
using System;

namespace PlatformInvoke
{

    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new StructureToPointer().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
