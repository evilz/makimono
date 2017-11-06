module Menu.Types

open Fable.Import.React
open Global

type FulmaModules =
    | Elements
    | Components
    | Layouts

type Library =
    | Fulma of FulmaModules
    | FulmaExtensions

type Fulma =
    { IsElementsExpanded : bool
      IsComponentsExpanded : bool
      IsLayoutExpanded : bool }

type FulmaExtensions =
    { IsExpanded : bool }

type Model =
    { Fulma : Fulma
      FulmaExtensions : FulmaExtensions
      CurrentPage : Page }

type Msg =
    | ToggleMenu of Library