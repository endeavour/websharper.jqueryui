#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("IntelliFactory.WebSharper.JQueryUI", "2.5")
        .References(fun r -> [r.Assembly "System.Web"])

let main =
    bt.WebSharper.Library("IntelliFactory.WebSharper.JQueryUI")
        .SourcesFromProject()

let test =
    bt.WebSharper.HtmlWebsite("IntelliFactory.WebSharper.JQueryUI.Tests")
        .SourcesFromProject()
        .References(fun r -> [r.Project main])

bt.Solution [

    main
    test

    bt.NuGet.CreatePackage()
        .Description("Bindings to JQuery UI")
        .Add(main)

]
|> bt.Dispatch
