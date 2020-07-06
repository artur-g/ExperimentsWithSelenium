Artur G
-----------------
DISCLAIMER:
Technically I cheated. I've read: https://github.com/qxf2/qa-interview-web-application and https://qxf2.com/a-better-way-to-interview-qa
Well, in my defense, I like using GitHub and searching for solutions is part of being a programmer, so I couldnt help myself.
-----------------

Technically I consider this solution a HACK. It needs xUnit and strong refactoring.
According to this: http://jimevansmusic.blogspot.com/2018/03/deprecating-parts-of-seleniums-net.html and this: https://stackoverflow.com/questions/48734097/the-name-pagefactory-does-not-exist-in-the-current-context
Selenium dropped/deprecated support for PageFactory object and other bindings in .Net circa 2018.
I was forced to use https://www.nuget.org/packages/DotNetSeleniumExtras.PageObjects.Core that is NOT supported by oryginal Selenium team. So my solution technically isnt "Best Practice".
Additionally I SHOULD have used xUnit to this. I found out too late, that it can be run on Linux, so I will try update my solution before a deadline but I dont think I will be fast enough.

Installation prerequisite:
https://docs.microsoft.com/en-us/dotnet/core/install/linux
Or https://dotnet.microsoft.com/download/dotnet/current

Installation (Debian):

install runtime enviornment
sudo apt-get install dotnet-runtime-3.1

(optional) install SDK
sudo apt-get install dotnet-sdk-3.1

Run:
run ./ExperimentsWithSelenium located in ./ExperimentsWithSelenium/bin/Release/netcoreapp3.1/publish

AFAIK nothing else is needed AKA "Works on mine"
-----------------

Discoveries:
-Firefox is EXTRA slow in automated testing.
-Mixup with Privacy and ToS was discovered by automated test.
-Wasnt able to access "Web Console" in C# in Firefox or Chrome. Waiting has to be implemented without feedback from console.
-Once again, xUnit should be used here.
-Result validation should be made only by analyzing stored "challenge and response values" and not calculating Factorial inside test
-Timeouts (connected to not handling "web console") while inputting randomly invalid data (strings) and too big numbers can lead to false positives. 
	After timeout occures, anything can happen, ajax can suddenly or never appear and it throws system into chaos (even with resetting forms).
	Possible solution to that is to monitor "Web Console" but I was unable to do so.


SCENARIOS:
-----------------

Feature: Actor wants to calculate factorial of X

  Scenario Outline: Actor provides valid (0-170) <integer> to calculate its factorial
    Given Actor visits home page
      And Actor inputs valid (0-170) <integer>
     When Actor pressed "Calculate!" (Submit button)
     Then Home page shows calculation <result>
  Examples:
    |   integer   |  result |
    |      0      |    1    |
    |      1      |    1    |
    |    169      |  (169!) |
    |    170      |  (170!) |
    |rand(0,170)=x|    x!   |	 

  Scenario Outline: Actor provides invalid <input> to calculate its factorial
    Given Actor visits home page
      And Actor inputs invalid <input>
     When Actor pressed "Calculate!" (Submit button)
     Then Home page shows error message "Please enter an integer"
  Examples:
    |    input    |
    |     -1      | 
    |    -11      | 
    |    171      | 
    |    180      | 
    | rand(171,x) | 	 
	|    989      | 
    |    990      | 
    |  23ff355    |
    |    1 71     | 
    |    d111     | 
    |    171dss   | 
    |     1&      | 
    |    "2"      | 
    |    '3'      | 


Feature: Actor has access to document describing "Terms and Conditions" of this site.

  Scenario: Actor wants to read "Terms and Conditions"
    Given Actor visits home page
     When Actor clicks "Terms and Conditions" link
     Then Actor is redirected to (or downloads) "Terms and Conditions" document. 
	  
Feature: Actor has access to document describing "Privacy" policy of this site.

  Scenario: Actor wants to read "Privacy" policy
    Given Actor visits home page
     When Actor clicks "Privacy" link
     Then Actor is redirected to (or downloads) "Privacy" document. 
	  
Feature: Actor has access to document describing "Copyright" ownership of this site.

  Scenario: Actor wants to know who owns site "Copyright"
    Given Actor visits home page
     When Actor clicks "Copyright" link
     Then Actor is redirected to (or downloads) "Copyright" document or copyrights owner site. 

-----------------
SOURCES:
https://github.com/qxf2/qa-interview-web-application
https://qxf2.com/a-better-way-to-interview-qa
https://www.selenium.dev/documentation/en/guidelines_and_recommendations/page_object_models/
https://www.pluralsight.com/guides/getting-started-with-page-object-pattern-for-your-selenium-tests
https://www.automatetheplanet.com/selenium-webdriver-csharp-cheat-sheet/
https://www.swtestacademy.com/page-object-model-c/

-----------------
BUGS:
-"Enter an integer" - misleading, should be "positive integer" and/or "smaller or equal to 170"
-Form accepts integers up to 989 (not 170), above that number, it does not send any error data to user, only throws "HTTP/1.1 500 INTERNAL SERVER ERROR" in Ajax call. No validation of integer range.
-Cannot use only keyboard to input data (no "Enter" key support)
-string "      4   " is treated as (int) 4. Technically its still string (error), but parsing spaces was a good idea IMHO.
-if you are fast enough, you can queue multiple ajax requests to a server. There is no mechanism that blocks user from sending multiple requests before server spits out an answer (AsyncCall). 
	I suggest having animated waiting icon of some sort, and blocking input fields until there is a response from Ajax call. Also, implement error handling for Ajax communication. 
-(cosmetic, minor) arrow is misleading, it looks like a button and cannot be clicked
-(cosmetic, minor) There is big gap between "The greatest factorial calculator!" and top of the page. On small resolution screens it will look unnatural. 
	I suggest using some kind of horizontal alignment instead of hard pixel (count) barier on top.  
-User is not aware of limitation of input range of the integer (0-170)
-The requirement "The app should work without any layout issues in any resolutions." is unachievable. 1px x 1px resolution app is unachievable. 
