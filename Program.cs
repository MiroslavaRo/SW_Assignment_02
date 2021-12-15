using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using OpenQA.Selenium.DevTools;

namespace Assignment_02
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string address = "https://en.wikipedia.org/wiki/Software_metric";
            Console.Write("test case started ");
            //create the reference for the browser  
            IWebDriver driver = new ChromeDriver();
            // navigate to URL  
            driver.Navigate().GoToUrl(address);
            Thread.Sleep(20);        
            //close the browser  
            driver.Close();
            Console.Write("test case ended ");*/
            remove_open_files(PATH_RESULTS);
            remove_open_files(PERF_PATH);
            remove_open_files(PATH_DICTS);
            remove_open_files(PATH_AVERAGE);
            var v = new TestResults();
            var v2=v.test_on_web_page();
           // my_func();

        }

        public static string ADDRESS = "https://en.wikipedia.org/wiki/Software_metric";
        public static void remove_open_files(string file_path)
        {
            if (File.Exists(file_path))
            {
                File.Delete(file_path);
            }
        }
        public static string PATH = @"D:\VUM STUDY\2 year\1 semester\Software Metrics - Tools and Methodology\3. Assignment\Assignment_02\Assignment_02\Assignment_02_python\";
        public static string PATH_RESULTS = @"perfomance_result_C.txt";
        public static string PERF_PATH = @"perfomance_C.csv";
        public static string PATH_DICTS = @"perfomance_dictionaries_C.csv";
        public static string PATH_AVERAGE = @"perfomance_dictionaries_C.csv";

        public StreamWriter all_results_file_object = new StreamWriter("perfomance_result_C.txt");
        public StreamWriter perf_file_object = new StreamWriter("perfomance_C.csv");
        public StreamWriter dicts_file_object = new StreamWriter("perfomance_dictionaries_C.csv");
        public StreamWriter ave_file_object = new StreamWriter("perfomance_dictionaries_C.csv");

        public class TestResults
        {    
            public virtual object test_on_web_page()
            {

                //Creating Chrome driver instance
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl(ADDRESS);
                Thread.Sleep(10);
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                string script = "return window.performance.getEntries();";
                var perf = js.ExecuteScript(script).ToString();
                var name = perf.IndexOf()
                    Console.WriteLine($"{curr['name']},{curr['duration']}\n");
                
                Console.WriteLine("");
                driver.Quit();
                return perf;

            }


        }
        }
    /*
        public void names_durations_dicts(Dictionary<string, float[]> names_durations)
        {
            all_results_file_object.WriteLine("--------Names and list of durations-------\n");
            for (var j=0;j<names_durations.Count;j++)
            {
                all_results_file_object.WriteLine("{j}:{names_durations[j]}\n");
                var perf_list = "";
                if (names_durations is float)
                {
                    perf_list = names_durations[j];
                }
                else
                {
                    foreach (var num in names_durations[j])
                    {
                        perf_list = num.ToString() + "," + perf_list;
                    }
                }
                dicts_file_object.WriteLine("{j},{perf_list}\n");
                perf_list = "";
            }
        }

        public static void average_perfomance(Dictionary<string, float[]> names_durations)
        {
            var average_dict = new Dictionary<object, object>
            {
            };
            all_results_file_object.WriteLine("--------Average Perfomance-------\n");
            Console.WriteLine("--------Average Perfomance-------\n");
            foreach (var j in names_durations)
            {
                if (names_durations[j] is float | names_durations[j] is int)
                {
                    average_dict[j] = names_durations[j];
                    all_results_file_object.WriteLine("{j}:{average_dict[j]}\n");
                    ave_file_object.WriteLine("{j},{average_dict[j]}\n");
                }
                else
                {
                    var average = names_durations[j].Sum() / names_durations[j].Count;
                    average_dict[j] = average;
                    all_results_file_object.WriteLine("{j}:{average_dict[j]}\n");
                    ave_file_object.WriteLine("{j},{average_dict[j]}\n");
                    average = 0;
                }
                Console.WriteLine("{j}:{average_dict[j]}\n");
            }
        }
        public void my_func()
        {
            var general_dict = new Dictionary<object, object>();
            var names_durations = new Dictionary<object, object>();
           
           // float average = 0;
            all_results_file_object.WriteLine("--------All Perfomances-------\n");
            var i = 0;
            var times = 3;
            for (i=0;i<times;i++)
            {
                // run "time" times
                TestResults test_results = new TestResults();
                var result = test_results.test_on_web_page();
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("end session {i+1}");
                var dict = new Dictionary<string, float[]>();

                foreach (var curr in result.ToString())
                {
                    dict[curr["name"]] = curr["duration"];
                    perf_file_object.write("{curr['name']},{curr['duration']}\n");
                }
                general_dict["dict_{i}"] = dict;
                all_results_file_object.write("Dictionary #{i+1}\n{dict}\n");
                // list of durations for one name
                if (i > 1)
                {
                    foreach (var k in general_dict["dict_0"].keys())
                    {
                        if (general_dict["dict_0"].keys().Contains(k))
                        {
                            if (general_dict["dict_{i}"].keys().Contains(k))
                            {
                                names_durations[k].append(general_dict["dict_{i}"][k]);
                            }
                            else
                            {
                                names_durations[k].append(0);
                            }
                        }
                        else if (general_dict["dict_{i}"].keys().Contains(k))
                        {
                            names_durations[k].append(0);
                        }
                    }
                    general_dict["dict_0"] = names_durations;
                }
                else if (i > 0)
                {
                    foreach (var k in general_dict["dict_0"].keys())
                    {
                        if (general_dict["dict_0"].keys().Contains(k))
                        {
                            if (general_dict["dict_1"].keys().Contains(k))
                            {
                                names_durations[k] = new List<object> {
                                    general_dict["dict_0"][k],
                                    general_dict["dict_1"][k]
                                };
                            }
                            else
                            {
                                names_durations[k] = new List<object> {
                                    general_dict["dict_0"][k],
                                    0
                                };
                            }
                        }
                        else if (general_dict["dict_1"].keys().Contains(k))
                        {
                            names_durations[k] = new List<object> {
                                0,
                                general_dict["dict_1"][k]
                            };
                        }
                    }
                    general_dict["dict_0"] = names_durations;
                }
                else
                {
                    names_durations = general_dict["dict_0"];
                }
            }
            names_durations_dicts(names_durations);
            average_perfomance(names_durations);
            
            Console.WriteLine("End Programm");
        }
    */
    }

    