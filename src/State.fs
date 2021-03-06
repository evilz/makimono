module App.State

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Import.Browser
open Global
open Types

let pageParser: Parser<Page->Page,Page> =
  oneOf [
    map Home (s "home")
    // Charp
    map (Csharp (GetStarted)) (s "csharp")
    map (Csharp (QuickStarts)) (s "csharp" </> s "quick-starts")
    map (Csharp (GetStarted)) (s "csharp" </> s "tour-of-csharp")
    // Fsharp
    map (Fsharp (FsharpPage.GetStarted)) (s "fsharp")
    map (Fsharp (FsharpPage.QuickStarts)) (s "fsharp" </> s "quick-starts")
    map (Fsharp (FsharpPage.IntroductionToFunctionalProgramming)) (s "fsharp" </> s "introduction-to-functional-programming")
  ]

let urlUpdate (result: Option<Page>) model =
  match result with
  | None ->
    console.error("Error parsing url")
    model,Navigation.modifyUrl (toHash model.currentPage)
  | Some page ->
      { model with currentPage = page }, []

let init result =
  let (counter, counterCmd) = Counter.State.init()
  let (home, homeCmd) = Home.State.init()
  let (model, cmd) =
    urlUpdate result
      { currentPage = Home
        counter = counter
        home = home }
  model, Cmd.batch [ cmd
                     Cmd.map CounterMsg counterCmd
                     Cmd.map HomeMsg homeCmd ]

let update msg model =
  match msg with
  | CounterMsg msg ->
      let (counter, counterCmd) = Counter.State.update msg model.counter
      { model with counter = counter }, Cmd.map CounterMsg counterCmd
  | HomeMsg msg ->
      let (home, homeCmd) = Home.State.update msg model.home
      { model with home = home }, Cmd.map HomeMsg homeCmd
