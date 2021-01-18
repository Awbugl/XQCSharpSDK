using System;
using System.Runtime.InteropServices;

namespace XQ.SDK.Core
{
    public class Kernel32
    {
        [DllImport("kernel32.dll", EntryPoint = "GetProcessHeap")]
        public static extern int GetProcessHeap();

        [DllImport("kernel32.dll", EntryPoint = "HeapFree")]
        public static extern int HeapFree(int hheap, int dwflags, IntPtr lpmeem);
    }
}