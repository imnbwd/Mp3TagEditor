using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System;
using System.Linq;

namespace PraiseHim.Tools.Mp3TagEditor.Models
{
    public class MediaTags
    {
        #region Properties

        [MediaProperty("Music.AlbumArtist")]
        public string AlbumArtist { get; set; }

        [MediaProperty("Music.AlbumTitle")]
        public string AlbumTitle { get; set; }

        [MediaProperty("Author")]
        public string Author { get; set; }

        [MediaProperty("Audio.EncodingBitrate")]
        public string BitRate { get; private set; }

        [MediaProperty("Comment")]
        public string Comment { get; set; }

        [MediaProperty("Media.Duration")]
        public string Duration { get; private set; }

        [MediaProperty("Music.Genre")]
        public string Genre { get; set; }

        [MediaProperty("Rating")]
        public uint? Rating { get; set; }

        [MediaProperty("Media.SubTitle")]
        public string SubTitle { get; set; }

        [MediaProperty("Title")]
        public string Title { get; set; }

        [MediaProperty("Music.TrackNumber")]
        public uint? TrackNumber { get; set; }

        [MediaProperty("Media.Year")]
        public uint? Year { get; set; }

        #endregion Properties

        public MediaTags(string mediaPath)
        {
            Init(mediaPath);
        }

        public void Commit(string mp3Path)
        {
            var old = new MediaTags(mp3Path);

            using (var obj = ShellObject.FromParsingName(mp3Path))
            {
                var mediaInfo = obj.Properties;
                foreach (var proper in this.GetType().GetProperties())
                {
                    Console.WriteLine($"Info: {proper.Name} - {proper.GetValue(old)}");
                    var oldValue = proper.GetValue(old, null);
                    var newValue = proper.GetValue(this, null);

                    if (oldValue == null && newValue == null)
                    {
                        continue;
                    }

                    if ((newValue != null) && (newValue.ToString().Trim().Length > 0) && (newValue != oldValue))
                    {
                        var mp3Att = proper.GetCustomAttributes(typeof(MediaPropertyAttribute), false).FirstOrDefault();
                        var shellProper = mediaInfo.GetProperty("System." + mp3Att);
                        Console.WriteLine(mp3Att);

                        if (newValue == null) newValue = string.Empty;

                        SetPropertyValue(shellProper, newValue);
                    }
                }
            }
        }

        private void Init(string mediaPath)
        {
            using (var obj = ShellObject.FromParsingName(mediaPath))
            {
                var mediaInfo = obj.Properties;
                foreach (var properItem in this.GetType().GetProperties())
                {
                    var mp3Att = properItem.GetCustomAttributes(typeof(MediaPropertyAttribute), false).FirstOrDefault();
                    var shellProper = mediaInfo.GetProperty("System." + mp3Att);
                    var value = shellProper == null ? null : shellProper.ValueAsObject;

                    if (value == null)
                    {
                        continue;
                    }

                    if (shellProper.ValueType == typeof(string[]))
                    {
                        properItem.SetValue(this, string.Join(";", value as string[]), null);
                    }
                    else if (properItem.PropertyType != shellProper.ValueType)
                    {
                        properItem.SetValue(this, value == null ? "" : shellProper.FormatForDisplay(PropertyDescriptionFormatOptions.ReadOnly), null);
                    }
                    else
                    {
                        properItem.SetValue(this, value, null);
                    }
                }
            }
        }

        #region SetPropertyValue

        private static void SetPropertyValue(IShellProperty prop, object value)
        {
            if (prop.ValueType == typeof(string[]))
            {
                string[] values = (value as string).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                (prop as ShellProperty<string[]>).Value = values;
            }
            if (prop.ValueType == typeof(string))
            {
                (prop as ShellProperty<string>).Value = value as string;
            }
            else if (prop.ValueType == typeof(ushort?))
            {
                (prop as ShellProperty<ushort?>).Value = value as ushort?;
            }
            else if (prop.ValueType == typeof(short?))
            {
                (prop as ShellProperty<short?>).Value = value as short?;
            }
            else if (prop.ValueType == typeof(uint?))
            {
                (prop as ShellProperty<uint?>).Value = value as uint?;
            }
            else if (prop.ValueType == typeof(int?))
            {
                (prop as ShellProperty<int?>).Value = value as int?;
            }
            else if (prop.ValueType == typeof(ulong?))
            {
                (prop as ShellProperty<ulong?>).Value = value as ulong?;
            }
            else if (prop.ValueType == typeof(long?))
            {
                (prop as ShellProperty<long?>).Value = value as long?;
            }
            else if (prop.ValueType == typeof(DateTime?))
            {
                (prop as ShellProperty<DateTime?>).Value = value as DateTime?;
            }
            else if (prop.ValueType == typeof(double?))
            {
                (prop as ShellProperty<double?>).Value = value as double?;
            }
        }

        #endregion SetPropertyValue

        #region MediaPropertyAttribute

        [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
        private sealed class MediaPropertyAttribute : Attribute
        {
            public MediaPropertyAttribute(string propertyKey)
            {
                this.PropertyKey = propertyKey;
            }

            public string PropertyKey { get; private set; }

            public override string ToString()
            {
                return PropertyKey;
            }
        }

        #endregion MediaPropertyAttribute
    }
}