module Home.View

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Types

[<Pojo>]
type DangerousInnerHtml =
    { __html : string }
let markdownSample:string = importAll "../sample.md"


let root model dispatch =
 div
    [ ]
    [ p
        [ ClassName "control" ]
        [ input
            [ ClassName "input"
              Type "text"
              Placeholder "Type your name"
              DefaultValue model
              AutoFocus true
              OnChange (fun ev -> !!ev.target?value |> ChangeStr |> dispatch ) ] ]
      br [ ]
      span
        [ ]
        [ str (sprintf "Hello %s" model) ] 
      div [ ClassName "content"; DangerouslySetInnerHTML { __html = markdownSample } ]  []
        ]
