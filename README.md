# MovieMVC

Imagine that we are creating a movie catalog website. For this test task, we will be working with only two related entities: films and categories. In the database, this is reflected by the following tables:

Table "films":

Int Id auto increment primary key
Varchar 200 name
Varchar 200 director
Datetime release
Table "categories":

Int Id auto increment primary key
Varchar 200 name
Int nullable parent_category_id key
Table "film_categories":

Int Id auto increment primary key
Int film_id key
Int category_id key
We are not creating new tables in the database. We need to implement both backend and frontend parts that will perform the following functions:

CRUD operations for films using ASP.NET MVC.
A JavaScript class that allows viewing related categories and adding/removing new ones with multi-selection capability on the film information viewing page. Also, an ASP.NET Web API for this class (you can use a ready-made library for the select input).
CRUD operations for categories using ASP.NET MVC (circular references from parent categories are prohibited).
A page listing all films in a table with an additional column listing the film's categories separated by commas. As a bonus, utilize a library to add sorting by date and filtering by director to the table. *An even bigger bonus would be to implement category filtering.
A page listing all categories in a table, adding two columns: the number of films in the category and the level of category nesting.
For design, it's sufficient to use basic Bootstrap.
