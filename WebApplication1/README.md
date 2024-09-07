# Project title: ToDoApplication (WeApplication1)
# Author: Valerija Racina
# Country: Latvia
# City: Riga
#### Video Demo:  https://www.youtube.com/watch?v=6_yZGozThFA
#### Description: ToDoApplication is a robust task management web application designed to enhance productivity and organization. Users can efficiently organize and keep track of their tasks by adding new entries to the list. The application captures essential details such as task name, description, due date, and current status. Upon completion, tasks can be marked as done and removed from the system.

Project Structure:
    Models:
        1. Category.cs
            This model is responsible for storing category id and name, providing a structured way to classify tasks within the database.
        2. Filters.cs
            The Filters model plays a crucial role in maintaining the application's flexibility. It applies a default route as "all-all-all" when instantiated through constructor injection, ensuring consistency and validation.
            
        3. Status.cs
            The Status model is responsible for storing status id and name from the database, offering a standardized way to track the progress of tasks.
        4. ToDo.cs
            The core entity model that users interact with. It encapsulates the essential attributes of a task, including name, description, due date, and status.
        5. ToDoDatabaseContext.cs
            This model encapsulates the database context and features a constructor that takes an instance of DbContextOptions as a parameter, providing configuration options for the database context.
    Views:
        1. Index.cshtml
            The main view that renders an IEnumerable of ToDo objects. It incorporates tag helpers within forms to seamlessly provide and collect data.
        2. Add.cshtml
            The Add view presents a form for users to input new ToDo models, ensuring a user-friendly experience for task creation.
        3. Privacy.cshtml
            The Privacy view serves as the default page for accessing privacy information, promoting transparency and user awareness.
        4. Layout.cshtml
            The Layout view represents the main template structure, featuring @RenderBody(), which dynamically renders the necessary view based on user interaction.
    Controllers:
        1. HomeController.cs
            The HomeController houses the main logic of the application, managing the routing to specific views or actions. It injects the Context class to facilitate database access and must be registered in services under the Program class.