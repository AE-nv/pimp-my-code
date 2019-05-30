Mars Rover Kata in F#
===

This solution is written in [F#](https://fsharpforfunandprofit.com/), a functional programming language on the .NET platform.

This solution makes use of:

* Immutable [Record and Sum types](https://fsharpforfunandprofit.com/posts/type-size-and-design/).
* The [Result monad](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/results) to handle rover crashes.
* Tooling used in the F# sphere: Unquote & Paket

Running the program
---

The solution is written as an F# script. This can be interpreted in the [fsi REPL](https://docs.microsoft.com/en-us/dotnet/fsharp/tutorials/fsharp-interactive/) which is included in Visual Studio & easily installed in [VSCode](http://ionide.io/). If you just want to play around with F#, I copied my solution on [repl.it](https://repl.it/@praGmatic/BoldFlakySecurity) where you can play around with the code in the browser.

Unquote
---

[Unquote](https://github.com/SwensenSoftware/unquote) is a popular testing library for F#. You can write any boolean expression as a test, which will fail if the expression evaluates to false. The neat feature of Unquote is that on failure it will give you a *step by step* evaluation of the expression.

Paket
---

Paket is an alternative for Nuget, very popular in the F# community.

More info [here](https://fsprojects.github.io/Paket/getting-started.html).

Installing dependencies for F#: in this folder, run `.paket\paket.exe install`