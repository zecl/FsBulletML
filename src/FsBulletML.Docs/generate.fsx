#I "../../packages/FSharp.Formatting.2.2.3/lib/net40" 
#I "../../packages/Microsoft.AspNet.Razor.2.0.30506.0/lib/net40"
#I "../../packages/RazorEngine.3.3.0/lib/net40"
#r "FSharp.CodeFormat.dll"
#r "FSharp.Literate.dll"
#r "System.Web.Razor.dll"
#r "RazorEngine.dll"
open FSharp.Literate
open System.IO

let source = __SOURCE_DIRECTORY__
let templatePath = "../../docs/templates/"
let templateFile = Path.Combine(source, templatePath + "template-file.html")
let script = Path.Combine(source, "Index.fsx")
//Literate.ProcessScriptFile(script, templateFile)
//let doc = Path.Combine(source, "Library1.md")
//Literate.ProcessMarkdown(doc, template)

// Load the template & specify project information
let projTemplateFile = Path.Combine(source, templatePath + "template-project.html")
let root = "http://zecl.github.io/FsBulletML.Core"
let projInfo =
  [ "page-description", "FsBulletML"
    "nuget-page","https://www.nuget.org/packages/FsBulletML.Core"
    "page-author", "zecl"
    "github-link", "https://github.com/zecl/FsBulletML"
    "project-name", "FsBulletML" 
    "license-page", "https://github.com/zecl/FsBulletML/blob/master/LICENSE.md"
    "root",root
    ]

// TODO : output ディレクトリがなかったら作る処理...

// Process all files and save results to 'output' directory
Literate.ProcessDirectory
  (source, projTemplateFile, "../../docs/output", replacements = projInfo)



