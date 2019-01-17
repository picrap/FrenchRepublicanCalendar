# French republican calendar

This is probably totally pointless. I did it for fun.  
Available as a [![NuGet Status](http://img.shields.io/nuget/v/FrenchRepublicanCalendar.svg?style=flat-square)](https://www.nuget.org/packages/FrenchRepublicanCalendar) package.

## How it works

The package exposes two main classes:
* `FrenchRepublicanDateTime`, a `DateTime` that exposes day, month and year (among others) of French Republican calendar.
* `FrenchRepublicanTimeSpan`, a `TimeSpan` that works with 10 hours a day, 100 minutes an hour and 100 seconds a minute. Right, French Republican seconds are not the same as our seconds. Ils sont fous ces français.
* `FrenchRepublicanCalendar`, a `Calendar` implementation for French Republican calendar.

## More about the French republican calender
Documentation and code come from https://www.fourmilab.ch/documents/calendar/  
More information about calendar can be found at https://en.wikipedia.org/wiki/French_Republican_calendar
