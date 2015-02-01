module ReverserPropertyTests

open FsCheck
open NUnit.Framework
open CaseStudy

type R = Reverser

let myConfig = Config.VerboseThrowOnFailure

[<Test>]
let ``string reversed and then reversed again is same as original string``() =
    Check.One(myConfig, (fun s -> R.Reverse(R.Reverse(s)) = s));
