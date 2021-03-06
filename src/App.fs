module App.View

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser
open Types
open App.State
open Global

importAll "../sass/main.sass"

open Fable.Helpers.React
open Fable.Helpers.React.Props

let menuItem label page currentPage =
    li
      [ ]
      [ a
          [ classList [ "is-active", page = currentPage ]
            Href (toHash page) ]
          [ str label ] ]


let menuItemAndSub label page currentPage =
    li
      [ ]
      [ a
          [ classList [ "menu-group", true;  "is-active", page = currentPage ]
            Href (toHash page) ]
          [ str label ]
        ul [  ] [
            menuItem "F#" (Page.Fsharp (FsharpPage.IntroductionToFunctionalProgramming)) currentPage
            menuItem "F#" (Page.Fsharp (FsharpPage.QuickStarts)) currentPage
          ]
      ]

let menu currentPage =
  aside
    [ ClassName "menu" ]
    [ p
        [ ClassName "menu-label" ]
        [ str "General" ]
      ul
        [ ClassName "menu-list" ]
        [ menuItem "Home" Home currentPage
          menuItem "C#" (Page.Csharp (GetStarted))  currentPage
          menuItemAndSub "F#" (Page.Fsharp (FsharpPage.GetStarted)) currentPage ] ]

let root model dispatch =

  let pageHtml =
    function
    | Page.Fsharp _ -> Info.View.root
    | Page.Csharp _ -> Counter.View.root model.counter (CounterMsg >> dispatch)
    | Home -> Home.View.root model.home (HomeMsg >> dispatch)

  div
    []
    [ div
        [ ClassName "navbar-bg" ]
        [ div
            [ ClassName "container" ]
            [ Navbar.View.root ] ]
      div
        [ ClassName "section" ]
        [ div
            [ ClassName "container" ]
            [ div
                [ ClassName "columns" ]
                [ div
                    [ ClassName "column is-3" ]
                    [ menu model.currentPage ]
                  div
                    [ ClassName "column" ]
                    [ pageHtml model.currentPage ] ] ] ] ]

open Elmish.React
open Elmish.Debug
open Elmish.HMR

// App
Program.mkProgram init update root
|> Program.toNavigable (parseHash pageParser) urlUpdate
#if DEBUG
|> Program.withDebugger
|> Program.withHMR
#endif
|> Program.withReact "elmish-app"
|> Program.run
