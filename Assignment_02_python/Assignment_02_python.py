"""Assignment_02"""
import os
import unittest
from selenium import webdriver
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.chrome.service import Service

ADDRESS = "https://en.wikipedia.org/wiki/Software_metric"

def remove_open_files(file_path):
    if os.path.exists(file_path):
        os.remove(file_path)

PATH_RESULTS = r"D:\VUM STUDY\2 year\1 semester\Software Metrics - Tools and Methodology\3. Assignment\Assignment_02\Assignment_02\Assignment_02_python\perfomance_result.txt"
remove_open_files(PATH_RESULTS)

PERF_PATH= r"D:\VUM STUDY\2 year\1 semester\Software Metrics - Tools and Methodology\3. Assignment\Assignment_02\Assignment_02\Assignment_02_python\perfomance.csv"
remove_open_files(PERF_PATH)

PATH_DICTS= r"D:\VUM STUDY\2 year\1 semester\Software Metrics - Tools and Methodology\3. Assignment\Assignment_02\Assignment_02\Assignment_02_python\perfomance_dictionaries.csv"
remove_open_files(PATH_DICTS)

PATH_AVERAGE= r"D:\VUM STUDY\2 year\1 semester\Software Metrics - Tools and Methodology\3. Assignment\Assignment_02\Assignment_02\Assignment_02_python\perfomance_average.csv"
remove_open_files(PATH_AVERAGE)


all_results_file_object = open(PATH_RESULTS, "w")
perf_file_object = open(PERF_PATH, "w")
dicts_file_object = open(PATH_DICTS, "w")
ave_file_object = open(PATH_AVERAGE, "w")


class TestResults(unittest.TestCase):
    def test_on_web_page(self):     #return perfomance
        serv = Service("C:\Program Files\chromedriver_win32\chromedriver.exe")
        self.driver = webdriver.Chrome(service=serv)
        self.wait = WebDriverWait(self.driver, 10)
        self.address = ADDRESS
        res = self.driver.get(ADDRESS)
        self.assertIn(self.address, self.driver.current_url)
        script = "return window.performance.getEntries();"
        perf = self.driver.execute_script(script)
        self.driver.quit()
        return perf

def names_durations_dicts(names_durations):
    all_results_file_object.write("--------Names and list of durations-------\n")
    for j in names_durations:
        all_results_file_object.write(f"{j}:{names_durations[j]}\n")
        perf_list=""
        if isinstance(names_durations[j], float)|isinstance(names_durations[j], int):
            perf_list=names_durations[j]
        else:
            for num in names_durations[j]:
                perf_list=str(num)+","+perf_list
        dicts_file_object.write(f"{j},{perf_list}\n")
        perf_list=""

def average_perfomance(names_durations):
    average_dict={}
    all_results_file_object.write("--------Average Perfomance-------\n")
    print("--------Average Perfomance-------\n")
    for j in names_durations:
        if isinstance(names_durations[j], float)|isinstance(names_durations[j], int):
            average_dict[j]=names_durations[j]
            all_results_file_object.write(f"{j}:{average_dict[j]}\n")
            ave_file_object.write(f"{j},{average_dict[j]}\n")
        else:
            average = sum(names_durations[j])/len(names_durations[j])
            average_dict[j]=average
            all_results_file_object.write(f"{j}:{average_dict[j]}\n")
            ave_file_object.write(f"{j},{average_dict[j]}\n")
            average=0
        print(f"{j}:{average_dict[j]}\n")

def my_func():
    general_dict = {}
    names_durations={}
    average=0
    all_results_file_object.write("--------All Perfomances-------\n")
    i=0
    while i<10: # run "time" times
        result = TestResults().test_on_web_page()
        print("-------------------------------------------")
        print(f"end session {i+1}")
        dict ={}
        for curr in result:
            dict[curr['name']]= curr['duration']
            perf_file_object.write(f"{curr['name']},{curr['duration']}\n")
        general_dict[f"dict_{i}"]=dict
        all_results_file_object.write(f"Dictionary #{i+1}\n{dict}\n")
        # list of durations for one name
        if i>1:
            for k in general_dict["dict_0"].keys():
                if k in general_dict["dict_0"].keys():
                    if k in general_dict[f"dict_{i}"].keys():
                        names_durations[k].append(general_dict[f"dict_{i}"][k])
                    else:
                        names_durations[k].append(0)
                elif k in general_dict[f"dict_{i}"].keys():
                    names_durations[k].append(0)
            general_dict["dict_0"] = names_durations
        elif i>0:
            for k in general_dict["dict_0"].keys():
                if k in general_dict["dict_0"].keys():
                    if k in general_dict["dict_1"].keys():
                        names_durations[k]=[general_dict["dict_0"][k], general_dict["dict_1"][k]]
                    else:
                        names_durations[k] = [general_dict["dict_0"][k], 0]
                elif k in general_dict["dict_1"].keys():
                    names_durations[k] = [0, general_dict["dict_1"][k]]
            general_dict["dict_0"] = names_durations
        else:
            names_durations=general_dict["dict_0"]
        i+=1
    names_durations_dicts(names_durations)
    average_perfomance(names_durations)

    all_results_file_object.close()
    dicts_file_object.close()
    ave_file_object.close()
    perf_file_object.close()
    print("End Programm")

if __name__ == "__main__":
    my_func()
 