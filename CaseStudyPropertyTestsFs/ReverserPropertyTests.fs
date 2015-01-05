module ReverserPropertyTests

open FsCheck
open FsCheck.NUnit
open CaseStudy

type R = Reverser

let myConfig = Config.VerboseThrowOnFailure

[<Property>]
let ``string reversed and then reversed again is same as original string``() =
    Check.One(myConfig, (fun s -> R.Reverse(R.Reverse(s)) = s));
