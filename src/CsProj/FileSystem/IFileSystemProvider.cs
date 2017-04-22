using System.Collections.Generic;

namespace Skarpdev.DotnetVersion.CsProj.FileSystem
{
    public interface IFileSystemProvider
    {
        /// <summary>
        /// List the items in the given path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IEnumerable<string> List(string path);

        /// <summary>
        /// Determines whether the given path is actually a csproj file or a path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool IsCsProjectFile(string path);

        /// <summary>
        /// Returns the path where the given csproj file is stored
        /// </summary>
        /// <param name="csProjFilePath"></param>
        /// <returns></returns>
        string GetPathFromProjectFile(string csProjFilePath);

        /// <summary>
        /// Get the current working directory
        /// </summary>
        /// <returns></returns>
        string Cwd();

        /// <summary>
        /// Loads all the content from the given file path as a string
        /// </summary>
        /// <param name="filePath"></param>
        string LoadContent(string filePath);
    }
}