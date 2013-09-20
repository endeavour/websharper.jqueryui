﻿// $begin{copyright}
//
// WebSharper examples
//
// Copyright (c) IntelliFactory, 2004-2009.
//
// All rights reserved.  Reproduction or use in whole or in part is
// prohibited without the written consent of the copyright holder.
//-----------------------------------------------------------------
// $end{copyright}

//JQueryUI Wrapping: (version Stable 1.8rc1)
namespace IntelliFactory.WebSharper.JQueryUI

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html

type TabsAjaxOptionsConfiguration =
    {
    [<Name "ajaxOptions">]
    async: bool
    }

    [<JavaScript>]
    static member Default = {async = false}


type TabsCookieConfiguration =
    {
    [<Name "cookie">]
    expires: int
    }

    [<JavaScript>]
    static member Default = {expires = 30}


type TabsFxConfiguration =
    {
    [<Name "fx">]
    opacity: string
    }
    [<JavaScript>]
    static member Dafault = {opacity = "toggle"}


type TabsInfo =
    {
        tab : JQuery.JQuery
        panel : JQuery.JQuery
    }

type TabsActivateEvent =
    {
        [<Name "newTab">]
        NewTab : JQuery.JQuery

        [<Name "oldTab">]
        OldTab : JQuery.JQuery

        [<Name "newPanel">]
        NewPanel : JQuery.JQuery

        [<Name "oldPanel">]
        OldPanel : JQuery.JQuery
    }

type TabsConfiguration[<JavaScript>]() =

    [<Name "active">]
    [<Stub>]
    //null by default
    member val Active = Unchecked.defaultof<int> with get, set

    [<Name "collapsible">]
    [<Stub>]
    //false by default
    member val Collapsible = Unchecked.defaultof<bool> with get, set

    [<Name "disabled">]
    [<Stub>]
    //[] by default
    member val Disabled = Unchecked.defaultof<array<int>> with get, set

    [<Name "event">]
    [<Stub>]
    //"click" by default
    member val Event = Unchecked.defaultof<string> with get, set

    [<Name "heightStyle">]
    [<Stub>]
    //"content" by default
    member val HeightStyle = Unchecked.defaultof<string> with get, set

    [<Name "hide">]
    [<Stub>]
    //null by default
    member val Hide = Unchecked.defaultof<string> with get, set

    [<Name "show">]
    [<Stub>]
    //null by default
    member val Show = Unchecked.defaultof<string> with get, set

[<AutoOpen>]
module internal TabsInternal =
    [<Inline "jQuery($el).tabs($conf)">]
    let Init(el: Dom.Element, conf: TabsConfiguration) = ()

    type JQuery.JQuery with
        [<Inline "$this.eq($i)">]
        member this.Eq(i: int) = this

[<Require(typeof<Dependencies.JQueryUIJs>)>]
[<Require(typeof<Dependencies.JQueryUICss>)>]
type Tabs[<JavaScript>] internal (tabContainer, panelContainer) =
    inherit Pagelet()

    (****************************************************************
    * Constructors
    *****************************************************************)
    /// Creates a new tabs object with panels and titles fromt the given
    /// list of name and element pairs and configuration settings object.
    [<JavaScript>]
    [<Name "New1">]
    static member New (els : List<string * Element>, conf: TabsConfiguration): Tabs =
        let panelContainer, tabContainer =
            let itemPanels =
                els
                |> List.map (fun (label, panel) ->
                   let id = NewId()
                   let item = LI [A [Attr.HRef ("#" + id); Text label]]
                   let tab = Div [Attr.Id id] -< [panel]
                   (item :> IPagelet, tab :> IPagelet)
                )
            let tabs = UL (Seq.map fst itemPanels)
            Div (tabs :> IPagelet :: List.map snd itemPanels), tabs

        let tabs = new Tabs (tabContainer, panelContainer)
        tabs.element <-
            panelContainer
            |>! OnAfterRender (fun el ->
                TabsInternal.Init(el.Body, conf)
            )
        tabs



    /// Creates a new tabs object using the default configuration.
    [<JavaScript>]
    [<Name "New2">]
    static member New (els : List<string * Element>): Tabs =
        Tabs.New(els, new TabsConfiguration())


    (****************************************************************
    * Methods
    *****************************************************************)
    /// Removes the tabs functionality completely.
    [<Inline "jQuery($this.element.Body).tabs('destroy')">]
    member this.Destroy() = ()

    /// Disables the tabs functionality.
    [<Inline "jQuery($this.element.Body).tabs('disable')">]
    member this.Disable () = ()

    /// Enables the tabs functionality.
    [<Inline "jQuery($this.element.Body).tabs('enable')">]
    member this.Enable () = ()

    /// Sets a tabs option.
    [<Inline "jQuery($this.element.Body).tabs('option', $name, $value)">]
    member this.Option (name: string, value: obj) = ()

    /// Gets a tabs option.
    [<Inline "jQuery($this.element.Body).tabs('option', $name)">]
    member this.Option (name: string) = X<obj>

    [<Inline "jQuery($this.element.Body).tabs('widget')">]
    member private this.getWidget () = X<Dom.Element>

    /// Returns the .ui-tabs element.
    [<JavaScript>]
    member this.Widget = this.getWidget()

    /// Reloads the content of an Ajax tab.
    [<Inline "jQuery($this.element.Body).tabs('load', $index)">]
    member this.Load (index: int) = ()

    /// Process any tabs that were added or removed directly in the DOM and recompute the height of the tab panels. Results depend on the content and the heightStyle option.
    [<Inline "jQuery($this.element.Body).tabs('refresh')">]
    member this.Refresh () = ()

    /// Retrieve the number of tabs of the first matched tab pane.
    [<JavaScript>]
    member this.Length = JQuery.JQuery.Of(tabContainer.Body).Children().Size()

    /// Add a new tab with the given element and label
    /// inserted at the specified index.
    [<JavaScript>]
    member this.Add(el: Element, label: string, ix: int) =
        let id = NewId()
        let tab = LI [A [Attr.HRef ("#" + id)] :> IPagelet; Text label]
        let panel = Div [Attr.Id id] -< [el]
        JQuery.JQuery.Of(tabContainer.Body).Children().Eq(ix).Before(tab.Body).Ignore
        JQuery.JQuery.Of(panelContainer.Body).Children().Eq(ix).After(panel.Body).Ignore // after because the first child is the tabset
        (tab :> IPagelet).Render()
        (panel :> IPagelet).Render()
        this.Refresh()

    /// Add a new tab with the given element and label.
    [<JavaScript>]
    member this.Add(el: Element, label: string) =
        let id = NewId()
        let tab = LI [A [Attr.HRef ("#" + id)] :> IPagelet; Text label]
        let panel = Div [Attr.Id id] -< [el]
        tabContainer.Append(tab)
        panelContainer.Append(panel)
        this.Refresh()

    (****************************************************************
    * Events
    *****************************************************************)
    [<Inline "jQuery($this.element.Body).bind('tabscreate', function (x,y) {($f(x))(y);})">]
    member private this.onCreate(f : JQuery.Event -> TabsInfo -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('tabsload', function (x,y) {($f(x))(y);})">]
    member private this.onLoad(f : JQuery.Event -> TabsInfo -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('tabsbeforeload', function (x,y) {($f(x))(y);})">]
    member private this.onBeforeLoad(f : JQuery.Event -> TabsInfo -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('tabsactivate', function (x,y) {($f(x))(y);})">]
    member private this.onActivate(f : JQuery.Event -> TabsActivateEvent -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('tabsbeforeactivate', function (x,y) {($f(x))(y);})">]
    member private this.onBeforeActivate(f : JQuery.Event -> TabsActivateEvent -> unit) = ()


    /// Event triggered when the tabs are created.
    [<JavaScript>]
    member this.OnCreate f =
        this |> OnAfterRender(fun _ ->
            this.onCreate f
        )

    /// Event triggered when a tab is loaded.
    [<JavaScript>]
    member this.OnLoad f =
        this |> OnAfterRender(fun _ ->
            this.onLoad f
        )

    /// Event triggered before a tab is loaded.
    [<JavaScript>]
    member this.OnBeforeLoad f =
        this |> OnAfterRender(fun _  ->
            this.onBeforeLoad f
        )

    /// Event triggered when a tab is activated.
    [<JavaScript>]
    member this.OnActivate f =
        this |> OnAfterRender(fun _  ->
            this.onActivate f
        )

    /// Event triggered when a tab is beforeActivated.
    [<JavaScript>]
    member this.OnBeforeActivate f =
        this |> OnAfterRender(fun _  ->
            this.onBeforeActivate f
        )
