// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace WrathModdingHelper.Templates
{
    using System.Linq;
    using WrathModdingHelper.Libraries;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class csproj : csprojBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"
<Project Sdk=""Microsoft.NET.Sdk"">
	<PropertyGroup>
		<LangVersion>latest</LangVersion>
		<TargetFramework>net472</TargetFramework>
		<AllowUnsafeBlocks>true</AllowUnsafeBlocks>
	</PropertyGroup>


	<ItemGroup>
		<Compile Remove=""lib\**"" />
		<EmbeddedResource Remove=""lib\**"" />
		<None Remove=""lib\**"" />
	</ItemGroup>

	<ItemGroup>
		<PackageReference Include=""AssemblyPublicizer"" Version=""1.0.1"">
			<PrivateAssets>all</PrivateAssets>
			<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
		</PackageReference>
");
            
            #line 27 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"
 if (MergeAssemblies.Any())
{

            
            #line default
            #line hidden
            this.Write("\t\t<PackageReference Include=\"ILRepack.MSBuild.Task\" Version=\"2.0.13\" />\r\n");
            
            #line 31 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"

}

            
            #line default
            #line hidden
            
            #line 34 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"
 foreach (NugetPackage pkg in NugetPackages)
{

            
            #line default
            #line hidden
            this.Write("\t\t<PackageReference Include=\"");
            
            #line 37 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pkg.Name));
            
            #line default
            #line hidden
            this.Write("\" Version=\"");
            
            #line 37 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(pkg.Version));
            
            #line default
            #line hidden
            this.Write("\" />\r\n");
            
            #line 38 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"

}

            
            #line default
            #line hidden
            this.Write("\t</ItemGroup>\r\n\r\n\t<ItemGroup>\r\n\t\t<!-- Main Wrath Assembly, Publicized -->\r\n\t\t<Ref" +
                    "erence Include=\"Assembly-CSharp\">\r\n\t\t\t<HintPath>$(ProjectDir)lib\\Assembly-CSharp" +
                    ".dll</HintPath>\r\n\t\t</Reference>\r\n\t\t\r\n\t\t<!-- Wrath Assemblies -->\r\n\t\t<Reference I" +
                    "nclude=\"Assembly-CSharp-firstpass.dll\">\r\n\t\t\t<HintPath>$(WrathPath)\\Wrath_Data\\Ma" +
                    "naged\\Assembly-CSharp-firstpass.dll</HintPath>\r\n\t\t</Reference>\r\n\t\t<Reference Inc" +
                    "lude=\"Owlcat.Runtime.Core\">\r\n\t\t\t<HintPath>$(WrathPath)\\Wrath_Data\\Managed\\Owlcat" +
                    ".Runtime.Core.dll</HintPath>\r\n\t\t</Reference>\r\n\t\t<Reference Include=\"Owlcat.Runti" +
                    "me.UI\">\r\n\t\t\t<HintPath>$(WrathPath)\\Wrath_Data\\Managed\\Owlcat.Runtime.UI.dll</Hin" +
                    "tPath>\r\n\t\t</Reference>\r\n\t\t<Reference Include=\"Owlcat.Runtime.Validation\">\r\n\t\t\t<H" +
                    "intPath>$(WrathPath)\\Wrath_Data\\Managed\\Owlcat.Runtime.Validation.dll</HintPath>" +
                    "\r\n\t\t</Reference>\r\n\t\t<Reference Include=\"Owlcat.Runtime.Visual\">\r\n\t\t\t<HintPath>$(" +
                    "WrathPath)\\Wrath_Data\\Managed\\Owlcat.Runtime.Visual.dll</HintPath>\r\n\t\t</Referenc" +
                    "e>\r\n\t\t<Reference Include=\"UnityEngine\">\r\n\t\t\t<HintPath>$(WrathPath)\\Wrath_Data\\Ma" +
                    "naged\\UnityEngine.dll</HintPath>\r\n\t\t</Reference>\r\n\t\t<Reference Include=\"UnityEng" +
                    "ine.CoreModule\">\r\n\t\t\t<HintPath>$(WrathPath)\\Wrath_Data\\Managed\\UnityEngine.CoreM" +
                    "odule.dll</HintPath>\r\n\t\t</Reference>\r\n\t\t\r\n\t\t<!-- Harmony & UMM -->\r\n\t\t<Reference" +
                    " Include=\"0Harmony\">\r\n\t\t\t<HintPath>$(WrathPath)\\Wrath_Data\\Managed\\UnityModManag" +
                    "er\\0Harmony.dll</HintPath>\r\n\t\t</Reference>\r\n\t\t<Reference Include=\"UnityModManage" +
                    "r\">\r\n\t\t\t<HintPath>$(WrathPath)\\Wrath_Data\\Managed\\UnityModManager\\UnityModManage" +
                    "r.dll</HintPath>\r\n\t\t</Reference>\r\n\t</ItemGroup>\r\n\t\r\n\t<ItemGroup>\r\n\t  <None Updat" +
                    "e=\"Info.json\">\r\n\t    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirector" +
                    "y>\r\n\t  </None>\r\n\t</ItemGroup>\r\n\r\n\t<!-- Generates Assembly-CSharp_public.dll -->\r" +
                    "\n\t<Target Name=\"Publicize\" AfterTargets=\"Clean\">\r\n\t\t<ItemGroup>\r\n\t\t\t<Assemblies " +
                    "Include=\"$(WrathPath)\\Wrath_Data\\Managed\\Assembly-CSharp.dll\" />\r\n\t\t\t<PublicAsse" +
                    "mbly Include=\"$(ProjectDir)lib\\Assembly-CSharp_public.dll\" />\r\n\t\t\t<RenamedAssemb" +
                    "ly Include=\"$(ProjectDir)lib\\Assembly-CSharp.dll\" />\r\n\t\t</ItemGroup>\r\n\r\n\t\t<Publi" +
                    "cizeTask InputAssemblies=\"@(Assemblies)\" OutputDir=\"$(ProjectDir)lib/\" />\r\n\t\t<!-" +
                    "- ILRepack requires the assembly name match the reference so remove _public -->\r" +
                    "\n\t\t<Move SourceFiles=\"@(PublicAssembly)\" DestinationFiles=\"@(RenamedAssembly)\" /" +
                    ">\r\n\t</Target>\r\n\r\n");
            
            #line 100 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(InjectedFragments));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n");
            
            #line 102 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"
 if (MergeAssemblies.Any())
{

            
            #line default
            #line hidden
            this.Write("\t<Target Name=\"ILRepack\" AfterTargets=\"Build\">\r\n\t\t<ItemGroup>\r\n\t\t\t<InputAssemblie" +
                    "s Include=\"$(AssemblyName).dll\" />\r\n");
            
            #line 108 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"
 foreach (string asm in MergeAssemblies)
{

            
            #line default
            #line hidden
            this.Write("\t\t\t<InputAssemblies Include=\"");
            
            #line 111 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(asm));
            
            #line default
            #line hidden
            this.Write("\" />\r\n");
            
            #line 112 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"

}

            
            #line default
            #line hidden
            this.Write(@"			<OutputAssembly Include=""$(AssemblyName).dll"" />
		</ItemGroup>

		<Message Text=""Merging: @(InputAssemblies) into @(OutputAssembly)"" Importance=""High"" />

		<ILRepack OutputType=""Dll"" MainAssembly=""@(OutputAssembly)"" OutputAssembly=""@(OutputAssembly)"" InputAssemblies=""@(InputAssemblies)"" WorkingDirectory=""$(OutputPath)"" />
	</Target>
");
            
            #line 122 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"

}

            
            #line default
            #line hidden
            this.Write("\r\n\t<!-- Automatically deploys the mod on build -->\r\n\t<Target Name=\"Deploy\" AfterT" +
                    "argets=\"");
            
            #line 127 "C:\Users\worce\source\repos\WrathModdingHelper\Templates\csproj.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PreDeployTarget));
            
            #line default
            #line hidden
            this.Write(@""">
		<ItemGroup>
			<Assembly Include=""$(OutputPath)\$(AssemblyName).dll"" />
			<ModConfig Include=""$(OutputPath)\Info.json"" />
		</ItemGroup>

		<Copy SourceFiles=""@(Assembly)"" DestinationFolder=""$(WrathPath)\Mods\$(MSBuildProjectName)"" />
		<Copy SourceFiles=""@(ModConfig)"" DestinationFolder=""$(WrathPath)\Mods\$(MSBuildProjectName)"" />
	</Target>

</Project>

");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class csprojBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
