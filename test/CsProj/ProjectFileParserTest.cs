using System;
using Skarp.Version.Cli.CsProj;
using Xunit;

namespace Skarp.Version.Cli.Test.CsProj
{
    public class ProjectFileParserTest
    {
        private readonly ProjectFileParser parser;

        public ProjectFileParserTest()
        {
            parser = new ProjectFileParser();
            
        }
        
        [Fact]
        public void CanParseWellFormedProjectFilesWithVersionTag()
        {
            const string csProjXml = "<Project Sdk=\"Microsoft.NET.Sdk\">"+
                                     "<PropertyGroup>" + 
                                     "<TargetFramework>netstandard1.6</TargetFramework>" +
                                     "<RootNamespace>Unit.For.The.Win</RootNamespace>" +
                                     "<PackageId>Unit.Testing.Library</PackageId>" +
                                     "<Version>1.0.0</Version>" +
                                     "</PropertyGroup>" +
                                     "</Project>";

            parser.Load(csProjXml);
            Assert.Equal("1.0.0", parser.Version);
        }

        [Fact]
        public void BailsWhenNoVersionIsDefined()
        {
            const string csProjXml = "<Project Sdk=\"Microsoft.NET.Sdk\">"+
                                     "<PropertyGroup>" + 
                                     "<TargetFramework>netstandard1.6</TargetFramework>" +
                                     "<RootNamespace>Unit.For.The.Win</RootNamespace>" +
                                     "<PackageId>Unit.Testing.Library</PackageId>" +
                                     "</PropertyGroup>" +
                                     "</Project>";

            var ex = Assert.Throws<ArgumentException>(() => 
                parser.Load(csProjXml)
            );
            
            Assert.Equal($"Provided csproj file does not contain a <Version>{Environment.NewLine}Parameter name: version", ex.Message);
            Assert.Equal("version", ex.ParamName);
        }

         [Fact]
        public void BailsOnMalformedProjectFile()
        {
            const string csProjXml = "<Projectttttt Sdk=\"Microsoft.NET.Sdk\">"+
                                     "<PropertyGroup>" + 
                                     "<TargetFramework>netstandard1.6</TargetFramework>" +
                                     "<RootNamespace>Unit.For.The.Win</RootNamespace>" +
                                     "<PackageId>Unit.Testing.Library</PackageId>" +
                                     "</PropertyGroup>" +
                                     "</Projectttttt>";

            var ex = Assert.Throws<ArgumentException>(() => 
                parser.Load(csProjXml)
            );
            
            Assert.Equal($"The provided csproj file seems malformed - no <Project> in the root{Environment.NewLine}Parameter name: project", ex.Message);
            Assert.Equal("project", ex.ParamName);
        }
    }
}
