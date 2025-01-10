## Varning 
The tests on 'FileService_Tests' will not mock the filesaving part. a real file and folder will be created.
I have added a Directory methods to delete the folder after the test is completed at the bottom of the tests.
Comment out that part if you want to be able to see the folder after the test is completed.
'public void LoadFromFile_ShouldReturnEmptyList_WhenExceptionIsThrown' have a valid file in a comment if you want to make the test fail.


## A Solution with 4 Projects.
1. **ContactApp**
   -Is a ConsoleApplication that contains MenuDialogs that prompts the user for data input.
2. **ContactApp.Business**
   -Contains all reused methods and logic.
3. **ContactApp_Tests**
   -Contains tests on all testable part in business.
4. **ContactApp_WPF**
   Is a Graphic User Interface that switches between pages and is connected to the .Business.


Both the console application and the WPF application use the same business logic and saves and loads data to the same file.
