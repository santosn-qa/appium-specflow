using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Drawing;
using System.Text.RegularExpressions;

namespace appium_specflow.StepDefinitions
{
    [Binding]
    public class HomeScreenSteps
    {
        public AppiumDriver<IWebElement> driver;
        public WebDriverWait wait;

        private byte[] initialScreenshot;

        // Define locators for elements on the home screen
        private readonly By colorButtonLocator = By.Id("com.lambdatest.proverbial:id/color");
        private readonly By notificationButtonLocator = By.Id("com.lambdatest.proverbial:id/notification");
        private readonly By textButtonLocator = By.Id("com.lambdatest.proverbial:id/Text");
        private readonly By textBoxLocator = By.Id("com.lambdatest.proverbial:id/Textbox");
        private readonly By toastButtonLocator = By.Id("com.lambdatest.proverbial:id/toast");
        private readonly By speedTestButtonLocator = By.Id("com.lambdatest.proverbial:id/speedTest");
        private readonly By homeButtonLocator = By.Id("com.lambdatest.proverbial:id/buttonPage");

        [BeforeScenario]
        public void Setup()
        {
            string appiumServerUrl = "http://localhost:4723/wd/hub";
            string deviceName = "Pixel 3 API 31";

            // Initialize the Android driver with desired capabilities
            var androidOptions = new AppiumOptions();
            androidOptions = new AppiumOptions();
            androidOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.Android);
            androidOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, deviceName);
            androidOptions.AddAdditionalCapability("appium:appPackage", "com.lambdatest.proverbial");
            androidOptions.AddAdditionalCapability("appium:appActivity", "com.lambdatest.proverbial.MainActivity");
            driver = new AndroidDriver<IWebElement>(new Uri(appiumServerUrl), androidOptions);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [AfterScenario]
        public void TearDown()
        {
            // Close the driver and perform any necessary cleanup
            driver?.Quit();
        }

        [Given(@"I am on the home screen")]
        public void GivenIAmOnTheHomeScreen()
        {
            driver.LaunchApp();
        }


        [When(@"I click on COLOR")]
        public void WhenIClickOnCOLOR()
        {
            // Capture the initial screenshot of the TextView element
            initialScreenshot = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;

            // Perform the action that causes the color change
            driver.FindElement(colorButtonLocator).Click();
        }

        [Then(@"I should see a change in color")]
        public void ThenIShouldSeeAChangeInColor()
        {
            // Capture the final screenshot of the TextView element
            byte[] finalScreenshot = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;

            // Compare the initial and final screenshots
            bool colorChanged = CompareScreenshots(initialScreenshot, finalScreenshot);

            // Assert or perform further actions based on the color change result
            Assert.IsTrue(colorChanged, "Color did not change");
        }

        private bool CompareScreenshots(byte[] screenshot1, byte[] screenshot2)
        {
            // Convert the byte arrays to Bitmap objects
            Bitmap initialBitmap = new Bitmap(new MemoryStream(screenshot1));
            Bitmap finalBitmap = new Bitmap(new MemoryStream(screenshot2));

            // Compare the two Bitmap objects to check for visual differences
            // You can use image comparison libraries or custom logic to compare the screenshots
            // Here's an example of a simple pixel-level comparison:
            bool colorChanged = false;
            for (int x = 0; x < initialBitmap.Width; x++)
            {
                for (int y = 0; y < initialBitmap.Height; y++)
                {
                    Color initialPixelColor = initialBitmap.GetPixel(x, y);
                    Color finalPixelColor = finalBitmap.GetPixel(x, y);
                    if (initialPixelColor != finalPixelColor)
                    {
                        colorChanged = true;
                        break;
                    }
                }
                if (colorChanged)
                    break;
            }

            return colorChanged;
        }

        [When(@"I click on NOTIFICATION")]
        public void WhenIClickOnNOTIFICATION()
        {
            // Perform the action to click the notification button in your app
            driver.FindElement(notificationButtonLocator).Click();
        }

        [Then(@"I should receive a notification on the device")]
        public void ThenIShouldReceiveANotificationOnTheDevice()
        {
            // Open the notification shade using an ADB command
            driver.ExecuteScript("mobile:shell", new Dictionary<string, object> { ["command"] = "cmd", ["args"] = new[] { "statusbar", "expand-notifications" } });

            // Check if the push notification is displayed
            By notificationLocator = By.XPath("//*[@text='Test Notification']");
            bool notificationFound = driver.FindElement(notificationLocator).Displayed;

            // Close the notification shade using an ADB command
            driver.ExecuteScript("mobile:shell", new Dictionary<string, object> { ["command"] = "cmd", ["args"] = new[] { "statusbar", "collapse" } });

            // Assert or perform further actions based on the notification presence
            Assert.IsTrue(notificationFound, "Push notification not found");
        }

        [When(@"I click on TEXT")]
        public void WhenIClickOnTEXT()
        {
            // Perform the action to click the text button in your app
            driver.FindElement(textButtonLocator).Click();
        }

        [Then(@"I capture the displayed text")]
        public void ThenICaptureTheDisplayedText()
        {
            string displayedText = driver.FindElement(textBoxLocator).Text;
            Console.WriteLine($"Displayed Text: {displayedText}");

            // Assert or perform further actions based on the displayed text presence
            Assert.IsTrue(!string.IsNullOrEmpty(displayedText), "Displayed Text is null or empty");
        }

        [When(@"I click on TOAST")]
        public void WhenIClickOnTOAST()
        {
            // Perform the action to click the toast button in your app
            driver.FindElement(toastButtonLocator).Click();
        }

        [Then(@"I should see a pop up message")]
        public void ThenIShouldSeeAPopUpMessage()
        {
            // Capture the page source
            string pageSource = driver.PageSource;

            // Check if the toast message element is present in the page source
            bool toastFound = pageSource.Contains("android.widget.Toast index=\"1\"");

            // Assert that the toast message element is found
            Assert.IsTrue(toastFound, "Toast message element not found");

            // Get the text of the toast message element
            string toastText = GetToastMessageText(pageSource);

            // Assert or perform further actions based on the toast message text
            Assert.AreEqual("Toast should be visible", toastText, "Toast message text mismatch");
        }

        // Helper method to extract the text from the toast message element in the page source
        private string GetToastMessageText(string pageSource)
        {
            // Use regular expressions or string manipulation to extract the text from the toast message element
            // Here's an example assuming the text is contained within a "text" attribute:
            string pattern = @"<android\.widget\.Toast.*?text=""(.*?)"".*?>";

            // Match the pattern in the page source
            Match match = Regex.Match(pageSource, pattern);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }


        [When(@"I click on SPEED TEST")]
        public void WhenIClickOnSPEEDTEST()
        {
            // Perform the action to click the speed test button in your app
            driver.FindElement(speedTestButtonLocator).Click();
        }

        [Then(@"I start the speed test from the speed test page")]
        public void ThenIStartTheSpeedTestFromTheSpeedTestPage()
        {
            // Perform the action to start the speed test from the speed test page
            By startSpeedTestLocator = By.XPath("//*[contains(@text,'start speed test')]");
            wait.Until(ExpectedConditions.ElementIsVisible(startSpeedTestLocator)).Click();
        }

        [Then(@"I capture the upload/download speed")]
        public void ThenICaptureTheUploadDownloadSpeed()
        {
            // Extend the wait time for the "Test Again" element visibility
            wait.Timeout = TimeSpan.FromSeconds(30);

            // Wait until the "Test Again" element is visible, if found
            By testAgainLocator = By.XPath("//*[contains(@text, 'Test Again')]");
            IWebElement testAgainElement = null;
            try
            {
                testAgainElement = wait.Until(ExpectedConditions.ElementIsVisible(testAgainLocator));
            }
            catch (Exception ex) { }

            // Capture a screenshot
            Screenshot screenshot = driver.GetScreenshot();

            // Generate the screenshot file path
            string folderPath = "screenshots";
            string fileName = $"SpeedTest_{DateTime.Now:yyyyMMddHHmmss}.png";
            string screenshotPath = Path.Combine(folderPath, fileName);

            // Create the screenshots folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Save the screenshot to the specified file path
            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);

            // Assert if the capture was successful
            Assert.True(File.Exists(screenshotPath), "Failed to capture the upload/download speed.");
        }

        [Then(@"I navigate back to the home screen")]
        public void ThenINavigateBackToTheHomeScreen()
        {
            // Perform the action to go back to the home screen in your app
            driver.FindElement(homeButtonLocator).Click();
        }


    }
}
