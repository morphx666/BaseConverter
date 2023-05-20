# BaseConverter
As simple numeric base conversion utility

```
BaseConverter 1.0.0.0

Usage:
    bc value fromBase toBase

    value: The numeric value to convert
           It can also be defined as a range of values: a-b
    fromBase: The base in which the value is expressed
    toBase: The desired target base

Example:
    q> bc 37 5 9
        (convert 37 from base 5 to base 9)
    r> 37₅ = 24₉

    q> bc 7-12 11 3
        (convert all values from 7 to 12 from base 11 to base 3)
    r> 7₁₁ = 21₃
    r> 8₁₁ = 22₃
    r> 9₁₁ = 100₃
    r> A₁₁ = 101₃
    r> 10₁₁ = 102₃
    r> 11₁₁ = 110₃
    r> 12₁₁ = 111₃
```
