namespace EzPing.Core.Diagnostic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    [NullableContext(1), Nullable((byte) 0)]
    public static class ProcessUtils
    {
        public static readonly BitmapSource DefaultProcessIcon;

        static ProcessUtils()
        {
            List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
            colors.Add(Colors.Transparent);
            DefaultProcessIcon = BitmapSource.Create(2, 2, 96.0, 96.0, PixelFormats.Indexed1, new BitmapPalette(colors), new byte[4], 1);
        }

        public static BitmapSource GetFileIcon(string location)
        {
            if (!File.Exists(location))
            {
                return DefaultProcessIcon;
            }
            using (Icon icon = Icon.ExtractAssociatedIcon(location))
            {
                return ((icon != null) ? (Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()) ?? DefaultProcessIcon) : DefaultProcessIcon);
            }
        }

        public static BitmapSource GetProcessIcon(Process process)
        {
            string processLocation = GetProcessLocation(process, 0x400);
            return (string.IsNullOrWhiteSpace(processLocation) ? DefaultProcessIcon : GetFileIcon(processLocation));
        }

        [return: Nullable((byte) 2)]
        public static string GetProcessLocation(Process process, int buffer = 0x400)
        {
            try
            {
                if (process.MainModule != null)
                {
                    return process.MainModule.FileName;
                }
            }
            catch
            {
            }
            StringBuilder lpExeName = new StringBuilder(buffer);
            uint lpdwSize = (uint) (lpExeName.Capacity + 1);
            try
            {
                return (QueryFullProcessImageName(process.Handle, 0, lpExeName, ref lpdwSize) ? lpExeName.ToString() : null);
            }
            catch
            {
                return null;
            }
        }

        [DllImport("Kernel32.dll")]
        private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);
    }
}

