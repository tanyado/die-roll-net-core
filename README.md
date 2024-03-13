*Note:* 

This project makes use of the Numpy.NET library (A .NET wrapper of Python's numpy library) to handle the choosing of the weighted probabilities.

*Relevant files:*

- readme.md
- Pages -> Index.cshtml
- Pages -> Index.cshtml.cs
- data -> output.csv


*Areas for improvement are below:*

I've create a simple web application to the specs of the take home-assignment. However, given more time or if this app was to be productionalize - for a more robust application the following backlog items should be made.

- Add unit test for RollTheDie() method. Using XUnit or NUnit framework. 
- Reduce the amount of type casting (likely by using more classes or restructuring code)
- Make `faces` array an enum, since it'll never change
- Use List instead of Array to make use of .Sum method and lambda functions for cleaner code. And to reduce number of for-loops.
- Validate UI input (required input, and limit numerical values for "faces")
- Also print out the values back to the View, in addition to the the `output.csv` file
- Abstract the RollSettings() class into separate file, I've left it in the same index.cshtml.cs file to simplify the project.


These changes may require several more hours of coding. And in keeping concious of time, I've listed them here instead as a backlog.
