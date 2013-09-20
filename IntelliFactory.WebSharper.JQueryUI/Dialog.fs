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

type DialogConfiguration[<JavaScript>]() =

    [<Name "appendTo">]
    [<Stub>]
    //"body" by default
    member val AppendTo = Unchecked.defaultof<string> with get, set

    [<Name "autoOpen">]
    [<Stub>]
    //true by default
    member val AutoOpen = Unchecked.defaultof<bool> with get, set

    [<Name "buttons">]
    //Stub type to be confirmed (string -> (string -> unit) -> unit)
    [<Stub>]
    //"" by default
    member val Buttons = Unchecked.defaultof<string> with get, set

    [<Name "closeOnEscape">]
    [<Stub>]
    //false by default
    member val CloseOnEscape = Unchecked.defaultof<bool> with get, set

    [<Name "closeText">]
    [<Stub>]
    //"close" by default
    member val CloseText = Unchecked.defaultof<string> with get, set

    [<Name "dialogClass">]
    [<Stub>]
    //"" by default
    member val DialogClass = Unchecked.defaultof<string> with get, set

    [<Name "draggable">]
    [<Stub>]
    //true by default
    member val Draggable = Unchecked.defaultof<bool> with get, set

    [<Name "height">]
    [<Stub>]
    member val Height = Unchecked.defaultof<int> with get, set

    [<Name "hide">]
    [<Stub>]
    //"" by default
    member val Hide = Unchecked.defaultof<string> with get, set

    [<Name "maxHeight">]
    [<Stub>]
    member val MaxHeight = Unchecked.defaultof<int> with get, set

    [<Name "maxWidth">]
    [<Stub>]
    member val MaxWidth = Unchecked.defaultof<int> with get, set

    [<Name "minHeight">]
    [<Stub>]
    member val MinHeight = Unchecked.defaultof<int> with get, set

    [<Name "minWidth">]
    [<Stub>]
    member val MinWidth = Unchecked.defaultof<int> with get, set

    [<Name "modal">]
    [<Stub>]
    //false by default
    member val Modal = Unchecked.defaultof<bool> with get, set

    [<Name "position">]
    [<Stub>]
    //"center" by default
    member val Position = Unchecked.defaultof<Position> with get, set

    [<Name "resizable">]
    [<Stub>]
    //true by default
    member val Resizable = Unchecked.defaultof<bool> with get, set

    [<Name "show">]
    [<Stub>]
    //"" by default
    member val Show = Unchecked.defaultof<string> with get, set

    [<Name "title">]
    [<Stub>]
    //"" by default
    member val Title = Unchecked.defaultof<string> with get, set

    [<Name "width">]
    [<Stub>]
    //300 by default
    member val Width = Unchecked.defaultof<int> with get, set


module DialogInternal =
    [<Inline "jQuery($el).dialog($conf)">]
    let Init (el: Dom.Element, conf: DialogConfiguration) = ()


[<Require(typeof<Dependencies.JQueryUIJs>)>]
[<Require(typeof<Dependencies.JQueryUICss>)>]
type Dialog[<JavaScript>]internal () =
    inherit Pagelet()

    /// Create a new dialog using the given element
    /// and configuration object.
    [<JavaScript>]
    [<Name "New1">]
    static member New (el: Element, conf: DialogConfiguration): Dialog =
        let d = new Dialog()
        el
        |> OnAfterRender(fun el ->
            DialogInternal.Init(el.Body, conf)
        )
        d.element <- el
        d

    /// Creates a new dialog given an element using
    /// default configuration settings.
    [<JavaScript>]
    [<Name "New2">]
    static member New (el: Element): Dialog =
        Dialog.New(el, DialogConfiguration())

    (****************************************************************
    * Methods
    *****************************************************************)
    /// Remove the dialog functionality.
    [<Inline "jQuery($this.element.Body).dialog('destroy')">]
    member this.Destroy() = ()

    /// Disables the dialog.
    [<Inline "jQuery($this.element.Body).dialog('disable')">]
    member this.Disable () = ()

    /// Enables the dialog.
    [<Inline "jQuery($this.element.Body).dialog('enable')">]
    member this.Enable () = ()

    /// Sets dialog option.
    [<Inline "jQuery($this.element.Body).dialog('option', $name, $value)">]
    member this.Option (name: string, value: obj) = ()

    /// Gets dialog option.
    [<Inline "jQuery($this.element.Body).dialog('option', $name)">]
    member this.Option (name: string) = X<obj>

    /// Gets all options.
    [<Inline "jQuery($this.element.Body).dialog('option')">]
    member this.Option () = X<DialogConfiguration>

    /// Sets one or more options.
    [<Inline "jQuery($this.element.Body).dialog('option', $options)">]
    member this.Option (options: DialogConfiguration) = X<unit>

    [<Inline "jQuery($this.element.Body).dialog('widget')">]
    member private this.getWidget () = X<Dom.Element>

    /// Returns the .ui-dialog element.
    [<JavaScript>]
    member this.Widget = this.getWidget()

    /// Close dialog.
    [<Inline "jQuery($this.element.Body).dialog('close')">]
    member this.Close () = ()

    /// Returns whether the dialog is open or not.
    [<Inline "jQuery($this.element.Body).dialog('isOpen')">]
    member this.IsOpen () = false

    /// Move the dialog to the top of the dialogs stack.
    [<Inline "jQuery($this.element.Body).dialog('moveToTop')">]
    member this.MoveToTop () = ()

    /// Open the dialog.
    [<Inline "jQuery($this.element.Body).dialog('open')">]
    member this.Open () = ()

    (****************************************************************
    * Events
    *****************************************************************)
    [<Inline "jQuery($this.element.Body).bind('dialogcreate', function (x,y) {$f(x);})">]
    member private this.onCreate(f : JQuery.Event -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('dialogbeforeclose', function (x,y) {$f(x);})">]
    member private this.onBeforeClose(f : JQuery.Event -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('dialogopen', function (x,y) {$f(x);})">]
    member private this.onOpen(f : JQuery.Event -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('dialogfocus', function (x,y) {$f(x);})">]
    member private this.onFocus(f : JQuery.Event -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('dialogdragstart', function (x,y) {$f(x);})">]
    member private this.onDragStart(f : JQuery.Event -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('dialogdrag', function (x,y) {$f(x);})">]
    member private this.onDrag(f : JQuery.Event -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('dialogdragstop', function (x,y) {$f(x);})">]
    member private this.onDragStop(f : JQuery.Event -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('dialogresizestart', function (x,y) {$f(x);})">]
    member private this.onResizeStart(f : JQuery.Event -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('dialogresize', function (x,y) {$f(x);})">]
    member private this.onResize(f : JQuery.Event -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('dialogresizestop', function (x,y) {$f(x);})">]
    member private this.onResizeStop(f : JQuery.Event -> unit) = ()

    [<Inline "jQuery($this.element.Body).bind('dialogclose', function (x,y) {$f(x);})">]
    member private this.onClose(f : JQuery.Event -> unit) = ()


    /// Triggered before the dialog is closed.
    [<JavaScript>]
    member this.OnCreate(f : JQuery.Event -> unit) =
        this |> OnAfterRender (fun _ -> this.onCreate f)

    /// Triggered before the dialog is closed.
    [<JavaScript>]
    member this.OnBeforeClose(f : JQuery.Event -> unit) =
        this |> OnAfterRender (fun _ -> this.onBeforeClose f)

    /// Triggered before the dialog is opened.
    [<JavaScript>]
    member this.OnOpen (f : JQuery.Event -> unit) =
        this |> OnAfterRender (fun _  -> this.onOpen f)

    /// Triggered before the dialog gains focus.
    [<JavaScript>]
    member this.OnFocus (f : JQuery.Event -> unit) =
        this |> OnAfterRender (fun _  -> this.onFocus f)

    /// Triggered at the beginning of the dialog being dragged
    [<JavaScript>]
    member this.OnDragStart (f : JQuery.Event -> unit) =
        this |> OnAfterRender (fun _ -> this.onDragStart f)


    /// Triggered when the dialog is dragged.
    [<JavaScript>]
    member this.OnDrag (f : JQuery.Event -> unit) =
        this |> OnAfterRender (fun _ -> this.onDrag f)


    /// Triggered after the dialog has been dragged.
    [<JavaScript>]
    member this.OnDragStop (f : JQuery.Event -> unit) =
        this |> OnAfterRender (fun _ -> this.onDragStop f)

    /// Triggered before the dialog is being resized.
    [<JavaScript>]
    member this.OnResizeStart (f : JQuery.Event -> unit) =
        this |> OnAfterRender (fun _ -> this.onResizeStart f)

    /// Triggered when the dialog is being resized.
    [<JavaScript>]
    member this.OnResize (f : JQuery.Event -> unit) =
        this |> OnAfterRender (fun _ -> this.onResize f)

    /// Triggered after the dialog has been resized.
    [<JavaScript>]
    member this.OnResizeStop (f : JQuery.Event -> unit) =
        this |> OnAfterRender (fun _ -> this.onResizeStop f)

    /// Triggered when the dialog is closed.
    [<JavaScript>]
    member this.OnClose (f : JQuery.Event -> unit) =
        this |> OnAfterRender (fun _ -> this.onClose f)





