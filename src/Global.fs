module Global

type FsharpPage =
  | GetStarted
  | QuickStarts
  | IntroductionToFunctionalProgramming

type CsharpPage  =
  | GetStarted
  | QuickStarts
  | TourOfCsharp‎


type Page =
  | Home
  | Csharp of CsharpPage
  | Fsharp of FsharpPage

 

let toHash page =
  match page with
  | Home -> "#home"
  | Csharp subpage -> match subpage with
                      | GetStarted -> "#csharp/"
                      | QuickStarts -> "#csharp/quick-starts/"
                      | TourOfCsharp‎ -> "#csharp/tour-of-csharp/"
    
  | Fsharp subpage -> match subpage with
                      | FsharpPage.GetStarted -> "#fsharp/"
                      | FsharpPage.QuickStarts -> "#fsharp/quick-starts/"
                      | IntroductionToFunctionalProgramming -> "#fsharp/introduction-to-functional-programming/"
