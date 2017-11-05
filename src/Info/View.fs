module Info.View

open Fable.Helpers.React
open Fable.Helpers.React.Props

let root =
  div
    [  ]
    [ h1
        [ ]
        [ str "About page" ]
      p [ ] [ str "This template is a simple application build with Fable + Elmish + React." ] 

      article [ClassName "message card is-dark"] [
        div [ClassName "message-header"] [
          p [] [str "Hello World"]
          button [ClassName "delete"] [] //   aria-label="delete"></button>
        ]
        div [ClassName "message-body"] [
          str "Lorem ipsum dolor sit amet, consectetur adipiscing elit. <strong>Pellentesque risus mi</strong>, tempus quis placerat ut, porta nec nulla. Vestibulum rhoncus ac ex sit amet fringilla. Nullam gravida purus diam, et dictum <a>felis venenatis</a> efficitur. Aenean ac <em>eleifend lacus</em>, in mollis lectus. Donec sodales, arcu et sollicitudin porttitor, tortor urna tempor ligula, id porttitor mi magna a neque. Donec dui urna, vehicula et sem eget, facilisis sodales sem."
        ]
      
       ]
  ]

