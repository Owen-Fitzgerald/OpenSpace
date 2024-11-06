using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace Geraldine.Standards.InfoSystem
{
    /// <summary></summary>
    public static class FileSystem
    {
        #region IFileSystem Members

        /// <summary></summary>
        public static bool FileExists(string FilePath)
        {
            return File.Exists(FilePath);
        }


        /// <summary></summary>
        public static bool DirectoryExists(string DirectoryPath)
        {
            return Directory.Exists(DirectoryPath);
        }


        /// <summary></summary>
        public static string[] GetFilesInDirectory(string DirectoryPath)
        {
            return Directory.GetFiles(DirectoryPath);
        }


        /// <summary></summary>
        public static string[] GetDirectoriesInDirectory(string DirectoryPath)
        {
            return Directory.GetDirectories(DirectoryPath);
        }


        /// <summary></summary>
        public static string GetFilenameWithExtension(string FilePath)
        {
            return Path.GetFileName(FilePath);
        }


        /// <summary></summary>
        public static string GetFilenameWithoutExtension(string FilePath)
        {
            return Path.GetFileNameWithoutExtension(FilePath);
        }


        /// <summary>
        /// Gets the name of the given directory.
        /// </summary>
        public static string GetNameOfDirectory(string DirectoryPath)
        {
            string dir_name = string.Empty;
            if (DirectoryExists(DirectoryPath))
            {
                // DirectoryPath is a directory. Return the directory name.
                dir_name = (new DirectoryInfo(DirectoryPath)).Name;
            }
            return dir_name;
        }


        /// <summary>
        /// Gets the name of the directory containing the given file.
        /// </summary>
        public static string GetDirectoryName(string FilePath)
        {
            string dir_name = string.Empty;
            if (FilePath != null)
            {
                // FilePath is a file name. Get the containing directory.
                dir_name = Path.GetDirectoryName(FilePath);
            }
            return dir_name;
        }

        /// <summary></summary>
        /// <returns>True if the directory was successfully created. Otherwise, false.</returns>
        public static bool CreateDirectory(string DirectoryPath)
        {
            bool created_dir = false;
            if (!DirectoryExists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
                created_dir = true;
            }
            return created_dir;
        }


        /// <summary></summary>
        /// <returns>True if the directory was successfully created. Otherwise, false.</returns>
        public static bool CreateDirectoryForFile(string FilePath)
        {
            FileInfo file_info = new FileInfo(FilePath);
            return CreateDirectory(file_info.DirectoryName);
        }


        /// <summary></summary>
        public static void CopyDirectory(string SourceDirectoryName, string DestDirectoryName)
        {
            CopyDirectory(SourceDirectoryName, DestDirectoryName, false);
        }


        /// <summary></summary>
        public static void CopyDirectory(string SourceDirectoryName, string DestDirectoryName, bool Recursive, bool Overwrite = false)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(SourceDirectoryName);
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + SourceDirectoryName);
            }
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(DestDirectoryName))
            {
                Directory.CreateDirectory(DestDirectoryName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temp_path = Path.Combine(DestDirectoryName, file.Name);
                file.CopyTo(temp_path, Overwrite);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (Recursive)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(DestDirectoryName, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, true, Overwrite);
                }
            }
        }


        /// <summary></summary>
        public static void DeleteFile(string FilePath)
        {
            File.Delete(FilePath);
        }


        /// <summary>Deletes empty directory.</summary>
        /// <param name="DirectoryPath">DirectoryPath</param>
        /// <param name="Recursive">True indicates that subdirectories should also be deleted</param>
        public static void DeleteDirectory(string DirectoryPath, bool Recursive)
        {
            Directory.Delete(DirectoryPath, Recursive);
        }


        /// <summary>Deletes directory and all files in it.</summary>
        public static void DeleteDirectoryAndFiles(string DirectoryPath, bool Recursive)
        {
            DirectoryInfo root_dir = new DirectoryInfo(DirectoryPath);
            foreach (FileInfo fi in root_dir.GetFiles())
            {
                fi.Delete();
            }
            if (Recursive)
            {
                foreach (DirectoryInfo di in root_dir.GetDirectories())
                {
                    DeleteDirectoryAndFiles(di.FullName, Recursive);
                }
            }
            Directory.Delete(DirectoryPath, Recursive);
        }


        /// <summary></summary>
        public static void CopyFile(string SourceFileName, string DestFileName)
        {
            File.Copy(SourceFileName, DestFileName);
        }


        /// <summary></summary>
        public static void CopyFile(string SourceFileName, string DestFileName, bool OverWrite)
        {
            File.Copy(SourceFileName, DestFileName, OverWrite);
        }


        /// <summary></summary>
        /// <returns>True if DirectoryPath is hidden. Otherwise, false.</returns>
        public static bool DirectoryIsHidden(string DirectoryPath)
        {
            bool is_hidden = false;
            DirectoryInfo directory_info = new DirectoryInfo(DirectoryPath);
            is_hidden = directory_info.Attributes.HasFlag(FileAttributes.Hidden);
            return is_hidden;
        }


        /// <summary></summary>
        public static bool DirectoryContainsMatchingFile(string Directory, string Extension)
        {
            string filename = Path.GetFileName(Directory) + "." + Extension;
            string file_path = Path.Combine(Directory, filename);

            return File.Exists(file_path);
        }


        /// <summary></summary>
        public static bool DirectoriesAreEqual(DirectoryInfo Dir1, DirectoryInfo Dir2)
        {
            bool ret = false;
            if (Dir1 == null && Dir2 == null)
            {
                ret = true;
            }
            else if (Dir1 != null && Dir2 != null && Dir1.FullName.ToLowerInvariant() == Dir2.FullName.ToLowerInvariant())
            {
                ret = true;
            }
            return ret;
        }


        /// <summary></summary>
        public static FileStream CreateLockedStreamFromFile(string FilePath)
        {
            return new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
        }


        /// <summary></summary>
        public static FileStream CreateReadOnlyStreamFromFile(string FilePath)
        {
            return new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }


        /// <summary></summary>
        public static void SaveFile(string FilePath, string Contents)
        {
            TextWriter writer = new StreamWriter(FilePath);
            writer.Write(Contents);
            writer.Close();
        }


        /// <summary></summary>
        public static string LoadFile(string FilePath)
        {
            string contents = String.Empty;
            using (StreamReader reader = new StreamReader(FilePath))
            {
                contents = reader.ReadToEnd();
                reader.Close();
            }
            return contents;
        }


        #region serialization

        /// <summary></summary>
        public static object DeserializeFile(TextAsset File, Type ObjectType)
        {

            object ret = null;
            XmlSerializer reader = new XmlSerializer(ObjectType);
            byte[] byteArray = Encoding.UTF8.GetBytes(File.text);
            MemoryStream stream = new MemoryStream(byteArray);
            ret = reader.Deserialize(stream);

            return ret;
        }

        /// <summary></summary>
        public static void SerializeFile(string FilePath, object ObjectToSerialize)
        {
            XmlSerializer s = new XmlSerializer(ObjectToSerialize.GetType());
            TextWriter writer = new StreamWriter(FilePath);
            s.Serialize(writer, ObjectToSerialize);
            writer.Close();
        }

        /// <summary></summary>
        public static void SerializeLockedFile(string FilePath, object ObjectToSerialize, ref FileStream Stream)
        {
            XmlSerializer s = new XmlSerializer(ObjectToSerialize.GetType());

            if (Stream != null)
            {
                Stream.Close();
                Stream.Dispose();
            }

            //no longer use former stream to lock, create a new one
            Stream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            Stream.SetLength(0);

            s.Serialize(Stream, ObjectToSerialize);
        }

        #endregion serialization

        #endregion IFileSystem Members


        #region Static Members

        /// <summary>
        /// Determines if the given file is an XML file.
        /// </summary>
        /// <param name="FileName">File to check.</param>
        /// <returns>True if the file is an image, otherwise false.</returns>
        public static bool IsXml(string FileName)
        {
            string extension = null;
            if (!string.IsNullOrEmpty(FileName))
            {
                try { extension = Path.GetExtension(FileName); }
                catch (Exception) { }
            }
            return string.Equals(extension, ".xml", StringComparison.OrdinalIgnoreCase);
        }

        #region sound files

        /// <summary>a string that is intended to be used for the Filter property of (win32) FileDialogs to show the kinds of audio files we support</summary>
        public static readonly string FileDialogFilterForSound = "Sound Files (*.wav)|*.wav";


        /// <summary>
        /// Determines if the given file is an audio file.
        /// </summary>
        /// <param name="FileName">File to check.</param>
        /// <returns>True if the file is an audio, otherwise false.</returns>
        public static bool IsSound(string FileName)
        {
            string extension = null;
            if (!string.IsNullOrEmpty(FileName))
            {
                try { extension = Path.GetExtension(FileName); }
                catch (Exception) { }
            }
            return string.Equals(extension, ".wav", StringComparison.OrdinalIgnoreCase);
        }

        #endregion sound files


        #region image files

        /// <summary>a string that is intended to be used for the Filter property of (win32) FileDialogs to show the kinds of images we support</summary>
        public static readonly string FileDialogFilterForImages = "Image Files (*.png, *.bmp)|*.png;*.bmp";

        /// <summary>as for FileDialogFilterForImages, but only the filename extensions</summary>
        public static readonly string FileDialogFilterForImagesOnlyExtensions = "*.png,*.bmp";

        /// <summary>
        /// Determines if the given file is an image file.
        /// </summary>
        /// <param name="FileName">File to check.</param>
        /// <returns>True if the file is an image, otherwise false.</returns>
        public static bool IsImage(string FileName)
        {
            string extension = null;
            if (!string.IsNullOrEmpty(FileName))
            {
                try { extension = Path.GetExtension(FileName); }
                catch (Exception) { }
            }
            return string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(extension, ".bmp", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Finds all image files in the specified Directory and adds FileInfo
        /// for each to the reference parameter Images. This does not inspect
        /// file contents, just looks at file extensions.
        /// </summary>
        /// <param name="Directory">the directory to check</param>
        /// <param name="Images">The list that will contain a FileInfo for every image file in the specified Directory</param>
        /// <remarks>
        /// Images will be cleared before adding any images.
        /// Significantly faster than FindImagesInDirectory method.
        /// </remarks>
        public static void FindImagesInDirectoryByExtension(DirectoryInfo Directory, IList<FileInfo> Images)
        {
            if (Images == null || Directory == null)
            {
                throw new NullReferenceException();
            }
            Images.Clear();
            if (Directory.Exists)
            {
                try
                {
                    foreach (FileInfo fi in Directory.EnumerateFiles())
                    {
                        if (IsImage(fi.FullName))
                        {
                            Images.Add(fi);
                        }
                    }
                }
                catch (Exception) { }  // in case EnumerateFiles fails
            }
        }

        #endregion image files


        #region movie files

        /// <summary>a string that is intended to be used for the Filter property of (win32) FileDialogs to show the kinds of audio files we support</summary>
        public static readonly string FileDialogFilterForMovies = "Movie Files (*.mp4, *.mov, *.mpg, *.mpeg, *.avi)|*.mp4;*.mov;*.mpg;*.mpeg;*.avi";

        /// <summary>as for FileDialogFilterForMovies, but only the filename extensions.</summary>
        public static readonly string FileDialogFilterForMoviesOnlyExtensions = "*.mp4;*.mov;*.mpg;*.mpeg;*.avi";

        /// <summary>is the file of the given name a movie file?</summary>
        public static bool IsVideo(string FileName)
        {
            string extension = null;
            try { extension = Path.GetExtension(FileName); }
            catch (Exception) { }
            return string.Equals(extension, ".mp4", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(extension, ".mov", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(extension, ".mpg", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(extension, ".mpeg", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(extension, ".avi", StringComparison.OrdinalIgnoreCase);
        }


        /// <summary> Finds all video files in the specified Directory and adds FileInfo
        /// for each to the reference parameter Video. </summary>
        ///<remarks>This only uses filename extension to determine whether a file is a video file.</remarks>
        public static void FindVideosInDirectory(DirectoryInfo Directory, IList<FileInfo> Videos)
        {
            if (Videos == null || Directory == null)
            {
                throw new NullReferenceException();
            }
            Videos.Clear();
            if (Directory.Exists)
            {
                try
                {
                    foreach (FileInfo fi in Directory.EnumerateFiles())
                    {
                        if (IsVideo(fi.FullName))
                        {
                            Videos.Add(fi);
                        }
                    }
                }
                catch (Exception) { }  // in case EnumerateFiles fails
            }
        }

        #endregion movie files


        #endregion Static Members
    }
}
