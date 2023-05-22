using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

using System.Configuration;

namespace appium_specflow.Support
{
    public static class AppLocators
    {
        public static By colorButtonLocator(string platform)
        {
            switch (platform)
            {
                case "android":
                    {
                        return By.Id("com.lambdatest.proverbial:id/color");
                    }
                case "ios":
                    {
                        return MobileBy.AccessibilityId("Colour");
                    }
                default:
                    {
                        throw new System.Exception("Unsupported platform");
                    }
            }
        }

        public static By notificationButtonLocator(string platform)
        {
            switch (platform)
            {
                case "android":
                    {
                        return By.Id("com.lambdatest.proverbial:id/notification");
                    }
                case "ios":
                    {
                        return MobileBy.AccessibilityId("Notification");
                    }
                default:
                    {
                        throw new System.Exception("Unsupported platform");
                    }
            }
        }

        public static By textButtonLocator(string platform)
        {
            switch (platform)
            {
                case "android":
                    {
                        return By.Id("com.lambdatest.proverbial:id/Text");
                    }
                case "ios":
                    {
                        return MobileBy.AccessibilityId("Text");
                    }
                default:
                    {
                        throw new System.Exception("Unsupported platform");
                    }
            }
        }

        public static By textBoxLocator(string platform)
        {
            switch (platform)
            {
                case "android":
                    {
                        return By.Id("com.lambdatest.proverbial:id/Textbox");
                    }
                case "ios":
                    {
                        return MobileBy.AccessibilityId("Textbox");
                    }
                default:
                    {
                        throw new System.Exception("Unsupported platform");
                    }
            }
        }

        public static By toastButtonLocator(string platform)
        {
            switch (platform)
            {
                case "android":
                    {
                        return By.Id("com.lambdatest.proverbial:id/toast");
                    }
                case "ios":
                    {
                        return MobileBy.AccessibilityId("Toast");
                    }
                default:
                    {
                        throw new System.Exception("Unsupported platform");
                    }
            }

        }

        public static By speedTestButtonLocator(string platform)
        {
            switch (platform)
            {
                case "android":
                    {
                        return By.Id("com.lambdatest.proverbial:id/speedTest");
                    }
                case "ios":
                    {
                        return MobileBy.AccessibilityId("Speed Test");
                    }
                default:
                    {
                        throw new System.Exception("Unsupported platform");
                    }
            }

        }

        public static By homeButtonLocator(string platform)
        {
            switch (platform)
            {
                case "android":
                    {
                        return By.Id("com.lambdatest.proverbial:id/buttonPage");
                    }
                case "ios":
                    {
                        return MobileBy.AccessibilityId("Home");
                    }
                default:
                    {
                        throw new System.Exception("Unsupported platform");
                    }
            }
        }
    }
}

